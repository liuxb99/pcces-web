package maintenance

import (
	"context"
	"os"
	"path/filepath"
	"testing"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func TestRunOnceCreatesBackupAndPrunesOldFiles(t *testing.T) {
	ctx := context.Background()
	root := t.TempDir()
	store, err := sqlite.Open(ctx, filepath.Join(root, "local.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	backupDir := filepath.Join(root, "backups")
	if err := os.MkdirAll(backupDir, 0o755); err != nil {
		t.Fatalf("mkdir backups: %v", err)
	}
	oldFiles := []string{
		"pcces-20200101T000000.000000000Z.db",
		"pcces-20210101T000000.000000000Z.db",
		"pcces-20220101T000000.000000000Z.db",
	}
	for _, name := range oldFiles {
		if err := os.WriteFile(filepath.Join(backupDir, name), []byte("old"), 0o600); err != nil {
			t.Fatalf("write old backup: %v", err)
		}
		time.Sleep(time.Millisecond)
	}

	service := NewBackupService(nil, store)
	info, err := service.RunOnce(ctx, backupDir, 2)
	if err != nil {
		t.Fatalf("run backup: %v", err)
	}
	if info.SizeBytes == 0 {
		t.Fatal("expected non-empty backup")
	}
	entries, err := os.ReadDir(backupDir)
	if err != nil {
		t.Fatalf("read backup directory: %v", err)
	}
	count := 0
	for _, entry := range entries {
		if !entry.IsDir() && filepath.Ext(entry.Name()) == ".db" {
			count++
		}
	}
	if count != 2 {
		t.Fatalf("expected retention to keep 2 backups, got %d", count)
	}
}

func TestNextBackupDelayRefreshesEnabledPolicyBeforeLongInterval(t *testing.T) {
	got := nextBackupDelay(true, 24*time.Hour, 0, time.Minute)
	if got != time.Minute {
		t.Fatalf("expected policy refresh in one minute, got %s", got)
	}
}

func TestNextBackupDelayUsesRemainingIntervalWhenSooner(t *testing.T) {
	got := nextBackupDelay(true, time.Hour, 59*time.Minute+45*time.Second, time.Minute)
	if got != 15*time.Second {
		t.Fatalf("expected remaining interval, got %s", got)
	}
}

func TestNextBackupDelayRefreshesDisabledPolicy(t *testing.T) {
	got := nextBackupDelay(false, 24*time.Hour, 23*time.Hour, 30*time.Second)
	if got != 30*time.Second {
		t.Fatalf("expected disabled policy refresh, got %s", got)
	}
}

func TestNextBackupDelayRunsImmediatelyWhenDue(t *testing.T) {
	got := nextBackupDelay(true, time.Hour, time.Hour, time.Minute)
	if got != 0 {
		t.Fatalf("expected immediate execution when due, got %s", got)
	}
}
