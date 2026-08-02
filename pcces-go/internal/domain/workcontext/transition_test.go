package workcontext

import (
	"encoding/json"
	"os"
	"path/filepath"
	"runtime"
	"testing"
)

type transitionFixture struct { Cases []struct {
	Name string `json:"name"`
	Current struct { Exists bool `json:"exists"`; Dirty bool `json:"dirty"`; RowVersion int64 `json:"row_version"` } `json:"current"`
	Command string `json:"command"`
	RequestRowVersion *int64 `json:"request_row_version"`
	Expected TransitionResult `json:"expected"`
} `json:"cases"` }

func TestSharedWorkContextTransitions(t *testing.T) {
	_, filename, _, _ := runtime.Caller(0)
	path := filepath.Clean(filepath.Join(filepath.Dir(filename), "../../../../specs/golden/work-context-transitions.json"))
	body, err := os.ReadFile(path)
	if err != nil { t.Fatal(err) }
	var fixture transitionFixture
	if err := json.Unmarshal(body, &fixture); err != nil { t.Fatal(err) }
	for _, tc := range fixture.Cases {
		t.Run(tc.Name, func(t *testing.T) {
			got := Transition(tc.Current.Exists, tc.Current.Dirty, tc.Current.RowVersion, tc.Command, tc.RequestRowVersion)
			if got != tc.Expected { t.Fatalf("got %+v want %+v", got, tc.Expected) }
		})
	}
}
