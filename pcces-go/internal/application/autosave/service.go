package autosave

import (
	"context"
	"crypto/rand"
	"encoding/hex"
	"log/slog"
	"strconv"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

// Result summarizes one autosave sweep.
type Result struct {
	Scanned int `json:"scanned"`
	Created int `json:"created"`
	Skipped int `json:"skipped"`
}

// Service converts dirty work-context drafts into durable recovery snapshots.
type Service struct {
	logger   *slog.Logger
	settings *sqlite.SettingsRepository
	contexts *sqlite.WorkContextRepository
	recovery *sqlite.RecoveryRepository
}

func New(logger *slog.Logger, store *sqlite.Store) *Service {
	if logger == nil {
		logger = slog.Default()
	}
	return &Service{
		logger:   logger,
		settings: sqlite.NewSettingsRepository(store),
		contexts: sqlite.NewWorkContextRepository(store),
		recovery: sqlite.NewRecoveryRepository(store),
	}
}

// RunOnce persists one snapshot per changed dirty context. Identical unresolved
// payloads are skipped to avoid unbounded duplicate autosaves.
func (s *Service) RunOnce(ctx context.Context) (Result, error) {
	items, err := s.contexts.ListDirty(ctx)
	if err != nil {
		return Result{}, err
	}
	result := Result{Scanned: len(items)}
	for _, item := range items {
		if item.DraftPayload == nil || *item.DraftPayload == "" {
			result.Skipped++
			continue
		}
		exists, err := s.recovery.HasPendingPayload(ctx, item.ID, *item.DraftPayload)
		if err != nil {
			return result, err
		}
		if exists {
			result.Skipped++
			continue
		}
		contextID := item.ID
		actionCode := item.ActionCode
		_, err = s.recovery.Create(ctx, sqlite.RecoverySnapshot{
			ID:          newID(),
			ActorID:     item.ActorID,
			ContextID:   &contextID,
			ProjectCode: item.ProjectCode,
			ActionCode:  &actionCode,
			Payload:     *item.DraftPayload,
			Reason:      "autosave",
		})
		if err != nil {
			return result, err
		}
		result.Created++
	}
	return result, nil
}

// Run starts the background autosave loop. Settings are reloaded before every
// sweep so changes take effect without restarting the local server.
func (s *Service) Run(ctx context.Context) {
	for {
		enabled, interval := s.currentPolicy(ctx)
		wait := interval
		if !enabled {
			wait = 30 * time.Second
		}
		timer := time.NewTimer(wait)
		select {
		case <-ctx.Done():
			timer.Stop()
			return
		case <-timer.C:
		}
		if !enabled {
			continue
		}
		result, err := s.RunOnce(ctx)
		if err != nil {
			s.logger.Error("local autosave sweep failed", "error", err)
			continue
		}
		if result.Created > 0 {
			s.logger.Info("local autosave sweep completed", "scanned", result.Scanned, "created", result.Created, "skipped", result.Skipped)
		}
	}
}

func (s *Service) currentPolicy(ctx context.Context) (bool, time.Duration) {
	enabled := true
	interval := 60 * time.Second
	if item, err := s.settings.Get(ctx, "autosave.enabled"); err == nil {
		if value, parseErr := strconv.ParseBool(item.Value); parseErr == nil {
			enabled = value
		}
	}
	if item, err := s.settings.Get(ctx, "autosave.interval_seconds"); err == nil {
		if seconds, parseErr := strconv.Atoi(item.Value); parseErr == nil && seconds >= 5 {
			interval = time.Duration(seconds) * time.Second
		}
	}
	return enabled, interval
}

func newID() string {
	var bytes [16]byte
	if _, err := rand.Read(bytes[:]); err != nil {
		return "autosave-" + strconv.FormatInt(time.Now().UnixNano(), 10)
	}
	return hex.EncodeToString(bytes[:])
}
