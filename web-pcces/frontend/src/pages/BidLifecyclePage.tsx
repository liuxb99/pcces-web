import React, { useEffect, useState } from 'react';
import { useParams, useSearchParams } from 'react-router-dom';

type Version = { id:string; label:string; status:string; total_amount:string; created_at:string; deep_link:string };

const headers = () => ({ 'Content-Type':'application/json', Authorization:`Bearer ${localStorage.getItem('access_token') || ''}` });

const BidLifecyclePage: React.FC = () => {
  const { projectCode = '' } = useParams();
  const [params] = useSearchParams();
  const [versions,setVersions] = useState<Version[]>([]);
  const [source,setSource] = useState('');
  const [label,setLabel] = useState('投標價格版次');
  const [message,setMessage] = useState('');
  const [variance,setVariance] = useState<any>(null);

  const load = async () => {
    const response = await fetch(`/api/decimal-budget/projects/${projectCode}/bid-price-versions`,{headers:headers()});
    if(response.ok) setVersions(await response.json());
  };
  useEffect(()=>{ void load(); },[projectCode]);

  const convert = async () => {
    const response=await fetch('/api/decimal-budget/bud-to-bid',{method:'POST',headers:headers(),body:JSON.stringify({source_project_code:source,target_project_code:projectCode})});
    const body=await response.json();setMessage(response.ok?`已複製 ${body.copied_items} 筆工項`:body.detail || '轉換失敗');
  };
  const snapshot = async () => {
    const response=await fetch(`/api/decimal-budget/projects/${projectCode}/bid-price-versions`,{method:'POST',headers:headers(),body:JSON.stringify({label,status:'SEALED'})});
    const body=await response.json();setMessage(response.ok?`已封存：${body.total_amount}`:body.detail || '封存失敗');if(response.ok) void load();
  };
  const compare = async () => {
    if(versions.length<2) return;
    const response=await fetch(`/api/decimal-budget/bid-price-versions/${versions[1].id}/variance/${versions[0].id}`,{headers:headers()});
    if(response.ok) setVariance(await response.json());
  };
  const rollback = async (id:string) => {
    const response=await fetch(`/api/decimal-budget/bid-price-versions/${id}/rollback`,{method:'POST',headers:headers(),body:'{}'});
    const body=await response.json();setMessage(response.ok?`已回轉 ${body.restored_items} 筆工項`:body.detail || '回轉失敗');
  };

  return <main style={{padding:24}}>
    <h1>BID 投標生命週期</h1>
    <p>專案：{projectCode} {params.get('version') ? `／定位版次 ${params.get('version')}` : ''}</p>
    <section>
      <h2>BUD → BID</h2>
      <input value={source} onChange={e=>setSource(e.target.value)} placeholder="來源 BUD 專案代碼" />
      <button onClick={convert}>建立 BID</button>
    </section>
    <section>
      <h2>價格版次</h2>
      <input value={label} onChange={e=>setLabel(e.target.value)} />
      <button onClick={snapshot}>封存目前投標總價</button>
      <button onClick={compare} disabled={versions.length<2}>比較最新兩版</button>
      {variance && <pre>{JSON.stringify(variance,null,2)}</pre>}
      <table><thead><tr><th>名稱</th><th>狀態</th><th>總價</th><th>時間</th><th>操作</th></tr></thead>
        <tbody>{versions.map(v=><tr key={v.id}><td>{v.label}</td><td>{v.status}</td><td>{v.total_amount}</td><td>{v.created_at}</td><td><button onClick={()=>rollback(v.id)}>回轉</button></td></tr>)}</tbody>
      </table>
    </section>
    {message && <p>{message}</p>}
  </main>;
};
export default BidLifecyclePage;
