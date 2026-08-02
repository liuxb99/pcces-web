package main

import (
	"context"
	"flag"
	"fmt"
	"log"
	"os"
	"path/filepath"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func main() {
	var dbPath string
	var check bool
	var backupPath string
	var restorePath string
	flag.StringVar(&dbPath, "db", defaultDBPath(), "SQLite database path")
	flag.BoolVar(&check, "integrity-check", false, "run SQLite integrity check and exit")
	flag.StringVar(&backupPath, "backup", "", "create a SQLite backup at this path and exit")
	flag.StringVar(&restorePath, "restore", "", "restore the SQLite database from this backup and exit")
	flag.Parse()

	ctx := context.Background()
	if restorePath != "" {
		if err := sqlite.RestoreFrom(ctx, restorePath, dbPath); err != nil {
			log.Fatal(err)
		}
		fmt.Printf("SQLite restore complete\ndatabase: %s\nsource: %s\n", dbPath, restorePath)
		return
	}

	store, err := sqlite.Open(ctx, dbPath)
	if err != nil {
		log.Fatal(err)
	}
	defer store.Close()

	if check {
		if err := store.IntegrityCheck(ctx); err != nil {
			log.Fatal(err)
		}
		fmt.Println("SQLite integrity check: ok")
		return
	}
	if backupPath != "" {
		info, err := store.Backup(ctx, backupPath)
		if err != nil {
			log.Fatal(err)
		}
		fmt.Printf("SQLite backup complete\npath: %s\nsize: %d bytes\n", info.Path, info.SizeBytes)
		return
	}

	fmt.Printf("PCCES Local Go ready\ndatabase: %s\n", store.Path())
}

func defaultDBPath() string {
	configDir, err := os.UserConfigDir()
	if err != nil {
		return "pcces-local.db"
	}
	dir := filepath.Join(configDir, "PCCES")
	if err := os.MkdirAll(dir, 0o755); err != nil {
		return "pcces-local.db"
	}
	return filepath.Join(dir, "pcces-local.db")
}
