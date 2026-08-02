package sqlite

import (
	"os"
	"path/filepath"
	"sort"
	"strings"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

// PruneBackups keeps only the newest matching backup files.
func PruneBackups(directory, prefix string, keep int) ([]string, error) {
	if keep < 1 {
		return nil, errx.New(errx.CodeInvalidArgument, "backup keep count must be at least one", "P0-G2")
	}
	entries, err := os.ReadDir(directory)
	if os.IsNotExist(err) {
		return []string{}, nil
	}
	if err != nil {
		return nil, errx.Wrap(errx.CodeInternal, "read backup directory", "P0-G2", err)
	}
	type candidate struct {
		path string
		name string
	}
	items := make([]candidate, 0)
	for _, entry := range entries {
		if entry.IsDir() || !strings.HasPrefix(entry.Name(), prefix) || !strings.HasSuffix(entry.Name(), ".db") {
			continue
		}
		items = append(items, candidate{path: filepath.Join(directory, entry.Name()), name: entry.Name()})
	}
	sort.Slice(items, func(i, j int) bool { return items[i].name > items[j].name })
	if len(items) <= keep {
		return []string{}, nil
	}
	removed := make([]string, 0, len(items)-keep)
	for _, item := range items[keep:] {
		if err := os.Remove(item.path); err != nil {
			return removed, errx.Wrap(errx.CodeInternal, "remove expired backup", "P0-G2", err)
		}
		removed = append(removed, item.path)
	}
	return removed, nil
}
