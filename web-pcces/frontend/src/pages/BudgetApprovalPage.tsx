import React, { useEffect, useState } from 'react';
import { Alert, Button, Card, Input, Space, Table, Tag, Typography, message } from 'antd';
import axios from 'axios';
import { useParams } from 'react-router-dom';

const client = axios.create({ baseURL: '/api' });
client.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

type State = { project_code: string; status: string; row_version: number; submitted_by?: string; reviewed_by?: string; comment?: string };

type Audit = { id: string; event_type: string; actor_id: string; from_status?: string; to_status?: string; created_at: string; payload?: unknown };

const statusColor: Record<string,string> = { DRAFT:'default', SUBMITTED:'processing', APPROVED:'success', RETURNED:'warning' };

const BudgetApprovalPage: React.FC = () => {
  const { projectCode = '' } = useParams();
  const [state, setState] = useState<State>();
  const [audits, setAudits] = useState<Audit[]>([]);
  const [comment, setComment] = useState('');
  const [loading, setLoading] = useState(false);

  const load = async () => {
    const [s, a] = await Promise.all([
      client.get(`/decimal-budget/projects/${projectCode}/approval`),
      client.get(`/decimal-budget/projects/${projectCode}/workflow-audit`),
    ]);
    setState(s.data); setAudits(a.data);
  };
  useEffect(() => { void load(); }, [projectCode]);

  const command = async (name: string) => {
    if (!state) return;
    setLoading(true);
    try {
      await client.post(`/decimal-budget/projects/${projectCode}/approval/${name}`, { comment, row_version: state.row_version });
      message.success('狀態已更新'); setComment(''); await load();
    } catch (error: any) {
      message.error(error?.response?.data?.detail || '操作失敗');
    } finally { setLoading(false); }
  };

  return <Space direction="vertical" size="large" style={{ width:'100%' }}>
    <Typography.Title level={2}>預算核定流程</Typography.Title>
    <Alert type="info" showIcon message="編製與審查權限分離；核定後全案自動凍結，退回後解除凍結。" />
    <Card title={`專案 ${projectCode}`}>
      <Space direction="vertical" style={{ width:'100%' }}>
        <Space><span>目前狀態</span><Tag color={statusColor[state?.status || 'DRAFT']}>{state?.status || 'DRAFT'}</Tag><span>版本 {state?.row_version ?? 0}</span></Space>
        <Input.TextArea value={comment} onChange={e=>setComment(e.target.value)} placeholder="簽核或退回意見" rows={3}/>
        <Space wrap>
          <Button type="primary" loading={loading} disabled={!state || !['DRAFT','RETURNED'].includes(state.status)} onClick={()=>void command('SUBMIT')}>送審</Button>
          <Button type="primary" loading={loading} disabled={state?.status!=='SUBMITTED'} onClick={()=>void command('APPROVE')}>核定</Button>
          <Button danger loading={loading} disabled={state?.status!=='SUBMITTED'} onClick={()=>void command('RETURN')}>退回</Button>
          <Button loading={loading} disabled={state?.status!=='APPROVED'} onClick={()=>void command('REOPEN')}>重新開啟</Button>
        </Space>
      </Space>
    </Card>
    <Card title="流程審計">
      <Table rowKey="id" dataSource={audits} pagination={false} columns={[
        {title:'時間',dataIndex:'created_at'}, {title:'事件',dataIndex:'event_type'},
        {title:'操作者',dataIndex:'actor_id'}, {title:'原狀態',dataIndex:'from_status'}, {title:'新狀態',dataIndex:'to_status'},
      ]}/>
    </Card>
  </Space>;
};

export default BudgetApprovalPage;
