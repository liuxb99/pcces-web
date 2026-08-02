package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type ResourceProjectReference struct {
	ID, TargetProjectCode, SourceProjectCode string
	SourceResourceID, TargetResourceID       string
	ReferenceType, SnapshotJSON              string
	CreatedBy, CreatedAt, DeepLink            string
}

type ResourceProjectReferenceRepository struct{ store *Store }

func NewResourceProjectReferenceRepository(store *Store) *ResourceProjectReferenceRepository {
	return &ResourceProjectReferenceRepository{store: store}
}

func (r *ResourceProjectReferenceRepository) Import(ctx context.Context, targetProject, sourceProject, sourceResourceID, targetResourceID, referenceType, actor string) (ResourceProjectReference, error) {
	kind := strings.ToUpper(strings.TrimSpace(referenceType))
	if kind != "PARENT" && kind != "HISTORICAL" {
		return ResourceProjectReference{}, errx.New(errx.CodeInvalidArgument, "reference_type must be PARENT or HISTORICAL", "P3-G-REF")
	}
	if strings.TrimSpace(targetProject) == "" || strings.TrimSpace(sourceProject) == "" || strings.TrimSpace(sourceResourceID) == "" || strings.TrimSpace(targetResourceID) == "" {
		return ResourceProjectReference{}, errx.New(errx.CodeInvalidArgument, "project and resource identifiers are required", "P3-G-REF")
	}
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil { return ResourceProjectReference{}, err }
	defer tx.Rollback()
	var source ResourceDecimal
	if err = tx.QueryRowContext(ctx, `SELECT id,code,name,unit,unit_price,price_scale,row_version FROM resources_decimal WHERE id=?`, sourceResourceID).Scan(&source.ID,&source.Code,&source.Name,&source.Unit,&source.UnitPrice,&source.PriceScale,&source.RowVersion); err != nil {
		if err == sql.ErrNoRows { return ResourceProjectReference{}, errx.New(errx.CodeNotFound, "source resource not found", "P3-G-REF") }
		return ResourceProjectReference{}, err
	}
	var exists int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM resources_decimal WHERE id=?`, targetResourceID).Scan(&exists); err != nil { return ResourceProjectReference{}, err }
	if exists != 0 { return ResourceProjectReference{}, errx.New(errx.CodeConflict, "target resource already exists", "P3-G-REF") }
	now := time.Now().UTC().Format(time.RFC3339Nano)
	if _, err = tx.ExecContext(ctx, `INSERT INTO resources_decimal(id,code,name,unit,unit_price,price_scale,created_at,updated_at,row_version) VALUES(?,?,?,?,?,?,?,?,1)`, targetResourceID,targetProject+":"+source.Code,source.Name,source.Unit,source.UnitPrice,source.PriceScale,now,now); err != nil { return ResourceProjectReference{}, err }
	snapshot, _ := json.Marshal(map[string]any{"id":source.ID,"code":source.Code,"name":source.Name,"unit":source.Unit,"unit_price":source.UnitPrice,"price_scale":source.PriceScale,"row_version":source.RowVersion})
	id := fmt.Sprintf("ref-%d", time.Now().UnixNano())
	if _, err = tx.ExecContext(ctx, `INSERT INTO resource_project_references(id,target_project_code,source_project_code,source_resource_id,target_resource_id,reference_type,snapshot_json,created_by,created_at) VALUES(?,?,?,?,?,?,?,?,?)`, id,targetProject,sourceProject,sourceResourceID,targetResourceID,kind,string(snapshot),actor,now); err != nil { return ResourceProjectReference{}, err }
	if err = tx.Commit(); err != nil { return ResourceProjectReference{}, err }
	return r.Get(ctx,id)
}

func (r *ResourceProjectReferenceRepository) Get(ctx context.Context, id string) (ResourceProjectReference,error) {
	var v ResourceProjectReference
	err:=r.store.db.QueryRowContext(ctx,`SELECT id,target_project_code,source_project_code,source_resource_id,target_resource_id,reference_type,snapshot_json,created_by,created_at FROM resource_project_references WHERE id=?`,id).Scan(&v.ID,&v.TargetProjectCode,&v.SourceProjectCode,&v.SourceResourceID,&v.TargetResourceID,&v.ReferenceType,&v.SnapshotJSON,&v.CreatedBy,&v.CreatedAt)
	if err==sql.ErrNoRows{return v,errx.New(errx.CodeNotFound,"resource project reference not found","P3-G-REF")}
	v.DeepLink="/app/project-resources?project="+v.TargetProjectCode+"&resource="+v.TargetResourceID
	return v,err
}

func (r *ResourceProjectReferenceRepository) ListTarget(ctx context.Context,targetProject string)([]ResourceProjectReference,error){
	rows,err:=r.store.db.QueryContext(ctx,`SELECT id,target_project_code,source_project_code,source_resource_id,target_resource_id,reference_type,snapshot_json,created_by,created_at FROM resource_project_references WHERE target_project_code=? ORDER BY created_at DESC`,targetProject)
	if err!=nil{return nil,err};defer rows.Close();out:=[]ResourceProjectReference{}
	for rows.Next(){var v ResourceProjectReference;if err=rows.Scan(&v.ID,&v.TargetProjectCode,&v.SourceProjectCode,&v.SourceResourceID,&v.TargetResourceID,&v.ReferenceType,&v.SnapshotJSON,&v.CreatedBy,&v.CreatedAt);err!=nil{return nil,err};v.DeepLink="/app/project-resources?project="+v.TargetProjectCode+"&resource="+v.TargetResourceID;out=append(out,v)}
	return out,rows.Err()
}
