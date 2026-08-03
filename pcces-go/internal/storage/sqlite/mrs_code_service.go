package sqlite

import (
	"regexp"
	"strings"
)

// MRSCodeValidation captures the deterministic subset of the Legacy
// PCCES.CODECHECK CodeValidator contract used by MRS and project resources.
type MRSCodeValidation struct {
	InputCode      string   `json:"input_code"`
	NormalizedCode string   `json:"normalized_code"`
	Valid          bool     `json:"valid"`
	ResourceType   string   `json:"resource_type"`
	ChapterCode    string   `json:"chapter_code,omitempty"`
	CanonicalUnit  string   `json:"canonical_unit,omitempty"`
	Errors         []string `json:"errors"`
	Warnings       []string `json:"warnings"`
}

type MRSCodeFitRequest struct {
	Code string `json:"code"`
	Unit string `json:"unit"`
	Name string `json:"name"`
}

type MRSCodeFitResult struct {
	OriginalCode  string   `json:"original_code"`
	FittedCode    string   `json:"fitted_code"`
	OriginalUnit  string   `json:"original_unit"`
	CanonicalUnit string   `json:"canonical_unit"`
	Changed       bool     `json:"changed"`
	Warnings      []string `json:"warnings"`
}

var pccesCodePattern = regexp.MustCompile(`^[0-9A-Z]+$`)

func canonicalMRSUnit(unit string) string {
	normalized := strings.ToUpper(strings.TrimSpace(unit))
	normalized = strings.ReplaceAll(normalized, "²", "2")
	normalized = strings.ReplaceAll(normalized, "³", "3")
	switch normalized {
	case "M", "公尺", "米":
		return "M"
	case "M2", "平方公尺", "平方米":
		return "M2"
	case "M3", "立方公尺", "立方米":
		return "M3"
	case "T", "公噸", "噸":
		return "T"
	case "KG", "公斤", "千克", "兛":
		return "KG"
	default:
		return strings.TrimSpace(unit)
	}
}

func ValidateMRSCode(code, unit string) MRSCodeValidation {
	input := code
	code = strings.ToUpper(strings.ReplaceAll(strings.TrimSpace(code), " ", ""))
	result := MRSCodeValidation{InputCode: input, NormalizedCode: code, CanonicalUnit: canonicalMRSUnit(unit), Errors: []string{}, Warnings: []string{}}
	if code == "" {
		result.Errors = append(result.Errors, "工項編碼不可空白")
		return result
	}
	if !pccesCodePattern.MatchString(code) {
		result.Errors = append(result.Errors, "編碼僅允許英文字母與數字")
	}
	first := code[0]
	switch {
	case first >= '0' && first <= '9':
		result.ResourceType = "WORK_ITEM"
		if len(code) < 10 {
			result.Errors = append(result.Errors, "工項編碼長度不足")
		}
		if len(code) >= 5 {
			result.ChapterCode = code[:5]
		}
	case first == 'M':
		result.ResourceType = "MATERIAL"
		if len(code) < 11 {
			result.Errors = append(result.Errors, "材料編碼長度不足")
		}
		if len(code) >= 6 {
			result.ChapterCode = code[1:6]
		}
	case first == 'L':
		result.ResourceType = "LABOR"
		if len(code) < 13 {
			result.Errors = append(result.Errors, "人工編碼長度不足")
		}
	case first == 'E':
		result.ResourceType = "EQUIPMENT"
		if len(code) < 13 {
			result.Errors = append(result.Errors, "機具編碼長度不足")
		}
	case first == 'W':
		result.ResourceType = "OTHER"
		if len(code) < 11 {
			result.Errors = append(result.Errors, "雜項編碼長度不足")
		}
	default:
		result.Errors = append(result.Errors, "非正常編碼(開頭不是L,E,M,W或數字)")
	}
	if result.CanonicalUnit == "" {
		result.Warnings = append(result.Warnings, "單位未提供")
	}
	result.Valid = len(result.Errors) == 0
	return result
}

func FitMRSCode(request MRSCodeFitRequest) MRSCodeFitResult {
	code := strings.ToUpper(strings.ReplaceAll(strings.TrimSpace(request.Code), " ", ""))
	unit := canonicalMRSUnit(request.Unit)
	warnings := []string{}
	if request.Code != code {
		warnings = append(warnings, "編碼已正規化為大寫並移除空白")
	}
	if strings.TrimSpace(request.Unit) != unit {
		warnings = append(warnings, "單位已轉換為Legacy標準單位")
	}
	return MRSCodeFitResult{OriginalCode: request.Code, FittedCode: code, OriginalUnit: request.Unit, CanonicalUnit: unit, Changed: request.Code != code || strings.TrimSpace(request.Unit) != unit, Warnings: warnings}
}
