package sqlite

import "testing"

func TestValidateMRSCodeLegacyPrefixesAndLengths(t *testing.T) {
	cases := []struct {
		code, unit, kind string
		valid            bool
		chapter, canonical string
	}{
		{"1234567890", "平方公尺", "WORK_ITEM", true, "12345", "M2"},
		{"M1234567890", "kg", "MATERIAL", true, "12345", "KG"},
		{"L123456789012", "公尺", "LABOR", true, "", "M"},
		{"E123456789012", "m3", "EQUIPMENT", true, "", "M3"},
		{"W1234567890", "噸", "OTHER", true, "", "T"},
		{"X123", "", "", false, "", ""},
		{"M123", "公斤", "MATERIAL", false, "", "KG"},
	}
	for _, tc := range cases {
		got := ValidateMRSCode(tc.code, tc.unit)
		if got.Valid != tc.valid || got.ResourceType != tc.kind || got.ChapterCode != tc.chapter || got.CanonicalUnit != tc.canonical {
			t.Fatalf("ValidateMRSCode(%q)=%+v", tc.code, got)
		}
	}
}

func TestFitMRSCodeNormalizesLegacyInput(t *testing.T) {
	got := FitMRSCode(MRSCodeFitRequest{Code: " m12345 67890 ", Unit: "平方米", Name: "材料"})
	if got.FittedCode != "M1234567890" || got.CanonicalUnit != "M2" || !got.Changed || len(got.Warnings) != 2 {
		t.Fatalf("fit=%+v", got)
	}
	unchanged := FitMRSCode(MRSCodeFitRequest{Code: "L123456789012", Unit: "KG"})
	if unchanged.Changed || len(unchanged.Warnings) != 0 {
		t.Fatalf("unchanged=%+v", unchanged)
	}
}
