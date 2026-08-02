package sqlite

import (
	"context"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
)

func (r *MRSOperationsRepository) DiffRecipeVersions(ctx context.Context,leftID,rightID string)(map[string]any,error){
	left,err:=r.GetRecipeVersion(ctx,leftID);if err!=nil{return nil,err}
	right,err:=r.GetRecipeVersion(ctx,rightID);if err!=nil{return nil,err}
	difference,err:=money.Sum([]string{right.UnitPrice,"-"+left.UnitPrice},2);if err!=nil{return nil,err}
	return map[string]any{
		"left_version_id":leftID,
		"right_version_id":rightID,
		"left_unit_price":left.UnitPrice,
		"right_unit_price":right.UnitPrice,
		"difference":difference,
		"changed":left.UnitPrice!=right.UnitPrice,
		"left_snapshot":left.Snapshot,
		"right_snapshot":right.Snapshot,
	},nil
}
