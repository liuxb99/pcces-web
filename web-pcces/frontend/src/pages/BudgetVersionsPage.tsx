import React, { useEffect, useState } from 'react';
import { Alert, Button, Card, Input, List, Space, Switch, Tag, Typography, message } from 'antd';
import { useParams, useSearchParams } from 'react-router-dom';
import axios from 'axios';

const client = axios.create({ baseURL: '/api' });
client.interceptors.request.use((config) => {
  const token = localStorage.getItem('pcces_token');
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

type Version = { id:string; label:string; status:string; created_at:string; deep_link:string; snapshot:any[] };
type LockState = { project_code:string; locked:boolean; reason?:string };

const BudgetVersionsPage: React.FC = () => {
  const { projectCode = '' } = useParams();
  const [search] = useSearchParams();
  const selectedVersion = search.get('version');
  const [versions,setVersions] = useState<Version[]>([]);
  const [lock,setLock] = useState<LockState>({project_code:projectCode,locked:false});
  const [label,setLabel] = useState('預算快照');
  const [diff,setDiff] = useState<any>(null);

  const load = async () => {
    const [v,l] = await Promise.all([
      client.get(`/decimal-budget/projects/${projectCode}/versions`),
      client.get(`/decimal-budget/projects/${projectCode}/lock`),
    ]);
    setVersions(v.data); setLock(l.data);
  };
  useEffect(() => { load().catch(() => message.error('載入預算版本失敗')); }, [projectCode]);

  const createVersion = async () => {
    await client.post(`/decimal-budget/projects/${projectCode}/versions`, { label, status:'DRAFT' });
    message.success('已建立預算快照'); await load();
  };
  const toggleLock = async (locked:boolean) => {
    const res = await client.put(`/decimal-budget/projects/${projectCode}/lock`, { locked, reason: locked ? '預算版本已凍結' : null });
    setLock(res.data); message.success(locked ? '預算已鎖定' : '預算已解鎖');
  };
  const restore = async (id:string) => {
    await client.post(`/decimal-budget/versions/${id}/restore`);
    message.success('版本已回復並建立新快照'); await load();
  };
  const compareLatest = async () => {
    if (versions.length < 2) return;
    const res = await client.get(`/decimal-budget/versions/${versions[1].id}/diff/${versions[0].id}`);
    setDiff(res.data);
  };

  return <Space direction="vertical" style={{width:'100%'}} size="large">
    <Typography.Title level={2}>預算版本與凍結</Typography.Title>
    {selectedVersion && <Alert type="info" showIcon message={`深連結版本：${selectedVersion}`} />}
    <Card title="專案鎖定">
      <Space><Switch checked={lock.locked} onChange={toggleLock}/><span>{lock.locked ? '已凍結，禁止預算寫入' : '可編輯'}</span>{lock.reason && <Tag>{lock.reason}</Tag>}</Space>
    </Card>
    <Card title="建立快照">
      <Space><Input value={label} onChange={e=>setLabel(e.target.value)} style={{width:320}}/><Button type="primary" onClick={createVersion}>建立版本</Button><Button onClick={compareLatest} disabled={versions.length<2}>比較最新兩版</Button></Space>
    </Card>
    {diff && <Card title="版本差異"><pre style={{whiteSpace:'pre-wrap'}}>{JSON.stringify(diff,null,2)}</pre></Card>}
    <Card title="版本歷史">
      <List dataSource={versions} renderItem={item => <List.Item actions={[<Button key="restore" danger disabled={lock.locked} onClick={()=>restore(item.id)}>回復</Button>]}>
        <List.Item.Meta title={<Space><span>{item.label}</span><Tag>{item.status}</Tag></Space>} description={`${item.created_at} · ${item.snapshot.length} 筆工項 · ${item.id}`} />
      </List.Item>} />
    </Card>
  </Space>;
};

export default BudgetVersionsPage;
