package authorization

// Decision is the stable capability response shared by CLI, localhost API and UI.
type Decision struct {
	ActorID       string `json:"actor_id"`
	ActionCode    string `json:"action_code"`
	ModuleCode    string `json:"module_code"`
	FunctionCode  string `json:"function_code,omitempty"`
	ModuleEnabled bool   `json:"module_enabled"`
	FunctionGrant bool   `json:"function_grant"`
	Allowed       bool   `json:"allowed"`
	Reason        string `json:"reason,omitempty"`
}

// GrantRequest changes a single actor/function-code grant using optimistic locking.
type GrantRequest struct {
	ActorID      string
	FunctionCode string
	Granted      bool
	RowVersion   int64
}

// EntitlementRequest changes a single actor/module entitlement using optimistic locking.
type EntitlementRequest struct {
	ActorID    string
	ModuleCode string
	Enabled    bool
	RowVersion int64
}

// Actor is a local identity. Local Go starts with local-admin but does not hard-code policy to that actor.
type Actor struct {
	ActorID     string `json:"actor_id"`
	DisplayName string `json:"display_name"`
	Active      bool   `json:"active"`
	RowVersion  int64  `json:"row_version"`
}
