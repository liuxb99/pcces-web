package sqlite

import (
	"database/sql"
	"testing"

	_ "modernc.org/sqlite"
)

type testStore struct {
	db *sql.DB
}

func newTestStore(t *testing.T) *Store {
	t.Helper()
	db, err := sql.Open("sqlite", ":memory:")
	if err != nil {
		t.Fatal(err)
	}
	t.Cleanup(func() { db.Close() })
	return &Store{db: db}
}

func strPtr(s string) *string { return &s }
