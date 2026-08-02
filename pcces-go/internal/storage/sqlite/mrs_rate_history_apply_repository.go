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

type MRSRateApplyComponent struct {
	CatalogItemID string `json:"catalog_item_id"`
	Quantity      string `json:"quantity"`
}

type MRSRateApplyResult struct {
	RecipeID         string                  `json:"recipe_id"`
	VersionID        string                  `json:"version_id"`
	Actor             string                  `json:"actor"`
	AppliedComponents []MRSRateApplyComponent `json:"applied_components"`
	ComponentCount    int                     `json:"component_count"`
	RowVersion        int64                   `json:"row_version"`
	DeepLink          string                  `json:"deep_link"`
}

func (r *MRSCatalogRepository) ApplyHistoricalRates(ctx context.Context, recipeID, versionID string, rowVersion int64, actor string) (MRSRateApplyResult, error) {
	result := MRSRateApplyResult{RecipeID: recipeID, VersionID: versionID, Actor: actor}
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil { return result, err }
	defer func(){ _ = tx.Rollback() }()

	var currentVersion int64
	if err = tx.QueryRowContext(ctx, `SELECT row_version FROM mrs_analysis_recipes WHERE id=?`, recipeID).Scan(&currentVersion); err != nil {
		if err == sql.ErrNoRows { return result, errx.New(errx.CodeNotFound,"recipe not found","P3-G-MRS-RATE-HISTORY") }
		return result, err
	}
	if currentVersion != rowVersion { return result, errx.New(errx.CodeConflict,"recipe row version conflict","P3-G-MRS-RATE-HISTORY") }

	var snapshotJSON string
	if err = tx.QueryRowContext(ctx, `SELECT snapshot_json FROM mrs_recipe_versions WHERE id=? AND recipe_id=?`, versionID, recipeID).Scan(&snapshotJSON); err != nil {
		if err == sql.ErrNoRows { return result, errx.New(errx.CodeNotFound,"recipe version not found","P3-G-MRS-RATE-HISTORY") }
		return result, err
	}
	var snapshot struct { Components []struct { CatalogItemID string `json:"catalog_item_id"`; Quantity string `json:"quantity"` } `json:"components"` }
	if err = json.Unmarshal([]byte(snapshotJSON), &snapshot); err != nil { return result, errx.Wrap(errx.CodeInvalidArgument,"invalid recipe version snapshot","P3-G-MRS-RATE-HISTORY",err) }
	if _, err = tx.ExecContext(ctx, `DELETE FROM mrs_analysis_components WHERE recipe_id=?`, recipeID); err != nil { return result, err }
	for index, component := range snapshot.Components {
		if component.CatalogItemID == "" { return result, errx.New(errx.CodeInvalidArgument,"historical component catalog_item_id is required","P3-G-MRS-RATE-HISTORY") }
		quantity, quantizeErr := money.Quantize(component.Quantity, 4)
		if quantizeErr != nil { return result, quantizeErr }
		var exists int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM mrs_catalog_items WHERE id=?`, component.CatalogItemID).Scan(&exists); err != nil { return result, err }
		if exists != 1 { return result, errx.New(errx.CodeNotFound,"historical component catalog item not found","P3-G-MRS-RATE-HISTORY") }
		id := fmt.Sprintf("%s-history-%d-%d", recipeID, time.Now().UnixNano(), index)
		if _, err = tx.ExecContext(ctx, `INSERT INTO mrs_analysis_components(id,recipe_id,catalog_item_id,quantity,quantity_scale,sequence) VALUES(?,?,?,?,4,?)`, id, recipeID, component.CatalogItemID, quantity, index); err != nil { return result, err }
		result.AppliedComponents = append(result.AppliedComponents, MRSRateApplyComponent{CatalogItemID:component.CatalogItemID, Quantity:quantity})
	}
	update, err := tx.ExecContext(ctx, `UPDATE mrs_analysis_recipes SET updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`, time.Now().UTC().Format(time.RFC3339Nano), recipeID, rowVersion)
	if err != nil { return result, err }
	if affected,_ := update.RowsAffected(); affected != 1 { return result, errx.New(errx.CodeConflict,"recipe row version conflict","P3-G-MRS-RATE-HISTORY") }
	if err = tx.Commit(); err != nil { return result, err }
	result.ComponentCount = len(result.AppliedComponents)
	result.RowVersion = rowVersion + 1
	result.DeepLink = "/app/mrs-operations?recipe="+recipeID+"&version="+versionID+"&applied=1"
	return result, nil
}
