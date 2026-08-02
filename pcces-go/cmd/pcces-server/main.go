package main

import (
	"context"
	"flag"
	"log/slog"
	"net/http"
	"os"
	"os/signal"
	"syscall"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/application/autosave"
	"github.com/liuxb99/pcces-web/pcces-go/internal/platform/httpapi"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func main() {
	var databasePath string
	var listenAddr string
	flag.StringVar(&databasePath, "db", "pcces-local.db", "SQLite database path")
	flag.StringVar(&listenAddr, "listen", "127.0.0.1:8787", "localhost API listen address")
	flag.Parse()

	logger := slog.New(slog.NewTextHandler(os.Stderr, &slog.HandlerOptions{Level: slog.LevelInfo}))
	ctx, stop := signal.NotifyContext(context.Background(), os.Interrupt, syscall.SIGTERM)
	defer stop()

	store, err := sqlite.Open(ctx, databasePath)
	if err != nil {
		logger.Error("open SQLite database", "error", err)
		os.Exit(1)
	}
	defer store.Close()

	settings := sqlite.NewSettingsRepository(store)
	if item, getErr := settings.Get(ctx, "sqlite.integrity_check_on_start"); getErr == nil && item.Value == "true" {
		if checkErr := store.IntegrityCheck(ctx); checkErr != nil {
			logger.Error("startup SQLite integrity check failed", "error", checkErr)
			os.Exit(1)
		}
	}

	autosaveService := autosave.New(logger, store)
	go autosaveService.Run(ctx)

	server := &http.Server{
		Addr:              listenAddr,
		Handler:           httpapi.New(logger, store).Handler(),
		ReadHeaderTimeout: 5 * time.Second,
		ReadTimeout:       30 * time.Second,
		WriteTimeout:      30 * time.Second,
		IdleTimeout:       60 * time.Second,
	}

	go func() {
		logger.Info("PCCES Local Go API started", "address", listenAddr, "database", store.Path())
		if err := server.ListenAndServe(); err != nil && err != http.ErrServerClosed {
			logger.Error("localhost API stopped unexpectedly", "error", err)
			stop()
		}
	}()

	<-ctx.Done()
	shutdownCtx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
	defer cancel()
	if err := server.Shutdown(shutdownCtx); err != nil {
		logger.Error("shutdown localhost API", "error", err)
		os.Exit(1)
	}
}
