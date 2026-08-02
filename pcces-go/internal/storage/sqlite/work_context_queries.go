package sqlite

import (
	"context"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/workcontext"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

// ListDirty returns all contexts that contain unsaved local draft state.
func (r *WorkContextRepository) ListDirty(ctx context.Context) ([]*workcontext.Context, error) {
	rows, err := r.store.DB().QueryContext(ctx, `
		SELECT id, actor_id, action_code, project_code, resource_type, resource_id,
		       dirty, draft_payload, created_at, updated_at, row_version
		FROM work_contexts
		WHERE dirty = 1 AND draft_payload IS NOT NULL AND length(trim(draft_payload)) > 0
		ORDER BY updated_at ASC`)
	if err != nil {
		return nil, errx.Wrap(errx.CodeDatabase, "list dirty work contexts", "P0-G4", err)
	}
	defer rows.Close()

	items := make([]*workcontext.Context, 0)
	for rows.Next() {
		item, err := scanWorkContext(rows)
		if err != nil {
			return nil, errx.Wrap(errx.CodeDatabase, "scan dirty work context", "P0-G4", err)
		}
		items = append(items, item)
	}
	if err := rows.Err(); err != nil {
		return nil, errx.Wrap(errx.CodeDatabase, "iterate dirty work contexts", "P0-G4", err)
	}
	return items, nil
}
