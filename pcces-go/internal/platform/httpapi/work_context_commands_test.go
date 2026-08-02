package httpapi

import (
	"bytes"
	"context"
	"encoding/json"
	"io"
	"net/http"
	"net/http/httptest"
	"path/filepath"
	"testing"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func TestWorkContextCommandLifecycle(t *testing.T) {
	store, err := sqlite.Open(context.Background(), filepath.Join(t.TempDir(), "commands.db"))
	if err != nil { t.Fatalf("open store: %v", err) }
	defer store.Close()
	server := httptest.NewServer(New(nil, store).Handler())
	defer server.Close()

	call := func(command string, body map[string]any) (*http.Response, map[string]any) {
		t.Helper()
		payload, _ := json.Marshal(body)
		resp, err := http.Post(server.URL+"/api/work-contexts/ctx-1/"+command, "application/json", bytes.NewReader(payload))
		if err != nil { t.Fatalf("post %s: %v", command, err) }
		defer resp.Body.Close()
		decoded := map[string]any{}
		_ = json.NewDecoder(resp.Body).Decode(&decoded)
		return resp, decoded
	}

	base := map[string]any{"actor_id":"local-admin", "action_code":"BUD", "draft_payload":"{\"name\":\"draft\"}"}
	resp, result := call("save-draft", base)
	if resp.StatusCode != http.StatusOK { t.Fatalf("save draft status=%d body=%v", resp.StatusCode, result) }
	contextValue := result["context"].(map[string]any)
	if contextValue["dirty"] != true { t.Fatalf("expected dirty context: %v", contextValue) }

	base["row_version"] = 1
	resp, result = call("cancel", base)
	if resp.StatusCode != http.StatusConflict || result["outcome"] != "DECISION_REQUIRED" {
		t.Fatalf("dirty cancel should require decision: status=%d body=%v", resp.StatusCode, result)
	}

	resp, result = call("discard", base)
	if resp.StatusCode != http.StatusOK { t.Fatalf("discard status=%d body=%v", resp.StatusCode, result) }
	contextValue = result["context"].(map[string]any)
	if contextValue["dirty"] != false { t.Fatalf("expected clean context after discard: %v", contextValue) }

	base["row_version"] = 2
	resp, result = call("cancel", base)
	if resp.StatusCode != http.StatusOK || result["outcome"] != "CANCELLED" {
		t.Fatalf("clean cancel should close context: status=%d body=%v", resp.StatusCode, result)
	}

	getResp, err := http.Get(server.URL + "/api/work-contexts/ctx-1")
	if err != nil { t.Fatal(err) }
	defer getResp.Body.Close()
	if getResp.StatusCode != http.StatusNotFound {
		body, _ := io.ReadAll(getResp.Body)
		t.Fatalf("expected deleted context, got %d %s", getResp.StatusCode, body)
	}
}
