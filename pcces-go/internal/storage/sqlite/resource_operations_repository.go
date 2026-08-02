package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"strings"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type ResourceReplacementResult struct {
	ProjectCode, SourceResourceID, TargetResourceID, ActorID string
	MovedLinks, DeduplicatedLinks                            int64
}

type ResourcePriceUpdate struct {
	ResourceID string `json:"resource_id"`
	UnitPrice  string `json:"unit_price"`
	RowVersion int64  `json:"row_version"`
}

type ResourcePriceChange struct {
	ResourceID, OldUnitPrice, NewUnitPrice string
	RowVersion                             int64
}

type ResourceBatchPriceResult struct {
	UpdatedResources   int                    `json:"updated_resources"`
	UpdatedBudgetItems int                    `json:"updated_budget_items"`
	Resources          []ResourcePriceChange  `json:"resources"`
	Lineage            []ResourcePriceLineage `json:"lineage"`
}

func (r *ResourceBudgetLineageRepository) ReplaceResource(ctx context.Context, projectCode, sourceID, targetID, actorID string) (ResourceReplacementResult, error) {
	result := ResourceReplacementResult{ProjectCode: projectCode, SourceResourceID: sourceID, TargetResourceID: targetID, ActorID: actorID}
	if strings.TrimSpace(projectCode) == "" || strings.TrimSpace(sourceID) == "" || strings.TrimSpace(targetID) == "" || sourceID == targetID {
		return result, errx.New(errx.CodeInvalidArgument, "project_code and distinct source/target resources are required", "P3-G-RESOURCE-OPS")
	}
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil { return result, err }
	defer tx.Rollback()
	for _, id := range []string{sourceID, targetID} {
		var count int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM resources_decimal WHERE id=?`, id).Scan(&count); err != nil { return result, err }
		if count != 1 { return result, errx.New(errx.CodeNotFound, "source or target resource not found", "P3-G-RESOURCE-OPS") }
	}
	rows, err := tx.QueryContext(ctx, `SELECT budget_item_id FROM resource_budget_links WHERE project_code=? AND resource_id=?`, projectCode, sourceID)
	if err != nil { return result, err }
	var items []string
	for rows.Next() { var id string; if err = rows.Scan(&id); err != nil { rows.Close(); return result, err }; items = append(items, id) }
	if err = rows.Close(); err != nil { return result, err }
	now := time.Now().UTC().Format(time.RFC3339Nano)
	for _, itemID := range items {
		newID := projectCode + ":" + targetID + ":" + itemID
		res, execErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO resource_budget_links(id,project_code,resource_id,budget_item_id,created_at) VALUES(?,?,?,?,?)`, newID, projectCode, targetID, itemID, now)
		if execErr != nil { return result, execErr }
		n, _ := res.RowsAffected(); if n == 1 { result.MovedLinks++ } else { result.DeduplicatedLinks++ }
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM resource_budget_links WHERE project_code=? AND resource_id=?`, projectCode, sourceID); err != nil { return result, err }
	if err = tx.Commit(); err != nil { return result, err }
	return result, nil
}

func (r *ResourceBudgetLineageRepository) BatchUpdatePrices(ctx context.Context, updates []ResourcePriceUpdate, trigger string) (ResourceBatchPriceResult, error) {
	result := ResourceBatchPriceResult{Resources: []ResourcePriceChange{}, Lineage: []ResourcePriceLineage{}}
	if len(updates) == 0 { return result, errx.New(errx.CodeInvalidArgument, "updates are required", "P3-G-RESOURCE-OPS") }
	if strings.TrimSpace(trigger) == "" { trigger = "BATCH_RESOURCE_PRICE_UPDATE" }
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil { return result, err }
	defer tx.Rollback()
	type prepared struct { update ResourcePriceUpdate; oldPrice, newPrice string; scale int }
	preparedRows := make([]prepared, 0, len(updates)); seen := map[string]bool{}
	for _, update := range updates {
		update.ResourceID = strings.TrimSpace(update.ResourceID)
		if update.ResourceID == "" || seen[update.ResourceID] { return result, errx.New(errx.CodeInvalidArgument, "resource_id must be present and unique", "P3-G-RESOURCE-OPS") }
		seen[update.ResourceID] = true
		var oldPrice string; var scale int; var currentVersion int64
		err = tx.QueryRowContext(ctx, `SELECT unit_price,price_scale,row_version FROM resources_decimal WHERE id=?`, update.ResourceID).Scan(&oldPrice,&scale,&currentVersion)
		if err == sql.ErrNoRows { return result, errx.New(errx.CodeNotFound, "resource not found", "P3-G-RESOURCE-OPS") }
		if err != nil { return result, err }
		if currentVersion != update.RowVersion { return result, errx.New(errx.CodeConflict, "resource row_version conflict", "P3-G-RESOURCE-OPS") }
		newPrice, qErr := money.Quantize(update.UnitPrice, scale); if qErr != nil { return result, qErr }
		preparedRows = append(preparedRows, prepared{update:update,oldPrice:oldPrice,newPrice:newPrice,scale:scale})
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	for _, row := range preparedRows {
		res, execErr := tx.ExecContext(ctx, `UPDATE resources_decimal SET unit_price=?,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`, row.newPrice, now, row.update.ResourceID, row.update.RowVersion)
		if execErr != nil { return result, execErr }; n,_:=res.RowsAffected(); if n!=1 { return result, errx.New(errx.CodeConflict,"resource row_version conflict","P3-G-RESOURCE-OPS") }
		result.Resources = append(result.Resources, ResourcePriceChange{ResourceID:row.update.ResourceID,OldUnitPrice:row.oldPrice,NewUnitPrice:row.newPrice,RowVersion:row.update.RowVersion+1})
		links, linkErr := tx.QueryContext(ctx, `SELECT project_code,budget_item_id FROM resource_budget_links WHERE resource_id=?`, row.update.ResourceID)
		if linkErr != nil { return result, linkErr }
		type link struct{ project, item string }; var linkRows []link
		for links.Next(){var v link;if linkErr=links.Scan(&v.project,&v.item);linkErr!=nil{links.Close();return result,linkErr};linkRows=append(linkRows,v)}
		links.Close()
		for index, link := range linkRows {
			var quantity, oldUnitPrice, oldAmount string; var priceScale, amountScale int; var itemVersion int64
			err = tx.QueryRowContext(ctx, `SELECT quantity,unit_price,amount,price_scale,amount_scale,row_version FROM budget_items_decimal WHERE id=?`, link.item).Scan(&quantity,&oldUnitPrice,&oldAmount,&priceScale,&amountScale,&itemVersion)
			if err == sql.ErrNoRows { continue }; if err != nil { return result, err }
			newItemPrice, calcErr := money.Quantize(row.newPrice, priceScale); if calcErr != nil { return result, calcErr }
			newAmount, calcErr := money.CalculateBudgetLeaf(quantity,newItemPrice,amountScale); if calcErr != nil { return result, calcErr }
			if _, err = tx.ExecContext(ctx, `UPDATE budget_items_decimal SET unit_price=?,amount=?,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`,newItemPrice,newAmount,now,link.item,itemVersion); err != nil { return result, err }
			trace,_:=json.Marshal(map[string]string{"operation":"BATCH_RESOURCE_PRICE_PROPAGATION","quantity":quantity,"resource_unit_price":newItemPrice,"result":newAmount})
			lineage:=ResourcePriceLineage{ID:fmt.Sprintf("batch-%d-%d",time.Now().UnixNano(),index),ProjectCode:link.project,ResourceID:row.update.ResourceID,BudgetItemID:link.item,OldUnitPrice:oldUnitPrice,NewUnitPrice:newItemPrice,OldAmount:oldAmount,NewAmount:newAmount,Trigger:trigger,TraceJSON:string(trace),CreatedAt:now}
			_,err=tx.ExecContext(ctx,`INSERT INTO resource_price_lineage(id,project_code,resource_id,budget_item_id,old_unit_price,new_unit_price,old_amount,new_amount,trigger,trace_json,created_at) VALUES(?,?,?,?,?,?,?,?,?,?,?)`,lineage.ID,lineage.ProjectCode,lineage.ResourceID,lineage.BudgetItemID,lineage.OldUnitPrice,lineage.NewUnitPrice,lineage.OldAmount,lineage.NewAmount,lineage.Trigger,lineage.TraceJSON,lineage.CreatedAt)
			if err != nil { return result, err }; result.Lineage=append(result.Lineage,lineage)
		}
	}
	if err = tx.Commit(); err != nil { return result, err }
	result.UpdatedResources=len(result.Resources); result.UpdatedBudgetItems=len(result.Lineage)
	return result,nil
}
