package sqlite

import (
	"context"
	"crypto/sha256"
	"database/sql"
	"encoding/hex"
	"encoding/json"
	"os"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

func (r *ReportAdminRepository) CreateGroup(ctx context.Context, id, code, name, actor string) (map[string]any, error) {
	code, name = strings.TrimSpace(code), strings.TrimSpace(name)
	if id == "" || code == "" || name == "" || actor == "" {
		return nil, errx.New(errx.CodeInvalidArgument, "required group fields are missing", "P8-G")
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	tx, e := r.store.db.BeginTx(ctx, nil)
	if e != nil {
		return nil, e
	}
	defer func() { _ = tx.Rollback() }()
	if _, e = tx.ExecContext(ctx, `INSERT INTO admin_groups(id,code,name,created_by,created_at) VALUES(?,?,?,?,?)`, id, code, name, actor, now); e != nil {
		return nil, e
	}
	payload, _ := json.Marshal(map[string]any{"code": code, "name": name})
	if _, e = tx.ExecContext(ctx, `INSERT INTO admin_audit(id,actor,action,target,payload_json,created_at) VALUES(?,?,?,?,?,?)`, id+"-audit", actor, "GROUP_CREATE", id, string(payload), now); e != nil {
		return nil, e
	}
	if e = tx.Commit(); e != nil {
		return nil, e
	}
	return map[string]any{"id": id, "code": code, "name": name}, nil
}

func (r *ReportAdminRepository) AddGroupMember(ctx context.Context, groupID, userID, actor string) (map[string]any, error) {
	var exists int
	if e := r.store.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM admin_groups WHERE id=?`, groupID).Scan(&exists); e != nil {
		return nil, e
	}
	if exists == 0 {
		return nil, errx.New(errx.CodeNotFound, "group not found", "P8-G")
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	_, e := r.store.db.ExecContext(ctx, `INSERT OR IGNORE INTO admin_group_members(group_id,user_id,created_at) VALUES(?,?,?)`, groupID, userID, now)
	if e != nil {
		return nil, e
	}
	return map[string]any{"group_id": groupID, "user_id": userID}, nil
}

func (r *ReportAdminRepository) CreateBackup(ctx context.Context, id, databaseURL, actor string) (map[string]any, error) {
	if !strings.HasPrefix(databaseURL, "sqlite:///") {
		return nil, errx.New(errx.CodeInvalidArgument, "automatic backup supports sqlite only", "P8-G")
	}
	path := strings.TrimPrefix(databaseURL, "sqlite:///")
	content, e := os.ReadFile(path)
	if e != nil && !os.IsNotExist(e) {
		return nil, e
	}
	sum := sha256.Sum256(content)
	digest := hex.EncodeToString(sum[:])
	pre, _ := json.Marshal(map[string]any{"supported": true, "database_url_present": databaseURL != ""})
	smoke, _ := json.Marshal(map[string]any{"sqlite_header": len(content) == 0 || strings.HasPrefix(string(content), "SQLite format 3"), "size_bytes": len(content)})
	now := time.Now().UTC().Format(time.RFC3339Nano)
	_, e = r.store.db.ExecContext(ctx, `INSERT INTO backup_runs(id,status,database_url,sha256,size_bytes,artifact,precheck_json,smoke_json,created_by,created_at,completed_at,row_version) VALUES(?,'COMPLETED',?,?,?,?,?,?,?,?,?,1)`, id, databaseURL, digest, len(content), content, string(pre), string(smoke), actor, now, now)
	if e != nil {
		return nil, e
	}
	return r.GetBackup(ctx, id)
}
func (r *ReportAdminRepository) GetBackup(ctx context.Context, id string) (map[string]any, error) {
	var status, digest, pre, smoke string
	var size int64
	var rv int64
	if e := r.store.db.QueryRowContext(ctx, `SELECT status,COALESCE(sha256,''),COALESCE(size_bytes,0),precheck_json,COALESCE(smoke_json,'{}'),row_version FROM backup_runs WHERE id=?`, id).Scan(&status, &digest, &size, &pre, &smoke, &rv); e == sql.ErrNoRows {
		return nil, errx.New(errx.CodeNotFound, "backup not found", "P8-G")
	} else if e != nil {
		return nil, e
	}
	var p, s map[string]any
	_ = json.Unmarshal([]byte(pre), &p)
	_ = json.Unmarshal([]byte(smoke), &s)
	return map[string]any{"id": id, "status": status, "sha256": digest, "size_bytes": size, "precheck": p, "smoke": s, "row_version": rv, "download_url": "/api/admin/backups/" + id + "/download"}, nil
}
func (r *ReportAdminRepository) BackupArtifact(ctx context.Context, id string) ([]byte, error) {
	var content []byte
	if e := r.store.db.QueryRowContext(ctx, `SELECT artifact FROM backup_runs WHERE id=? AND status='COMPLETED'`, id).Scan(&content); e == sql.ErrNoRows {
		return nil, errx.New(errx.CodeNotFound, "backup artifact not found", "P8-G")
	} else if e != nil {
		return nil, e
	}
	return content, nil
}
func (r *ReportAdminRepository) Health(ctx context.Context) (map[string]any, error) {
	if e := r.store.db.QueryRowContext(ctx, `SELECT 1`).Scan(new(int)); e != nil {
		return nil, e
	}
	return map[string]any{"status": "ok", "database": "reachable", "timestamp": time.Now().UTC().Format(time.RFC3339Nano)}, nil
}
