package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"strings"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/authorization"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

// AuthorizationRepository evaluates module entitlements and Legacy function-code grants.
type AuthorizationRepository struct {
	store *Store
}

func NewAuthorizationRepository(store *Store) *AuthorizationRepository {
	return &AuthorizationRepository{store: store}
}

func (r *AuthorizationRepository) GetActor(ctx context.Context, actorID string) (authorization.Actor, error) {
	if strings.TrimSpace(actorID) == "" {
		return authorization.Actor{}, errx.New(errx.CodeInvalidArgument, "actor_id is required", "P0-G3")
	}
	var actor authorization.Actor
	var active int
	err := r.store.db.QueryRowContext(ctx, `
		SELECT actor_id, display_name, active, row_version
		FROM local_actors WHERE actor_id = ?`, actorID,
	).Scan(&actor.ActorID, &actor.DisplayName, &active, &actor.RowVersion)
	if err == sql.ErrNoRows {
		return authorization.Actor{}, errx.New(errx.CodeNotFound, "actor not found", "P0-G3")
	}
	if err != nil {
		return authorization.Actor{}, errx.Wrap(errx.CodeDatabase, "load local actor", "P0-G3", err)
	}
	actor.Active = active == 1
	return actor, nil
}

func (r *AuthorizationRepository) Decide(ctx context.Context, actorID, actionCode string) (authorization.Decision, error) {
	if strings.TrimSpace(actorID) == "" || strings.TrimSpace(actionCode) == "" {
		return authorization.Decision{}, errx.New(errx.CodeInvalidArgument, "actor_id and action_code are required", "P0-G3")
	}

	var decision authorization.Decision
	var actorActive, moduleCatalogEnabled, moduleEntitled, functionCatalogEnabled, functionGranted int
	var functionCode sql.NullString
	err := r.store.db.QueryRowContext(ctx, `
		SELECT a.actor_id,
		       ac.code,
		       ac.module_code,
		       ac.function_code,
		       la.active,
		       m.enabled,
		       COALESCE(ame.enabled, 0),
		       COALESCE(fc.enabled, 1),
		       CASE WHEN ac.function_code IS NULL THEN 1 ELSE COALESCE(afc.granted, 0) END
		FROM actions ac
		JOIN local_actors la ON la.actor_id = ?
		JOIN modules m ON m.code = ac.module_code
		LEFT JOIN function_codes fc ON fc.code = ac.function_code
		LEFT JOIN actor_module_entitlements ame ON ame.actor_id = la.actor_id AND ame.module_code = ac.module_code
		LEFT JOIN actor_function_codes afc ON afc.actor_id = la.actor_id AND afc.function_code = ac.function_code
		CROSS JOIN (SELECT ? AS actor_id) a
		WHERE ac.code = ?`, actorID, actorID, actionCode,
	).Scan(
		&decision.ActorID, &decision.ActionCode, &decision.ModuleCode, &functionCode,
		&actorActive, &moduleCatalogEnabled, &moduleEntitled, &functionCatalogEnabled, &functionGranted,
	)
	if err == sql.ErrNoRows {
		return authorization.Decision{
			ActorID: actorID, ActionCode: actionCode,
			Allowed: false, Reason: "ACTION_NOT_FOUND",
		}, nil
	}
	if err != nil {
		return authorization.Decision{}, errx.Wrap(errx.CodeDatabase, "evaluate local capability", "P0-G3", err)
	}
	if functionCode.Valid {
		decision.FunctionCode = functionCode.String
	}
	decision.ModuleEnabled = moduleCatalogEnabled == 1 && moduleEntitled == 1
	decision.FunctionGrant = functionCatalogEnabled == 1 && functionGranted == 1
	decision.Allowed = actorActive == 1 && decision.ModuleEnabled && decision.FunctionGrant

	switch {
	case actorActive != 1:
		decision.Reason = "ACTOR_DISABLED"
	case moduleCatalogEnabled != 1:
		decision.Reason = "MODULE_DISABLED"
	case moduleEntitled != 1:
		decision.Reason = "MODULE_NOT_ENTITLED"
	case functionCatalogEnabled != 1:
		decision.Reason = "FUNCTION_DISABLED"
	case functionGranted != 1:
		decision.Reason = "FUNCTION_NOT_GRANTED"
	}
	return decision, nil
}

func (r *AuthorizationRepository) SetFunctionGrant(ctx context.Context, request authorization.GrantRequest) error {
	if strings.TrimSpace(request.ActorID) == "" || strings.TrimSpace(request.FunctionCode) == "" {
		return errx.New(errx.CodeInvalidArgument, "actor_id and function_code are required", "P0-G3")
	}
	granted := 0
	if request.Granted {
		granted = 1
	}
	return r.store.WithTx(ctx, func(tx *sql.Tx) error {
		var exists int
		if err := tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM actor_function_codes WHERE actor_id=? AND function_code=?`, request.ActorID, request.FunctionCode).Scan(&exists); err != nil {
			return errx.Wrap(errx.CodeDatabase, "check function grant", "P0-G3", err)
		}
		if exists == 0 {
			if request.RowVersion != 0 {
				return errx.New(errx.CodeConflict, "function grant does not exist at requested row_version", "P0-G3")
			}
			_, err := tx.ExecContext(ctx, `INSERT INTO actor_function_codes(actor_id,function_code,granted) VALUES(?,?,?)`, request.ActorID, request.FunctionCode, granted)
			if err != nil {
				return errx.Wrap(errx.CodeDatabase, "insert function grant", "P0-G3", err)
			}
		} else {
			result, err := tx.ExecContext(ctx, `UPDATE actor_function_codes SET granted=?, updated_at=strftime('%Y-%m-%dT%H:%M:%fZ','now'), row_version=row_version+1 WHERE actor_id=? AND function_code=? AND row_version=?`, granted, request.ActorID, request.FunctionCode, request.RowVersion)
			if err != nil {
				return errx.Wrap(errx.CodeDatabase, "update function grant", "P0-G3", err)
			}
			rows, _ := result.RowsAffected()
			if rows != 1 {
				return errx.New(errx.CodeConflict, "stale function grant row_version", "P0-G3")
			}
		}
		return r.insertAudit(ctx, tx, request.ActorID, "P0-G3", "AUTHZ_FUNCTION_GRANT_CHANGED", "function_code", request.FunctionCode, request)
	})
}

func (r *AuthorizationRepository) SetModuleEntitlement(ctx context.Context, request authorization.EntitlementRequest) error {
	if strings.TrimSpace(request.ActorID) == "" || strings.TrimSpace(request.ModuleCode) == "" {
		return errx.New(errx.CodeInvalidArgument, "actor_id and module_code are required", "P0-G3")
	}
	enabled := 0
	if request.Enabled {
		enabled = 1
	}
	return r.store.WithTx(ctx, func(tx *sql.Tx) error {
		var exists int
		if err := tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM actor_module_entitlements WHERE actor_id=? AND module_code=?`, request.ActorID, request.ModuleCode).Scan(&exists); err != nil {
			return errx.Wrap(errx.CodeDatabase, "check module entitlement", "P0-G3", err)
		}
		if exists == 0 {
			if request.RowVersion != 0 {
				return errx.New(errx.CodeConflict, "module entitlement does not exist at requested row_version", "P0-G3")
			}
			_, err := tx.ExecContext(ctx, `INSERT INTO actor_module_entitlements(actor_id,module_code,enabled) VALUES(?,?,?)`, request.ActorID, request.ModuleCode, enabled)
			if err != nil {
				return errx.Wrap(errx.CodeDatabase, "insert module entitlement", "P0-G3", err)
			}
		} else {
			result, err := tx.ExecContext(ctx, `UPDATE actor_module_entitlements SET enabled=?, updated_at=strftime('%Y-%m-%dT%H:%M:%fZ','now'), row_version=row_version+1 WHERE actor_id=? AND module_code=? AND row_version=?`, enabled, request.ActorID, request.ModuleCode, request.RowVersion)
			if err != nil {
				return errx.Wrap(errx.CodeDatabase, "update module entitlement", "P0-G3", err)
			}
			rows, _ := result.RowsAffected()
			if rows != 1 {
				return errx.New(errx.CodeConflict, "stale module entitlement row_version", "P0-G3")
			}
		}
		return r.insertAudit(ctx, tx, request.ActorID, "P0-G3", "AUTHZ_MODULE_ENTITLEMENT_CHANGED", "module", request.ModuleCode, request)
	})
}

func (r *AuthorizationRepository) insertAudit(ctx context.Context, tx *sql.Tx, actorID, featureID, eventType, resourceType, resourceID string, payload any) error {
	body, err := json.Marshal(payload)
	if err != nil {
		return errx.Wrap(errx.CodeInternal, "encode audit payload", featureID, err)
	}
	_, err = tx.ExecContext(ctx, `INSERT INTO audit_events(actor_id,feature_id,event_type,resource_type,resource_id,payload) VALUES(?,?,?,?,?,?)`, actorID, featureID, eventType, resourceType, resourceID, string(body))
	if err != nil {
		return errx.Wrap(errx.CodeDatabase, "insert audit event", featureID, err)
	}
	return nil
}
