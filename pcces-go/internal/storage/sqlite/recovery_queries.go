package sqlite

import (
	"context"
	"crypto/sha256"
	"database/sql"
	"encoding/hex"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

// HasPendingPayload reports whether an identical unresolved recovery snapshot
// already exists for the same work context.
func (r *RecoveryRepository) HasPendingPayload(ctx context.Context, contextID, payload string) (bool, error) {
	digest := sha256.Sum256([]byte(payload))
	hash := hex.EncodeToString(digest[:])
	var marker int
	err := r.store.DB().QueryRowContext(ctx, `
		SELECT 1
		FROM recovery_snapshots
		WHERE context_id = ? AND payload_hash = ?
		  AND restored_at IS NULL AND discarded_at IS NULL
		LIMIT 1`, contextID, hash).Scan(&marker)
	if err == sql.ErrNoRows {
		return false, nil
	}
	if err != nil {
		return false, errx.Wrap(errx.CodeDatabase, "check pending recovery snapshot", "P0-G4", err)
	}
	return true, nil
}
