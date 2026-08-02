package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestApplyHistoricalMRSPriceIsAtomicAndAudited(t *testing.T){
	ctx:=context.Background();store,err:=Open(ctx,filepath.Join(t.TempDir(),"mrs-history.db"));if err!=nil{t.Fatal(err)};defer store.Close()
	repo:=NewMRSCatalogRepository(store)
	item,err:=repo.SaveItem(ctx,MRSCatalogItem{ID:"M1",Code:"M0000100000",Name:"鋼筋",Category:"MATERIAL",CurrentPrice:"10.0000",PriceScale:4,Enabled:true},"u1","2026-01-01");if err!=nil{t.Fatal(err)}
	item.CurrentPrice="12.5000";item.RowVersion=1
	item,err=repo.SaveItem(ctx,item,"u1","2026-02-01");if err!=nil{t.Fatal(err)}
	history,err:=repo.History(ctx,"M1");if err!=nil{t.Fatal(err)}
	oldest:=history[len(history)-1]
	result,err:=repo.ApplyHistoricalPrice(ctx,"M1",oldest.ID,"u2",item.RowVersion);if err!=nil{t.Fatal(err)}
	if result.NewPrice!="10.0000"||result.RowVersion!=item.RowVersion+1{t.Fatalf("unexpected result %#v",result)}
	updated,err:=repo.GetItem(ctx,"M1");if err!=nil{t.Fatal(err)}
	if updated.CurrentPrice!="10.0000"{t.Fatalf("price=%s",updated.CurrentPrice)}
	latest,err:=repo.History(ctx,"M1");if err!=nil{t.Fatal(err)}
	if latest[0].Source==nil||*latest[0].Source!="HISTORY_APPLY:"+oldest.ID{t.Fatalf("history source %#v",latest[0].Source)}
	if _,err=repo.ApplyHistoricalPrice(ctx,"M1",oldest.ID,"u2",item.RowVersion);err==nil{t.Fatal("expected stale row version conflict")}
}

func TestApplyHistoricalMRSPriceRejectsForeignHistory(t *testing.T){
	ctx:=context.Background();store,err:=Open(ctx,filepath.Join(t.TempDir(),"mrs-history-foreign.db"));if err!=nil{t.Fatal(err)};defer store.Close()
	repo:=NewMRSCatalogRepository(store)
	for _,id:=range []string{"M1","M2"}{if _,err=repo.SaveItem(ctx,MRSCatalogItem{ID:id,Code:id+"000000000",Name:id,Category:"MATERIAL",CurrentPrice:"1.0000",PriceScale:4,Enabled:true},"u1","");err!=nil{t.Fatal(err)}}
	history,err:=repo.History(ctx,"M2");if err!=nil{t.Fatal(err)}
	if _,err=repo.ApplyHistoricalPrice(ctx,"M1",history[0].ID,"u2",1);err==nil{t.Fatal("expected foreign history rejection")}
	item,err:=repo.GetItem(ctx,"M1");if err!=nil{t.Fatal(err)}
	if item.CurrentPrice!="1.0000"||item.RowVersion!=1{t.Fatalf("unexpected mutation %#v",item)}
}
