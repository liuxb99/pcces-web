package maintenance

import (
	"context"
	"log/slog"
	"path/filepath"
	"strconv"
	"strings"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

const backupPolicyRefreshInterval = time.Minute

// BackupService creates periodic SQLite backups and enforces retention.
type BackupService struct {
	logger   *slog.Logger
	store    *sqlite.Store
	settings *sqlite.SettingsRepository
}

func NewBackupService(logger *slog.Logger, store *sqlite.Store) *BackupService {
	if logger == nil {
		logger = slog.Default()
	}
	return &BackupService{logger: logger, store: store, settings: sqlite.NewSettingsRepository(store)}
}

func (s *BackupService) Run(ctx context.Context) {
	lastRun := time.Now()
	for {
		enabled, interval, directory, keep := s.policy(ctx)
		elapsed := time.Since(lastRun)
		if enabled && elapsed >= interval {
			if _, err := s.RunOnce(ctx, directory, keep); err != nil {
				s.logger.Error("automatic SQLite backup failed", "error", err)
			} else {
				lastRun = time.Now()
			}
			continue
		}

		wait := nextBackupDelay(enabled, interval, elapsed, backupPolicyRefreshInterval)
		timer := time.NewTimer(wait)
		select {
		case <-ctx.Done():
			timer.Stop()
			return
		case <-timer.C:
		}
	}
}

func nextBackupDelay(enabled bool, interval, elapsed, refreshInterval time.Duration) time.Duration {
	if refreshInterval <= 0 {
		refreshInterval = backupPolicyRefreshInterval
	}
	if !enabled {
		return refreshInterval
	}
	remaining := interval - elapsed
	if remaining <= 0 {
		return 0
	}
	if remaining < refreshInterval {
		return remaining
	}
	return refreshInterval
}

func (s *BackupService) RunOnce(ctx context.Context, directory string, keep int) (sqlite.BackupInfo, error) {
	if strings.TrimSpace(directory) == "" {
		directory = filepath.Join(filepath.Dir(s.store.Path()), "backups")
	}
	name := "pcces-" + time.Now().UTC().Format("20060102T150405.000000000Z") + ".db"
	info, err := s.store.Backup(ctx, filepath.Join(directory, name))
	if err != nil {
		return sqlite.BackupInfo{}, err
	}
	removed, err := sqlite.PruneBackups(directory, "pcces-", keep)
	if err != nil {
		return sqlite.BackupInfo{}, err
	}
	s.logger.Info("automatic SQLite backup completed", "path", info.Path, "removed", len(removed))
	return info, nil
}

func (s *BackupService) policy(ctx context.Context) (bool, time.Duration, string, int) {
	enabled := true
	interval := 24 * time.Hour
	directory := ""
	keep := 10
	if item, err := s.settings.Get(ctx, "backup.auto_enabled"); err == nil {
		if value, parseErr := strconv.ParseBool(item.Value); parseErr == nil {
			enabled = value
		}
	}
	if item, err := s.settings.Get(ctx, "backup.interval_hours"); err == nil {
		if hours, parseErr := strconv.Atoi(item.Value); parseErr == nil && hours >= 1 {
			interval = time.Duration(hours) * time.Hour
		}
	}
	if item, err := s.settings.Get(ctx, "backup.directory"); err == nil {
		directory = strings.TrimSpace(item.Value)
	}
	if item, err := s.settings.Get(ctx, "backup.keep_count"); err == nil {
		if count, parseErr := strconv.Atoi(item.Value); parseErr == nil && count >= 1 {
			keep = count
		}
	}
	return enabled, interval, directory, keep
}
