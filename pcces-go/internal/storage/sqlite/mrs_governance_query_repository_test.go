package sqlite

import (
	"context"
	"testing"
)

func TestMRSGovernanceFilteredQueries(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	catalog := NewMRSCatalogRepository(store)
	if _, err := catalog.SaveItem(ctx, MRSCatalogItem{ID: "M1", Code: "M-1", Name: "Material", Category: "MATERIAL", CurrentPrice: "100", PriceScale: 2, Enabled: true}, "7", ""); err != nil { t.Fatal(err) }
	repo := NewMRSGovernanceRepository(store)
	first, err := repo.CreateRelease(ctx, "REL-DRAFT", "draft", "7")
	if err != nil { t.Fatal(err) }
	second, err := repo.CreateRelease(ctx, "REL-PUBLISHED", "published", "7")
	if err != nil { t.Fatal(err) }
	second, err = repo.TransitionRelease(ctx, second.ID, "SUBMIT", "7", "", second.RowVersion)
	if err != nil { t.Fatal(err) }
	second, err = repo.TransitionRelease(ctx, second.ID, "APPROVE", "8", "ok", second.RowVersion)
	if err != nil { t.Fatal(err) }
	second, err = repo.TransitionRelease(ctx, second.ID, "PUBLISH", "8", "", second.RowVersion)
	if err != nil { t.Fatal(err) }

	drafts, err := repo.ListReleasesFiltered(ctx, "draft")
	if err != nil || len(drafts) != 1 || drafts[0].ID != first.ID { t.Fatalf("drafts=%+v err=%v", drafts, err) }
	published, err := repo.ListReleasesFiltered(ctx, " PUBLISHED ")
	if err != nil || len(published) != 1 || published[0].ID != second.ID { t.Fatalf("published=%+v err=%v", published, err) }
	all, err := repo.ListReleasesFiltered(ctx, "")
	if err != nil || len(all) != 2 { t.Fatalf("all=%+v err=%v", all, err) }
	if _, err = repo.ListReleasesFiltered(ctx, "UNKNOWN"); err == nil { t.Fatal("invalid status filter must fail") }

	created, err := repo.ListAuditFiltered(ctx, "catalog_release", first.ID, "release_created")
	if err != nil || len(created) != 1 || created[0].ResourceID != first.ID { t.Fatalf("created=%+v err=%v", created, err) }
	publishedAudit, err := repo.ListAuditFiltered(ctx, "CATALOG_RELEASE", second.ID, "RELEASE_PUBLISH")
	if err != nil || len(publishedAudit) != 1 || publishedAudit[0].Payload["to"] != "PUBLISHED" { t.Fatalf("published audit=%+v err=%v", publishedAudit, err) }
	count, err := repo.CountAuditFiltered(ctx, "CATALOG_RELEASE", second.ID, "")
	if err != nil || count != 4 { t.Fatalf("count=%d err=%v", count, err) }
}
