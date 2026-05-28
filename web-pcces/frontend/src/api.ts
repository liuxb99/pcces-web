/* API 服務層 */

import axios, { AxiosError } from 'axios';
import type {
  LoginData, RegisterData, TokenResponse,
  Project, ProjectCreateData, DashboardStats,
  BudgetItem, BudgetItemCreateData, BudgetItemUpdateData,
  Resource,
} from './types';

const api = axios.create({
  baseURL: '/api',
  timeout: 30000,
  headers: { 'Content-Type': 'application/json' },
});

// 請求攔截器：自動帶入 JWT token
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('pcces_token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// 回應攔截器：統一錯誤處理
api.interceptors.response.use(
  (res) => res,
  (error: AxiosError) => {
    return Promise.reject(error);
  }
);

// ═══ 認證 ═══

export const authApi = {
  login: async (data: LoginData): Promise<TokenResponse> => {
    const res = await api.post('/auth/login', data);
    return res.data;
  },
  register: async (data: RegisterData): Promise<TokenResponse> => {
    const res = await api.post('/auth/register', data);
    return res.data;
  },
};

// ═══ 專案 ═══

export const projectApi = {
  list: async (): Promise<Project[]> => {
    const res = await api.get('/projects/');
    return res.data;
  },
  get: async (id: number): Promise<Project> => {
    const res = await api.get(`/projects/${id}`);
    return res.data;
  },
  create: async (data: ProjectCreateData): Promise<Project> => {
    const res = await api.post('/projects/', data);
    return res.data;
  },
  update: async (id: number, data: Partial<ProjectCreateData>): Promise<Project> => {
    const res = await api.put(`/projects/${id}`, data);
    return res.data;
  },
  delete: async (id: number): Promise<void> => {
    await api.delete(`/projects/${id}`);
  },
  getStats: async (): Promise<DashboardStats> => {
    const res = await api.get('/projects/stats');
    return res.data;
  },
};

// ═══ 預算項目 ═══

export const budgetApi = {
  getTree: async (projectId: number): Promise<BudgetItem[]> => {
    const res = await api.get(`/projects/${projectId}/budget/tree`);
    return res.data;
  },
  getList: async (projectId: number): Promise<BudgetItem[]> => {
    const res = await api.get(`/projects/${projectId}/budget/`);
    return res.data;
  },
  create: async (projectId: number, data: BudgetItemCreateData): Promise<BudgetItem> => {
    const res = await api.post(`/projects/${projectId}/budget/`, data);
    return res.data;
  },
  update: async (projectId: number, itemId: number, data: BudgetItemUpdateData): Promise<BudgetItem> => {
    const res = await api.put(`/projects/${projectId}/budget/${itemId}`, data);
    return res.data;
  },
  delete: async (projectId: number, itemId: number): Promise<void> => {
    await api.delete(`/projects/${projectId}/budget/${itemId}`);
  },
  move: async (projectId: number, itemId: number, newParentId: number | null): Promise<void> => {
    await api.post(`/projects/${projectId}/budget/${itemId}/move`, null, {
      params: { new_parent_id: newParentId },
    });
  },
  recalc: async (projectId: number): Promise<void> => {
    await api.post(`/projects/${projectId}/budget/recalc`);
  },
};

// ═══ 資源 ═══

export const resourceApi = {
  list: async (projectId: number): Promise<Resource[]> => {
    const res = await api.get(`/projects/${projectId}/resources/`);
    return res.data;
  },
  create: async (projectId: number, data: Partial<Resource>): Promise<Resource> => {
    const res = await api.post(`/projects/${projectId}/resources/`, data);
    return res.data;
  },
  updatePrice: async (projectId: number, resourceId: number, unitPrice: number): Promise<Resource> => {
    const res = await api.put(`/projects/${projectId}/resources/${resourceId}/price`, null, {
      params: { unit_price: unitPrice },
    });
    return res.data;
  },
  /** 更新資源欄位（含單價分析相關欄位） */
  update: async (projectId: number, resourceId: number, data: Record<string, any>): Promise<Resource> => {
    const res = await api.put(`/projects/${projectId}/resources/${resourceId}`, data);
    return res.data;
  },
  /** 列出啟用單價分析的資源（含各細項） */
  listAnalysis: async (projectId: number): Promise<any[]> => {
    const res = await api.get(`/projects/${projectId}/resources/analysis`);
    return res.data;
  },
  /** 取得資源單價分析細項列表 */
  getBreakdown: async (projectId: number, resourceId: number): Promise<any[]> => {
    const res = await api.get(`/projects/${projectId}/resources/${resourceId}/breakdown`);
    return res.data;
  },
  /** 新增資源單價分析細項 */
  createBreakdown: async (projectId: number, resourceId: number, data: Record<string, any>): Promise<any> => {
    const res = await api.post(`/projects/${projectId}/resources/${resourceId}/breakdown`, data);
    return res.data;
  },
  /** 刪除資源單價分析細項 */
  deleteBreakdown: async (projectId: number, resourceId: number, breakdownId: number): Promise<void> => {
    await api.delete(`/projects/${projectId}/resources/${resourceId}/breakdown/${breakdownId}`);
  },
  /** 重新計算所有啟用分析的資源單價 */
  recalcAnalysis: async (projectId: number): Promise<void> => {
    await api.post(`/projects/${projectId}/resources/analysis/recalc`);
  },
};

// ═══ 報表 ═══

export const reportApi = {
  getSummary: async (projectId: number) => {
    const res = await api.get(`/projects/${projectId}/reports/summary`);
    return res.data;
  },
  getExcelUrl: (projectId: number) => {
    return `/api/projects/${projectId}/reports/excel`;
  },
};

export default api;
