package sqlite

import (
	"context"
	"crypto/sha256"
	"database/sql"
	"encoding/hex"
	"encoding/json"
	"encoding/xml"
	"fmt"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type ConversionExportItem struct {
	SourceBudgetItemID string `json:"source_budget_item_id"`
	ID                 string `json:"id"`
	Code               string `json:"code"`
	Name               string `json:"name"`
	Unit               string `json:"unit"`
	Quantity           string `json:"quantity"`
	UnitPrice          string `json:"unit_price"`
	Amount             string `json:"amount"`
}

type ConversionExportRequest struct {
	WizardSessionID      string                 `json:"wizard_session_id"`
	SourceBudgetVersionID string                `json:"source_budget_version_id"`
	TargetProjectCode    string                 `json:"target_project_code"`
	Format               string                 `json:"format"`
	Items                []ConversionExportItem `json:"items"`
	ActorID              string                 `json:"actor_id"`
}

type ConversionExportJob struct {
	ID                    string         `json:"id"`
	WizardSessionID       string         `json:"wizard_session_id"`
	SourceBudgetVersionID string         `json:"source_budget_version_id"`
	TargetProjectCode     string         `json:"target_project_code"`
	Format                string         `json:"format"`
	Status                string         `json:"status"`
	Filename              string         `json:"filename"`
	ContentType           string         `json:"content_type"`
	SizeBytes             int            `json:"size_bytes"`
	SHA256                string         `json:"sha256"`
	Metadata              map[string]any `json:"metadata"`
	CreatedBy             string         `json:"created_by"`
	CreatedAt             string         `json:"created_at"`
	RowVersion            int64          `json:"row_version"`
	DownloadURL           string         `json:"download_url"`
}

type exportXMLDocument struct {
	XMLName xml.Name
	Version string          `xml:"version,attr"`
	Header  exportXMLHeader `xml:"Header"`
	Items   exportXMLItems
}

type exportXMLHeader struct {
	ProjectCode         string `xml:"ProjectCode"`
	SourceBudgetVersion string `xml:"SourceBudgetVersion"`
}

type exportXMLItems struct {
	XMLName xml.Name
	Rows    []exportXMLRow `xml:",any"`
}

type exportXMLRow struct {
	XMLName     xml.Name
	Sequence    int    `xml:"sequence,attr"`
	SourceID    string `xml:"SourceItemId"`
	Code        string `xml:"Code"`
	Name        string `xml:"Name"`
	Unit        string `xml:"Unit"`
	Quantity    string `xml:"Quantity"`
	UnitPrice   string `xml:"UnitPrice"`
	Amount      string `xml:"Amount"`
}

type ConversionExportJobRepository struct{ store *Store }

func NewConversionExportJobRepository(store *Store) *ConversionExportJobRepository {
	return &ConversionExportJobRepository{store: store}
}

func serializeConversionXML(items []ConversionExportItem, projectCode, sourceVersion string, legacy bool) ([]byte, error) {
	rows := make([]exportXMLRow, 0, len(items))
	for i, item := range items {
		sourceID := strings.TrimSpace(item.SourceBudgetItemID)
		if sourceID == "" {
			sourceID = strings.TrimSpace(item.ID)
		}
		rows = append(rows, exportXMLRow{
			Sequence: i + 1, SourceID: sourceID,
			Code: strings.ToUpper(strings.TrimSpace(item.Code)), Name: strings.TrimSpace(item.Name),
			Unit: strings.TrimSpace(item.Unit), Quantity: defaultString(item.Quantity, "0"),
			UnitPrice: defaultString(item.UnitPrice, "0"), Amount: defaultString(item.Amount, "0"),
		})
	}
	rootName, version, collectionName, rowName := "PCCESBidExchange", "2.0", "Items", "Item"
	if legacy {
		rootName, version, collectionName, rowName = "PCCES", "1.0", "Detail", "Record"
	}
	for i := range rows {
		rows[i].XMLName = xml.Name{Local: rowName}
	}
	doc := exportXMLDocument{
		XMLName: xml.Name{Local: rootName}, Version: version,
		Header: exportXMLHeader{ProjectCode: projectCode, SourceBudgetVersion: sourceVersion},
		Items: exportXMLItems{XMLName: xml.Name{Local: collectionName}, Rows: rows},
	}
	payload, err := xml.MarshalIndent(doc, "", "  ")
	if err != nil {
		return nil, err
	}
	return append([]byte(xml.Header), payload...), nil
}

func (r *ConversionExportJobRepository) Create(ctx context.Context, req ConversionExportRequest) (ConversionExportJob, error) {
	req.WizardSessionID = strings.TrimSpace(req.WizardSessionID)
	req.SourceBudgetVersionID = strings.TrimSpace(req.SourceBudgetVersionID)
	req.TargetProjectCode = strings.TrimSpace(req.TargetProjectCode)
	req.Format = strings.ToUpper(strings.TrimSpace(req.Format))
	if req.WizardSessionID == "" || req.SourceBudgetVersionID == "" || req.TargetProjectCode == "" {
		return ConversionExportJob{}, errx.New(errx.CodeInvalidArgument, "wizard session, source version and target project are required", "P4-EXPORT-001")
	}
	if len(req.Items) == 0 {
		return ConversionExportJob{}, errx.New(errx.CodeInvalidArgument, "items are required", "P4-EXPORT-001")
	}
	var payload []byte
	var err error
	extension, contentType := "json", "application/json; charset=utf-8"
	switch req.Format {
	case "BID_JSON":
		payload, err = json.MarshalIndent(map[string]any{"project_code": req.TargetProjectCode, "source_budget_version_id": req.SourceBudgetVersionID, "items": req.Items}, "", "  ")
	case "XML_NEW":
		extension, contentType = "xml", "application/xml; charset=utf-8"
		payload, err = serializeConversionXML(req.Items, req.TargetProjectCode, req.SourceBudgetVersionID, false)
	case "XML_LEGACY":
		extension, contentType = "xml", "application/xml; charset=utf-8"
		payload, err = serializeConversionXML(req.Items, req.TargetProjectCode, req.SourceBudgetVersionID, true)
	default:
		return ConversionExportJob{}, errx.New(errx.CodeInvalidArgument, "unsupported export format", "P4-EXPORT-001")
	}
	if err != nil {
		return ConversionExportJob{}, err
	}
	hash := sha256.Sum256(payload)
	jobID := fmt.Sprintf("EXP-%d", time.Now().UTC().UnixNano())
	filename := fmt.Sprintf("%s-%s.%s", req.TargetProjectCode, strings.ToLower(req.Format), extension)
	metadata, _ := json.Marshal(map[string]any{"item_count": len(req.Items), "serializer": "P4-EXPORT-001", "format_version": map[bool]string{true: "1.0", false: "2.0"}[req.Format == "XML_LEGACY"]})
	now := time.Now().UTC().Format(time.RFC3339Nano)
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO conversion_export_jobs(id,wizard_session_id,source_budget_version_id,target_project_code,format,status,filename,content_type,size_bytes,sha256,artifact,metadata_json,created_by,created_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,1)`,
		jobID, req.WizardSessionID, req.SourceBudgetVersionID, req.TargetProjectCode, req.Format, "COMPLETED", filename, contentType, len(payload), hex.EncodeToString(hash[:]), payload, string(metadata), req.ActorID, now)
	if err != nil {
		return ConversionExportJob{}, err
	}
	return r.Get(ctx, jobID)
}

func (r *ConversionExportJobRepository) Get(ctx context.Context, id string) (ConversionExportJob, error) {
	var item ConversionExportJob
	var metadata string
	err := r.store.db.QueryRowContext(ctx, `SELECT id,wizard_session_id,source_budget_version_id,target_project_code,format,status,filename,content_type,size_bytes,sha256,metadata_json,created_by,created_at,row_version FROM conversion_export_jobs WHERE id=?`, id).
		Scan(&item.ID, &item.WizardSessionID, &item.SourceBudgetVersionID, &item.TargetProjectCode, &item.Format, &item.Status, &item.Filename, &item.ContentType, &item.SizeBytes, &item.SHA256, &metadata, &item.CreatedBy, &item.CreatedAt, &item.RowVersion)
	if err == sql.ErrNoRows {
		return item, errx.New(errx.CodeNotFound, "conversion export job not found", "P4-EXPORT-001")
	}
	if err != nil {
		return item, err
	}
	_ = json.Unmarshal([]byte(metadata), &item.Metadata)
	item.DownloadURL = "/api/conversions/export-jobs/" + item.ID + "/download"
	return item, nil
}

func (r *ConversionExportJobRepository) Artifact(ctx context.Context, id string) ([]byte, string, string, error) {
	var content []byte
	var contentType, filename string
	err := r.store.db.QueryRowContext(ctx, `SELECT artifact,content_type,filename FROM conversion_export_jobs WHERE id=?`, id).Scan(&content, &contentType, &filename)
	if err == sql.ErrNoRows {
		return nil, "", "", errx.New(errx.CodeNotFound, "conversion export job not found", "P4-EXPORT-001")
	}
	return content, contentType, filename, err
}
