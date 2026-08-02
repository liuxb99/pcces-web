const API_BASE = import.meta.env.VITE_API_BASE_URL || "/api";

type Json = Record<string, unknown>;

async function call<T>(path: string, init?: RequestInit): Promise<T> {
  const token = localStorage.getItem("access_token");
  const response = await fetch(`${API_BASE}${path}`, {
    ...init,
    headers: {
      "Content-Type": "application/json",
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...(init?.headers || {}),
    },
  });
  if (!response.ok) {
    const error = await response.json().catch(() => ({ detail: response.statusText }));
    throw new Error(String(error.detail || error.code || response.statusText));
  }
  return response.json() as Promise<T>;
}

export type ContractItem = {
  id: string;
  source_budget_item_id: string;
  item_no?: string;
  name: string;
  unit?: string;
  quantity: string;
  unit_price: string;
  amount: string;
  deep_link: string;
};

export type ContractRecord = {
  id: string;
  project_code: string;
  budget_version_id: string;
  contract_no: string;
  name: string;
  status: string;
  contract_amount: string;
  row_version: number;
  items: ContractItem[];
  deep_link: string;
};

export const contractsApi = {
  eligibility: (projectCode: string, budgetVersionId: string) =>
    call<{ eligible: boolean; reasons: string[] }>(
      `/contracts/eligibility?project_code=${encodeURIComponent(projectCode)}&budget_version_id=${encodeURIComponent(budgetVersionId)}`,
    ),
  create: (body: Json) => call<ContractRecord>("/contracts", { method: "POST", body: JSON.stringify(body) }),
  get: (id: string) => call<ContractRecord>(`/contracts/${id}`),
  allocationBasis: (id: string) => call<Json>(`/contracts/${id}/allocation-basis`),
  addItems: (id: string, body: Json) => call<ContractRecord>(`/contracts/${id}/items`, { method: "POST", body: JSON.stringify(body) }),
  linkSubcontract: (parentId: string, childId: string) =>
    call<Json>(`/contracts/${parentId}/subcontracts/${childId}`, { method: "POST", body: "{}" }),
  createVersion: (id: string, rowVersion: number, note?: string) =>
    call<Json>(`/contracts/${id}/versions`, { method: "POST", body: JSON.stringify({ row_version: rowVersion, note }) }),
  transitionVersion: (versionId: string, status: string, rowVersion: number) =>
    call<Json>(`/contracts/versions/${versionId}/transition`, { method: "POST", body: JSON.stringify({ status, row_version: rowVersion }) }),
  createChangeCase: (id: string, body: Json) =>
    call<Json>(`/contracts/${id}/change-cases`, { method: "POST", body: JSON.stringify(body) }),
  transitionChangeCase: (caseId: string, status: string, rowVersion: number) =>
    call<Json>(`/contracts/change-cases/${caseId}/transition`, { method: "POST", body: JSON.stringify({ status, row_version: rowVersion }) }),
};

export const executionApi = {
  createInvoice: (contractId: string, body: Json) =>
    call<Json>(`/contracts/${contractId}/invoice-periods`, { method: "POST", body: JSON.stringify(body) }),
  transitionInvoice: (periodId: string, status: string, rowVersion: number) =>
    call<Json>(`/contracts/invoice-periods/${periodId}/transition`, { method: "POST", body: JSON.stringify({ status, row_version: rowVersion }) }),
  createSettlement: (contractId: string, body: Json) =>
    call<Json>(`/contracts/${contractId}/settlements`, { method: "POST", body: JSON.stringify(body) }),
  transitionSettlement: (id: string, status: string, rowVersion: number) =>
    call<Json>(`/contracts/settlements/${id}/transition`, { method: "POST", body: JSON.stringify({ status, row_version: rowVersion }) }),
  createAcceptance: (contractId: string, body: Json) =>
    call<Json>(`/contracts/${contractId}/acceptances`, { method: "POST", body: JSON.stringify(body) }),
  transitionAcceptance: (id: string, status: string, rowVersion: number) =>
    call<Json>(`/contracts/acceptances/${id}/transition`, { method: "POST", body: JSON.stringify({ status, row_version: rowVersion }) }),
};

export type ReportJob = {
  id: string;
  definition_code: string;
  project_code: string;
  business_version_id: string;
  format: string;
  status: string;
  progress: number;
  row_version: number;
  artifact?: { id: string; filename: string; sha256: string; download_url: string };
};

export const reportsApi = {
  definitions: () => call<Json[]>("/reports/definitions"),
  createJob: (body: Json) => call<ReportJob>("/reports/jobs", { method: "POST", body: JSON.stringify(body) }),
  getJob: (id: string) => call<ReportJob>(`/reports/jobs/${id}`),
  render: (id: string, rowVersion: number) =>
    call<ReportJob>(`/reports/jobs/${id}/render`, { method: "POST", body: JSON.stringify({ row_version: rowVersion }) }),
  downloadUrl: (artifactId: string) => `${API_BASE}/reports/artifacts/${artifactId}/download`,
};

export type TypedSetting = {
  key: string;
  category: string;
  value_type: string;
  value: unknown;
  default: unknown;
  constraints: Json;
  row_version: number;
};

export const adminApi = {
  settings: () => call<TypedSetting[]>("/admin/settings"),
  updateSetting: (key: string, value: unknown, rowVersion: number) =>
    call<TypedSetting>(`/admin/settings/${encodeURIComponent(key)}`, {
      method: "PUT",
      body: JSON.stringify({ value, row_version: rowVersion }),
    }),
  createGroup: (code: string, name: string) =>
    call<Json>("/admin/groups", { method: "POST", body: JSON.stringify({ code, name }) }),
  addGroupMember: (groupId: string, userId: string) =>
    call<Json>(`/admin/groups/${groupId}/members/${userId}`, { method: "PUT", body: "{}" }),
  createBackup: () => call<Json>("/admin/backups", { method: "POST", body: "{}" }),
  health: () => call<Json>("/admin/health"),
};
