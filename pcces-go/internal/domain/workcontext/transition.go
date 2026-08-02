package workcontext

import "strings"

// TransitionResult is shared by Local Go lifecycle commands and golden tests.
type TransitionResult struct {
	Exists     bool   `json:"exists"`
	Dirty      bool   `json:"dirty"`
	RowVersion int64  `json:"row_version"`
	Outcome    string `json:"outcome"`
}

// Transition evaluates Save, Save Draft, Discard and Cancel deterministically.
func Transition(exists, dirty bool, rowVersion int64, command string, requestRowVersion *int64) TransitionResult {
	if exists && requestRowVersion != nil && *requestRowVersion != rowVersion {
		return TransitionResult{exists, dirty, rowVersion, "CONFLICT"}
	}
	switch strings.ToUpper(command) {
	case "SAVE_DRAFT":
		if !exists { rowVersion = 0 }
		return TransitionResult{true, true, rowVersion + 1, "DRAFT_SAVED"}
	case "SAVE":
		if !exists { rowVersion = 0 }
		return TransitionResult{true, false, rowVersion + 1, "SAVED"}
	case "DISCARD":
		if !exists { return TransitionResult{false, false, 0, "NOT_FOUND"} }
		return TransitionResult{true, false, rowVersion + 1, "DISCARDED"}
	case "CANCEL":
		if !exists { return TransitionResult{false, false, 0, "CANCELLED"} }
		if dirty { return TransitionResult{true, true, rowVersion, "DECISION_REQUIRED"} }
		return TransitionResult{false, false, 0, "CANCELLED"}
	default:
		return TransitionResult{exists, dirty, rowVersion, "INVALID_COMMAND"}
	}
}
