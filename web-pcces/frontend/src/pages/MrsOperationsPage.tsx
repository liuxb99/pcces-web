import React, { useEffect, useMemo, useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import api from '../services/api';

const MrsOperationsPage: React.FC = () => {
  const [params] = useSearchParams();
  const [usage, setUsage] = useState<any>(null);
  const [recipeId, setRecipeId] = useState(params.get('recipe') || '');
  const [itemId, setItemId] = useState(params.get('item') || '');
  const [versions, setVersions] = useState<any[]>([]);
  const [lineage, setLineage] = useState<any>(null);
  const [payload, setPayload] = useState('[{"id":"M-NEW","code":"M-NEW","name":"新材料","category":"MATERIAL","current_price":"0","price_scale":2}]');
  const [job, setJob] = useState<any>(null);
  const headers = useMemo(() => ({ Authorization: `Bearer ${localStorage.getItem('token') || ''}` }), []);

  const loadUsage = async () => setUsage((await api.get('/mrs/usage-summary', { headers })).data);
  const loadVersions = async () => { if (recipeId) setVersions((await api.get(`/mrs/analysis-recipes/${recipeId}/versions`, { headers })).data); };
  const loadLineage = async () => { if (itemId) setLineage((await api.get(`/mrs/catalog/${itemId}/lineage`, { headers })).data); };
  useEffect(() => { loadUsage(); }, []);
  useEffect(() => { if (params.get('recipe')) loadVersions(); if (params.get('item')) loadLineage(); }, []);

  const createVersion = async () => {
    await api.post(`/mrs/analysis-recipes/${recipeId}/versions`, { label: `版次 ${new Date().toLocaleString()}` }, { headers });
    await loadVersions();
  };
  const createJob = async () => {
    const created = (await api.post('/mrs/import-jobs', { format: 'json', payload, overwrite: false }, { headers })).data;
    setJob(created);
  };
  const runJob = async () => setJob((await api.post(`/mrs/import-jobs/${job.id}/run`, {}, { headers })).data);
  const cancelJob = async () => setJob((await api.post(`/mrs/import-jobs/${job.id}/cancel`, {}, { headers })).data);

  return <div className="p-6 space-y-6">
    <div><h1 className="text-2xl font-bold">MRS 作業中心</h1><p className="text-gray-500">工料機用量、配方版次、價格來源與批次匯入。</p></div>
    <section className="rounded border p-4">
      <h2 className="font-semibold mb-3">工料機用量彙總</h2>
      <div className="grid grid-cols-3 gap-3 mb-3"><div>工料機：{usage?.catalog_items ?? 0}</div><div>引用：{usage?.recipe_links ?? 0}</div><div>估算金額：{usage?.estimated_amount ?? '0.00'}</div></div>
      <div className="space-y-2">{usage?.items?.map((x:any)=><div key={x.catalog_item_id} className="border rounded p-2">{x.code} {x.name}｜數量 {x.total_quantity}｜估算 {x.estimated_amount}</div>)}</div>
    </section>
    <section className="rounded border p-4 space-y-3">
      <h2 className="font-semibold">配方版次</h2>
      <div className="flex gap-2"><input className="border p-2 flex-1" value={recipeId} onChange={e=>setRecipeId(e.target.value)} placeholder="Recipe ID"/><button className="border px-3" onClick={loadVersions}>查詢</button><button className="border px-3" onClick={createVersion}>建立版次</button></div>
      {versions.map(v=><div className="border rounded p-2" key={v.id}>{v.label}｜{v.unit_price}｜{v.created_at}</div>)}
    </section>
    <section className="rounded border p-4 space-y-3">
      <h2 className="font-semibold">價格來源 Lineage</h2>
      <div className="flex gap-2"><input className="border p-2 flex-1" value={itemId} onChange={e=>setItemId(e.target.value)} placeholder="Catalog Item ID"/><button className="border px-3" onClick={loadLineage}>查詢</button></div>
      {lineage?.events?.map((e:any)=><div className="border rounded p-2" key={e.id}>{e.type}｜{e.vendor || e.source || '-'}｜{e.price || e.new_price}</div>)}
    </section>
    <section className="rounded border p-4 space-y-3">
      <h2 className="font-semibold">批次匯入工作</h2>
      <textarea className="border p-2 w-full h-32" value={payload} onChange={e=>setPayload(e.target.value)}/>
      <div className="flex gap-2"><button className="border px-3 py-2" onClick={createJob}>建立工作</button><button className="border px-3 py-2" disabled={!job || job.status !== 'PENDING'} onClick={runJob}>執行</button><button className="border px-3 py-2" disabled={!job || !['PENDING','RUNNING'].includes(job.status)} onClick={cancelJob}>取消</button></div>
      {job && <pre className="bg-gray-50 p-3 overflow-auto">{JSON.stringify(job, null, 2)}</pre>}
    </section>
  </div>;
};
export default MrsOperationsPage;
