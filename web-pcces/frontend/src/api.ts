/* API 服務層 */

import axios, { AxiosError } from 'axios';
import type {
  User, LoginData, RegisterData, TokenResponse,
  Project, ProjectCreateData, DashboardStats,
  BudgetItem, BudgetItemCreateData, BudgetItemUpdateData,
  Resource,
  Invoice, InvoiceItem,
  CreateInvoiceRequest, CreateInvoiceItemRequest, BatchCreateItemsRequest,
  Contract, ContractCreateData,
  ContractItem, ContractItemCreateData,
  ContractIssue, ContractIssueCreateData,
  ContractIssueItem, ContractIssueItemCreateData,
  ContractSettlement, ContractSettlementCreateData,
  ContractSettlementItem, ContractSettlementItemCreateData,
  ContractFinalAcceptance, ContractFinalAcceptanceCreateData,
  ContractFinalAcceptanceItem, ContractFinalAcceptanceItemCreateData,
  MrsBaseCategory, MrsBaseCategoryCreateData,
  MrsBaseItem, MrsBaseItemCreateData, MrsBaseItemUpdateData,
  MrsBaseBreakdownItem, MrsBaseBreakdownCreateData,
  MrsBaseBookmark, PaginatedMrsBaseItems,
  /* admin types */
  UserCreateData, UserUpdateData,
  SystemParameter, SystemParamCreateData,
  CodeTable, CodeItem, CodeItemCreateData,
  Organization, OrganizationCreateData,
  /* compare types */
  CompareResult, CompareRequest,
  MrsBasePriceCompareResult, MrsBasePriceCompareRequest,
  /* feature flag & version types */
  FeatureFlag, FeatureFlagCreateData, FeatureFlagUpdateData,
  VersionInfo, HealthStatus,
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

// ═══ 計價（Invoice） ═══

export const invoiceApi = {
  /** 取得專案的計價列表 */
  list: async (projectId: number): Promise<Invoice[]> => {
    const res = await api.get(`/projects/${projectId}/invoices/`);
    return res.data;
  },
  /** 建立計價單（自動產生期別） */
  create: async (projectId: number, data: CreateInvoiceRequest): Promise<Invoice> => {
    const res = await api.post(`/projects/${projectId}/invoices/`, data);
    return res.data;
  },
  /** 取得單筆計價 */
  get: async (projectId: number, invoiceId: number): Promise<Invoice> => {
    const res = await api.get(`/projects/${projectId}/invoices/${invoiceId}`);
    return res.data;
  },
  /** 更新計價單（草稿） */
  update: async (projectId: number, invoiceId: number, data: Partial<CreateInvoiceRequest>): Promise<Invoice> => {
    const res = await api.put(`/projects/${projectId}/invoices/${invoiceId}`, data);
    return res.data;
  },
  /** 刪除計價單（草稿） */
  delete: async (projectId: number, invoiceId: number): Promise<void> => {
    await api.delete(`/projects/${projectId}/invoices/${invoiceId}`);
  },
  /** 重算計價金額 */
  recalc: async (invoiceId: number): Promise<void> => {
    await api.post(`/api/invoices/${invoiceId}/recalc`);
  },
  /** 提交審核 */
  submit: async (invoiceId: number): Promise<Invoice> => {
    const res = await api.post(`/api/invoices/${invoiceId}/submit`);
    return res.data;
  },
  /** 核准 */
  approve: async (invoiceId: number): Promise<Invoice> => {
    const res = await api.post(`/api/invoices/${invoiceId}/approve`);
    return res.data;
  },

  // ── 明細 ──
  /** 取得計價明細列表 */
  listItems: async (invoiceId: number): Promise<InvoiceItem[]> => {
    const res = await api.get(`/api/invoices/${invoiceId}/items/`);
    return res.data;
  },
  /** 新增計價明細 */
  createItem: async (invoiceId: number, data: CreateInvoiceItemRequest): Promise<InvoiceItem> => {
    const res = await api.post(`/api/invoices/${invoiceId}/items/`, data);
    return res.data;
  },
  /** 更新計價明細 */
  updateItem: async (invoiceId: number, itemId: number, data: Partial<CreateInvoiceItemRequest>): Promise<InvoiceItem> => {
    const res = await api.put(`/api/invoices/${invoiceId}/items/${itemId}`, data);
    return res.data;
  },
  /** 刪除計價明細 */
  deleteItem: async (invoiceId: number, itemId: number): Promise<void> => {
    await api.delete(`/api/invoices/${invoiceId}/items/${itemId}`);
  },
  /** 批次匯入預算項目 */
  batchCreateItems: async (invoiceId: number, data: BatchCreateItemsRequest): Promise<{ created: InvoiceItem[]; count: number }> => {
    const res = await api.post(`/api/invoices/${invoiceId}/items/batch`, data);
    return res.data;
  },

  // ── 報表 ──
  /** HTML 報表預覽 URL */
  getReportUrl: (invoiceId: number): string => {
    return `/api/invoices/${invoiceId}/report`;
  },
  /** Excel 匯出 URL */
  getExportExcelUrl: (invoiceId: number): string => {
    return `/api/invoices/${invoiceId}/export/excel`;
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

// ═══ 分包合約（Contract） ═══

export const contractApi = {
  // ── Contract CRUD ──
  list: async (projectId: number): Promise<Contract[]> => {
    const res = await api.get(`/api/projects/${projectId}/contracts/`);
    return res.data;
  },
  get: async (projectId: number, contractId: number): Promise<Contract> => {
    const res = await api.get(`/api/projects/${projectId}/contracts/${contractId}`);
    return res.data;
  },
  create: async (projectId: number, data: ContractCreateData): Promise<Contract> => {
    const res = await api.post(`/api/projects/${projectId}/contracts/`, data);
    return res.data;
  },
  update: async (projectId: number, contractId: number, data: Partial<ContractCreateData>): Promise<Contract> => {
    const res = await api.put(`/api/projects/${projectId}/contracts/${contractId}`, data);
    return res.data;
  },
  delete: async (projectId: number, contractId: number): Promise<void> => {
    await api.delete(`/api/projects/${projectId}/contracts/${contractId}`);
  },
  close: async (projectId: number, contractId: number): Promise<Contract> => {
    const res = await api.post(`/api/projects/${projectId}/contracts/${contractId}/close`);
    return res.data;
  },
  finalize: async (projectId: number, contractId: number): Promise<Contract> => {
    const res = await api.post(`/api/projects/${projectId}/contracts/${contractId}/finalize`);
    return res.data;
  },

  // ── Contract Items ──
  listItems: async (contractId: number): Promise<ContractItem[]> => {
    const res = await api.get(`/api/contracts/${contractId}/items/`);
    return res.data;
  },
  createItem: async (contractId: number, data: ContractItemCreateData): Promise<ContractItem> => {
    const res = await api.post(`/api/contracts/${contractId}/items/`, data);
    return res.data;
  },
  updateItem: async (contractId: number, itemId: number, data: Partial<ContractItemCreateData>): Promise<ContractItem> => {
    const res = await api.put(`/api/contracts/${contractId}/items/${itemId}`, data);
    return res.data;
  },
  deleteItem: async (contractId: number, itemId: number): Promise<void> => {
    await api.delete(`/api/contracts/${contractId}/items/${itemId}`);
  },
  batchCreateItems: async (contractId: number, data: { budget_item_ids?: number[]; include_all_leaf?: boolean }): Promise<{ created: ContractItem[]; count: number }> => {
    const res = await api.post(`/api/contracts/${contractId}/items/batch`, data);
    return res.data;
  },

  // ── Issues ──
  listIssues: async (contractId: number): Promise<ContractIssue[]> => {
    const res = await api.get(`/api/contracts/${contractId}/issues/`);
    return res.data;
  },
  createIssue: async (contractId: number, data: ContractIssueCreateData): Promise<ContractIssue> => {
    const res = await api.post(`/api/contracts/${contractId}/issues/`, data);
    return res.data;
  },
  getIssue: async (contractId: number, issueId: number): Promise<ContractIssue> => {
    const res = await api.get(`/api/contracts/${contractId}/issues/${issueId}`);
    return res.data;
  },
  updateIssue: async (contractId: number, issueId: number, data: Partial<ContractIssueCreateData>): Promise<ContractIssue> => {
    const res = await api.put(`/api/contracts/${contractId}/issues/${issueId}`, data);
    return res.data;
  },
  deleteIssue: async (contractId: number, issueId: number): Promise<void> => {
    await api.delete(`/api/contracts/${contractId}/issues/${issueId}`);
  },
  submitIssue: async (contractId: number, issueId: number): Promise<ContractIssue> => {
    const res = await api.post(`/api/contracts/${contractId}/issues/${issueId}/submit`);
    return res.data;
  },
  approveIssue: async (contractId: number, issueId: number): Promise<ContractIssue> => {
    const res = await api.post(`/api/contracts/${contractId}/issues/${issueId}/approve`);
    return res.data;
  },

  // ── Issue Items ──
  listIssueItems: async (issueId: number): Promise<ContractIssueItem[]> => {
    const res = await api.get(`/api/issues/${issueId}/items/`);
    return res.data;
  },
  createIssueItem: async (issueId: number, data: ContractIssueItemCreateData): Promise<ContractIssueItem> => {
    const res = await api.post(`/api/issues/${issueId}/items/`, data);
    return res.data;
  },
  updateIssueItem: async (issueId: number, itemId: number, data: Partial<ContractIssueItemCreateData>): Promise<ContractIssueItem> => {
    const res = await api.put(`/api/issues/${issueId}/items/${itemId}`, data);
    return res.data;
  },
  deleteIssueItem: async (issueId: number, itemId: number): Promise<void> => {
    await api.delete(`/api/issues/${issueId}/items/${itemId}`);
  },
  recalcIssue: async (issueId: number): Promise<void> => {
    await api.post(`/api/issues/${issueId}/items/recalc`);
  },
  batchIssueItemsFromContract: async (issueId: number): Promise<{ created: ContractIssueItem[]; count: number }> => {
    const res = await api.post(`/api/issues/${issueId}/items/batch-from-contract`);
    return res.data;
  },

  // ── Settlements ──
  listSettlements: async (contractId: number): Promise<ContractSettlement[]> => {
    const res = await api.get(`/api/contracts/${contractId}/settlements/`);
    return res.data;
  },
  createSettlement: async (contractId: number, data: ContractSettlementCreateData): Promise<ContractSettlement> => {
    const res = await api.post(`/api/contracts/${contractId}/settlements/`, data);
    return res.data;
  },
  getSettlement: async (contractId: number, settlementId: number): Promise<ContractSettlement> => {
    const res = await api.get(`/api/contracts/${contractId}/settlements/${settlementId}`);
    return res.data;
  },
  updateSettlement: async (contractId: number, settlementId: number, data: Partial<ContractSettlementCreateData>): Promise<ContractSettlement> => {
    const res = await api.put(`/api/contracts/${contractId}/settlements/${settlementId}`, data);
    return res.data;
  },
  deleteSettlement: async (contractId: number, settlementId: number): Promise<void> => {
    await api.delete(`/api/contracts/${contractId}/settlements/${settlementId}`);
  },
  submitSettlement: async (contractId: number, settlementId: number): Promise<ContractSettlement> => {
    const res = await api.post(`/api/contracts/${contractId}/settlements/${settlementId}/submit`);
    return res.data;
  },
  approveSettlement: async (contractId: number, settlementId: number): Promise<ContractSettlement> => {
    const res = await api.post(`/api/contracts/${contractId}/settlements/${settlementId}/approve`);
    return res.data;
  },

  // ── Settlement Items ──
  listSettlementItems: async (settlementId: number): Promise<ContractSettlementItem[]> => {
    const res = await api.get(`/api/settlements/${settlementId}/items/`);
    return res.data;
  },
  createSettlementItem: async (settlementId: number, data: ContractSettlementItemCreateData): Promise<ContractSettlementItem> => {
    const res = await api.post(`/api/settlements/${settlementId}/items/`, data);
    return res.data;
  },
  updateSettlementItem: async (settlementId: number, itemId: number, data: Partial<ContractSettlementItemCreateData>): Promise<ContractSettlementItem> => {
    const res = await api.put(`/api/settlements/${settlementId}/items/${itemId}`, data);
    return res.data;
  },
  deleteSettlementItem: async (settlementId: number, itemId: number): Promise<void> => {
    await api.delete(`/api/settlements/${settlementId}/items/${itemId}`);
  },
  recalcSettlement: async (settlementId: number): Promise<void> => {
    await api.post(`/api/settlements/${settlementId}/items/recalc`);
  },

  // ── Acceptances ──
  listAcceptances: async (contractId: number): Promise<ContractFinalAcceptance[]> => {
    const res = await api.get(`/api/contracts/${contractId}/acceptances/`);
    return res.data;
  },
  createAcceptance: async (contractId: number, data: ContractFinalAcceptanceCreateData): Promise<ContractFinalAcceptance> => {
    const res = await api.post(`/api/contracts/${contractId}/acceptances/`, data);
    return res.data;
  },
  getAcceptance: async (contractId: number, acceptanceId: number): Promise<ContractFinalAcceptance> => {
    const res = await api.get(`/api/contracts/${contractId}/acceptances/${acceptanceId}`);
    return res.data;
  },
  updateAcceptance: async (contractId: number, acceptanceId: number, data: Partial<ContractFinalAcceptanceCreateData>): Promise<ContractFinalAcceptance> => {
    const res = await api.put(`/api/contracts/${contractId}/acceptances/${acceptanceId}`, data);
    return res.data;
  },
  deleteAcceptance: async (contractId: number, acceptanceId: number): Promise<void> => {
    await api.delete(`/api/contracts/${contractId}/acceptances/${acceptanceId}`);
  },
  submitAcceptance: async (contractId: number, acceptanceId: number): Promise<ContractFinalAcceptance> => {
    const res = await api.post(`/api/contracts/${contractId}/acceptances/${acceptanceId}/submit`);
    return res.data;
  },
  approveAcceptance: async (contractId: number, acceptanceId: number): Promise<ContractFinalAcceptance> => {
    const res = await api.post(`/api/contracts/${contractId}/acceptances/${acceptanceId}/approve`);
    return res.data;
  },

  // ── Acceptance Items ──
  listAcceptanceItems: async (acceptanceId: number): Promise<ContractFinalAcceptanceItem[]> => {
    const res = await api.get(`/api/acceptances/${acceptanceId}/items/`);
    return res.data;
  },
  createAcceptanceItem: async (acceptanceId: number, data: ContractFinalAcceptanceItemCreateData): Promise<ContractFinalAcceptanceItem> => {
    const res = await api.post(`/api/acceptances/${acceptanceId}/items/`, data);
    return res.data;
  },
  updateAcceptanceItem: async (acceptanceId: number, itemId: number, data: Partial<ContractFinalAcceptanceItemCreateData>): Promise<ContractFinalAcceptanceItem> => {
    const res = await api.put(`/api/acceptances/${acceptanceId}/items/${itemId}`, data);
    return res.data;
  },
  deleteAcceptanceItem: async (acceptanceId: number, itemId: number): Promise<void> => {
    await api.delete(`/api/acceptances/${acceptanceId}/items/${itemId}`);
  },
  recalcAcceptance: async (acceptanceId: number): Promise<void> => {
    await api.post(`/api/acceptances/${acceptanceId}/items/recalc`);
  },
  batchAcceptanceItemsFromContract: async (acceptanceId: number): Promise<{ created: ContractFinalAcceptanceItem[]; count: number }> => {
    const res = await api.post(`/api/acceptances/${acceptanceId}/items/batch-from-contract`);
    return res.data;
  },
};

// ═══ MrsBase 公共單價庫 ═══

export const mrsBaseApi = {
  // ── 分類 ──
  /** 取得分類樹（巢狀 JSON） */
  getCategories: async (): Promise<MrsBaseCategory[]> => {
    const res = await api.get('/mrs-base/categories');
    return res.data;
  },
  /** 取得分類平面列表 */
  getCategoriesFlat: async (): Promise<MrsBaseCategory[]> => {
    const res = await api.get('/mrs-base/categories/flat');
    return res.data;
  },
  /** 建立分類 */
  createCategory: async (data: MrsBaseCategoryCreateData): Promise<MrsBaseCategory> => {
    const res = await api.post('/mrs-base/categories', data);
    return res.data;
  },
  /** 更新分類 */
  updateCategory: async (id: number, data: Partial<MrsBaseCategoryCreateData>): Promise<MrsBaseCategory> => {
    const res = await api.put(`/mrs-base/categories/${id}`, data);
    return res.data;
  },
  /** 刪除分類 */
  deleteCategory: async (id: number): Promise<void> => {
    await api.delete(`/mrs-base/categories/${id}`);
  },

  // ── 項目 ──
  /** 列表（支援查詢參數） */
  listItems: async (params?: {
    category_id?: number;
    q?: string;
    kind?: string;
    approved?: string;
    page?: number;
    per_page?: number;
  }): Promise<PaginatedMrsBaseItems> => {
    const res = await api.get('/mrs-base/items', { params });
    return res.data;
  },
  /** 單筆（含 breakdown_items） */
  getItem: async (id: number): Promise<MrsBaseItem> => {
    const res = await api.get(`/mrs-base/items/${id}`);
    return res.data;
  },
  /** 新增項目 */
  createItem: async (data: MrsBaseItemCreateData): Promise<MrsBaseItem> => {
    const res = await api.post('/mrs-base/items', data);
    return res.data;
  },
  /** 更新項目 */
  updateItem: async (id: number, data: MrsBaseItemUpdateData): Promise<MrsBaseItem> => {
    const res = await api.put(`/mrs-base/items/${id}`, data);
    return res.data;
  },
  /** 刪除項目 */
  deleteItem: async (id: number): Promise<void> => {
    await api.delete(`/mrs-base/items/${id}`);
  },
  /** 批次刪除 */
  batchDeleteItems: async (ids: number[]): Promise<{ message: string; deleted: number }> => {
    const res = await api.post('/mrs-base/items/batch-delete', { ids });
    return res.data;
  },
  /** 審核通過 */
  approveItem: async (id: number): Promise<MrsBaseItem> => {
    const res = await api.post(`/mrs-base/items/${id}/approve`);
    return res.data;
  },
  /** 取消審核 */
  unapproveItem: async (id: number): Promise<MrsBaseItem> => {
    const res = await api.post(`/mrs-base/items/${id}/unapprove`);
    return res.data;
  },

  // ── 工料機組成（Breakdown） ──
  /** 取得工料機組成列表 */
  getBreakdownItems: async (itemId: number): Promise<MrsBaseBreakdownItem[]> => {
    const res = await api.get(`/mrs-base/items/${itemId}/breakdown`);
    return res.data;
  },
  /** 新增工料機組成 */
  createBreakdownItem: async (itemId: number, data: MrsBaseBreakdownCreateData): Promise<MrsBaseBreakdownItem> => {
    const res = await api.post(`/mrs-base/items/${itemId}/breakdown`, data);
    return res.data;
  },
  /** 更新工料機組成 */
  updateBreakdownItem: async (itemId: number, bdId: number, data: Partial<MrsBaseBreakdownCreateData>): Promise<MrsBaseBreakdownItem> => {
    const res = await api.put(`/mrs-base/items/${itemId}/breakdown/${bdId}`, data);
    return res.data;
  },
  /** 刪除工料機組成 */
  deleteBreakdownItem: async (itemId: number, bdId: number): Promise<void> => {
    await api.delete(`/mrs-base/items/${itemId}/breakdown/${bdId}`);
  },
  /** 重新計算單價 */
  recalcBreakdown: async (itemId: number): Promise<{ message: string; unit_price: number }> => {
    const res = await api.post(`/mrs-base/items/${itemId}/breakdown/recalc`);
    return res.data;
  },

  // ── 書籤 ──
  /** 取得我的書籤 */
  getBookmarks: async (): Promise<MrsBaseBookmark[]> => {
    const res = await api.get('/mrs-base/bookmarks');
    return res.data;
  },
  /** 新增書籤 */
  createBookmark: async (itemId: number): Promise<MrsBaseBookmark> => {
    const res = await api.post('/mrs-base/bookmarks', { item_id: itemId });
    return res.data;
  },
  /** 移除書籤 */
  deleteBookmark: async (id: number): Promise<void> => {
    await api.delete(`/mrs-base/bookmarks/${id}`);
  },

  // ── 搜尋 ──
  /** 模糊搜尋 */
  search: async (params: { q?: string; category?: string; kind?: string }): Promise<MrsBaseItem[]> => {
    const res = await api.get('/mrs-base/search', { params });
    return res.data;
  },

  // ── 引用 ──
  /** 連結到預算項 */
  linkToBudget: async (itemId: number, projectId: number, budgetItemId: number): Promise<any> => {
    const res = await api.post(`/mrs-base/items/${itemId}/link-to-budget`, {
      project_id: projectId,
      budget_item_id: budgetItemId,
    });
    return res.data;
  },
  /** 列出引用此單價的專案/預算項 */
  getLinkedProjects: async (itemId: number): Promise<any[]> => {
    const res = await api.get(`/mrs-base/items/${itemId}/linked-projects`);
    return res.data;
  },
};

// ═══ 系統維護（Admin Only） ═══

export const adminApi = {
  // ── 使用者管理 ──
  listUsers: async (params?: { q?: string; role?: string; is_active?: string; page?: number; per_page?: number }): Promise<{ users: User[]; total: number }> => {
    const res = await api.get('/api/admin/users', { params });
    return res.data;
  },
  getUser: async (id: number): Promise<User> => {
    const res = await api.get(`/api/admin/users/${id}`);
    return res.data;
  },
  createUser: async (data: UserCreateData): Promise<User> => {
    const res = await api.post('/api/admin/users', data);
    return res.data;
  },
  updateUser: async (id: number, data: UserUpdateData): Promise<User> => {
    const res = await api.put(`/api/admin/users/${id}`, data);
    return res.data;
  },
  deleteUser: async (id: number): Promise<void> => {
    await api.delete(`/api/admin/users/${id}`);
  },
  toggleUserActive: async (id: number): Promise<User> => {
    const res = await api.post(`/api/admin/users/${id}/toggle-active`);
    return res.data;
  },
  changeUserRole: async (id: number, role: string): Promise<User> => {
    const res = await api.post(`/api/admin/users/${id}/change-role`, { role });
    return res.data;
  },

  // ── 系統參數 ──
  listParams: async (category?: string): Promise<SystemParameter[]> => {
    const res = await api.get('/api/admin/params', { params: { category } });
    return res.data;
  },
  createParam: async (data: SystemParamCreateData): Promise<SystemParameter> => {
    const res = await api.post('/api/admin/params', data);
    return res.data;
  },
  updateParam: async (id: number, data: Partial<SystemParamCreateData>): Promise<SystemParameter> => {
    const res = await api.put(`/api/admin/params/${id}`, data);
    return res.data;
  },
  deleteParam: async (id: number): Promise<void> => {
    await api.delete(`/api/admin/params/${id}`);
  },

  // ── 代碼表 ──
  listCodeTables: async (): Promise<CodeTable[]> => {
    const res = await api.get('/api/admin/code-tables');
    return res.data;
  },
  createCodeTable: async (data: Partial<CodeTable>): Promise<CodeTable> => {
    const res = await api.post('/api/admin/code-tables', data);
    return res.data;
  },
  updateCodeTable: async (id: number, data: Partial<CodeTable>): Promise<CodeTable> => {
    const res = await api.put(`/api/admin/code-tables/${id}`, data);
    return res.data;
  },
  deleteCodeTable: async (id: number): Promise<void> => {
    await api.delete(`/api/admin/code-tables/${id}`);
  },
  listCodeItems: async (tableId: number): Promise<CodeItem[]> => {
    const res = await api.get(`/api/admin/code-tables/${tableId}/items`);
    return res.data;
  },
  createCodeItem: async (tableId: number, data: CodeItemCreateData): Promise<CodeItem> => {
    const res = await api.post(`/api/admin/code-tables/${tableId}/items`, data);
    return res.data;
  },
  updateCodeItem: async (id: number, data: Partial<CodeItemCreateData>): Promise<CodeItem> => {
    const res = await api.put(`/api/admin/code-items/${id}`, data);
    return res.data;
  },
  deleteCodeItem: async (id: number): Promise<void> => {
    await api.delete(`/api/admin/code-items/${id}`);
  },

  // ── 組織機構 ──
  listOrganizations: async (): Promise<Organization[]> => {
    const res = await api.get('/api/admin/organizations');
    return res.data;
  },
  createOrganization: async (data: OrganizationCreateData): Promise<Organization> => {
    const res = await api.post('/api/admin/organizations', data);
    return res.data;
  },
  updateOrganization: async (id: number, data: Partial<OrganizationCreateData>): Promise<Organization> => {
    const res = await api.put(`/api/admin/organizations/${id}`, data);
    return res.data;
  },
  deleteOrganization: async (id: number): Promise<void> => {
    await api.delete(`/api/admin/organizations/${id}`);
  },

  // ── 功能開關管理（Admin Only） ──
  featureFlags: {
    list: async (params?: { category?: string; page?: number; per_page?: number }): Promise<{ total: number; flags: FeatureFlag[] }> => {
      const res = await api.get('/api/admin/feature-flags', { params });
      return res.data;
    },
    create: async (data: FeatureFlagCreateData): Promise<FeatureFlag> => {
      const res = await api.post('/api/admin/feature-flags', data);
      return res.data;
    },
    update: async (id: number, data: FeatureFlagUpdateData): Promise<FeatureFlag> => {
      const res = await api.put(`/api/admin/feature-flags/${id}`, data);
      return res.data;
    },
    delete: async (id: number): Promise<void> => {
      await api.delete(`/api/admin/feature-flags/${id}`);
    },
    toggle: async (id: number): Promise<FeatureFlag> => {
      const res = await api.post(`/api/admin/feature-flags/${id}/toggle`);
      return res.data;
    },
  },
};

// ═══ 系統資訊（公開） ═══

export const systemApi = {
  /** 取得系統版本資訊 */
  getVersion: async (): Promise<VersionInfo> => {
    const res = await api.get('/api/system/version');
    return res.data;
  },
  /** 系統健康檢查 */
  getHealth: async (): Promise<HealthStatus> => {
    const res = await api.get('/api/system/health');
    return res.data;
  },
};

// ═══ 功能開關（公開 — 用於前端決定 UI 顯示） ═══

export const featureFlagApi = {
  /** 取得所有已啟用的功能開關 */
  listEnabled: async (): Promise<FeatureFlag[]> => {
    const res = await api.get('/api/feature-flags');
    return res.data;
  },
};

// ═══ 比較分析 ═══

export const compareApi = {
  /** 比較兩個專案的預算項目（POST 版） */
  compareBudgetItems: async (data: CompareRequest): Promise<CompareResult> => {
    const res = await api.post('/api/compare/budget-items', data);
    return res.data;
  },
  /** 比較兩個專案的預算項目（GET 版） */
  compareBudgetItemsGet: async (projectAId: number, projectBId: number): Promise<CompareResult> => {
    const res = await api.get('/api/compare/budget-items', {
      params: { project_a_id: projectAId, project_b_id: projectBId },
    });
    return res.data;
  },
  /** 匯出工項比較報表 Excel */
  exportExcel: async (data: CompareRequest): Promise<{ data: Blob; filename: string }> => {
    const res = await api.post('/api/compare/budget-items/export/excel', data, {
      responseType: 'blob',
    });
    // 從 response headers 解析檔名
    const disposition = res.headers?.['content-disposition'] as string | undefined;
    let filename = 'PCCES_比較報表.xlsx';
    if (disposition) {
      const match = disposition.match(/filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/);
      if (match) filename = match[1].replace(/['"]/g, '');
    }
    return { data: res.data, filename };
  },
  /** MrsBase 單價比較 / 一覽 */
  compareMrsBasePrices: async (data: MrsBasePriceCompareRequest): Promise<MrsBasePriceCompareResult> => {
    const res = await api.post('/api/compare/mrs-base-prices', data);
    return res.data;
  },
};

export default api;
