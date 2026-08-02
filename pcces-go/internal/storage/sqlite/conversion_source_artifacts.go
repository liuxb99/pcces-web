package sqlite

import (
	"context"
	"crypto/sha256"
	"database/sql"
	"encoding/csv"
	"encoding/hex"
	"encoding/json"
	"fmt"
	"strconv"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type ConversionSourceArtifact struct {
	ID, SessionType, SessionID, OriginalFilename, ContentType string
	Format, FormatVersion, SHA256, CreatedBy, CreatedAt        string
	SizeBytes, RowVersion                                      int64
	DownloadURL                                                string
}

type ConversionErrorCatalogue struct {
	ID, SessionType, SessionID, CreatedBy, CreatedAt string
	ErrorCount, WarningCount, RowVersion             int64
	Catalogue                                         map[string]any
	DownloadURL                                       string
}

type ConversionSourceArtifactRepository struct{ store *Store }

func NewConversionSourceArtifactRepository(store *Store) *ConversionSourceArtifactRepository {
	return &ConversionSourceArtifactRepository{store: store}
}

func (r *ConversionSourceArtifactRepository) CreateSource(ctx context.Context, item ConversionSourceArtifact, content []byte) (ConversionSourceArtifact, error) {
	item.SessionType = strings.ToUpper(strings.TrimSpace(item.SessionType))
	item.SessionID = strings.TrimSpace(item.SessionID)
	item.OriginalFilename = strings.TrimSpace(item.OriginalFilename)
	if item.SessionType == "" || item.SessionID == "" || item.OriginalFilename == "" || len(content) == 0 {
		return item, errx.New(errx.CodeInvalidArgument, "session type, session id, filename and content are required", "P4-SOURCE-001")
	}
	if item.ID == "" { item.ID = fmt.Sprintf("SRC-%d", time.Now().UTC().UnixNano()) }
	if item.ContentType == "" { item.ContentType = "application/octet-stream" }
	if item.Format == "" { item.Format = "UNKNOWN" }
	if item.FormatVersion == "" { item.FormatVersion = "UNKNOWN" }
	hash := sha256.Sum256(content)
	item.SHA256 = hex.EncodeToString(hash[:])
	item.SizeBytes = int64(len(content))
	item.CreatedAt = time.Now().UTC().Format(time.RFC3339Nano)
	item.RowVersion = 1
	_, err := r.store.db.ExecContext(ctx, `INSERT INTO conversion_source_artifacts(id,session_type,session_id,original_filename,content_type,format,format_version,size_bytes,sha256,content,created_by,created_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,1)`, item.ID,item.SessionType,item.SessionID,item.OriginalFilename,item.ContentType,item.Format,item.FormatVersion,item.SizeBytes,item.SHA256,content,item.CreatedBy,item.CreatedAt)
	if err != nil { return item, err }
	item.DownloadURL = "/api/conversions/source-artifacts/" + item.ID + "/download"
	return item, nil
}

func (r *ConversionSourceArtifactRepository) GetSource(ctx context.Context, id string) (ConversionSourceArtifact, error) {
	var item ConversionSourceArtifact
	err := r.store.db.QueryRowContext(ctx, `SELECT id,session_type,session_id,original_filename,content_type,format,format_version,size_bytes,sha256,created_by,created_at,row_version FROM conversion_source_artifacts WHERE id=?`, id).Scan(&item.ID,&item.SessionType,&item.SessionID,&item.OriginalFilename,&item.ContentType,&item.Format,&item.FormatVersion,&item.SizeBytes,&item.SHA256,&item.CreatedBy,&item.CreatedAt,&item.RowVersion)
	if err == sql.ErrNoRows { return item, errx.New(errx.CodeNotFound, "conversion source artifact not found", "P4-SOURCE-001") }
	item.DownloadURL = "/api/conversions/source-artifacts/" + item.ID + "/download"
	return item, err
}

func (r *ConversionSourceArtifactRepository) SourceContent(ctx context.Context, id string) ([]byte,string,string,error) {
	var content []byte; var contentType, filename string
	err := r.store.db.QueryRowContext(ctx, `SELECT content,content_type,original_filename FROM conversion_source_artifacts WHERE id=?`, id).Scan(&content,&contentType,&filename)
	if err == sql.ErrNoRows { return nil,"","",errx.New(errx.CodeNotFound,"conversion source artifact not found","P4-SOURCE-001") }
	return content,contentType,filename,err
}

func (r *ConversionSourceArtifactRepository) CreateCatalogue(ctx context.Context, sessionType, sessionID string, errors, warnings []map[string]any, actor string) (ConversionErrorCatalogue,error) {
	if strings.TrimSpace(sessionType)=="" || strings.TrimSpace(sessionID)=="" { return ConversionErrorCatalogue{},errx.New(errx.CodeInvalidArgument,"session type and session id are required","P4-SOURCE-001") }
	id:=fmt.Sprintf("CAT-%d",time.Now().UTC().UnixNano()); now:=time.Now().UTC().Format(time.RFC3339Nano)
	payload,_:=json.Marshal(map[string]any{"errors":errors,"warnings":warnings})
	_,err:=r.store.db.ExecContext(ctx,`INSERT INTO conversion_error_catalogues(id,session_type,session_id,error_count,warning_count,catalogue_json,created_by,created_at,row_version) VALUES(?,?,?,?,?,?,?,?,1)`,id,strings.ToUpper(sessionType),sessionID,len(errors),len(warnings),string(payload),actor,now)
	if err!=nil{return ConversionErrorCatalogue{},err}
	return r.GetCatalogue(ctx,id)
}

func (r *ConversionSourceArtifactRepository) GetCatalogue(ctx context.Context,id string)(ConversionErrorCatalogue,error){
	var item ConversionErrorCatalogue; var raw string
	err:=r.store.db.QueryRowContext(ctx,`SELECT id,session_type,session_id,error_count,warning_count,catalogue_json,created_by,created_at,row_version FROM conversion_error_catalogues WHERE id=?`,id).Scan(&item.ID,&item.SessionType,&item.SessionID,&item.ErrorCount,&item.WarningCount,&raw,&item.CreatedBy,&item.CreatedAt,&item.RowVersion)
	if err==sql.ErrNoRows{return item,errx.New(errx.CodeNotFound,"conversion error catalogue not found","P4-SOURCE-001")}; if err!=nil{return item,err}
	_ = json.Unmarshal([]byte(raw),&item.Catalogue); item.DownloadURL="/api/conversions/error-catalogues/"+item.ID+"/download"; return item,nil
}

func (r *ConversionSourceArtifactRepository) CatalogueCSV(ctx context.Context,id string)([]byte,string,error){
	item,err:=r.GetCatalogue(ctx,id); if err!=nil{return nil,"",err}
	var b strings.Builder; w:=csv.NewWriter(&b); _=w.Write([]string{"severity","code","index","item_code","detail"})
	for _,severity:=range []string{"errors","warnings"}{
		rows,_:=item.Catalogue[severity].([]any)
		for _,raw:=range rows{m,_:=raw.(map[string]any); _=w.Write([]string{strings.ToUpper(strings.TrimSuffix(severity,"s")),fmt.Sprint(m["code"]),strconv.Itoa(intValue(m["index"])),fmt.Sprint(m["item_code"]),fmt.Sprint(m["detail"])})}
	}
	w.Flush(); return append([]byte{0xEF,0xBB,0xBF},[]byte(b.String())...),"conversion-errors-"+id+".csv",w.Error()
}

func intValue(v any) int { switch x:=v.(type){case float64:return int(x);case int:return x;default:return 0} }
