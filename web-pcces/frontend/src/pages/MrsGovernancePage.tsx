import React, { useEffect, useMemo, useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import api from '../services/api';

const MrsGovernancePage: React.FC = () => {
  const [params] = useSearchParams();
  const [releases, setReleases] = useState<any[]>([]);
  const [alerts, setAlerts] = useState<any[]>([]);
  const [itemId, setItemId] = useState(params.get('item') || '');
  const [recipeId, setRecipeId] = useState(params.get('recipe') || '');
  const [versionId, setVersionId] = useState(params.get('version') || '');
  const [validTo, setValidTo] = useState('');
  const [audit, setAudit] = useState<any[]>([]);
  const headers = useMemo(() => ({ Authorization: `Bearer ${localStorage.getItem('token') || ''}` }), []);

  const load = async () => {
    const [r, a, g] = await Promise.all([
      api.get('/mrs/catalog-releases', { headers }),
      api.get('/mrs/expiry-alerts', { headers }),
      api.get('/mrs/governance-audit', { headers }),
    ]);
    setReleases(r.data); setAlerts(a.data); setAudit(g.data);
  };
  useEffect(() => { load(); }, []);

  const createRelease = async () => {
    await api.post('/mrs/catalog-releases', { label: `Catalog ${new Date().toLocaleString()}` }, { headers });
    await load();
  };
  const transition = async (release: any, command: string) => {
    await api.post(`/mrs/catalog-releases/${release.id}/${command}`, { row_version: release.row_version }, { headers });
    await load();
  };
  const saveValidity = async () => {
    await api.put(`/mrs/catalog/${itemId}/validity`, { valid_to: validTo, status: 'ACTIVE', row_version: 0 }, { headers });
    await load();
  };
  const freezeRecipe = async () => {
    await api.put(`/mrs/analysis-recipes/${recipeId}/freeze`, { version_id: versionId, frozen: true, reason: '正式採用', row_version: 0 }, { headers });
  };

  return <div className="p-6 space-y-6">
    <div><h1 className="text-2xl font-bold">MRS 治理中心</h1><p className="text-gray-500">Catalog 發布、價格有效期、配方凍結與稽核。</p></div>
    <section className="border rounded p-4 space-y-3">
      <div className="flex justify-between"><h2 className="font-semibold">Catalog 發布版次</h2><button className="border px-3 py-2" onClick={createRelease}>建立版次</button></div>
      {releases.map(r => <div key={r.id} className="border rounded p-3 flex items-center gap-2">
        <div className="flex-1">{r.label}｜{r.status}｜{r.snapshot?.length ?? 0} 項</div>
        {r.status === 'DRAFT' && <button className="border px-2" onClick={()=>transition(r,'SUBMIT')}>送審</button>}
        {r.status === 'SUBMITTED' && <><button className="border px-2" onClick={()=>transition(r,'APPROVE')}>核准</button><button className="border px-2" onClick={()=>transition(r,'RETURN')}>退回</button></>}
        {r.status === 'APPROVED' && <button className="border px-2" onClick={()=>transition(r,'PUBLISH')}>發布</button>}
      </div>)}
    </section>
    <section className="border rounded p-4 space-y-3">
      <h2 className="font-semibold">價格有效期</h2>
      <div className="flex gap-2"><input className="border p-2" value={itemId} onChange={e=>setItemId(e.target.value)} placeholder="Catalog Item ID"/><input className="border p-2" type="date" value={validTo} onChange={e=>setValidTo(e.target.value)}/><button className="border px-3" onClick={saveValidity}>設定</button></div>
      {alerts.map(a => <div key={a.catalog_item_id} className="border rounded p-2">{a.code} {a.name}｜{a.status}｜{a.valid_to || '-'}</div>)}
    </section>
    <section className="border rounded p-4 space-y-3">
      <h2 className="font-semibold">配方引用凍結</h2>
      <div className="grid grid-cols-3 gap-2"><input className="border p-2" value={recipeId} onChange={e=>setRecipeId(e.target.value)} placeholder="Recipe ID"/><input className="border p-2" value={versionId} onChange={e=>setVersionId(e.target.value)} placeholder="Version ID"/><button className="border px-3" onClick={freezeRecipe}>凍結採用</button></div>
    </section>
    <section className="border rounded p-4"><h2 className="font-semibold mb-3">治理審計</h2>{audit.slice(0,20).map(e=><div className="border-b py-2" key={e.id}>{e.event_type}｜{e.resource_type}:{e.resource_id}｜{e.created_at}</div>)}</section>
  </div>;
};
export default MrsGovernancePage;
