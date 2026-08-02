package sqlite

import (
	"context"
	"database/sql"
	"embed"
	"fmt"
	"path/filepath"
	"time"

	_ "modernc.org/sqlite"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

//go:embed migrations/*.sql
var migrationFS embed.FS

// Store owns the Local Go SQLite connection and transaction boundary.
type Store struct {
	db   *sql.DB
	path string
}

func Open(ctx context.Context, path string) (*Store, error) {
	if path == "" {
		return nil, errx.New(errx.CodeInvalidArgument, "SQLite database path is required", "P0-G2")
	}
	abs, err := filepath.Abs(path)
	if err != nil {
		return nil, errx.Wrap(errx.CodeInvalidArgument, "resolve SQLite database path", "P0-G2", err)
	}

	dsn := fmt.Sprintf("file:%s?_pragma=foreign_keys(1)&_pragma=journal_mode(WAL)&_pragma=busy_timeout(5000)&_pragma=synchronous(NORMAL)", filepath.ToSlash(abs))
	db, err := sql.Open("sqlite", dsn)
	if err != nil {
		return nil, errx.Wrap(errx.CodeDatabase, "open SQLite database", "P0-G2", err)
	}
	db.SetMaxOpenConns(1)
	db.SetMaxIdleConns(1)
	db.SetConnMaxLifetime(0)

	pingCtx, cancel := context.WithTimeout(ctx, 10*time.Second)
	defer cancel()
	if err := db.PingContext(pingCtx); err != nil {
		db.Close()
		return nil, errx.Wrap(errx.CodeDatabase, "ping SQLite database", "P0-G2", err)
	}

	store := &Store{db: db, path: abs}
	if err := store.Migrate(ctx); err != nil {
		db.Close()
		return nil, err
	}
	return store, nil
}

func (s *Store) DB() *sql.DB { return s.db }
func (s *Store) Path() string { return s.path }
func (s *Store) Close() error { return s.db.Close() }

func (s *Store) Migrate(ctx context.Context) error {
	entries, err := migrationFS.ReadDir("migrations")
	if err != nil {
		return errx.Wrap(errx.CodeInternal, "read embedded migrations", "P0-G2", err)
	}
	for _, entry := range entries {
		if entry.IsDir() {
			continue
		}
		body, err := migrationFS.ReadFile("migrations/" + entry.Name())
		if err != nil {
			return errx.Wrap(errx.CodeInternal, "read migration "+entry.Name(), "P0-G2", err)
		}
		if _, err := s.db.ExecContext(ctx, string(body)); err != nil {
			return errx.Wrap(errx.CodeDatabase, "apply migration "+entry.Name(), "P0-G2", err)
		}
	}
	return nil
}

func (s *Store) WithTx(ctx context.Context, fn func(*sql.Tx) error) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return errx.Wrap(errx.CodeDatabase, "begin SQLite transaction", "P0-G2", err)
	}
	defer tx.Rollback()
	if err := fn(tx); err != nil {
		return err
	}
	if err := tx.Commit(); err != nil {
		return errx.Wrap(errx.CodeDatabase, "commit SQLite transaction", "P0-G2", err)
	}
	return nil
}

func (s *Store) IntegrityCheck(ctx context.Context) error {
	var result string
	if err := s.db.QueryRowContext(ctx, "PRAGMA integrity_check").Scan(&result); err != nil {
		return errx.Wrap(errx.CodeDatabase, "run SQLite integrity check", "P0-G2", err)
	}
	if result != "ok" {
		return errx.New(errx.CodeDatabase, "SQLite integrity check failed: "+result, "P0-G2")
	}
	return nil
}
