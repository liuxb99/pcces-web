package httpapi

import (
	"bytes"
	"context"
	"encoding/json"
	"io"
	"log/slog"
	"net/http"
	"net/http/httptest"
	"path/filepath"
	"testing"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/authorization"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func TestAuthorizationEndpointsGateWorkContext(t *testing.T) {
	ctx := context.Background()
	store, err := sqlite.Open(ctx, filepath.Join(t.TempDir(), "http-authz.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	server := httptest.NewServer(New(slog.New(slog.NewTextHandler(io.Discard, nil)), store).Handler())
	defer server.Close()

	resp, err := http.Get(server.URL + "/api/actors/local-admin/capabilities/BUD")
	if err != nil {
		t.Fatalf("get capability: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected 200, got %d", resp.StatusCode)
	}
	var initial authorization.Decision
	if err := json.NewDecoder(resp.Body).Decode(&initial); err != nil {
		t.Fatalf("decode capability: %v", err)
	}
	resp.Body.Close()
	if !initial.Allowed {
		t.Fatalf("expected seeded local-admin capability, got reason %q", initial.Reason)
	}

	allowedContext, _ := json.Marshal(map[string]any{
		"actor_id": "local-admin", "action_code": "BUD", "dirty": true,
		"draft_payload": `{"name":"draft"}`, "row_version": 0,
	})
	req, _ := http.NewRequest(http.MethodPut, server.URL+"/api/work-contexts/context-allowed", bytes.NewReader(allowedContext))
	req.Header.Set("Content-Type", "application/json")
	resp, err = http.DefaultClient.Do(req)
	if err != nil {
		t.Fatalf("save allowed context: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected 200 saving allowed context, got %d", resp.StatusCode)
	}
	resp.Body.Close()

	body, _ := json.Marshal(map[string]any{"granted": false, "row_version": 1})
	req, _ = http.NewRequest(http.MethodPut, server.URL+"/api/actors/local-admin/function-codes/F003", bytes.NewReader(body))
	req.Header.Set("Content-Type", "application/json")
	resp, err = http.DefaultClient.Do(req)
	if err != nil {
		t.Fatalf("revoke function: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected 200 revoking grant, got %d", resp.StatusCode)
	}
	resp.Body.Close()

	resp, err = http.Get(server.URL + "/api/actors/local-admin/capabilities/BUD")
	if err != nil {
		t.Fatalf("get denied capability: %v", err)
	}
	var denied authorization.Decision
	if err := json.NewDecoder(resp.Body).Decode(&denied); err != nil {
		t.Fatalf("decode denied capability: %v", err)
	}
	resp.Body.Close()
	if denied.Allowed || denied.Reason != "FUNCTION_NOT_GRANTED" {
		t.Fatalf("expected FUNCTION_NOT_GRANTED decision, got allowed=%v reason=%q", denied.Allowed, denied.Reason)
	}

	workContext, _ := json.Marshal(map[string]any{
		"actor_id": "local-admin", "action_code": "BUD", "dirty": true,
		"draft_payload": `{"name":"draft"}`, "row_version": 0,
	})
	req, _ = http.NewRequest(http.MethodPut, server.URL+"/api/work-contexts/context-denied", bytes.NewReader(workContext))
	req.Header.Set("Content-Type", "application/json")
	resp, err = http.DefaultClient.Do(req)
	if err != nil {
		t.Fatalf("save denied context: %v", err)
	}
	defer resp.Body.Close()
	if resp.StatusCode != http.StatusForbidden {
		t.Fatalf("expected 403 after revoking F003, got %d", resp.StatusCode)
	}
}

func TestModuleEntitlementImmediatelyChangesCapability(t *testing.T) {
	ctx := context.Background()
	store, err := sqlite.Open(ctx, filepath.Join(t.TempDir(), "http-module-authz.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	server := httptest.NewServer(New(nil, store).Handler())
	defer server.Close()

	body := []byte(`{"enabled":false,"row_version":1}`)
	req, _ := http.NewRequest(http.MethodPut, server.URL+"/api/actors/local-admin/modules/BUDGET", bytes.NewReader(body))
	req.Header.Set("Content-Type", "application/json")
	resp, err := http.DefaultClient.Do(req)
	if err != nil {
		t.Fatalf("disable module: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected 200 disabling module, got %d", resp.StatusCode)
	}
	resp.Body.Close()

	resp, err = http.Get(server.URL + "/api/actors/local-admin/capabilities/BUD")
	if err != nil {
		t.Fatalf("get capability: %v", err)
	}
	defer resp.Body.Close()
	var decision authorization.Decision
	if err := json.NewDecoder(resp.Body).Decode(&decision); err != nil {
		t.Fatalf("decode capability: %v", err)
	}
	if decision.Allowed || decision.Reason != "MODULE_NOT_ENTITLED" {
		t.Fatalf("expected MODULE_NOT_ENTITLED, got allowed=%v reason=%q", decision.Allowed, decision.Reason)
	}
}

func TestUnknownJSONFieldsAreRejected(t *testing.T) {
	ctx := context.Background()
	store, err := sqlite.Open(ctx, filepath.Join(t.TempDir(), "http-json.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	server := httptest.NewServer(New(nil, store).Handler())
	defer server.Close()

	body := []byte(`{"enabled":false,"row_version":1,"unexpected":true}`)
	req, _ := http.NewRequest(http.MethodPut, server.URL+"/api/actors/local-admin/modules/BUDGET", bytes.NewReader(body))
	req.Header.Set("Content-Type", "application/json")
	resp, err := http.DefaultClient.Do(req)
	if err != nil {
		t.Fatalf("send request: %v", err)
	}
	defer resp.Body.Close()
	if resp.StatusCode != http.StatusBadRequest {
		t.Fatalf("expected 400 for unknown field, got %d", resp.StatusCode)
	}
}
