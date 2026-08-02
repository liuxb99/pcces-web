export interface CostStructureRunVersion {
  id: string;
  project_code: string;
  run_id: string;
  budget_version_id: string;
  budget_status: string;
  direct_cost: string;
  total_cost: string;
  trace: Record<string, unknown>;
  created_by: string;
  created_at: string;
  row_version: number;
  deep_link: string;
}

export interface CostStructureRunComparison {
  project_code: string;
  left: string;
  right: string;
  left_budget_version_id: string;
  right_budget_version_id: string;
  direct_cost_delta: string;
  total_cost_delta: string;
  deep_link: string;
}

function headers(): HeadersInit {
  const token = localStorage.getItem('pcces_token');
  return token ? { Authorization: `Bearer ${token}` } : {};
}

async function readJson<T>(response: Response): Promise<T> {
  const payload = await response.json();
  if (!response.ok) {
    throw new Error(payload.detail || payload.code || `HTTP ${response.status}`);
  }
  return payload as T;
}

export const costStructureVersionApi = {
  get: async (runId: string): Promise<CostStructureRunVersion> => {
    const response = await fetch(`/api/cost-structures/runs/${encodeURIComponent(runId)}/budget-version`, {
      headers: headers(),
    });
    return readJson<CostStructureRunVersion>(response);
  },

  compare: async (leftRunId: string, rightRunId: string): Promise<CostStructureRunComparison> => {
    const query = new URLSearchParams({ left: leftRunId, right: rightRunId });
    const response = await fetch(`/api/cost-structures/runs/compare?${query.toString()}`, {
      headers: headers(),
    });
    return readJson<CostStructureRunComparison>(response);
  },
};
