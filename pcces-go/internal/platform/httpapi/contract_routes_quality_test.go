package httpapi

import (
	"encoding/json"
	"io"
	"net/http"
	"net/http/httptest"
	"path/filepath"
	"strings"
	"testing"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func TestContractsDispatcherDomainErrorCodes(t *testing.T) {
	store, err := sqlite.Open(t.Context(), filepath.Join(t.TempDir(), "contract-errors.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	ts := httptest.NewServer(New(nil, store).Handler())
	defer ts.Close()

	tests := []struct {
		name        string
		method      string
		path        string
		body        string
		wantCode    int
		wantErrCode string // expected PCCES error code in JSON
	}{
		{
			name:        "contract not found",
			method:      http.MethodGet,
			path:        "/api/contracts/nonexistent",
			wantCode:    http.StatusNotFound,
			wantErrCode: "PCCES_NOT_FOUND",
		},
		{
			name:        "version not found",
			method:      http.MethodGet,
			path:        "/api/contracts/versions/nonexistent",
			wantCode:    http.StatusNotFound,
			wantErrCode: "PCCES_NOT_FOUND",
		},
		{
			name:     "eligibility returns JSON",
			method:   http.MethodGet,
			path:     "/api/contracts/eligibility?project_code=X&budget_version_id=Y",
			wantCode: http.StatusOK,
		},
		{
			name:     "POST without body rejected",
			method:   http.MethodPost,
			path:     "/api/contracts",
			wantCode: http.StatusBadRequest,
		},
	}

	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			var body io.Reader
			if tt.body != "" {
				body = strings.NewReader(tt.body)
			}
			req, _ := http.NewRequest(tt.method, ts.URL+tt.path, body)
			req.Header.Set("Content-Type", "application/json")
			resp, err := http.DefaultClient.Do(req)
			if err != nil {
				t.Fatal(err)
			}
			defer resp.Body.Close()

			if tt.wantCode != 0 && resp.StatusCode != tt.wantCode {
				t.Errorf("status: got %d, want %d", resp.StatusCode, tt.wantCode)
			}

			if tt.wantErrCode != "" {
				var payload map[string]interface{}
				if err := json.NewDecoder(resp.Body).Decode(&payload); err == nil {
					if code, ok := payload["code"].(string); ok && code != tt.wantErrCode {
						t.Errorf("error code: got %q, want %q", code, tt.wantErrCode)
					}
				}
			}
		})
	}
}

func TestContractsDispatcherURLEncodedID(t *testing.T) {
	store, err := sqlite.Open(t.Context(), filepath.Join(t.TempDir(), "contract-url.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	ts := httptest.NewServer(New(nil, store).Handler())
	defer ts.Close()

	// URL-encoded IDs should be routed correctly
	// Note: IDs containing "/" after decoding cause extra segments → 404 (expected)
	paths := []string{
		"/api/contracts/C%201",          // space in contract ID
		"/api/contracts/versions/V%201", // space in version ID
	}

	for _, p := range paths {
		t.Run(p, func(t *testing.T) {
			resp, err := http.Get(ts.URL + p)
			if err != nil {
				t.Fatal(err)
			}
			body, _ := io.ReadAll(resp.Body)
			resp.Body.Close()
			// Should get a JSON error, not plain-text 404
			if resp.StatusCode == http.StatusNotFound && len(body) > 0 && body[0] != '{' {
				t.Errorf("URL-encoded ID %s returned plain-text 404 (route not matched)", p)
			}
		})
	}
}

func TestContractsDispatcherPathConsistency(t *testing.T) {
	// Verify Go paths match Web/Frontend canonical paths
	canonical := map[string]string{
		"versions":              "/api/contracts/versions/{versionID}",
		"versions-trans":        "/api/contracts/versions/{versionID}/transition",
		"changes":               "/api/contracts/changes/{changeID}",
		"change-cases":          "/api/contracts/change-cases/{caseID}",
		"change-cases-trans":    "/api/contracts/change-cases/{caseID}/transition",
		"invoice-periods":       "/api/contracts/invoice-periods/{periodID}",
		"invoice-periods-trans": "/api/contracts/invoice-periods/{periodID}/transition",
		"settlements":           "/api/contracts/settlements/{settlementID}",
		"settlements-trans":     "/api/contracts/settlements/{settlementID}/transition",
		"acceptances":           "/api/contracts/acceptances/{acceptanceID}",
		"acceptances-trans":     "/api/contracts/acceptances/{acceptanceID}/transition",
	}

	store, err := sqlite.Open(t.Context(), filepath.Join(t.TempDir(), "contract-consistency.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	ts := httptest.NewServer(New(nil, store).Handler())
	defer ts.Close()

	for name, pattern := range canonical {
		// Replace {var} with test IDs
		testPath := strings.NewReplacer(
			"{versionID}", "V1",
			"{changeID}", "CH1",
			"{caseID}", "CC1",
			"{periodID}", "IP1",
			"{settlementID}", "S1",
			"{acceptanceID}", "A1",
		).Replace(pattern)

		t.Run(name, func(t *testing.T) {
			method := http.MethodGet
			if strings.Contains(name, "trans") {
				method = http.MethodPost
			}
			var body io.Reader
			if method == http.MethodPost {
				body = strings.NewReader(`{"status":"SUBMITTED","row_version":1}`)
			}
			req, _ := http.NewRequest(method, ts.URL+testPath, body)
			req.Header.Set("Content-Type", "application/json")
			resp, err := http.DefaultClient.Do(req)
			if err != nil {
				t.Fatal(err)
			}
			respBody, _ := io.ReadAll(resp.Body)
			resp.Body.Close()

			// Must not be plain-text 404 (route not matched)
			if resp.StatusCode == http.StatusNotFound && len(respBody) > 0 && respBody[0] != '{' {
				t.Errorf("%s: route not matched (plain-text 404)", pattern)
			}
		})
	}
}
