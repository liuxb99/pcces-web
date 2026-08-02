package catalog

// Module describes a Legacy module entitlement shared by Web and Local Go.
type Module struct {
	Code       string `json:"code"`
	Name       string `json:"name"`
	Enabled    bool   `json:"enabled"`
	RowVersion int64  `json:"row_version"`
}

// FunctionCode is the fine-grained Legacy authorization unit.
type FunctionCode struct {
	Code       string `json:"code"`
	Name       string `json:"name"`
	Enabled    bool   `json:"enabled"`
	RowVersion int64  `json:"row_version"`
}

// Action maps a user operation to its module and required function code.
type Action struct {
	Code         string  `json:"code"`
	Name         string  `json:"name"`
	ModuleCode   string  `json:"module_code"`
	FunctionCode *string `json:"function_code,omitempty"`
	RowVersion   int64   `json:"row_version"`
}

// Capability reports whether an action can be executed locally.
type Capability struct {
	ActionCode string `json:"action_code"`
	Allowed    bool   `json:"allowed"`
	ReasonCode string `json:"reason_code,omitempty"`
}
