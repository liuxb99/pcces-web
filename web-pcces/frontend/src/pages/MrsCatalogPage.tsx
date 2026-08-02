import React, { useEffect, useState } from 'react';

const api = async (path: string, options?: RequestInit) => {
  const token = localStorage.getItem('token');
  const response = await fetch(path, {
    ...options,
    headers: { 'Content-Type': 'application/json', Authorization: `Bearer ${token}`, ...(options?.headers || {}) },
  });
  if (!response.ok) throw new Error(await response.text());
  return response.json();
};

const MrsCatalogPage: React.FC = () => {
  const [items, setItems] = useState<any[]>([]);
  const [query, setQuery] = useState('');
  const [form, setForm] = useState({ id: '', code: '', name: '', category: 'MATERIAL', unit: '', current_price: '0.00', price_scale: 2 });
  const [history, setHistory] = useState<any[]>([]);
  const [recipe, setRecipe] = useState<any>(null);
  const load = async () => setItems(await api(`/api/mrs/catalog?q=${encodeURIComponent(query)}`));
  useEffect(() => { load().catch(console.error); }, []);
  const save = async () => {
    await api(`/api/mrs/catalog/${form.id}`, { method: 'PUT', body: JSON.stringify(form) });
    await load();
  };
  const select = async (item: any) => {
    setForm({ id: item.id, code: item.code, name: item.name, category: item.category, unit: item.unit || '', current_price: item.current_price, price_scale: item.price_scale });
    setHistory(await api(`/api/mrs/catalog/${item.id}/price-history`));
  };
  const buildRecipe = async () => {
    if (!items.length) return;
    const result = await api('/api/mrs/analysis-recipes/demo', { method: 'PUT', body: JSON.stringify({ code: 'DEMO', name: '單價分析示例', unit: '式', price_scale: 2, components: [{ catalog_item_id: items[0].id, quantity: '1.00', quantity_scale: 2 }] }) });
    setRecipe(result);
  };
  return <div style={{ padding: 24 }}>
    <h1>MRS 工料機目錄</h1>
    <div style={{ display: 'flex', gap: 8, marginBottom: 16 }}>
      <input value={query} onChange={e => setQuery(e.target.value)} placeholder="搜尋代碼或名稱" />
      <button onClick={() => load()}>搜尋</button>
      <a href="/api/mrs/catalog/export?format=csv">匯出 CSV</a>
    </div>
    <div style={{ display: 'grid', gridTemplateColumns: '2fr 1fr', gap: 20 }}>
      <section>
        <table style={{ width: '100%' }}><thead><tr><th>代碼</th><th>名稱</th><th>分類</th><th>單位</th><th>現價</th></tr></thead><tbody>
          {items.map(item => <tr key={item.id} onClick={() => select(item)} style={{ cursor: 'pointer' }}><td>{item.code}</td><td>{item.name}</td><td>{item.category}</td><td>{item.unit}</td><td>{item.current_price}</td></tr>)}
        </tbody></table>
      </section>
      <section>
        <h3>新增／修改</h3>
        {Object.entries(form).map(([key, value]) => <div key={key} style={{ marginBottom: 8 }}><label>{key}</label><input value={String(value)} onChange={e => setForm({ ...form, [key]: key === 'price_scale' ? Number(e.target.value) : e.target.value })} /></div>)}
        <button onClick={save}>儲存</button>
        <button onClick={buildRecipe} style={{ marginLeft: 8 }}>建立分析配方</button>
        {recipe && <pre>{JSON.stringify(recipe, null, 2)}</pre>}
        <h3>價格歷史</h3>
        <ul>{history.map(row => <li key={row.id}>{row.old_price || '—'} → {row.new_price}（{row.source || '未註明'}）</li>)}</ul>
      </section>
    </div>
  </div>;
};

export default MrsCatalogPage;
