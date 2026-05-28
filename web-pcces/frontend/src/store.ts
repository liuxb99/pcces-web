/* 全域狀態管理 */

import { create } from 'zustand';
import type { User, Project, BudgetItem } from './types';

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
}));
