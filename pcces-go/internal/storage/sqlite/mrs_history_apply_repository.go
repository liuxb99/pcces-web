package sqlite

import (
	"context"
	"database/sql"
	"fmt"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type MRSHistoryApplyResult struct {
	CatalogItemID, HistoryID, ApplyEventID string
	OldPrice, NewPrice, Source              string
	EffectiveDate                           *string
	RowVersion                              int64
	DeepLink                                string
}

func (r *MRSCatalogRepository) ApplyHistoricalPrice(ctx context.Context, itemID, historyID, actor string, rowVersion int64) (MRSHistoryApplyResult, error) {
	var out MRSHistoryApplyResult
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil { return out, err }
	defer func(){ _ = tx.Rollback() }()

	var currentPrice string
	var scale int
	var currentVersion int64
	if err = tx.QueryRowContext(ctx, `SELECT current_price,price_scale,row_version FROM mrs_catalog_items WHERE id=?`, itemID).Scan(&currentPrice,&scale,&currentVersion); err != nil {
		if err == sql.ErrNoRows { return out, errx.New(errx.CodeNotFound,"MRS item not found","P3-G-HISTORY") }
		return out, err
	}
	if currentVersion != rowVersion { return out, errx.New(errx.CodeConflict,"MRS row version conflict","P3-G-HISTORY") }

	var historicalPrice string
	var effectiveDate *string
	if err = tx.QueryRowContext(ctx, `SELECT new_price,effective_date FROM mrs_price_history WHERE id=? AND catalog_item_id=?`, historyID,itemID).Scan(&historicalPrice,&effectiveDate); err != nil {
		if err == sql.ErrNoRows { return out, errx.New(errx.CodeNotFound,"historical price not found","P3-G-HISTORY") }
		return out, err
	}
	appliedPrice, err := money.Quantize(historicalPrice, scale)
	if err != nil { return out, err }
	now := time.Now().UTC().Format(time.RFC3339Nano)
	res, err := tx.ExecContext(ctx, `UPDATE mrs_catalog_items SET current_price=?,source=?,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`, appliedPrice,"HISTORY:"+historyID,now,itemID,rowVersion)
	if err != nil { return out, err }
	if n,_ := res.RowsAffected(); n != 1 { return out, errx.New(errx.CodeConflict,"MRS row version conflict","P3-G-HISTORY") }
	applyID := fmt.Sprintf("mrs-history-apply-%d", time.Now().UnixNano())
	_, err = tx.ExecContext(ctx, `INSERT INTO mrs_price_history(id,catalog_item_id,old_price,new_price,source,effective_date,created_by,created_at) VALUES(?,?,?,?,?,?,?,?)`, applyID,itemID,currentPrice,appliedPrice,"HISTORY_APPLY:"+historyID,effectiveDate,actor,now)
	if err != nil { return out, err }
	if err = tx.Commit(); err != nil { return out, err }
	oldPrice, _ := money.Quantize(currentPrice, scale)
	return MRSHistoryApplyResult{CatalogItemID:itemID,HistoryID:historyID,ApplyEventID:applyID,OldPrice:oldPrice,NewPrice:appliedPrice,Source:"HISTORY:"+historyID,EffectiveDate:effectiveDate,RowVersion:rowVersion+1,DeepLink:"/app/mrs-operations?item="+itemID+"&history="+historyID},nil
}
