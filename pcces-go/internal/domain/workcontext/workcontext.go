package workcontext

import "time"

// Context represents the current Local Go editing/action context.
type Context struct {
	ID           string    `json:"id"`
	ActorID      string    `json:"actor_id"`
	ActionCode   string    `json:"action_code"`
	ProjectCode  *string   `json:"project_code,omitempty"`
	ResourceType *string   `json:"resource_type,omitempty"`
	ResourceID   *string   `json:"resource_id,omitempty"`
	Dirty        bool      `json:"dirty"`
	DraftPayload *string   `json:"draft_payload,omitempty"`
	CreatedAt    time.Time `json:"created_at"`
	UpdatedAt    time.Time `json:"updated_at"`
	RowVersion   int64     `json:"row_version"`
}

// SaveRequest is used to create or replace a local work context.
type SaveRequest struct {
	ID           string
	ActorID      string
	ActionCode   string
	ProjectCode  *string
	ResourceType *string
	ResourceID   *string
	Dirty        bool
	DraftPayload *string
	RowVersion   int64
}
