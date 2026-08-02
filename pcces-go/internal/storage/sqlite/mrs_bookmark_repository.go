package sqlite

import (
	"context"
	"strings"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

// MRSBookmark preserves the Legacy bookmark owner, source item identity and
// creation time while returning the current catalog projection.
type MRSBookmark struct {
	ActorID      string         `json:"actor_id"`
	CatalogItem  MRSCatalogItem `json:"catalog_item"`
	CreatedAt    string         `json:"created_at"`
	DeepLink     string         `json:"deep_link"`
}

// ListBookmarks returns the actor's bookmarks in stable catalog-code order.
// Query and category follow the Web catalog search semantics.
func (r *MRSCatalogRepository) ListBookmarks(ctx context.Context, actor, query, category string) ([]MRSBookmark, error) {
	actor = strings.TrimSpace(actor)
	if actor == "" {
		return nil, errx.New(errx.CodeInvalidArgument, "actor_id is required", "P3-G-MRS-BOOKMARK")
	}
	query = strings.ToLower(strings.TrimSpace(query))
	category = strings.ToUpper(strings.TrimSpace(category))
	rows, err := r.store.db.QueryContext(ctx, `
SELECT b.actor_id,b.created_at,
       i.id,i.code,i.name,i.category,i.unit,i.current_price,i.price_scale,i.source,
       i.enabled,i.row_version,i.created_at,i.updated_at
FROM mrs_bookmarks b
JOIN mrs_catalog_items i ON i.id=b.catalog_item_id
WHERE b.actor_id=?
  AND (?='' OR i.category=?)
  AND (?='' OR lower(i.code) LIKE '%'||?||'%' OR lower(i.name) LIKE '%'||?||'%')
ORDER BY i.code,i.id`, actor, category, category, query, query, query)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	items := make([]MRSBookmark, 0)
	for rows.Next() {
		var item MRSBookmark
		if err = rows.Scan(
			&item.ActorID, &item.CreatedAt,
			&item.CatalogItem.ID, &item.CatalogItem.Code, &item.CatalogItem.Name,
			&item.CatalogItem.Category, &item.CatalogItem.Unit,
			&item.CatalogItem.CurrentPrice, &item.CatalogItem.PriceScale,
			&item.CatalogItem.Source, &item.CatalogItem.Enabled,
			&item.CatalogItem.RowVersion, &item.CatalogItem.CreatedAt,
			&item.CatalogItem.UpdatedAt,
		); err != nil {
			return nil, err
		}
		item.DeepLink = "/app/mrs?item=" + item.CatalogItem.ID
		items = append(items, item)
	}
	return items, rows.Err()
}

// IsBookmarked exposes the current state required by bookmark toggles without
// forcing the client to download the complete bookmark list.
func (r *MRSCatalogRepository) IsBookmarked(ctx context.Context, actor, item string) (bool, error) {
	actor = strings.TrimSpace(actor)
	item = strings.TrimSpace(item)
	if actor == "" || item == "" {
		return false, errx.New(errx.CodeInvalidArgument, "actor_id and catalog item id are required", "P3-G-MRS-BOOKMARK")
	}
	var count int
	if err := r.store.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM mrs_bookmarks WHERE actor_id=? AND catalog_item_id=?`, actor, item).Scan(&count); err != nil {
		return false, err
	}
	return count > 0, nil
}
