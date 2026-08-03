package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/workcontext"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type workContextCommandBody struct {
	ActorID      string  `json:"actor_id"`
	ActionCode   string  `json:"action_code"`
	ProjectCode  *string `json:"project_code"`
	ResourceType *string `json:"resource_type"`
	ResourceID   *string `json:"resource_id"`
	DraftPayload *string `json:"draft_payload"`
	RowVersion   *int64  `json:"row_version"`
}

func (s *Server) saveDraftWorkContext(w http.ResponseWriter, r *http.Request) {
	s.executeWorkContextCommand(w, r, "SAVE_DRAFT")
}
func (s *Server) saveWorkContext(w http.ResponseWriter, r *http.Request) {
	s.executeWorkContextCommand(w, r, "SAVE")
}
func (s *Server) discardWorkContext(w http.ResponseWriter, r *http.Request) {
	s.executeWorkContextCommand(w, r, "DISCARD")
}
func (s *Server) cancelWorkContext(w http.ResponseWriter, r *http.Request) {
	s.executeWorkContextCommand(w, r, "CANCEL")
}

func (s *Server) executeWorkContextCommand(w http.ResponseWriter, r *http.Request, command string) {
	var body workContextCommandBody
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}

	var current *workcontext.Context
	item, err := s.contexts.Get(r.Context(), r.PathValue("id"))
	if err == nil {
		current = item
	} else if appErr, ok := err.(*errx.Error); !ok || appErr.Code != errx.CodeNotFound {
		writeError(w, err)
		return
	}

	currentVersion := int64(0)
	currentDirty := false
	if current != nil {
		currentVersion = current.RowVersion
		currentDirty = current.Dirty
	}
	transition := workcontext.Transition(current != nil, currentDirty, currentVersion, command, body.RowVersion)

	switch transition.Outcome {
	case "CONFLICT":
		writeError(w, errx.New(errx.CodeConflict, "work context row_version conflict", "P0-G4"))
		return
	case "DECISION_REQUIRED":
		writeJSON(w, http.StatusConflict, transition)
		return
	case "NOT_FOUND":
		writeError(w, errx.New(errx.CodeNotFound, "work context not found", "P0-G4"))
		return
	case "INVALID_COMMAND":
		writeError(w, errx.New(errx.CodeInvalidArgument, "invalid work context command", "P0-G4"))
		return
	}

	if !transition.Exists {
		if current != nil {
			if err := s.contexts.Delete(r.Context(), r.PathValue("id"), current.RowVersion); err != nil {
				writeError(w, err)
				return
			}
		}
		writeJSON(w, http.StatusOK, transition)
		return
	}

	actorID, actionCode := body.ActorID, body.ActionCode
	projectCode, resourceType, resourceID := body.ProjectCode, body.ResourceType, body.ResourceID
	if current != nil {
		if actorID == "" {
			actorID = current.ActorID
		}
		if actionCode == "" {
			actionCode = current.ActionCode
		}
		if projectCode == nil {
			projectCode = current.ProjectCode
		}
		if resourceType == nil {
			resourceType = current.ResourceType
		}
		if resourceID == nil {
			resourceID = current.ResourceID
		}
	}

	var draftPayload *string
	if transition.Dirty {
		draftPayload = body.DraftPayload
	}
	saveVersion := int64(0)
	if current != nil {
		saveVersion = current.RowVersion
	}
	updated, err := s.contexts.Save(r.Context(), workcontext.SaveRequest{
		ID: r.PathValue("id"), ActorID: actorID, ActionCode: actionCode,
		ProjectCode: projectCode, ResourceType: resourceType, ResourceID: resourceID,
		Dirty: transition.Dirty, DraftPayload: draftPayload, RowVersion: saveVersion,
	})
	if err != nil {
		writeError(w, err)
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{"transition": transition, "context": updated})
}
