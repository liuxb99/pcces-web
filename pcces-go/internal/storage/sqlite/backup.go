package sqlite

import (
	"context"
	"fmt"
	"io"
	"os"
	"path/filepath"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type BackupInfo struct {
	Path      string    `json:"path"`
	SizeBytes int64     `json:"size_bytes"`
	CreatedAt time.Time `json:"created_at"`
}

// Backup checkpoints WAL content, copies the database atomically to a temporary
// file and only then renames it to the final destination.
func (s *Store) Backup(ctx context.Context, destination string) (BackupInfo, error) {
	if destination == "" {
		return BackupInfo{}, errx.New(errx.CodeInvalidArgument, "backup destination is required", "P0-G2")
	}
	if _, err := s.db.ExecContext(ctx, "PRAGMA wal_checkpoint(FULL)"); err != nil {
		return BackupInfo{}, errx.Wrap(errx.CodeDatabase, "checkpoint SQLite WAL before backup", "P0-G2", err)
	}
	abs, err := filepath.Abs(destination)
	if err != nil {
		return BackupInfo{}, errx.Wrap(errx.CodeInvalidArgument, "resolve backup destination", "P0-G2", err)
	}
	if err := os.MkdirAll(filepath.Dir(abs), 0o755); err != nil {
		return BackupInfo{}, errx.Wrap(errx.CodeInternal, "create backup directory", "P0-G2", err)
	}
	tmp := abs + ".tmp"
	if err := copyFile(s.path, tmp); err != nil {
		return BackupInfo{}, errx.Wrap(errx.CodeInternal, "copy SQLite backup", "P0-G2", err)
	}
	if err := os.Rename(tmp, abs); err != nil {
		_ = os.Remove(tmp)
		return BackupInfo{}, errx.Wrap(errx.CodeInternal, "commit SQLite backup", "P0-G2", err)
	}
	stat, err := os.Stat(abs)
	if err != nil {
		return BackupInfo{}, errx.Wrap(errx.CodeInternal, "stat SQLite backup", "P0-G2", err)
	}
	return BackupInfo{Path: abs, SizeBytes: stat.Size(), CreatedAt: stat.ModTime().UTC()}, nil
}

// RestoreFrom replaces the local database file. The Store must not be used
// concurrently while restore is running; callers should stop the localhost API.
func RestoreFrom(ctx context.Context, source, destination string) error {
	if source == "" || destination == "" {
		return errx.New(errx.CodeInvalidArgument, "restore source and destination are required", "P0-G2")
	}
	src, err := filepath.Abs(source)
	if err != nil {
		return errx.Wrap(errx.CodeInvalidArgument, "resolve restore source", "P0-G2", err)
	}
	dst, err := filepath.Abs(destination)
	if err != nil {
		return errx.Wrap(errx.CodeInvalidArgument, "resolve restore destination", "P0-G2", err)
	}
	if _, err := os.Stat(src); err != nil {
		return errx.Wrap(errx.CodeNotFound, "backup file not found", "P0-G2", err)
	}
	if err := os.MkdirAll(filepath.Dir(dst), 0o755); err != nil {
		return errx.Wrap(errx.CodeInternal, "create restore directory", "P0-G2", err)
	}
	tmp := fmt.Sprintf("%s.restore-%d.tmp", dst, time.Now().UnixNano())
	if err := copyFile(src, tmp); err != nil {
		return errx.Wrap(errx.CodeInternal, "stage restored SQLite database", "P0-G2", err)
	}
	probe, err := Open(ctx, tmp)
	if err != nil {
		_ = os.Remove(tmp)
		return errx.Wrap(errx.CodeDatabase, "open staged restore database", "P0-G2", err)
	}
	checkErr := probe.IntegrityCheck(ctx)
	_ = probe.Close()
	if checkErr != nil {
		_ = os.Remove(tmp)
		return checkErr
	}
	if err := os.Rename(tmp, dst); err != nil {
		_ = os.Remove(tmp)
		return errx.Wrap(errx.CodeInternal, "commit restored SQLite database", "P0-G2", err)
	}
	return nil
}

func copyFile(source, destination string) error {
	in, err := os.Open(source)
	if err != nil {
		return err
	}
	defer in.Close()
	out, err := os.OpenFile(destination, os.O_CREATE|os.O_TRUNC|os.O_WRONLY, 0o600)
	if err != nil {
		return err
	}
	defer func() { _ = out.Close() }()
	if _, err := io.Copy(out, in); err != nil {
		return err
	}
	return out.Sync()
}
