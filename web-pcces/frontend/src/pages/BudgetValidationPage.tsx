import React, { useEffect, useState } from 'react';
import { Alert, Button, Card, Input, Select, Space, Table, Tag, Typography, message } from 'antd';
import axios from 'axios';
import { useParams, useSearchParams } from 'react-router-dom';

type Issue={code:string;item_id?:string;item_ids?:string[];blocking:boolean;detail?:string};
type Result={id:string;project_code:string;mode:string;passed:boolean;blocking_issues:number;warnings:number;issues:Issue[];created_at:string};

const BudgetValidationPage:React.FC=()=>{
  const {projectCode=''}=useParams(); const [params]=useSearchParams();
  const [mode,setMode]=useState('BUD'); const [version,setVersion]=useState('0'); const [result,setResult]=useState<Result|null>(null);
  const [sourceItem,setSourceItem]=useState(''); const [targetProject,setTargetProject]=useState(''); const [targetItem,setTargetItem]=useState('');
  const headers={Authorization:`Bearer ${localStorage.getItem('token')||''}`};
  const load=async()=>{const r=await axios.get(`/api/decimal-budget/projects/${projectCode}/mode`,{headers});setMode(r.data.mode);setVersion(String(r.data.row_version||0));};
  useEffect(()=>{load().catch(()=>message.error('載入預算模式失敗'));},[projectCode]);
  const saveMode=async()=>{const r=await axios.put(`/api/decimal-budget/projects/${projectCode}/mode`,{mode,row_version:version},{headers});setVersion(String(r.data.row_version));message.success('模式已更新');};
  const check=async()=>{try{const r=await axios.post(`/api/decimal-budget/projects/${projectCode}/self-check`,{blocking:true},{headers});setResult(r.data);}catch(e:any){if(e.response?.data?.issues)setResult(e.response.data);else message.error('自我檢查失敗');}};
  const link=async()=>{await axios.post('/api/decimal-budget/cross-project-references',{source_project_code:projectCode,source_item_id:sourceItem,target_project_code:targetProject,target_item_id:targetItem},{headers});message.success('跨專案引用已建立');};
  return <Space direction="vertical" size="large" style={{width:'100%'}}>
    <Typography.Title level={2}>預算模式與自我檢查</Typography.Title>
    {params.get('check')&&<Alert type="info" showIcon message={`定位檢查：${params.get('check')}`}/>} 
    <Card title="BUD／BID 模式"><Space><Select value={mode} style={{width:160}} options={[{value:'BUD',label:'BUD 預算編製'},{value:'BID',label:'BID 投標單'}]} onChange={setMode}/><Button type="primary" onClick={saveMode}>儲存模式</Button><Button onClick={check}>執行阻擋式自我檢查</Button></Space></Card>
    <Card title="跨專案引用"><Space wrap><Input placeholder="來源工項 ID" value={sourceItem} onChange={e=>setSourceItem(e.target.value)}/><Input placeholder="目標專案代碼" value={targetProject} onChange={e=>setTargetProject(e.target.value)}/><Input placeholder="目標工項 ID" value={targetItem} onChange={e=>setTargetItem(e.target.value)}/><Button onClick={link}>建立明確引用</Button></Space></Card>
    {result&&<Card title="檢查結果" extra={<Tag color={result.passed?'green':'red'}>{result.passed?'通過':'阻擋'}</Tag>}><Space style={{marginBottom:16}}><Tag>模式 {result.mode}</Tag><Tag color="red">阻擋 {result.blocking_issues}</Tag><Tag color="orange">警告 {result.warnings}</Tag></Space><Table rowKey={(r,i)=>`${r.code}-${r.item_id||i}`} pagination={false} dataSource={result.issues} columns={[{title:'等級',render:(_,r)=><Tag color={r.blocking?'red':'orange'}>{r.blocking?'阻擋':'警告'}</Tag>},{title:'代碼',dataIndex:'code'},{title:'工項',render:(_,r)=>r.item_id||r.item_ids?.join(', ')||'-'},{title:'說明',dataIndex:'detail'}]}/></Card>}
  </Space>;
};
export default BudgetValidationPage;
