package httpapi

import (
	"context"
	"encoding/json"
	"net/http"
	"net/http/httptest"
	"path/filepath"
	"strings"
	"testing"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func TestPhase0LocalAPI(t *testing.T) {
	ctx := context.Background()
	store, err := sqlite.Open(ctx, filepath.Join(t.TempDir(), "api.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	ts := httptest.NewServer(New(nil, store).Handler())
	defer ts.Close()

	resp, err := http.Get(ts.URL + "/api/health")
	if err != nil {
		t.Fatalf("health request: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected 200, got %d", resp.StatusCode)
	}
	resp.Body.Close()

	resp, err = http.Get(ts.URL + "/api/catalog/actions")
	if err != nil {
		t.Fatalf("actions request: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected 200, got %d", resp.StatusCode)
	}
	var actions []map[string]any
	if err := json.NewDecoder(resp.Body).Decode(&actions); err != nil {
		t.Fatalf("decode actions: %v", err)
	}
	resp.Body.Close()
	if len(actions) == 0 {
		t.Fatal("expected seeded actions")
	}

	body := `{"actor_id":"local-user","action_code":"BUD","project_code":"P-001","dirty":true,"row_version":0}`
	req, err := http.NewRequest(http.MethodPut, ts.URL+"/api/work-contexts/ctx-1", strings.NewReader(body))
	if err != nil {
		t.Fatalf("new request: %v", err)
	}
	req.Header.Set("Content-Type", "application/json")
	resp, err = http.DefaultClient.Do(req)
	if err != nil {
		t.Fatalf("save context: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected 200, got %d", resp.StatusCode)
	}
	var created map[string]any
	if err := json.NewDecoder(resp.Body).Decode(&created); err != nil {
		t.Fatalf("decode context: %v", err)
	}
	resp.Body.Close()
	if created["row_version"].(float64) != 1 {
		t.Fatalf("expected row_version 1, got %v", created["row_version"])
	}

	stale := `{"actor_id":"local-user","action_code":"BUD","project_code":"P-001","dirty":false,"row_version":9}`
	req, _ = http.NewRequest(http.MethodPut, ts.URL+"/api/work-contexts/ctx-1", strings.NewReader(stale))
	req.Header.Set("Content-Type", "application/json")
	resp, err = http.DefaultClient.Do(req)
	if err != nil {
		t.Fatalf("stale update: %v", err)
	}
	if resp.StatusCode != http.StatusConflict {
		t.Fatalf("expected 409, got %d", resp.StatusCode)
	}
	resp.Body.Close()
}
