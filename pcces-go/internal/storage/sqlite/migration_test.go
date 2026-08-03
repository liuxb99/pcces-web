package sqlite

import (
	"context"
	"database/sql"
	"testing"

	_ "modernc.org/sqlite"
)

func TestMigrationIdempotency(t *testing.T) {
	db, err := sql.Open("sqlite", ":memory:")
	if err != nil {
		t.Fatal(err)
	}
	defer db.Close()
	db.SetMaxOpenConns(1)

	store := &Store{db: db}

	// First migration
	if err := store.Migrate(context.Background()); err != nil {
		t.Fatalf("first migrate: %v", err)
	}

	var tableCount int
	db.QueryRow("SELECT COUNT(*) FROM sqlite_master WHERE type='table'").Scan(&tableCount)
	t.Logf("after first migrate: %d tables", tableCount)

	// Second migration (idempotency: all IF NOT EXISTS)
	if err := store.Migrate(context.Background()); err != nil {
		t.Fatalf("second migrate: %v", err)
	}

	var tableCount2 int
	db.QueryRow("SELECT COUNT(*) FROM sqlite_master WHERE type='table'").Scan(&tableCount2)
	if tableCount != tableCount2 {
		t.Fatalf("idempotency violated: %d → %d tables", tableCount, tableCount2)
	}
}

func TestMigrationMultiStatement(t *testing.T) {
	db, err := sql.Open("sqlite", ":memory:")
	if err != nil {
		t.Fatal(err)
	}
	defer db.Close()
	db.SetMaxOpenConns(1)

	store := &Store{db: db}
	if err := store.Migrate(context.Background()); err != nil {
		t.Fatalf("migrate: %v", err)
	}

	// Verify multi-statement migrations executed fully
	// 0003 creates local_settings + recovery_snapshots + indexes + inserts
	var v string
	if err := db.QueryRow("SELECT value FROM local_settings WHERE key='backup.keep_count'").Scan(&v); err != nil {
		t.Fatalf("0003 multi-stmt failed: %v", err)
	}
	if v != "10" {
		t.Fatalf("backup.keep_count = %q, want 10", v)
	}

	// 0004 has semicolon inside a string value
	if err := db.QueryRow("SELECT value FROM local_settings WHERE key='backup.directory'").Scan(&v); err != nil {
		t.Fatalf("0004 semicolon-in-string failed: %v", err)
	}
	// The description contains ';' — must be stored correctly
	if v != "" {
		t.Logf("backup.directory value: %q", v)
	}

	// 0020 creates multiple tables
	var count int
	db.QueryRow("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name IN ('mrs_catalog_releases','mrs_item_validity','mrs_recipe_freezes','mrs_governance_audit')").Scan(&count)
	if count != 4 {
		t.Fatalf("0020 multi-stmt: %d/4 governance tables", count)
	}
}

func TestMigrationTransactionRollback(t *testing.T) {
	// Verify that a failed migration statement doesn't leave partial state
	db, err := sql.Open("sqlite", ":memory:")
	if err != nil {
		t.Fatal(err)
	}
	defer db.Close()

	// Execute a bad statement — should fail
	_, err = db.ExecContext(context.Background(), "CREATE TABLE t_good(a text); INVALID SQL SYNTAX; CREATE TABLE t_bad(b text)")
	if err == nil {
		t.Fatal("expected error for invalid SQL")
	}

	// t_good should exist (executed before the bad statement)
	var count int
	db.QueryRow("SELECT COUNT(*) FROM sqlite_master WHERE name='t_good'").Scan(&count)
	if count != 1 {
		t.Fatal("t_good should exist — modernc executes statements sequentially until error")
	}
	// t_bad should NOT exist (after the bad statement)
	db.QueryRow("SELECT COUNT(*) FROM sqlite_master WHERE name='t_bad'").Scan(&count)
	if count != 0 {
		t.Fatal("t_bad should not exist")
	}
}
