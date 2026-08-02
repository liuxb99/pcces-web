package sqlite

import (
	"archive/zip"
	"bytes"
	"encoding/csv"
	"encoding/json"
	"fmt"
	"sort"
	"strconv"
	"strings"
)

func reportColumn(index int) string {
	result := ""
	for index > 0 {
		index--
		result = string(rune('A'+index%26)) + result
		index /= 26
	}
	return result
}

func reportEscape(value any) string {
	text := fmt.Sprint(value)
	text = strings.ReplaceAll(text, "&", "&amp;")
	text = strings.ReplaceAll(text, "<", "&lt;")
	text = strings.ReplaceAll(text, ">", "&gt;")
	return text
}

func buildReportCSV(snapshot string) ([]byte, error) {
	var data map[string]any
	if err := json.Unmarshal([]byte(snapshot), &data); err != nil { return nil, err }
	rows, _ := data["rows"].([]any)
	headersMap := map[string]bool{}
	for _, raw := range rows { if row, ok := raw.(map[string]any); ok { for key := range row { headersMap[key] = true } } }
	headers := make([]string, 0, len(headersMap)); for key := range headersMap { headers = append(headers, key) }; sort.Strings(headers)
	out := &bytes.Buffer{}; writer := csv.NewWriter(out); _ = writer.Write(headers)
	for _, raw := range rows { row, _ := raw.(map[string]any); values := make([]string, len(headers)); for i, key := range headers { values[i] = fmt.Sprint(row[key]) }; _ = writer.Write(values) }
	writer.Flush(); return out.Bytes(), writer.Error()
}

func buildReportXLSX(snapshot string) ([]byte, error) {
	var data map[string]any
	if err := json.Unmarshal([]byte(snapshot), &data); err != nil { return nil, err }
	rows, _ := data["rows"].([]any); headersMap := map[string]bool{}
	for _, raw := range rows { if row, ok := raw.(map[string]any); ok { for key := range row { headersMap[key] = true } } }
	headers := make([]string, 0, len(headersMap)); for key := range headersMap { headers = append(headers, key) }; sort.Strings(headers)
	matrix := [][]any{make([]any, len(headers))}; for i, header := range headers { matrix[0][i] = header }
	for _, raw := range rows { row, _ := raw.(map[string]any); values := make([]any, len(headers)); for i, key := range headers { values[i] = row[key] }; matrix = append(matrix, values) }
	var sheet strings.Builder
	sheet.WriteString(`<?xml version="1.0" encoding="UTF-8" standalone="yes"?><worksheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main"><sheetData>`)
	for ri, row := range matrix { sheet.WriteString(`<row r="`+strconv.Itoa(ri+1)+`">`); for ci, value := range row { ref := reportColumn(ci+1)+strconv.Itoa(ri+1); sheet.WriteString(`<c r="`+ref+`" t="inlineStr"><is><t>`+reportEscape(value)+`</t></is></c>`) }; sheet.WriteString(`</row>`) }
	sheet.WriteString(`</sheetData></worksheet>`)
	files := map[string]string{
		"[Content_Types].xml": `<?xml version="1.0"?><Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types"><Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/><Default Extension="xml" ContentType="application/xml"/><Override PartName="/xl/workbook.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml"/><Override PartName="/xl/worksheets/sheet1.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"/></Types>`,
		"_rels/.rels": `<?xml version="1.0"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="xl/workbook.xml"/></Relationships>`,
		"xl/workbook.xml": `<?xml version="1.0"?><workbook xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships"><sheets><sheet name="報表" sheetId="1" r:id="rId1"/></sheets></workbook>`,
		"xl/_rels/workbook.xml.rels": `<?xml version="1.0"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" Target="worksheets/sheet1.xml"/></Relationships>`,
		"xl/worksheets/sheet1.xml": sheet.String(),
	}
	out := &bytes.Buffer{}; zw := zip.NewWriter(out)
	for name, content := range files { entry, err := zw.Create(name); if err != nil { return nil, err }; if _, err = entry.Write([]byte(content)); err != nil { return nil, err } }
	if err := zw.Close(); err != nil { return nil, err }; return out.Bytes(), nil
}

func buildReportPDF(snapshot string) []byte {
	text := strings.ReplaceAll(snapshot, `\`, `\\`); text = strings.ReplaceAll(text, `(`, `\(`); text = strings.ReplaceAll(text, `)`, `\)`); if len(text) > 4000 { text = text[:4000] }
	stream := []byte("BT /F1 10 Tf 40 800 Td ("+strings.ReplaceAll(text, "\n", ") Tj 0 -14 Td (")+") Tj ET")
	objects := [][]byte{[]byte("<< /Type /Catalog /Pages 2 0 R >>"), []byte("<< /Type /Pages /Kids [3 0 R] /Count 1 >>"), []byte("<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 5 0 R >> >> /Contents 4 0 R >>"), []byte("<< /Length "+strconv.Itoa(len(stream))+" >>\nstream\n"+string(stream)+"\nendstream"), []byte("<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>")}
	out := &bytes.Buffer{}; out.WriteString("%PDF-1.4\n"); offsets := []int{0}
	for i, object := range objects { offsets = append(offsets, out.Len()); out.WriteString(strconv.Itoa(i+1)+" 0 obj\n"); out.Write(object); out.WriteString("\nendobj\n") }
	xref := out.Len(); out.WriteString("xref\n0 "+strconv.Itoa(len(objects)+1)+"\n0000000000 65535 f \n"); for _, offset := range offsets[1:] { out.WriteString(fmt.Sprintf("%010d 00000 n \n", offset)) }; out.WriteString("trailer << /Size "+strconv.Itoa(len(objects)+1)+" /Root 1 0 R >>\nstartxref\n"+strconv.Itoa(xref)+"\n%%EOF\n"); return out.Bytes()
}

func buildReportArtifact(format, snapshot string) ([]byte, string, string, error) {
	switch format {
	case "PDF": return buildReportPDF(snapshot), "pdf", "application/pdf", nil
	case "CSV": content, err := buildReportCSV(snapshot); return content, "csv", "text/csv; charset=utf-8", err
	case "XLSX": content, err := buildReportXLSX(snapshot); return content, "xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", err
	default: return []byte(snapshot), "json", "application/json; charset=utf-8", nil
	}
}
