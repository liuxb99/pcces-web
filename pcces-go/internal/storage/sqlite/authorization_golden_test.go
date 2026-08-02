package sqlite

import (
	"context"
	"encoding/json"
	"os"
	"path/filepath"
	"runtime"
	"testing"
)

type authorizationGoldenFixture struct {
	Actor string                    `json:"actor"`
	Cases []authorizationGoldenCase `json:"cases"`
}

type authorizationGoldenCase struct {
	Name              string `json:"name"`
	ActionCode        string `json:"action_code"`
	ModuleEnabled     bool   `json:"module_enabled"`
	ModuleEntitled    bool   `json:"module_entitled"`
	FunctionEnabled   bool   `json:"function_enabled"`
	FunctionGranted   bool   `json:"function_granted"`
	Expected          struct {
		Allowed      bool   `json:"allowed"`
		Reason       string `json:"reason"`
		ModuleCode   string `json:"module_code"`
		FunctionCode string `json:"function_code"`
	} `json:"expected"`
}

func TestAuthorizationGoldenFixtures(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "authorization-golden.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	fixture := loadAuthorizationGoldenFixture(t)
	if _, err := store.DB().ExecContext(ctx,
		`INSERT OR IGNORE INTO local_actors(actor_id, display_name, active) VALUES(?, 'Fixture User', 1)`,
		fixture.Actor,
	); err != nil {
		t.Fatalf("seed fixture actor: %v", err)
	}

	repository := NewAuthorizationRepository(store)
	for _, testCase := range fixture.Cases {
		t.Run(testCase.Name, func(t *testing.T) {
			resetAuthorizationFixture(t, ctx, store, fixture.Actor, testCase)
			decision, err := repository.Decide(ctx, fixture.Actor, testCase.ActionCode)
			if err != nil {
				t.Fatalf("decide %s: %v", testCase.ActionCode, err)
			}
			if decision.Allowed != testCase.Expected.Allowed ||
				decision.Reason != testCase.Expected.Reason ||
				decision.ModuleCode != testCase.Expected.ModuleCode ||
				decision.FunctionCode != testCase.Expected.FunctionCode {
				t.Fatalf("decision mismatch: got allowed=%v reason=%q module=%q function=%q; want allowed=%v reason=%q module=%q function=%q",
					decision.Allowed, decision.Reason, decision.ModuleCode, decision.FunctionCode,
					testCase.Expected.Allowed, testCase.Expected.Reason, testCase.Expected.ModuleCode, testCase.Expected.FunctionCode,
				)
			}
		})
	}
}

func loadAuthorizationGoldenFixture(t *testing.T) authorizationGoldenFixture {
	t.Helper()
	_, filename, _, ok := runtime.Caller(0)
	if !ok {
		t.Fatal("resolve test file path")
	}
	path := filepath.Clean(filepath.Join(filepath.Dir(filename), "../../../../specs/golden/authorization-decisions.json"))
	body, err := os.ReadFile(path)
	if err != nil {
		t.Fatalf("read golden fixture %s: %v", path, err)
	}
	var fixture authorizationGoldenFixture
	if err := json.Unmarshal(body, &fixture); err != nil {
		t.Fatalf("decode golden fixture: %v", err)
	}
	return fixture
}

func resetAuthorizationFixture(t *testing.T, ctx context.Context, store *Store, actorID string, testCase authorizationGoldenCase) {
	t.Helper()
	if _, err := store.DB().ExecContext(ctx, `UPDATE modules SET enabled = 1`); err != nil {
		t.Fatalf("reset modules: %v", err)
	}
	if _, err := store.DB().ExecContext(ctx, `UPDATE function_codes SET enabled = 1`); err != nil {
		t.Fatalf("reset function codes: %v", err)
	}
	if _, err := store.DB().ExecContext(ctx, `DELETE FROM actor_module_entitlements WHERE actor_id = ?`, actorID); err != nil {
		t.Fatalf("reset module entitlements: %v", err)
	}
	if _, err := store.DB().ExecContext(ctx, `DELETE FROM actor_function_codes WHERE actor_id = ?`, actorID); err != nil {
		t.Fatalf("reset function grants: %v", err)
	}

	var moduleCode string
	var functionCode *string
	var nullableFunctionCode interface{}
	err := store.DB().QueryRowContext(ctx, `SELECT module_code, function_code FROM actions WHERE code = ?`, testCase.ActionCode).
		Scan(&moduleCode, &nullableFunctionCode)
	if err != nil {
		return // Unknown actions are intentionally evaluated without setup.
	}
	if nullableFunctionCode != nil {
		value, ok := nullableFunctionCode.(string)
		if ok {
			functionCode = &value
		}
	}

	if _, err := store.DB().ExecContext(ctx, `UPDATE modules SET enabled = ? WHERE code = ?`, boolToInt(testCase.ModuleEnabled), moduleCode); err != nil {
		t.Fatalf("set module state: %v", err)
	}
	if _, err := store.DB().ExecContext(ctx,
		`INSERT INTO actor_module_entitlements(actor_id, module_code, enabled) VALUES(?,?,?)`,
		actorID, moduleCode, boolToInt(testCase.ModuleEntitled),
	); err != nil {
		t.Fatalf("set module entitlement: %v", err)
	}
	if functionCode == nil {
		return
	}
	if _, err := store.DB().ExecContext(ctx, `UPDATE function_codes SET enabled = ? WHERE code = ?`, boolToInt(testCase.FunctionEnabled), *functionCode); err != nil {
		t.Fatalf("set function state: %v", err)
	}
	if _, err := store.DB().ExecContext(ctx,
		`INSERT INTO actor_function_codes(actor_id, function_code, granted) VALUES(?,?,?)`,
		actorID, *functionCode, boolToInt(testCase.FunctionGranted),
	); err != nil {
		t.Fatalf("set function grant: %v", err)
	}
}
