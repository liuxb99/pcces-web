package sqlite

import (
	"context"
	"encoding/json"
	"os"
	"path/filepath"
	"runtime"
	"sort"
	"testing"
)

type sharedActionCatalog struct {
	Modules       []sharedCatalogItem   `json:"modules"`
	FunctionCodes []sharedCatalogItem   `json:"function_codes"`
	Actions       []sharedCatalogAction `json:"actions"`
}

type sharedCatalogItem struct {
	Code string `json:"code"`
	Name string `json:"name"`
}

type sharedCatalogAction struct {
	Code         string  `json:"code"`
	Name         string  `json:"name"`
	ModuleCode   string  `json:"module_code"`
	FunctionCode *string `json:"function_code"`
}

func TestSQLiteCatalogMatchesSharedContract(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "catalog-contract.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	contract := loadSharedActionCatalog(t)
	repository := NewCatalogRepository(store)

	modulesActual, err := repository.ListModules(ctx)
	if err != nil {
		t.Fatalf("list modules: %v", err)
	}
	functionsActual, err := repository.ListFunctionCodes(ctx)
	if err != nil {
		t.Fatalf("list function codes: %v", err)
	}
	actionsActual, err := repository.ListActions(ctx)
	if err != nil {
		t.Fatalf("list actions: %v", err)
	}

	modulePairs := make([]string, 0, len(modulesActual))
	for _, item := range modulesActual {
		modulePairs = append(modulePairs, item.Code+"\x00"+item.Name)
	}
	expectedModulePairs := make([]string, 0, len(contract.Modules))
	for _, item := range contract.Modules {
		expectedModulePairs = append(expectedModulePairs, item.Code+"\x00"+item.Name)
	}
	assertSameStrings(t, "modules", modulePairs, expectedModulePairs)

	functionPairs := make([]string, 0, len(functionsActual))
	for _, item := range functionsActual {
		functionPairs = append(functionPairs, item.Code+"\x00"+item.Name)
	}
	expectedFunctionPairs := make([]string, 0, len(contract.FunctionCodes))
	for _, item := range contract.FunctionCodes {
		expectedFunctionPairs = append(expectedFunctionPairs, item.Code+"\x00"+item.Name)
	}
	assertSameStrings(t, "function codes", functionPairs, expectedFunctionPairs)

	actionPairs := make([]string, 0, len(actionsActual))
	for _, item := range actionsActual {
		functionCode := ""
		if item.FunctionCode != nil {
			functionCode = *item.FunctionCode
		}
		actionPairs = append(actionPairs, item.Code+"\x00"+item.Name+"\x00"+item.ModuleCode+"\x00"+functionCode)
	}
	expectedActionPairs := make([]string, 0, len(contract.Actions))
	for _, item := range contract.Actions {
		functionCode := ""
		if item.FunctionCode != nil {
			functionCode = *item.FunctionCode
		}
		expectedActionPairs = append(expectedActionPairs, item.Code+"\x00"+item.Name+"\x00"+item.ModuleCode+"\x00"+functionCode)
	}
	assertSameStrings(t, "actions", actionPairs, expectedActionPairs)
}

func loadSharedActionCatalog(t *testing.T) sharedActionCatalog {
	t.Helper()
	_, filename, _, ok := runtime.Caller(0)
	if !ok {
		t.Fatal("resolve test file path")
	}
	path := filepath.Clean(filepath.Join(filepath.Dir(filename), "../../../../specs/catalog/phase0-action-catalog.json"))
	body, err := os.ReadFile(path)
	if err != nil {
		t.Fatalf("read shared catalog %s: %v", path, err)
	}
	var contract sharedActionCatalog
	if err := json.Unmarshal(body, &contract); err != nil {
		t.Fatalf("decode shared catalog: %v", err)
	}
	return contract
}

func assertSameStrings(t *testing.T, label string, actual, expected []string) {
	t.Helper()
	sort.Strings(actual)
	sort.Strings(expected)
	if len(actual) != len(expected) {
		t.Fatalf("%s count mismatch: got %d want %d\ngot=%v\nwant=%v", label, len(actual), len(expected), actual, expected)
	}
	for index := range actual {
		if actual[index] != expected[index] {
			t.Fatalf("%s mismatch at %d: got %q want %q", label, index, actual[index], expected[index])
		}
	}
}
