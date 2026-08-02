import React, { useEffect, useMemo, useState } from 'react';
import { Link, useParams, useSearchParams } from 'react-router-dom';

interface GraphData {
  project_code: string;
  nodes: Array<{ id: string; type: string }>;
  edges: Array<{ from: string; to: string; type: string }>;
  price_history: Array<Record<string, any>>;
  runs: Array<Record<string, any>>;
}

const TraceabilityPage: React.FC = () => {
  const { projectCode = '' } = useParams();
  const [params] = useSearchParams();
  const [data, setData] = useState<GraphData | null>(null);
  const [error, setError] = useState('');
  const selected = useMemo(() => ({
    resource: params.get('resource'), history: params.get('history'),
    run: params.get('run'), trace: params.get('trace'),
  }), [params]);

  useEffect(() => {
    const token = localStorage.getItem('pcces_token');
    fetch(`/api/dependency-graph/projects/${encodeURIComponent(projectCode)}`, {
      headers: token ? { Authorization: `Bearer ${token}` } : {},
    })
      .then(async response => {
        if (!response.ok) throw new Error((await response.json()).detail || `HTTP ${response.status}`);
        return response.json();
      })
      .then(setData)
      .catch(err => setError(err.message));
  }, [projectCode]);

  if (error) return <div className="p-6"><h1>追溯資料載入失敗</h1><p>{error}</p></div>;
  if (!data) return <div className="p-6">載入追溯資料中…</div>;

  const selectedHistory = data.price_history.find(row => row.id === selected.history);
  const selectedRun = data.runs.find(row => row.id === selected.run);
  const resourceEdges = selected.resource ? data.edges.filter(edge => edge.from === selected.resource) : data.edges;

  return (
    <div className="p-6 space-y-6">
      <div>
        <h1 className="text-2xl font-bold">計算與價格追溯</h1>
        <p>專案：{data.project_code}</p>
      </div>

      {(selected.resource || selected.history || selected.run || selected.trace) && (
        <section className="rounded border p-4">
          <h2 className="font-semibold">深連結定位</h2>
          {selected.resource && <p>資源：{selected.resource}</p>}
          {selected.trace && <p>Calculation Trace：<Link className="underline" to={`/app/projects/by-code/${projectCode}/traceability?trace=${selected.trace}`}>{selected.trace}</Link></p>}
          {selectedHistory && <pre className="overflow-auto text-sm">{JSON.stringify(selectedHistory, null, 2)}</pre>}
          {selectedRun && <pre className="overflow-auto text-sm">{JSON.stringify(selectedRun, null, 2)}</pre>}
        </section>
      )}

      <section className="rounded border p-4">
        <h2 className="font-semibold">依賴關係</h2>
        <p>節點 {data.nodes.length}，連線 {data.edges.length}</p>
        <ul className="mt-2 space-y-1">
          {resourceEdges.map((edge, index) => (
            <li key={`${edge.from}-${edge.to}-${index}`}>
              <Link className="underline" to={`?resource=${encodeURIComponent(edge.from)}`}>{edge.from}</Link>
              {' → '}{edge.to}（{edge.type}）
            </li>
          ))}
        </ul>
      </section>

      <section className="rounded border p-4">
        <h2 className="font-semibold">資源價格歷史</h2>
        <ul className="mt-2 space-y-1">
          {data.price_history.map(row => (
            <li key={row.id} className={row.id === selected.history ? 'font-bold' : ''}>
              <Link className="underline" to={`?resource=${encodeURIComponent(row.resource_id)}&history=${row.id}`}>{row.old_price} → {row.new_price}</Link>
              {' '}{row.source} {row.created_at}
            </li>
          ))}
        </ul>
      </section>

      <section className="rounded border p-4">
        <h2 className="font-semibold">依賴圖重算批次</h2>
        <ul className="mt-2 space-y-1">
          {data.runs.map(row => (
            <li key={row.id} className={row.id === selected.run ? 'font-bold' : ''}>
              <Link className="underline" to={`?run=${row.id}`}>{row.scope}／{row.status}</Link>
              {' '}{row.created_at}
            </li>
          ))}
        </ul>
      </section>
    </div>
  );
};

export default TraceabilityPage;
