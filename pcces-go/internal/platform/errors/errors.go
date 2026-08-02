package errors

import (
	"encoding/json"
	"errors"
	"fmt"
)

// Code is stable across Legacy parity specs, PCCES Web and PCCES Local Go.
type Code string

const (
	CodeInvalidArgument Code = "PCCES_INVALID_ARGUMENT"
	CodeUnauthorized    Code = "PCCES_UNAUTHORIZED"
	CodeForbidden       Code = "PCCES_FORBIDDEN"
	CodeNotFound        Code = "PCCES_NOT_FOUND"
	CodeConflict        Code = "PCCES_CONFLICT"
	CodeDatabase        Code = "PCCES_DATABASE_ERROR"
	CodeInternal        Code = "PCCES_INTERNAL_ERROR"
)

// Error is the canonical application error returned by CLI and localhost API.
type Error struct {
	Code      Code           `json:"code"`
	Message   string         `json:"message"`
	FeatureID string         `json:"feature_id,omitempty"`
	Details   map[string]any `json:"details,omitempty"`
	Cause     error          `json:"-"`
}

func (e *Error) Error() string {
	if e == nil {
		return "<nil>"
	}
	if e.FeatureID != "" {
		return fmt.Sprintf("%s [%s]: %s", e.Code, e.FeatureID, e.Message)
	}
	return fmt.Sprintf("%s: %s", e.Code, e.Message)
}

func (e *Error) Unwrap() error { return e.Cause }

func (e *Error) MarshalJSON() ([]byte, error) {
	type payload struct {
		Code      Code           `json:"code"`
		Message   string         `json:"message"`
		FeatureID string         `json:"feature_id,omitempty"`
		Details   map[string]any `json:"details,omitempty"`
	}
	return json.Marshal(payload{e.Code, e.Message, e.FeatureID, e.Details})
}

func New(code Code, message, featureID string) *Error {
	return &Error{Code: code, Message: message, FeatureID: featureID}
}

func Wrap(code Code, message, featureID string, cause error) *Error {
	return &Error{Code: code, Message: message, FeatureID: featureID, Cause: cause}
}

func As(err error) (*Error, bool) {
	var target *Error
	ok := errors.As(err, &target)
	return target, ok
}
