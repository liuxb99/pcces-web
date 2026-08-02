package sqlite

import (
	"context"
	"path/filepath"
	"testing"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
)

func TestBudgetTraceRepositoryLifecycle(t *testing.T){
	ctx:=context.Background();store,err:=Open(ctx,filepath.Join(t.TempDir(),"trace.db"));if err!=nil{t.Fatal(err)};defer store.Close()
	repo:=NewBudgetTraceRepository(store)
	rec,err:=repo.Calculate(ctx,"trace-1","P1",nil,"F",2,money.BudgetKindInput{Base:"1000",Rate:"0.075"});if err!=nil{t.Fatal(err)}
	if rec.Result!="75.00"{t.Fatalf("got %s",rec.Result)}
	loaded,err:=repo.Get(ctx,"trace-1");if err!=nil{t.Fatal(err)};if len(loaded.Steps)!=1||loaded.Steps[0].Operation!="MULTIPLY_BASE_RATE"{t.Fatalf("unexpected steps: %#v",loaded.Steps)}
	items,err:=repo.ListProject(ctx,"P1");if err!=nil{t.Fatal(err)};if len(items)!=1{t.Fatalf("got %d traces",len(items))}
	if _,err=repo.Calculate(ctx,"trace-1","P1",nil,"L",2,money.BudgetKindInput{Quantity:"1",UnitPrice:"1"});err==nil{t.Fatal("expected duplicate trace conflict")}
}
