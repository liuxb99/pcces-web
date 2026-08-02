package sqlite

import (
	"context"
	"fmt"
	"testing"
)

func TestMRSGovernancePagingAndTotals(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	catalog := NewMRSCatalogRepository(store)
	if _, err := catalog.SaveItem(ctx, MRSCatalogItem{ID: "M1", Code: "M-1", Name: "Material", Category: "MATERIAL", CurrentPrice: "100", PriceScale: 2, Enabled: true}, "7", ""); err != nil {
		t.Fatal(err)
	}
	repo := NewMRSGovernanceRepository(store)
	for i := 1; i <= 5; i++ {
		id := fmt.Sprintf("REL%d", i)
		release, err := repo.CreateRelease(ctx, id, id, "7")
		if err != nil {
			t.Fatal(err)
		}
		if i%2 == 0 {
			release, err = repo.TransitionRelease(ctx, id, "SUBMIT", "7", "", release.RowVersion)
			if err != nil {
				t.Fatal(err)
			}
			if _, err = repo.TransitionRelease(ctx, id, "APPROVE", "8", "ok", release.RowVersion); err != nil {
				t.Fatal(err)
			}
		}
	}

	page, err := repo.QueryReleases(ctx, "", 2, 1)
	if err != nil {
		t.Fatal(err)
	}
	if page.Total != 5 || page.Limit != 2 || page.Offset != 1 || len(page.Items) != 2 {
		t.Fatalf("unexpected page: %+v", page)
	}
	approved, err := repo.QueryReleases(ctx, " approved ", 50, 0)
	if err != nil {
		t.Fatal(err)
	}
	if approved.Total != 2 || len(approved.Items) != 2 {
		t.Fatalf("approved page: %+v", approved)
	}
	if _, err = repo.QueryReleases(ctx, "invalid", 10, 0); err == nil {
		t.Fatal("invalid release status must fail")
	}

	audit, err := repo.QueryAudit(ctx, "catalog_release", "REL2", "release_approve", 1, 0)
	if err != nil {
		t.Fatal(err)
	}
	if audit.Total != 1 || len(audit.Items) != 1 || audit.Items[0].EventType != "RELEASE_APPROVE" {
		t.Fatalf("audit page: %+v", audit)
	}
	capped, err := repo.QueryAudit(ctx, "", "", "", 999, -10)
	if err != nil {
		t.Fatal(err)
	}
	if capped.Limit != mrsGovernanceMaxPageSize || capped.Offset != 0 || capped.Total != 9 {
		t.Fatalf("capped page: %+v", capped)
	}
}
