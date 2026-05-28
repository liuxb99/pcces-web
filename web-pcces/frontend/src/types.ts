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

// ─── 計價（Invoice） ───
export interface Invoice {
  id: number;
  project_id: number;
  period_no: number;
  invoice_no: string;
  c_name?: string;
  status: 'draft' | 'submitted' | 'approved';
  description?: string;
  total_amount: number;
  cumulative_amount: number;
  progress_rate: number;
  invoice_date?: string;
  remark?: string;
  created_by?: number;
  created_at: string;
  updated_at: string;
  item_count?: number;
}

export interface InvoiceItem {
  id: number;
  invoice_id: number;
  budget_item_id?: number;
  item_no?: string;
  print_no?: string;
  c_name?: string;
  c_unit?: string;
  contract_qty: number;
  unit_price: number;
  prev_completed_qty: number;
  this_completed_qty: number;
  total_completed_qty: number;
  remain_qty: number;
  this_amount: number;
  cumulative_amount: number;
  progress_rate: number;
  sort_order?: string;
  remark?: string;
}

export interface CreateInvoiceRequest {
  period_no?: number;
  invoice_no?: string;
  c_name?: string;
  description?: string;
  invoice_date?: string;
  remark?: string;
}

export interface CreateInvoiceItemRequest {
  budget_item_id?: number;
  c_name?: string;
  c_unit?: string;
  contract_qty?: number;
  unit_price?: number;
  prev_completed_qty?: number;
  this_completed_qty?: number;
  remark?: string;
}

export interface BatchCreateItemsRequest {
  budget_item_ids?: number[];
  include_all_leaf?: boolean;
}

// ─── 分包合約 ───

export interface Contract {
  id: number;
  project_id: number;
  contract_no: string;
  c_name: string;
  contractor: string | null;
  contract_amount: number;
  total_paid_amount: number;
  total_issue_amount: number;
  settlement_amount: number;
  status: string;          // draft / active / closed / finalized
  start_date: string | null;
  end_date: string | null;
  remark: string | null;
  created_at: string;
  updated_at: string;
  item_count?: number;
}

export interface ContractCreateData {
  contract_no?: string;
  c_name: string;
  contractor?: string;
  contract_amount?: number;
  total_paid_amount?: number;
  total_issue_amount?: number;
  settlement_amount?: number;
  status?: string;
  start_date?: string;
  end_date?: string;
  remark?: string;
}

export interface ContractItem {
  id: number;
  contract_id: number;
  budget_item_id: number | null;
  item_no: string | null;
  print_no: string | null;
  c_name: string | null;
  c_unit: string | null;
  contract_qty: number;
  unit_price: number;
  amount: number;
  completed_qty: number;
  completed_amount: number;
  remark: string | null;
  sort_order: number;
}

export interface ContractItemCreateData {
  budget_item_id?: number | null;
  item_no?: string;
  print_no?: string;
  c_name?: string;
  c_unit?: string;
  contract_qty?: number;
  unit_price?: number;
  completed_qty?: number;
  completed_amount?: number;
  remark?: string;
  sort_order?: number;
}

// ─── 期別計價 ───

export interface ContractIssue {
  id: number;
  contract_id: number;
  issue_no: number;
  c_name: string | null;
  status: string;          // draft / submitted / approved
  total_amount: number;
  cumulative_amount: number;
  progress_rate: number;
  remark: string | null;
  issue_date: string | null;
  created_by: number | null;
  created_at: string;
  updated_at: string;
  item_count?: number;
}

export interface ContractIssueCreateData {
  c_name?: string;
  remark?: string;
  issue_date?: string;
}

export interface ContractIssueItem {
  id: number;
  issue_id: number;
  contract_item_id: number | null;
  c_name: string | null;
  c_unit: string | null;
  contract_qty: number;
  unit_price: number;
  prev_completed_qty: number;
  this_completed_qty: number;
  total_completed_qty: number;
  remain_qty: number;
  this_amount: number;
  cumulative_amount: number;
  progress_rate: number;
  remark: string | null;
  created_at: string;
  updated_at: string;
}

export interface ContractIssueItemCreateData {
  contract_item_id?: number;
  c_name?: string;
  c_unit?: string;
  contract_qty?: number;
  unit_price?: number;
  prev_completed_qty?: number;
  this_completed_qty?: number;
  remark?: string;
}

// ─── 結算 ───

export interface ContractSettlement {
  id: number;
  contract_id: number;
  settlement_no: string;
  c_name: string | null;
  settlement_date: string | null;
  contract_amount: number;
  total_add_amount: number;
  total_deduct_amount: number;
  settlement_amount: number;
  remark: string | null;
  status: string;          // draft / submitted / approved
  created_by: number | null;
  created_at: string;
  updated_at: string;
  item_count?: number;
}

export interface ContractSettlementCreateData {
  settlement_no?: string;
  c_name?: string;
  settlement_date?: string;
  remark?: string;
}

export interface ContractSettlementItem {
  id: number;
  settlement_id: number;
  budget_item_id: number | null;
  c_name: string | null;
  c_unit: string | null;
  contract_qty: number;
  contract_unit_price: number;
  contract_amount: number;
  actual_qty: number;
  actual_unit_price: number;
  actual_amount: number;
  diff_amount: number;
  remark: string | null;
  created_at: string;
  updated_at: string;
}

export interface ContractSettlementItemCreateData {
  budget_item_id?: number;
  c_name?: string;
  c_unit?: string;
  contract_qty?: number;
  contract_unit_price?: number;
  actual_qty?: number;
  actual_unit_price?: number;
  remark?: string;
}

// ─── 終驗 ───

export interface ContractFinalAcceptance {
  id: number;
  contract_id: number;
  acceptance_no: string;
  c_name: string | null;
  acceptance_date: string | null;
  inspector: string | null;
  result: string | null;       // pass / conditional_pass / fail
  defect_description: string | null;
  remark: string | null;
  status: string;              // draft / submitted / approved
  created_by: number | null;
  created_at: string;
  updated_at: string;
  item_count?: number;
}

export interface ContractFinalAcceptanceCreateData {
  acceptance_no?: string;
  c_name?: string;
  acceptance_date?: string;
  inspector?: string;
  result?: string;
  defect_description?: string;
  remark?: string;
}

export interface ContractFinalAcceptanceItem {
  id: number;
  acceptance_id: number;
  budget_item_id: number | null;
  c_name: string | null;
  c_unit: string | null;
  contract_qty: number;
  actual_qty: number;
  accepted_qty: number;
  rejected_qty: number;
  remark: string | null;
  created_at: string;
  updated_at: string;
}

export interface ContractFinalAcceptanceItemCreateData {
  budget_item_id?: number;
  c_name?: string;
  c_unit?: string;
  contract_qty?: number;
  actual_qty?: number;
  accepted_qty?: number;
  rejected_qty?: number;
  remark?: string;
}

// ─── MrsBase 公共單價庫 ───

export interface MrsBaseCategory {
  id: number;
  parent_id: number | null;
  code: string;
  c_name: string;
  sort_order: number;
  level_no: number;
  created_at: string;
  updated_at: string;
  children: MrsBaseCategory[];
  item_count?: number;
}

export interface MrsBaseCategoryCreateData {
  parent_id?: number | null;
  code?: string;
  c_name: string;
  sort_order?: number;
}

export interface MrsBaseItem {
  id: number;
  category_id: number;
  code: string;
  pub_code: string | null;
  c_name: string;
  e_name: string | null;
  c_unit: string;
  e_unit: string | null;
  unit_price: number;
  cost_kind: string;         // 1=工, 2=料, 3=機, 4=雜
  item_type: string;          // B/L/W…
  is_analysis: boolean;
  labor_rate: number;
  material_rate: number;
  equipment_rate: number;
  misc_rate: number;
  decimal_qty: number;
  decimal_price: number;
  decimal_amount: number;
  memo: string | null;
  is_approved: boolean;
  approved_by: number | null;
  approved_at: string | null;
  created_by: number;
  created_at: string;
  updated_at: string;
  breakdown_items?: MrsBaseBreakdownItem[];
  breakdown_total?: number;
}

export interface MrsBaseItemCreateData {
  category_id: number;
  code: string;
  pub_code?: string;
  c_name: string;
  e_name?: string;
  c_unit?: string;
  e_unit?: string;
  unit_price?: number;
  cost_kind?: string;
  item_type?: string;
  is_analysis?: boolean;
  labor_rate?: number;
  material_rate?: number;
  equipment_rate?: number;
  misc_rate?: number;
  decimal_qty?: number;
  decimal_price?: number;
  decimal_amount?: number;
  memo?: string;
}

export interface MrsBaseItemUpdateData {
  category_id?: number;
  code?: string;
  pub_code?: string;
  c_name?: string;
  e_name?: string;
  c_unit?: string;
  e_unit?: string;
  unit_price?: number;
  cost_kind?: string;
  item_type?: string;
  is_analysis?: boolean;
  labor_rate?: number;
  material_rate?: number;
  equipment_rate?: number;
  misc_rate?: number;
  decimal_qty?: number;
  decimal_price?: number;
  decimal_amount?: number;
  memo?: string;
}

export interface MrsBaseBreakdownItem {
  id: number;
  item_id: number;
  code: string;
  c_name: string;
  c_unit: string;
  quantity: number;
  unit_price: number;
  amount: number;
  category: string;           // labor/material/equipment/misc
  remark: string | null;
  created_at: string;
  updated_at: string;
}

export interface MrsBaseBreakdownCreateData {
  code?: string;
  c_name: string;
  c_unit?: string;
  quantity?: number;
  unit_price?: number;
  category?: string;
  remark?: string;
}

export interface MrsBaseBookmark {
  id: number;
  user_id: number;
  item_id: number;
  created_at: string;
  item?: MrsBaseItem;
}

export interface PaginatedMrsBaseItems {
  items: MrsBaseItem[];
  total: number;
  page: number;
  per_page: number;
}

// ═══════════════════════════════════════════════
// 系統維護（Admin）
// ═══════════════════════════════════════════════

// ─── 使用者管理擴充 ───
export interface UserCreateData {
  username: string;
  password: string;
  display_name?: string;
  email?: string;
  company?: string;
  department?: string;
  phone?: string;
  role?: string;
  is_active?: boolean;
}

export interface UserUpdateData {
  display_name?: string;
  email?: string;
  company?: string;
  department?: string;
  phone?: string;
  role?: string;
  is_active?: boolean;
  password?: string;
}

// ─── 系統參數 ───
export interface SystemParameter {
  id: number;
  category: string;
  code: string;
  c_name: string;
  c_value: string | null;
  c_default: string | null;
  sort_order: number;
  is_active: boolean;
  memo: string | null;
  created_at: string;
  updated_at: string;
}

export interface SystemParamCreateData {
  category: string;
  code: string;
  c_name?: string;
  c_value?: string;
  c_default?: string;
  sort_order?: number;
  is_active?: boolean;
  memo?: string;
}

// ─── 代碼表 ───
export interface CodeTable {
  id: number;
  table_code: string;
  table_name: string;
  memo: string | null;
  is_active: boolean;
  created_at: string;
  updated_at: string;
}

export interface CodeItem {
  id: number;
  table_id: number;
  parent_id: number | null;
  code: string;
  c_name: string;
  sort_order: number;
  is_active: boolean;
  ext_data: Record<string, any> | null;
  memo: string | null;
  created_at: string;
  updated_at: string;
  children?: CodeItem[];
}

export interface CodeItemCreateData {
  parent_id?: number | null;
  code: string;
  c_name: string;
  sort_order?: number;
  is_active?: boolean;
  ext_data?: Record<string, any>;
  memo?: string;
}

// ─── 組織機構 ───
export interface Organization {
  id: number;
  parent_id: number | null;
  code: string;
  c_name: string;
  org_type: string;
  sort_order: number;
  is_active: boolean;
  contact_person: string | null;
  contact_phone: string | null;
  address: string | null;
  memo: string | null;
  created_at: string;
  updated_at: string;
  children?: Organization[];
}

export interface OrganizationCreateData {
  parent_id?: number | null;
  code: string;
  c_name: string;
  org_type?: string;
  sort_order?: number;
  is_active?: boolean;
  contact_person?: string;
  contact_phone?: string;
  address?: string;
  memo?: string;
}

// ═══════════════════════════════════════════════
// 比較分析
// ═══════════════════════════════════════════════

export interface CompareDiffValue {
  quantity: number;
  unit_price: number;
  amount: number;
}

export interface CompareDiffPct {
  quantity: number | null;
  unit_price: number | null;
  amount: number | null;
}

export interface CompareItem {
  key: string;
  c_name: string;
  c_unit: string;
  a: CompareDiffValue;
  b: CompareDiffValue;
  diff: CompareDiffValue;
  diff_pct: CompareDiffPct;
  status: 'added' | 'removed' | 'modified' | 'unchanged';
}

export interface CompareSummary {
  total_a: number;
  total_b: number;
  diff: number;
  diff_pct: number | null;
  added_count: number;
  removed_count: number;
  modified_count: number;
  unchanged_count: number;
}

export interface CompareResult {
  project_a: { id: number; name: string };
  project_b: { id: number; name: string };
  items: CompareItem[];
  summary: CompareSummary;
}

export interface CompareRequest {
  project_a_id: number;
  project_b_id: number;
  scope?: 'leaf' | 'all';
}

export interface MrsBasePriceCompareSummary {
  total: number;
  avg_price: number;
  max_price: number;
  min_price: number;
}

export interface MrsBasePriceCompareResult {
  items: MrsBaseItem[];
  summary: MrsBasePriceCompareSummary;
}

export interface MrsBasePriceCompareRequest {
  category_id?: number;
  item_ids?: number[];
  compare_type?: 'all' | 'changed_only';
}

// ─── API 回呼 ───
export interface ApiResponse<T> {
  data?: T;
  error?: string;
}

// ═══════════════════════════════════════════════
// 功能開關（Feature Flag）
// ═══════════════════════════════════════════════

export interface FeatureFlag {
  id: number;
  flag_key: string;
  display_name: string;
  description: string | null;
  category: string;
  is_enabled: boolean;
  is_system: boolean;
  sort_order: number;
  created_at: string;
  updated_at: string;
}

export interface FeatureFlagCreateData {
  flag_key: string;
  display_name: string;
  description?: string;
  category?: string;
  is_enabled?: boolean;
  is_system?: boolean;
  sort_order?: number;
}

export interface FeatureFlagUpdateData {
  display_name?: string;
  description?: string;
  category?: string;
  is_enabled?: boolean;
  sort_order?: number;
}

// ═══════════════════════════════════════════════
// 版本資訊
// ═══════════════════════════════════════════════

export interface VersionInfo {
  app_name: string;
  app_version: string;
  build_date: string;
  repo_url: string;
  release_notes_url: string;
  changelog: ChangelogEntry[];
  dependencies: Record<string, Record<string, string>>;
}

export interface ChangelogEntry {
  version: string;
  date: string;
  changes: string[];
}

export interface HealthStatus {
  status: 'healthy' | 'degraded' | 'down';
  database: 'connected' | 'disconnected';
  uptime_seconds: number;
  timestamp: string;
}
