package httpapi

import (
	"net/http"
	"net/http/httptest"
	"path/filepath"
	"strings"
	"testing"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func TestContractsDispatcherCanonicalPaths(t *testing.T) {
	store, err := sqlite.Open(t.Context(), filepath.Join(t.TempDir(), "contract-routes.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	ts := httptest.NewServer(New(nil, store).Handler())
	defer ts.Close()

	cases := []struct {
		method string
		path   string
		want   int // 0 = not 404
	}{
		// Core
		{"GET", "/api/contracts/eligibility", 0},
		{"POST", "/api/contracts", 0},
		{"GET", "/api/contracts/C1", 0},

		// Allocation
		{"GET", "/api/contracts/C1/allocation-basis", 0},

		// Versions
		{"POST", "/api/contracts/C1/versions", 0},
		{"GET", "/api/contracts/versions/V1", 0},
		{"POST", "/api/contracts/versions/V1/transition", 0},

		// Changes
		{"POST", "/api/contracts/C1/changes", 0},
		{"GET", "/api/contracts/changes/CH1", 0},

		// Change Cases
		{"POST", "/api/contracts/C1/change-cases", 0},
		{"GET", "/api/contracts/change-cases/CC1", 0},
		{"POST", "/api/contracts/change-cases/CC1/transition", 0},

		// Invoice Periods
		{"POST", "/api/contracts/C1/invoice-periods", 0},
		{"GET", "/api/contracts/invoice-periods/IP1", 0},
		{"POST", "/api/contracts/invoice-periods/IP1/transition", 0},

		// Settlements
		{"POST", "/api/contracts/C1/settlements", 0},
		{"GET", "/api/contracts/settlements/S1", 0},
		{"POST", "/api/contracts/settlements/S1/transition", 0},

		// Acceptances
		{"POST", "/api/contracts/C1/acceptances", 0},
		{"GET", "/api/contracts/acceptances/A1", 0},
		{"POST", "/api/contracts/acceptances/A1/transition", 0},
	}

	for _, c := range cases {
		t.Run(c.method+" "+c.path, func(t *testing.T) {
			req, err := http.NewRequest(c.method, ts.URL+c.path, nil)
			if err != nil {
				t.Fatal(err)
			}
			if c.method == http.MethodPost || c.method == http.MethodPut {
				req.Header.Set("Content-Type", "application/json")
			}
			resp, err := http.DefaultClient.Do(req)
			if err != nil {
				t.Fatalf("request failed: %v", err)
			}
			body := make([]byte, 50)
			n, _ := resp.Body.Read(body)
			resp.Body.Close()
			// Accept any response except 404 from ServeMux (returns plain text "404 page not found")
			// Business-level PCCES_NOT_FOUND (JSON body with code) means routing succeeded
			if resp.StatusCode == http.StatusNotFound && (n == 0 || body[0] != '{') {
				t.Errorf("route not found for %s %s", c.method, c.path)
			}
		})
	}
}

func TestContractsDispatcherRejectsWrongMethod(t *testing.T) {
	store, err := sqlite.Open(t.Context(), filepath.Join(t.TempDir(), "contract-method.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	ts := httptest.NewServer(New(nil, store).Handler())
	defer ts.Close()

	// POST should not be valid on GET-only endpoints
	cases := []struct {
		method string
		path   string
	}{
		{"POST", "/api/contracts/eligibility"},
		{"DELETE", "/api/contracts/versions/V1"},
		{"GET", "/api/contracts/versions/V1/transition"},
	}

	for _, c := range cases {
		t.Run(c.method+" "+c.path, func(t *testing.T) {
			req, _ := http.NewRequest(c.method, ts.URL+c.path, strings.NewReader(`{}`))
			req.Header.Set("Content-Type", "application/json")
			resp, err := http.DefaultClient.Do(req)
			if err != nil {
				t.Fatal(err)
			}
			resp.Body.Close()
			if resp.StatusCode != http.StatusMethodNotAllowed && resp.StatusCode != http.StatusNotFound {
				t.Errorf("%s %s: got %d, want 405 or 404", c.method, c.path, resp.StatusCode)
			}
		})
	}
}

func TestContractsDispatcherRejectsEmptyID(t *testing.T) {
	store, err := sqlite.Open(t.Context(), filepath.Join(t.TempDir(), "contract-empty.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	ts := httptest.NewServer(New(nil, store).Handler())
	defer ts.Close()

	emptyIDPaths := []string{
		"/api/contracts/versions/",        // empty versionID
		"/api/contracts/changes/",         // empty changeID
		"/api/contracts/change-cases/",    // empty caseID
		"/api/contracts/invoice-periods/", // empty periodID
		"/api/contracts/settlements/",     // empty settlementID
		"/api/contracts/acceptances/",     // empty acceptanceID
	}

	for _, p := range emptyIDPaths {
		t.Run("GET "+p, func(t *testing.T) {
			resp, err := http.Get(ts.URL + p)
			if err != nil {
				t.Fatal(err)
			}
			resp.Body.Close()
			// Empty ID should be rejected — dispatcher yields empty string after TrimPrefix
			if resp.StatusCode < 400 {
				t.Errorf("GET %s: got %d, want 4xx", p, resp.StatusCode)
			}
		})
	}
}

func TestContractsDispatcherExtraSegments(t *testing.T) {
	store, err := sqlite.Open(t.Context(), filepath.Join(t.TempDir(), "contract-extra.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	ts := httptest.NewServer(New(nil, store).Handler())
	defer ts.Close()

	// Extra path segments should 404
	extraPaths := []string{
		"/api/contracts/versions/V1/extra",
		"/api/contracts/changes/CH1/extra",
		"/api/contracts/settlements/S1/extra",
		"/api/contracts/acceptances/A1/extra",
	}

	for _, p := range extraPaths {
		t.Run("GET "+p, func(t *testing.T) {
			resp, err := http.Get(ts.URL + p)
			if err != nil {
				t.Fatal(err)
			}
			resp.Body.Close()
			if resp.StatusCode != http.StatusNotFound {
				t.Errorf("GET %s: got %d, want 404", p, resp.StatusCode)
			}
		})
	}
}
