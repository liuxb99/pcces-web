package sqlite

import (
	"archive/zip"
	"bytes"
	"context"
	"encoding/xml"
	"fmt"
	"strconv"
	"strings"
)

// MRSExcelExporter writes a minimal standards-compliant XLSX workbook with the
// two grids exposed by Legacy FormBudgetRes: project resources and references.
type MRSExcelExporter struct{ store *Store }
func NewMRSExcelExporter(store *Store) *MRSExcelExporter { return &MRSExcelExporter{store:store} }

type excelRow []string

func xmlEscape(v string) string { var b bytes.Buffer; _ = xml.EscapeText(&b, []byte(v)); return b.String() }
func cell(ref, value string, numeric bool, style int) string {
	if numeric { return fmt.Sprintf(`<c r="%s" s="%d"><v>%s</v></c>`,ref,style,xmlEscape(value)) }
	return fmt.Sprintf(`<c r="%s" t="inlineStr" s="%d"><is><t>%s</t></is></c>`,ref,style,xmlEscape(value))
}
func columnName(n int) string { return string(rune('A'+n)) }

func sheetXML(rows []excelRow, numeric map[int]bool, styles map[int]int) string {
	var b strings.Builder
	b.WriteString(`<?xml version="1.0" encoding="UTF-8" standalone="yes"?><worksheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main"><sheetViews><sheetView workbookViewId="0"><pane ySplit="1" topLeftCell="A2" activePane="bottomLeft" state="frozen"/></sheetView></sheetViews><sheetData>`)
	for r,row:=range rows { b.WriteString(`<row r="`+strconv.Itoa(r+1)+`">`); for c,v:=range row { s:=0;if r==0{s=1}else if styles[c]>0{s=styles[c]};b.WriteString(cell(columnName(c)+strconv.Itoa(r+1),v,r>0&&numeric[c],s)) };b.WriteString(`</row>`) }
	b.WriteString(`</sheetData><autoFilter ref="A1:`+columnName(len(rows[0])-1)+strconv.Itoa(len(rows))+`"/></worksheet>`)
	return b.String()
}

func (e *MRSExcelExporter) ExportProject(ctx context.Context, projectCode string) ([]byte,error) {
	resourceRows:=[]excelRow{{"資源編碼","資源名稱","單位","單價","引用工項數"}}
	rows,err:=e.store.db.QueryContext(ctx,`SELECT r.code,r.name,COALESCE(r.unit,''),r.unit_price,COUNT(l.budget_item_id) FROM resource_budget_links l JOIN resources_decimal r ON r.id=l.resource_id WHERE l.project_code=? GROUP BY r.id,r.code,r.name,r.unit,r.unit_price ORDER BY r.code`,projectCode);if err!=nil{return nil,err}
	for rows.Next(){var code,name,unit,price string;var count int;if err=rows.Scan(&code,&name,&unit,&price,&count);err!=nil{rows.Close();return nil,err};resourceRows=append(resourceRows,excelRow{code,name,unit,price,strconv.Itoa(count)})};if err=rows.Close();err!=nil{return nil,err}
	referenceRows:=[]excelRow{{"資源編碼","工項編號","工項名稱","數量","單價","金額"}}
	rows,err=e.store.db.QueryContext(ctx,`SELECT r.code,COALESCE(b.item_no,''),b.name,b.quantity,b.unit_price,b.amount FROM resource_budget_links l JOIN resources_decimal r ON r.id=l.resource_id JOIN budget_items_decimal b ON b.id=l.budget_item_id WHERE l.project_code=? AND b.project_code=? ORDER BY r.code,b.item_no,b.id`,projectCode,projectCode);if err!=nil{return nil,err}
	for rows.Next(){var a,b,c,d,f,g string;if err=rows.Scan(&a,&b,&c,&d,&f,&g);err!=nil{rows.Close();return nil,err};referenceRows=append(referenceRows,excelRow{a,b,c,d,f,g})};if err=rows.Close();err!=nil{return nil,err}

	mainQty,mainPrice,mainAmount,analysisPrice:=2,2,0,4
	_ = e.store.db.QueryRowContext(ctx,`SELECT main_quantity_scale,main_price_scale,main_amount_scale,analysis_price_scale FROM mrs_precision_policies WHERE project_code=?`,projectCode).Scan(&mainQty,&mainPrice,&mainAmount,&analysisPrice)
	styles:=func(scales ...int)string{var x strings.Builder;x.WriteString(`<?xml version="1.0" encoding="UTF-8" standalone="yes"?><styleSheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main"><numFmts count="4">`);for i,s:=range scales{format:="0";if s>0{format+="."+strings.Repeat("0",s)};x.WriteString(fmt.Sprintf(`<numFmt numFmtId="%d" formatCode="%s"/>`,164+i,format))};x.WriteString(`</numFmts><fonts count="2"><font/><font><b/></font></fonts><fills count="1"><fill><patternFill patternType="none"/></fill></fills><borders count="1"><border/></borders><cellStyleXfs count="1"><xf/></cellStyleXfs><cellXfs count="6"><xf/><xf fontId="1" applyFont="1"/><xf numFmtId="164" applyNumberFormat="1"/><xf numFmtId="165" applyNumberFormat="1"/><xf numFmtId="166" applyNumberFormat="1"/><xf numFmtId="167" applyNumberFormat="1"/></cellXfs></styleSheet>`);return x.String()}(analysisPrice,mainQty,mainPrice,mainAmount)

	var out bytes.Buffer; zw:=zip.NewWriter(&out)
	files:=map[string]string{
		"[Content_Types].xml":`<?xml version="1.0" encoding="UTF-8" standalone="yes"?><Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types"><Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/><Default Extension="xml" ContentType="application/xml"/><Override PartName="/xl/workbook.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml"/><Override PartName="/xl/worksheets/sheet1.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"/><Override PartName="/xl/worksheets/sheet2.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"/><Override PartName="/xl/styles.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml"/></Types>`,
		"_rels/.rels":`<?xml version="1.0" encoding="UTF-8" standalone="yes"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="xl/workbook.xml"/></Relationships>`,
		"xl/workbook.xml":`<?xml version="1.0" encoding="UTF-8" standalone="yes"?><workbook xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships"><sheets><sheet name="專案資源" sheetId="1" r:id="rId1"/><sheet name="引用工項" sheetId="2" r:id="rId2"/></sheets></workbook>`,
		"xl/_rels/workbook.xml.rels":`<?xml version="1.0" encoding="UTF-8" standalone="yes"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" Target="worksheets/sheet1.xml"/><Relationship Id="rId2" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" Target="worksheets/sheet2.xml"/><Relationship Id="rId3" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles" Target="styles.xml"/></Relationships>`,
		"xl/styles.xml":styles,
		"xl/worksheets/sheet1.xml":sheetXML(resourceRows,map[int]bool{3:true,4:true},map[int]int{3:2}),
		"xl/worksheets/sheet2.xml":sheetXML(referenceRows,map[int]bool{3:true,4:true,5:true},map[int]int{3:3,4:4,5:5}),
	}
	for name,data:=range files{w,er:=zw.Create(name);if er!=nil{return nil,er};if _,er=w.Write([]byte(data));er!=nil{return nil,er}}
	if err=zw.Close();err!=nil{return nil,err};return out.Bytes(),nil
}
