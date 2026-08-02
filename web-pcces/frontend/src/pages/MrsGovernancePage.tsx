import React, { useCallback, useEffect, useMemo, useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import api from '../services/api';

type ReleaseStatus = '' | 'DRAFT' | 'SUBMITTED' | 'RETURNED' | 'APPROVED' | 'PUBLISHED';
type CatalogRelease = {
  id: string;
  label: string;
  status: Exclude<ReleaseStatus, ''>;
  row_version: number;
  snapshot?: unknown[];
};
type GovernanceAudit = {
  id: string;
  event_type: string;
  resource_type: string;
  resource_id: string;
  actor_id: string;
  created_at: string;
};
type ExpiryAlert = { catalog_item_id: string; code: string; name: string; status: string; valid_to?: string | null };
type Page<T> = { items: T[]; total: number; limit: number; offset: number };

const PAGE_SIZE = 20;
const EMPTY_PAGE = <T,>(): Page<T> => ({ items: [], total: 0, limit: PAGE_SIZE, offset: 0 });

const MrsGovernancePage: React.FC = () => {
  const [params] = useSearchParams();
  const [releasePage, setReleasePage] = useState<Page<CatalogRelease>>(EMPTY_PAGE);
  const [auditPage, setAuditPage] = useState<Page<GovernanceAudit>>(EMPTY_PAGE);
  const [alerts, setAlerts] = useState<ExpiryAlert[]>([]);
  const [releaseStatus, setReleaseStatus] = useState<ReleaseStatus>('');
  const [releaseOffset, setReleaseOffset] = useState(0);
  const [auditOffset, setAuditOffset] = useState(0);
  const [auditResourceType, setAuditResourceType] = useState('');
  const [auditResourceId, setAuditResourceId] = useState('');
  const [auditEventType, setAuditEventType] = useState('');
  const [itemId, setItemId] = useState(params.get('item') || '');
  const [recipeId, setRecipeId] = useState(params.get('recipe') || '');
  const [versionId, setVersionId] = useState(params.get('version') || '');
  const [validTo, setValidTo] = useState('');
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState('');
  const headers = useMemo(() => ({ Authorization: `Bearer ${localStorage.getItem('token') || ''}` }), []);

  const loadReleases = useCallback(async () => {
    const response = await api.get<Page<CatalogRelease>>('/mrs/catalog-releases', {
      headers,
      params: { status: releaseStatus || undefined, limit: PAGE_SIZE, offset: releaseOffset },
    });
    setReleasePage(response.data);
  }, [headers, releaseOffset, releaseStatus]);

  const loadAudit = useCallback(async () => {
    const response = await api.get<Page<GovernanceAudit>>('/mrs/governance-audit', {
      headers,
      params: {
        resource_type: auditResourceType || undefined,
        resource_id: auditResourceId || undefined,
        event_type: auditEventType || undefined,
        limit: PAGE_SIZE,
        offset: auditOffset,
      },
    });
    setAuditPage(response.data);
  }, [auditEventType, auditOffset, auditResourceId, auditResourceType, headers]);

  const loadAlerts = useCallback(async () => {
    const response = await api.get<ExpiryAlert[]>('/mrs/expiry-alerts', { headers });
    setAlerts(response.data);
  }, [headers]);

  const run = useCallback(async (operation: () => Promise<void>) => {
    setBusy(true);
    setError('');
    try {
      await operation();
    } catch (cause) {
      const message = cause instanceof Error ? cause.message : 'MRS 治理操作失敗';
      setError(message);
    } finally {
      setBusy(false);
    }
  }, []);

  useEffect(() => { void run(loadReleases); }, [loadReleases, run]);
  useEffect(() => { void run(loadAudit); }, [loadAudit, run]);
  useEffect(() => { void run(loadAlerts); }, [loadAlerts, run]);

  const createRelease = () => run(async () => {
    await api.post('/mrs/catalog-releases', { label: `Catalog ${new Date().toLocaleString()}` }, { headers });
    setReleaseOffset(0);
    await Promise.all([loadReleases(), loadAudit()]);
  });
  const transition = (release: CatalogRelease, command: string) => run(async () => {
    await api.post(`/mrs/catalog-releases/${release.id}/${command}`, { row_version: release.row_version }, { headers });
    await Promise.all([loadReleases(), loadAudit()]);
  });
  const saveValidity = () => run(async () => {
    if (!itemId.trim()) throw new Error('請輸入 Catalog Item ID');
    const current = await api.get<{ row_version: number }>(`/mrs/catalog/${itemId.trim()}/validity`, { headers });
    await api.put(`/mrs/catalog/${itemId.trim()}/validity`, {
      valid_to: validTo || null,
      status: 'ACTIVE',
      row_version: current.data.row_version,
    }, { headers });
    await Promise.all([loadAlerts(), loadAudit()]);
  });
  const freezeRecipe = () => run(async () => {
    if (!recipeId.trim() || !versionId.trim()) throw new Error('請輸入 Recipe ID 與 Version ID');
    const current = await api.get<{ row_version: number }>(`/mrs/analysis-recipes/${recipeId.trim()}/freeze`, { headers });
    await api.put(`/mrs/analysis-recipes/${recipeId.trim()}/freeze`, {
      version_id: versionId.trim(),
      frozen: true,
      reason: '正式採用',
      row_version: current.data.row_version,
    }, { headers });
    await loadAudit();
  });

  const releaseCanNext = releasePage.offset + releasePage.items.length < releasePage.total;
  const auditCanNext = auditPage.offset + auditPage.items.length < auditPage.total;

  return <div className="p-6 space-y-6">
    <div><h1 className="text-2xl font-bold">MRS 治理中心</h1><p className="text-gray-500">Catalog 發布、價格有效期、配方凍結與可追溯稽核。</p></div>
    {error && <div role="alert" className="border border-red-300 bg-red-50 text-red-700 rounded p-3">{error}</div>}

    <section className="border rounded p-4 space-y-3">
      <div className="flex flex-wrap justify-between gap-3">
        <div><h2 className="font-semibold">Catalog 發布版次</h2><p className="text-sm text-gray-500">共 {releasePage.total} 筆</p></div>
        <div className="flex gap-2">
          <select aria-label="版次狀態" className="border px-3 py-2" value={releaseStatus} onChange={e => { setReleaseStatus(e.target.value as ReleaseStatus); setReleaseOffset(0); }}>
            <option value="">全部狀態</option><option>DRAFT</option><option>SUBMITTED</option><option>RETURNED</option><option>APPROVED</option><option>PUBLISHED</option>
          </select>
          <button disabled={busy} className="border px-3 py-2 disabled:opacity-50" onClick={createRelease}>建立版次</button>
        </div>
      </div>
      {releasePage.items.length === 0 && <p className="text-gray-500">目前沒有符合條件的版次。</p>}
      {releasePage.items.map(release => <div key={release.id} className="border rounded p-3 flex flex-wrap items-center gap-2">
        <div className="flex-1 min-w-64">{release.label}｜{release.status}｜{release.snapshot?.length ?? 0} 項</div>
        {release.status === 'DRAFT' && <button className="border px-2" disabled={busy} onClick={() => transition(release, 'SUBMIT')}>送審</button>}
        {release.status === 'SUBMITTED' && <><button className="border px-2" disabled={busy} onClick={() => transition(release, 'APPROVE')}>核准</button><button className="border px-2" disabled={busy} onClick={() => transition(release, 'RETURN')}>退回</button></>}
        {release.status === 'RETURNED' && <button className="border px-2" disabled={busy} onClick={() => transition(release, 'SUBMIT')}>重新送審</button>}
        {release.status === 'APPROVED' && <button className="border px-2" disabled={busy} onClick={() => transition(release, 'PUBLISH')}>發布</button>}
      </div>)}
      <div className="flex justify-end items-center gap-2">
        <button className="border px-3 py-1" disabled={releaseOffset === 0 || busy} onClick={() => setReleaseOffset(Math.max(0, releaseOffset - PAGE_SIZE))}>上一頁</button>
        <span className="text-sm">{releasePage.total === 0 ? 0 : releasePage.offset + 1}–{releasePage.offset + releasePage.items.length}</span>
        <button className="border px-3 py-1" disabled={!releaseCanNext || busy} onClick={() => setReleaseOffset(releaseOffset + PAGE_SIZE)}>下一頁</button>
      </div>
    </section>

    <section className="border rounded p-4 space-y-3">
      <h2 className="font-semibold">價格有效期</h2>
      <div className="flex flex-wrap gap-2"><input className="border p-2" value={itemId} onChange={e => setItemId(e.target.value)} placeholder="Catalog Item ID"/><input className="border p-2" type="date" value={validTo} onChange={e => setValidTo(e.target.value)}/><button disabled={busy} className="border px-3 disabled:opacity-50" onClick={saveValidity}>設定</button></div>
      {alerts.map(alert => <div key={alert.catalog_item_id} className="border rounded p-2">{alert.code} {alert.name}｜{alert.status}｜{alert.valid_to || '-'}</div>)}
    </section>

    <section className="border rounded p-4 space-y-3">
      <h2 className="font-semibold">配方引用凍結</h2>
      <div className="grid md:grid-cols-3 gap-2"><input className="border p-2" value={recipeId} onChange={e => setRecipeId(e.target.value)} placeholder="Recipe ID"/><input className="border p-2" value={versionId} onChange={e => setVersionId(e.target.value)} placeholder="Version ID"/><button disabled={busy} className="border px-3 disabled:opacity-50" onClick={freezeRecipe}>凍結採用</button></div>
    </section>

    <section className="border rounded p-4 space-y-3">
      <div><h2 className="font-semibold">治理審計</h2><p className="text-sm text-gray-500">共 {auditPage.total} 筆</p></div>
      <div className="grid md:grid-cols-3 gap-2">
        <input aria-label="資源類型" className="border p-2" value={auditResourceType} onChange={e => { setAuditResourceType(e.target.value); setAuditOffset(0); }} placeholder="Resource Type"/>
        <input aria-label="資源 ID" className="border p-2" value={auditResourceId} onChange={e => { setAuditResourceId(e.target.value); setAuditOffset(0); }} placeholder="Resource ID"/>
        <input aria-label="事件類型" className="border p-2" value={auditEventType} onChange={e => { setAuditEventType(e.target.value); setAuditOffset(0); }} placeholder="Event Type"/>
      </div>
      {auditPage.items.length === 0 && <p className="text-gray-500">目前沒有符合條件的稽核紀錄。</p>}
      {auditPage.items.map(event => <div className="border-b py-2" key={event.id}>{event.event_type}｜{event.resource_type}:{event.resource_id}｜{event.actor_id}｜{event.created_at}</div>)}
      <div className="flex justify-end items-center gap-2">
        <button className="border px-3 py-1" disabled={auditOffset === 0 || busy} onClick={() => setAuditOffset(Math.max(0, auditOffset - PAGE_SIZE))}>上一頁</button>
        <span className="text-sm">{auditPage.total === 0 ? 0 : auditPage.offset + 1}–{auditPage.offset + auditPage.items.length}</span>
        <button className="border px-3 py-1" disabled={!auditCanNext || busy} onClick={() => setAuditOffset(auditOffset + PAGE_SIZE)}>下一頁</button>
      </div>
    </section>
  </div>;
};

export default MrsGovernancePage;
