import { defineConfig, type Plugin } from 'vite';
import react from '@vitejs/plugin-react';
import path from 'path';

/**
 * Phase 0 compatibility guard.
 *
 * The shared Axios instance already uses `/api` as baseURL, while parts of the
 * legacy client still pass `/api/...` request paths. Normalize only Axios call
 * sites during transformation so they cannot become `/api/api/...`.
 * Direct browser download/report URLs intentionally keep their `/api` prefix.
 */
function normalizeApiClientPaths(): Plugin {
  return {
    name: 'pcces-normalize-api-client-paths',
    enforce: 'pre',
    transform(code, id) {
      if (!id.replace(/\\/g, '/').endsWith('/src/api.ts')) {
        return null;
      }
      const normalized = code.replace(
        /(api\.(?:get|post|put|patch|delete)\(\s*[`'"])\/api\//g,
        '$1/',
      );
      return normalized === code ? null : { code: normalized, map: null };
    },
  };
}

export default defineConfig({
  plugins: [normalizeApiClientPaths(), react()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  server: {
    port: 5173,
    proxy: {
      '/api': {
        target: 'http://localhost:8000',
        changeOrigin: true,
      },
    },
  },
  build: {
    outDir: 'dist',
    sourcemap: false,
  },
});
