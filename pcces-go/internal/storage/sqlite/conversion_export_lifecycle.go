package sqlite

import (
	"archive/zip"
	"bytes"
	"context"
	"crypto/sha256"
	"encoding/hex"
	"encoding/json"
	"encoding/xml"
	"fmt"
	"html"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type ExportArtifactVersion struct {
	ID          string         `json:"id"`
	JobID       string         `json:"job_id"`
	VersionNo   int            `json:"version_no"`
	Format      string         `json:"format"`
	Status      string         `json:"status"`
	Filename    string         `json:"filename"`
	ContentType string         `json:"content_type"`
	SizeBytes   int            `json:"size_bytes"`
	SHA256      string         `json:"sha256"`
	Validation  map[string]any `json:"validation"`
	CreatedBy   string         `json:"created_by"`
	CreatedAt   string         `json:"created_at"`
	DownloadURL string         `json:"download_url"`
}

func ValidateConversionXML(payload []byte, format string) map[string]any {
	var root struct {
		XMLName xml.Name
		Version string    `xml:"version,attr"`
		Header  *struct{} `xml:"Header"`
		Items   *struct{} `xml:"Items"`
		Detail  *struct{} `xml:"Detail"`
	}
	errors := []string{}
	if err := xml.Unmarshal(payload, &root); err != nil {
		errors = append(errors, err.Error())
	} else {
		expected, version := "PCCESBidExchange", "2.0"
		if strings.ToUpper(format) == "XML_LEGACY" {
			expected, version = "PCCES", "1.0"
		}
		if root.XMLName.Local != expected {
			errors = append(errors, "invalid root")
		}
		if root.Version != version {
			errors = append(errors, "invalid version")
		}
		if root.Header == nil {
			errors = append(errors, "Header is required")
		}
		if strings.ToUpper(format) == "XML_LEGACY" && root.Detail == nil {
			errors = append(errors, "Detail is required")
		}
		if strings.ToUpper(format) != "XML_LEGACY" && root.Items == nil {
			errors = append(errors, "Items is required")
		}
	}
	return map[string]any{"valid": len(errors) == 0, "errors": errors, "schema": "PCCES-" + strings.ToUpper(format) + "-1"}
}

func SerializeBidXLSX(items []ConversionExportItem, project, source string) ([]byte, error) {
	var out bytes.Buffer
	zw := zip.NewWriter(&out)
	files := map[string]string{"[Content_Types].xml": `<?xml version="1.0"?><Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types"><Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/><Default Extension="xml" ContentType="application/xml"/><Override PartName="/xl/workbook.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml"/><Override PartName="/xl/worksheets/sheet1.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"/></Types>`, "_rels/.rels": `<?xml version="1.0"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="xl/workbook.xml"/></Relationships>`, "xl/workbook.xml": fmt.Sprintf(`<?xml version="1.0"?><workbook xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships"><sheets><sheet name="電子標單" sheetId="1" r:id="rId1"/></sheets><definedNames><definedName name="ProjectCode">"%s"</definedName><definedName name="SourceVersion">"%s"</definedName></definedNames></workbook>`, html.EscapeString(project), html.EscapeString(source)), "xl/_rels/workbook.xml.rels": `<?xml version="1.0"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" Target="worksheets/sheet1.xml"/></Relationships>`}
	headers := []string{"來源工項ID", "工項編碼", "名稱", "單位", "數量", "單價", "金額"}
	rows := [][]string{headers}
	for _, i := range items {
		id := i.SourceBudgetItemID
		if id == "" {
			id = i.ID
		}
		rows = append(rows, []string{id, strings.ToUpper(i.Code), i.Name, i.Unit, i.Quantity, i.UnitPrice, i.Amount})
	}
	var sheet strings.Builder
	sheet.WriteString(`<?xml version="1.0"?><worksheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main"><sheetData>`)
	for r, row := range rows {
		sheet.WriteString(fmt.Sprintf(`<row r="%d">`, r+1))
		for c, v := range row {
			sheet.WriteString(fmt.Sprintf(`<c r="%c%d" t="inlineStr"><is><t>%s</t></is></c>`, rune('A'+c), r+1, html.EscapeString(v)))
		}
		sheet.WriteString(`</row>`)
	}
	sheet.WriteString(`</sheetData></worksheet>`)
	files["xl/worksheets/sheet1.xml"] = sheet.String()
	for name, content := range files {
		w, err := zw.Create(name)
		if err != nil {
			return nil, err
		}
		if _, err = w.Write([]byte(content)); err != nil {
			return nil, err
		}
	}
	if err := zw.Close(); err != nil {
		return nil, err
	}
	return out.Bytes(), nil
}

func (r *ConversionExportJobRepository) CreateXLSXVersion(ctx context.Context, jobID, project, source, actor string, items []ConversionExportItem) (ExportArtifactVersion, error) {
	if strings.TrimSpace(jobID) == "" || strings.TrimSpace(project) == "" || strings.TrimSpace(source) == "" || len(items) == 0 {
		return ExportArtifactVersion{}, errx.New(errx.CodeInvalidArgument, "job, project, source and items are required", "P4-EXPORT-002")
	}
	payload, err := SerializeBidXLSX(items, project, source)
	if err != nil {
		return ExportArtifactVersion{}, err
	}
	var current int
	_ = r.store.db.QueryRowContext(ctx, `SELECT COALESCE(MAX(version_no),0) FROM conversion_export_artifact_versions WHERE job_id=?`, jobID).Scan(&current)
	version := current + 1
	id := fmt.Sprintf("EXPV-%d", time.Now().UTC().UnixNano())
	hash := sha256.Sum256(payload)
	validation, _ := json.Marshal(map[string]any{"valid": true, "errors": []string{}, "schema": "OOXML-XLSX"})
	now := time.Now().UTC().Format(time.RFC3339Nano)
	filename := fmt.Sprintf("%s-bid-v%d.xlsx", project, version)
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO conversion_export_artifact_versions(id,job_id,version_no,format,status,filename,content_type,size_bytes,sha256,artifact,validation_json,error_message,created_by,created_at) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?)`, id, jobID, version, "XLSX", "COMPLETED", filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", len(payload), hex.EncodeToString(hash[:]), payload, string(validation), "", actor, now)
	if err != nil {
		return ExportArtifactVersion{}, err
	}
	return ExportArtifactVersion{ID: id, JobID: jobID, VersionNo: version, Format: "XLSX", Status: "COMPLETED", Filename: filename, ContentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", SizeBytes: len(payload), SHA256: hex.EncodeToString(hash[:]), Validation: map[string]any{"valid": true, "errors": []string{}, "schema": "OOXML-XLSX"}, CreatedBy: actor, CreatedAt: now, DownloadURL: "/api/conversions/export-artifacts/" + id + "/download"}, nil
}
