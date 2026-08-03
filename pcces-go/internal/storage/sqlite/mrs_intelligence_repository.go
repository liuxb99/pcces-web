package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type MRSQuote struct {
	ID, CatalogItemID, Vendor, QuotedPrice string
	PriceScale                             int
	SourceDocument, EffectiveDate          *string
	CreatedBy, CreatedAt                   string
}

type MRSRecipeSnapshot struct {
	ID, RecipeID, UnitPrice, CreatedBy, CreatedAt, DeepLink string
	Snapshot                                                MRSAnalysisResult
}

type MRSImpactResult struct {
	ID, CatalogItemID, OldPrice, NewPrice, TotalComponentDelta, DeepLink, CreatedAt string
	AffectedCount                                                                   int
	AffectedRecipes                                                                 []map[string]any
}

type MRSIntelligenceRepository struct{ store *Store }

func NewMRSIntelligenceRepository(store *Store) *MRSIntelligenceRepository {
	return &MRSIntelligenceRepository{store: store}
}

func (r *MRSIntelligenceRepository) AddQuote(ctx context.Context, quote MRSQuote) (MRSQuote, error) {
	if quote.ID == "" || quote.CatalogItemID == "" || quote.Vendor == "" {
		return quote, errx.New(errx.CodeInvalidArgument, "id, catalog item and vendor are required", "P3-G-MRS-INTEL")
	}
	if _, err := NewMRSCatalogRepository(r.store).GetItem(ctx, quote.CatalogItemID); err != nil {
		return quote, err
	}
	price, err := money.Quantize(quote.QuotedPrice, quote.PriceScale)
	if err != nil {
		return quote, err
	}
	quote.QuotedPrice = price
	quote.CreatedAt = time.Now().UTC().Format(time.RFC3339Nano)
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO mrs_price_quotes(id,catalog_item_id,vendor,quoted_price,price_scale,source_document,effective_date,created_by,created_at) VALUES(?,?,?,?,?,?,?,?,?)`, quote.ID, quote.CatalogItemID, quote.Vendor, quote.QuotedPrice, quote.PriceScale, quote.SourceDocument, quote.EffectiveDate, quote.CreatedBy, quote.CreatedAt)
	return quote, err
}

func (r *MRSIntelligenceRepository) CompareQuotes(ctx context.Context, itemID string) (map[string]any, error) {
	item, err := NewMRSCatalogRepository(r.store).GetItem(ctx, itemID)
	if err != nil {
		return nil, err
	}
	rows, err := r.store.db.QueryContext(ctx, `SELECT id,catalog_item_id,vendor,quoted_price,price_scale,source_document,effective_date,created_by,created_at FROM mrs_price_quotes WHERE catalog_item_id=? ORDER BY created_at DESC`, itemID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var quotes []MRSQuote
	var low, high string
	for rows.Next() {
		var q MRSQuote
		if err = rows.Scan(&q.ID, &q.CatalogItemID, &q.Vendor, &q.QuotedPrice, &q.PriceScale, &q.SourceDocument, &q.EffectiveDate, &q.CreatedBy, &q.CreatedAt); err != nil {
			return nil, err
		}
		quotes = append(quotes, q)
		if low == "" {
			low, high = q.QuotedPrice, q.QuotedPrice
		} else {
			less, _ := money.Sum([]string{q.QuotedPrice, "-" + low}, item.PriceScale)
			if less[0] == '-' {
				low = q.QuotedPrice
			}
			more, _ := money.Sum([]string{q.QuotedPrice, "-" + high}, item.PriceScale)
			if more[0] != '-' && more != moneyZero(item.PriceScale) {
				high = q.QuotedPrice
			}
		}
	}
	spread, currentDelta := moneyZero(item.PriceScale), moneyZero(item.PriceScale)
	if low != "" {
		spread, err = money.Sum([]string{high, "-" + low}, item.PriceScale)
		if err != nil {
			return nil, err
		}
		currentDelta, err = money.Sum([]string{item.CurrentPrice, "-" + low}, item.PriceScale)
		if err != nil {
			return nil, err
		}
	}
	return map[string]any{"catalog_item_id": itemID, "current_price": item.CurrentPrice, "quotes": quotes, "lowest_quote": nullableString(low), "highest_quote": nullableString(high), "spread": spread, "current_vs_lowest": currentDelta}, nil
}

func (r *MRSIntelligenceRepository) SnapshotRecipe(ctx context.Context, id, recipeID, actor string) (MRSRecipeSnapshot, error) {
	calculation, err := NewMRSCatalogRepository(r.store).CalculateRecipe(ctx, recipeID)
	if err != nil {
		return MRSRecipeSnapshot{}, err
	}
	payload, _ := json.Marshal(calculation)
	now := time.Now().UTC().Format(time.RFC3339Nano)
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO mrs_analysis_snapshots(id,recipe_id,unit_price,snapshot_json,created_by,created_at) VALUES(?,?,?,?,?,?)`, id, recipeID, calculation.UnitPrice, string(payload), actor, now)
	if err != nil {
		return MRSRecipeSnapshot{}, err
	}
	return MRSRecipeSnapshot{ID: id, RecipeID: recipeID, UnitPrice: calculation.UnitPrice, CreatedBy: actor, CreatedAt: now, DeepLink: fmt.Sprintf("/app/mrs-insights?recipe=%s&snapshot=%s", recipeID, id), Snapshot: calculation}, nil
}

func (r *MRSIntelligenceRepository) Impact(ctx context.Context, id, itemID, oldPrice, newPrice, actor string) (MRSImpactResult, error) {
	item, err := NewMRSCatalogRepository(r.store).GetItem(ctx, itemID)
	if err != nil {
		return MRSImpactResult{}, err
	}
	if oldPrice == "" {
		oldPrice = item.CurrentPrice
	}
	if newPrice == "" {
		newPrice = item.CurrentPrice
	}
	oldPrice, err = money.Quantize(oldPrice, item.PriceScale)
	if err != nil {
		return MRSImpactResult{}, err
	}
	newPrice, err = money.Quantize(newPrice, item.PriceScale)
	if err != nil {
		return MRSImpactResult{}, err
	}
	rows, err := r.store.db.QueryContext(ctx, `SELECT c.recipe_id,c.quantity,r.code,r.name,r.price_scale FROM mrs_analysis_components c JOIN mrs_analysis_recipes r ON r.id=c.recipe_id WHERE c.catalog_item_id=?`, itemID)
	if err != nil {
		return MRSImpactResult{}, err
	}
	defer rows.Close()
	result := MRSImpactResult{ID: id, CatalogItemID: itemID, OldPrice: oldPrice, NewPrice: newPrice, DeepLink: fmt.Sprintf("/app/mrs-insights?item=%s&impact=%s", itemID, id), CreatedAt: time.Now().UTC().Format(time.RFC3339Nano)}
	var deltas []string
	for rows.Next() {
		var recipeID, quantity, code, name string
		var scale int
		if err = rows.Scan(&recipeID, &quantity, &code, &name, &scale); err != nil {
			return result, err
		}
		oldAmount, e := money.Multiply(quantity, oldPrice, scale)
		if e != nil {
			return result, e
		}
		newAmount, e := money.Multiply(quantity, newPrice, scale)
		if e != nil {
			return result, e
		}
		delta, e := money.Sum([]string{newAmount, "-" + oldAmount}, scale)
		if e != nil {
			return result, e
		}
		deltas = append(deltas, delta)
		result.AffectedRecipes = append(result.AffectedRecipes, map[string]any{"recipe_id": recipeID, "recipe_code": code, "recipe_name": name, "quantity": quantity, "old_amount": oldAmount, "new_amount": newAmount, "delta": delta})
	}
	result.AffectedCount = len(result.AffectedRecipes)
	result.TotalComponentDelta, err = money.Sum(deltas, item.PriceScale)
	if err != nil {
		return result, err
	}
	payload, _ := json.Marshal(result)
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO mrs_impact_runs(id,catalog_item_id,old_price,new_price,result_json,created_by,created_at) VALUES(?,?,?,?,?,?,?)`, id, itemID, oldPrice, newPrice, string(payload), actor, result.CreatedAt)
	return result, err
}

func moneyZero(scale int) string { z, _ := money.Quantize("0", scale); return z }
func nullableString(value string) any {
	if value == "" {
		return nil
	}
	return value
}

func (r *MRSIntelligenceRepository) SnapshotCount(ctx context.Context, recipeID string) (int, error) {
	var count int
	err := r.store.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM mrs_analysis_snapshots WHERE recipe_id=?`, recipeID).Scan(&count)
	if err == sql.ErrNoRows {
		return 0, nil
	}
	return count, err
}
