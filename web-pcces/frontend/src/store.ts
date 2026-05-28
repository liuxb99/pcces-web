/* 全域狀態管理 */

import { create } from 'zustand';
import type { User, Project, BudgetItem } from './types';
import { featureFlagApi } from './api';

interface AppState {
  // 認證
  user: User | null;
  token: string | null;
  setAuth: (user: User, token: string) => void;
  logout: () => void;
  isAuthenticated: () => boolean;

  // 專案
  currentProject: Project | null;
  setCurrentProject: (project: Project | null) => void;

  // 側邊欄
  sidebarCollapsed: boolean;
  toggleSidebar: () => void;

  // 全域載入
  loading: boolean;
  setLoading: (loading: boolean) => void;

  // 功能開關
  featureFlags: Record<string, boolean>;
  loadFeatureFlags: () => Promise<void>;
  isFeatureEnabled: (flagKey: string) => boolean;
}

export const useAppStore = create<AppState>((set, get) => ({
  // 從 localStorage 恢復認證狀態
  user: JSON.parse(localStorage.getItem('pcces_user') || 'null'),
  token: localStorage.getItem('pcces_token'),
  setAuth: (user, token) => {
    localStorage.setItem('pcces_token', token);
    localStorage.setItem('pcces_user', JSON.stringify(user));
    set({ user, token });
  },
  logout: () => {
    localStorage.removeItem('pcces_token');
    localStorage.removeItem('pcces_user');
    set({ user: null, token: null, currentProject: null });
  },
  isAuthenticated: () => !!get().token,

  currentProject: null,
  setCurrentProject: (project) => set({ currentProject: project }),

  sidebarCollapsed: false,
  toggleSidebar: () => set((s) => ({ sidebarCollapsed: !s.sidebarCollapsed })),

  loading: false,
  setLoading: (loading) => set({ loading }),

  // 功能開關
  featureFlags: {},
  loadFeatureFlags: async () => {
    try {
      const flags = await featureFlagApi.listEnabled();
      const map: Record<string, boolean> = {};
      flags.forEach((f) => { map[f.flag_key] = f.is_enabled; });
      set({ featureFlags: map });
    } catch {
      // 預設全部啟用（向後相容）
    }
  },
  isFeatureEnabled: (flagKey) => {
    const state = get();
    // 若 store 中無該 key，預設為 true（向後相容）
    return state.featureFlags[flagKey] ?? true;
  },
}));
