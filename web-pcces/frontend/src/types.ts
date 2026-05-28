/* PCCES 前端型別定義 */

// ─── 使用者 ───
export interface User {
  id: number;
  username: string;
  display_name: string;
  email: string | null;
  company: string | null;
  department: string | null;
  phone: string | null;
  role: string;
  is_active: boolean;
  created_at: string;
}

export interface LoginData {
  username: string;
  password: string;
}

export interface RegisterData {
  username: string;
  password: string;
  display_name: string;
  email?: string;
  company?: string;
  department?: string;
  phone?: string;
}

export interface TokenResponse {
  access_token: string;
  token_type: string;
  user: User;
}

// ─── 專案 ───
export interface Project {
  id: number;
  code: string;
  name: string;
  name_en: string | null;
  location: string | null;
  account_code: string | null;
  description: string | null;
  scope: number | null;
  scope_unit: string | null;
  owner_id: number;
  status: string;
  created_at: string;
  updated_at: string;
  budget_total?: number;
  item_count?: number;
}

export interface ProjectCreateData {
  code: string;
  name: string;
  name_en?: string;
  location?: string;
  account_code?: string;
  description?: string;
  scope?: number;
  scope_unit?: string;
}

// ─── 預算項目（核心） ───
export type BudgetItemKind = 'B' | 'L' | 'F' | 'S' | 'Z' | 'U' | 'W';

export interface BudgetItem {
  id: number;
  project_id: number;
  parent_id: number | null;
  item_no: string | null;
  print_no: string | null;
  c_name: string | null;
  e_name: string | null;
  c_unit: string | null;
  e_unit: string | null;
  quantity: number;
  unit_price: number;
  amount: number;
  kind: BudgetItemKind;
  formula: string | null;
  memo: string | null;
  sort_order: string | null;
  level_no: number;
  is_fixed_price: boolean;
  is_locked: boolean;
  is_green_item: boolean;
  created_at: string;
  children: BudgetItem[];
}

export interface BudgetItemCreateData {
  parent_id?: number | null;
  item_no?: string;
  print_no?: string;
  c_name?: string;
  e_name?: string;
  c_unit?: string;
  e_unit?: string;
  quantity?: number;
  unit_price?: number;
  kind?: BudgetItemKind;
  formula?: string;
  memo?: string;
  sort_order?: string;
  is_fixed_price?: boolean;
}

export interface BudgetItemUpdateData {
  parent_id?: number | null;
  item_no?: string;
  print_no?: string;
  c_name?: string;
  e_name?: string;
  c_unit?: string;
  e_unit?: string;
  quantity?: number;
  unit_price?: number;
  kind?: BudgetItemKind;
  formula?: string;
  memo?: string;
  sort_order?: string;
  is_fixed_price?: boolean;
  is_locked?: boolean;
}

// ─── 資源 ───
export interface Resource {
  id: number;
  project_id: number;
  code: string;
  c_name: string;
  e_name: string | null;
  c_unit: string;
  e_unit: string | null;
  unit_price: number;
  category: 'labor' | 'material' | 'equipment' | 'other';
  is_public: boolean;
  remark: string | null;
  created_at: string;
}

// ─── 儀表板 ───
export interface DashboardStats {
  total_projects: number;
  active_projects: number;
  total_budget_items: number;
  total_budget_amount: number;
  total_resources: number;
  recent_projects: Project[];
}

// ─── 資源單價分析細項 ───
export interface ResourceBreakdownItem {
  id: number;
  resource_id: number;
  code: string;
  c_name: string;
  c_unit: string;
  quantity: number;
  unit_price: number;
  amount: number;
  remark: string | null;
  created_at: string;
  updated_at: string;
}

// 延展的資源型別（含單價分析資料）
export interface ResourceWithAnalysis extends Resource {
  is_analysis: boolean;
  labor_rate: number;
  material_rate: number;
  equipment_rate: number;
  misc_rate: number;
  breakdown_items?: ResourceBreakdownItem[];
  breakdown_total?: number;
}

// ─── API 回呼 ───
export interface ApiResponse<T> {
  data?: T;
  error?: string;
}
