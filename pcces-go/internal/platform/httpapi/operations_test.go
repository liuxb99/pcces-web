package httpapi

import (
	"context"
	"encoding/json"
	"net/http"
	"net/http/httptest"
	"os"
	"path/filepath"
	"strings"
	"testing"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func TestLocalOperationsAPI(t *testing.T) {
	ctx := context.Background()
	tempDir := t.TempDir()
	store, err := sqlite.Open(ctx, filepath.Join(tempDir, "operations.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	ts := httptest.NewServer(New(nil, store).Handler())
	defer ts.Close()

	resp, err := http.Get(ts.URL + "/api/settings")
	if err != nil {
		t.Fatalf("list settings: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected settings 200, got %d", resp.StatusCode)
	}
	resp.Body.Close()

	settingBody := `{"value":"45","value_type":"int","description":"autosave seconds","row_version":1}`
	req, _ := http.NewRequest(http.MethodPut, ts.URL+"/api/settings/autosave.interval_seconds", strings.NewReader(settingBody))
	req.Header.Set("Content-Type", "application/json")
	resp, err = http.DefaultClient.Do(req)
	if err != nil {
		t.Fatalf("update setting: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected setting 200, got %d", resp.StatusCode)
	}
	resp.Body.Close()

	backupPath := filepath.Join(tempDir, "backups", "operations.db")
	backupBody := `{"destination":` + quoteJSON(backupPath) + `}`
	req, _ = http.NewRequest(http.MethodPost, ts.URL+"/api/system/backups", strings.NewReader(backupBody))
	req.Header.Set("Content-Type", "application/json")
	resp, err = http.DefaultClient.Do(req)
	if err != nil {
		t.Fatalf("create backup: %v", err)
	}
	if resp.StatusCode != http.StatusCreated {
		t.Fatalf("expected backup 201, got %d", resp.StatusCode)
	}
	resp.Body.Close()
	if _, err := os.Stat(backupPath); err != nil {
		t.Fatalf("backup file missing: %v", err)
	}

	resp, err = http.Get(ts.URL + "/api/system/integrity")
	if err != nil {
		t.Fatalf("integrity request: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected integrity 200, got %d", resp.StatusCode)
	}
	resp.Body.Close()

	recoveryBody := `{"id":"recovery-1","actor_id":"local-admin","payload":"{\"draft\":true}","reason":"crash"}`
	req, _ = http.NewRequest(http.MethodPost, ts.URL+"/api/recovery-snapshots", strings.NewReader(recoveryBody))
	req.Header.Set("Content-Type", "application/json")
	resp, err = http.DefaultClient.Do(req)
	if err != nil {
		t.Fatalf("create recovery snapshot: %v", err)
	}
	if resp.StatusCode != http.StatusCreated {
		t.Fatalf("expected recovery 201, got %d", resp.StatusCode)
	}
	var created sqlite.RecoverySnapshot
	if err := json.NewDecoder(resp.Body).Decode(&created); err != nil {
		t.Fatalf("decode recovery snapshot: %v", err)
	}
	resp.Body.Close()
	if created.PayloadHash == "" || created.RowVersion != 1 {
		t.Fatalf("unexpected recovery snapshot: %+v", created)
	}

	resp, err = http.Get(ts.URL + "/api/recovery-snapshots?actor_id=local-admin")
	if err != nil {
		t.Fatalf("list recovery snapshots: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected recovery list 200, got %d", resp.StatusCode)
	}
	resp.Body.Close()

	restoreBody := `{"row_version":1}`
	req, _ = http.NewRequest(http.MethodPost, ts.URL+"/api/recovery-snapshots/recovery-1/restore", strings.NewReader(restoreBody))
	req.Header.Set("Content-Type", "application/json")
	resp, err = http.DefaultClient.Do(req)
	if err != nil {
		t.Fatalf("restore recovery snapshot: %v", err)
	}
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("expected recovery restore 200, got %d", resp.StatusCode)
	}
	resp.Body.Close()

	req, _ = http.NewRequest(http.MethodPost, ts.URL+"/api/recovery-snapshots/recovery-1/discard", strings.NewReader(restoreBody))
	req.Header.Set("Content-Type", "application/json")
	resp, err = http.DefaultClient.Do(req)
	if err != nil {
		t.Fatalf("discard restored recovery snapshot: %v", err)
	}
	if resp.StatusCode != http.StatusConflict {
		t.Fatalf("expected terminal-state conflict 409, got %d", resp.StatusCode)
	}
	resp.Body.Close()
}

func quoteJSON(value string) string {
	encoded, _ := json.Marshal(value)
	return string(encoded)
}
