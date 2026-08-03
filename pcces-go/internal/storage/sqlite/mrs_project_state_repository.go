package sqlite

import (
	"context"
	"database/sql"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type MRSProjectState struct {
	ProjectCode       string  `json:"project_code"`
	State             string  `json:"state"`
	Template          bool    `json:"template"`
	Readonly          bool    `json:"readonly"`
	Reason            *string `json:"reason"`
	UpdatedBy         string  `json:"updated_by"`
	UpdatedAt         string  `json:"updated_at"`
	RowVersion        int64   `json:"row_version"`
	EffectiveReadonly bool    `json:"effective_readonly"`
}

type MRSProjectStateRepository struct{ store *Store }

func NewMRSProjectStateRepository(store *Store) *MRSProjectStateRepository {
	return &MRSProjectStateRepository{store: store}
}

func (r *MRSProjectStateRepository) Get(ctx context.Context, projectCode string) (MRSProjectState, error) {
	var v MRSProjectState
	var template, readonly int
	err := r.store.db.QueryRowContext(ctx, `SELECT project_code,state,template,readonly,reason,updated_by,updated_at,row_version FROM mrs_project_states WHERE project_code=?`, projectCode).Scan(&v.ProjectCode, &v.State, &template, &readonly, &v.Reason, &v.UpdatedBy, &v.UpdatedAt, &v.RowVersion)
	if err == sql.ErrNoRows {
		return MRSProjectState{ProjectCode: projectCode, State: "DRAFT"}, nil
	}
	if err != nil {
		return v, err
	}
	v.Template = template == 1
	v.Readonly = readonly == 1
	v.EffectiveReadonly = v.Template || v.Readonly || v.State == "APPROVED" || v.State == "ARCHIVED"
	return v, nil
}

func (r *MRSProjectStateRepository) Save(ctx context.Context, projectCode, state string, template, readonly bool, reason, actor string, rowVersion int64) (MRSProjectState, error) {
	state = strings.ToUpper(strings.TrimSpace(state))
	allowed := map[string]bool{"DRAFT": true, "SUBMITTED": true, "APPROVED": true, "ARCHIVED": true}
	if !allowed[state] {
		return MRSProjectState{}, errx.New(errx.CodeInvalidArgument, "invalid MRS project state", "P3-STATE")
	}
	current, err := r.Get(ctx, projectCode)
	if err != nil {
		return current, err
	}
	if current.RowVersion != rowVersion {
		return current, errx.New(errx.CodeConflict, "project state row_version conflict", "P3-STATE")
	}
	transitions := map[string]map[string]bool{"DRAFT": {"DRAFT": true, "SUBMITTED": true}, "SUBMITTED": {"DRAFT": true, "SUBMITTED": true, "APPROVED": true}, "APPROVED": {"APPROVED": true, "ARCHIVED": true}, "ARCHIVED": {"ARCHIVED": true}}
	if !transitions[current.State][state] {
		return current, errx.New(errx.CodeInvalidArgument, "invalid MRS project state transition", "P3-STATE")
	}
	if template && state == "APPROVED" {
		return current, errx.New(errx.CodeInvalidArgument, "template project cannot be approved", "P3-STATE")
	}
	t := 0
	if template {
		t = 1
	}
	ro := 0
	if readonly {
		ro = 1
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	if rowVersion == 0 {
		_, err = r.store.db.ExecContext(ctx, `INSERT INTO mrs_project_states(project_code,state,template,readonly,reason,updated_by,updated_at,row_version) VALUES(?,?,?,?,?,?,?,1)`, projectCode, state, t, ro, nullableString(reason), actor, now)
	} else {
		res, e := r.store.db.ExecContext(ctx, `UPDATE mrs_project_states SET state=?,template=?,readonly=?,reason=?,updated_by=?,updated_at=?,row_version=row_version+1 WHERE project_code=? AND row_version=?`, state, t, ro, nullableString(reason), actor, now, projectCode, rowVersion)
		err = e
		if err == nil {
			n, _ := res.RowsAffected()
			if n != 1 {
				err = errx.New(errx.CodeConflict, "project state row_version conflict", "P3-STATE")
			}
		}
	}
	if err != nil {
		return current, err
	}
	return r.Get(ctx, projectCode)
}

func (r *MRSProjectStateRepository) AssertWritable(ctx context.Context, projectCode string) error {
	v, err := r.Get(ctx, projectCode)
	if err != nil {
		return err
	}
	if v.EffectiveReadonly {
		return errx.New(errx.CodeConflict, "project MRS is read-only", "P3-STATE")
	}
	return nil
}
