package sqlite

import (
	"context"
	"testing"
)

func TestMRSBookmarkListFilterStateAndRemoval(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	repo := NewMRSCatalogRepository(store)
	items := []MRSCatalogItem{
		{ID: "M1", Code: "MAT-001", Name: "水泥", Category: "MATERIAL", CurrentPrice: "100", PriceScale: 2, Enabled: true},
		{ID: "L1", Code: "LAB-001", Name: "技術工", Category: "LABOR", CurrentPrice: "200", PriceScale: 2, Enabled: true},
		{ID: "E1", Code: "EQP-001", Name: "挖土機", Category: "EQUIPMENT", CurrentPrice: "300", PriceScale: 2, Enabled: true},
	}
	for _, item := range items {
		if _, err := repo.SaveItem(ctx, item, "7", ""); err != nil {
			t.Fatal(err)
		}
	}
	if err := repo.SetBookmark(ctx, "7", "L1", true); err != nil {
		t.Fatal(err)
	}
	if err := repo.SetBookmark(ctx, "7", "M1", true); err != nil {
		t.Fatal(err)
	}
	if err := repo.SetBookmark(ctx, "8", "E1", true); err != nil {
		t.Fatal(err)
	}

	bookmarked, err := repo.IsBookmarked(ctx, "7", "M1")
	if err != nil || !bookmarked {
		t.Fatalf("bookmarked=%v err=%v", bookmarked, err)
	}
	otherActor, err := repo.IsBookmarked(ctx, "8", "M1")
	if err != nil || otherActor {
		t.Fatalf("otherActor=%v err=%v", otherActor, err)
	}

	all, err := repo.ListBookmarks(ctx, "7", "", "")
	if err != nil || len(all) != 2 {
		t.Fatalf("all=%+v err=%v", all, err)
	}
	if all[0].CatalogItem.Code != "LAB-001" || all[1].CatalogItem.Code != "MAT-001" {
		t.Fatalf("order=%+v", all)
	}
	if all[1].DeepLink != "/app/mrs?item=M1" || all[1].ActorID != "7" {
		t.Fatalf("lineage=%+v", all[1])
	}

	materials, err := repo.ListBookmarks(ctx, "7", "泥", "material")
	if err != nil || len(materials) != 1 || materials[0].CatalogItem.ID != "M1" {
		t.Fatalf("materials=%+v err=%v", materials, err)
	}
	codeSearch, err := repo.ListBookmarks(ctx, "7", "lab-", "")
	if err != nil || len(codeSearch) != 1 || codeSearch[0].CatalogItem.ID != "L1" {
		t.Fatalf("codeSearch=%+v err=%v", codeSearch, err)
	}

	if err = repo.SetBookmark(ctx, "7", "M1", false); err != nil {
		t.Fatal(err)
	}
	bookmarked, err = repo.IsBookmarked(ctx, "7", "M1")
	if err != nil || bookmarked {
		t.Fatalf("removed=%v err=%v", bookmarked, err)
	}
	remaining, err := repo.ListBookmarks(ctx, "7", "", "")
	if err != nil || len(remaining) != 1 || remaining[0].CatalogItem.ID != "L1" {
		t.Fatalf("remaining=%+v err=%v", remaining, err)
	}
}

func TestMRSBookmarkQueriesRequireActorAndExistingItem(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	repo := NewMRSCatalogRepository(store)
	if _, err := repo.ListBookmarks(ctx, "", "", ""); err == nil {
		t.Fatal("empty actor must fail")
	}
	if _, err := repo.IsBookmarked(ctx, "", "M1"); err == nil {
		t.Fatal("empty actor state query must fail")
	}
	if err := repo.SetBookmark(ctx, "7", "missing", true); err == nil {
		t.Fatal("missing catalog item must fail")
	}
}
