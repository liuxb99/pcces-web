import React, { useEffect, useState } from 'react';
import { useSearchParams } from 'react-router-dom';

const headers = () => ({
  'Content-Type': 'application/json',
  Authorization: `Bearer ${localStorage.getItem('token') || ''}`,
});

const MrsInsightsPage: React.FC = () => {
  const [params] = useSearchParams();
  const [itemId, setItemId] = useState(params.get('item') || '');
  const [recipeId, setRecipeId] = useState(params.get('recipe') || '');
  const [vendor, setVendor] = useState('');
  const [price, setPrice] = useState('');
  const [summary, setSummary] = useState<any>(null);
  const [comparison, setComparison] = useState<any>(null);
  const [snapshots, setSnapshots] = useState<any[]>([]);
  const [impact, setImpact] = useState<any>(null);
  const [message, setMessage] = useState('');

  const loadSummary = async () => {
    const response = await fetch('/api/mrs/summary', { headers: headers() });
    if (response.ok) setSummary(await response.json());
  };

  const loadQuotes = async () => {
    if (!itemId) return;
    const response = await fetch(`/api/mrs/catalog/${encodeURIComponent(itemId)}/quote-comparison`, { headers: headers() });
    if (response.ok) setComparison(await response.json());
  };

  const loadSnapshots = async () => {
    if (!recipeId) return;
    const response = await fetch(`/api/mrs/analysis-recipes/${encodeURIComponent(recipeId)}/snapshots`, { headers: headers() });
    if (response.ok) setSnapshots(await response.json());
  };

  useEffect(() => { loadSummary(); }, []);
  useEffect(() => { loadQuotes(); }, [itemId]);
  useEffect(() => { loadSnapshots(); }, [recipeId]);

  const addQuote = async () => {
    const response = await fetch(`/api/mrs/catalog/${encodeURIComponent(itemId)}/quotes`, {
      method: 'POST', headers: headers(), body: JSON.stringify({ vendor, quoted_price: price, price_scale: 2 }),
    });
    setMessage(response.ok ? '報價已加入' : '報價加入失敗');
    if (response.ok) { setVendor(''); setPrice(''); loadQuotes(); loadSummary(); }
  };

  const createSnapshot = async () => {
    const response = await fetch(`/api/mrs/analysis-recipes/${encodeURIComponent(recipeId)}/snapshots`, {
      method: 'POST', headers: headers(), body: '{}',
    });
    setMessage(response.ok ? '分析快照已建立' : '快照建立失敗');
    if (response.ok) loadSnapshots();
  };

  const calculateImpact = async () => {
    const response = await fetch(`/api/mrs/catalog/${encodeURIComponent(itemId)}/impact`, {
      method: 'POST', headers: headers(), body: JSON.stringify({ new_price: price || undefined }),
    });
    if (response.ok) { setImpact(await response.json()); setMessage('影響分析完成'); }
    else setMessage('影響分析失敗');
  };

  return (
    <div style={{ padding: 24 }}>
      <h1>MRS 價格情報與影響分析</h1>
      {message && <p>{message}</p>}

      <section>
        <h2>Catalog 彙總</h2>
        <pre>{summary ? JSON.stringify(summary, null, 2) : '載入中...'}</pre>
      </section>

      <section>
        <h2>供應商報價比較</h2>
        <input value={itemId} onChange={e => setItemId(e.target.value)} placeholder="Catalog Item ID" />
        <input value={vendor} onChange={e => setVendor(e.target.value)} placeholder="供應商" />
        <input value={price} onChange={e => setPrice(e.target.value)} placeholder="報價／新價格" />
        <button onClick={addQuote} disabled={!itemId || !vendor || !price}>加入報價</button>
        <button onClick={calculateImpact} disabled={!itemId}>分析價格影響</button>
        <pre>{comparison ? JSON.stringify(comparison, null, 2) : '尚無比較資料'}</pre>
      </section>

      <section>
        <h2>單價分析快照</h2>
        <input value={recipeId} onChange={e => setRecipeId(e.target.value)} placeholder="Recipe ID" />
        <button onClick={createSnapshot} disabled={!recipeId}>建立快照</button>
        <pre>{JSON.stringify(snapshots, null, 2)}</pre>
      </section>

      <section>
        <h2>價格異動影響</h2>
        <pre>{impact ? JSON.stringify(impact, null, 2) : '尚未執行'}</pre>
      </section>
    </div>
  );
};

export default MrsInsightsPage;
