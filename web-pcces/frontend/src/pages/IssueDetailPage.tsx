/* 期別計價明細頁面 — AG Grid 可編輯表格 */

import React, { useEffect, useState, useCallback, useRef } from 'react';
import {
  Card, Button, Space, Modal, message, Typography, Descriptions,
  Spin, Tag, Tooltip,
} from 'antd';
import {
  ReloadOutlined, SendOutlined, CheckCircleOutlined,
  ArrowLeftOutlined, PlusOutlined, CalculatorOutlined,
  DollarOutlined,
} from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';

import { AgGridReact } from 'ag-grid-react';
import type { AgGridReact as AgGridReactType } from 'ag-grid-react';
import { AllCommunityModule, ModuleRegistry } from 'ag-grid-community';
import type { ColDef, CellValueChangedEvent } from 'ag-grid-community';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';

import { contractApi } from '../api';
import type { ContractIssue, ContractIssueItem } from '../types';

const { Title, Text } = Typography;

ModuleRegistry.registerModules([AllCommunityModule]);

const statusConfig: Record<string, { color: string; label: string }> = {
  draft: { color: 'default', label: '草稿' },
  submitted: { color: 'processing', label: '已提交' },
  approved: { color: 'success', label: '已核准' },
};

const IssueDetailPage: React.FC = () => {
  const { id: projectId, contractId, issueId } = useParams<{ id: string; contractId: string; issueId: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');
  const cid = parseInt(contractId || '0');
  const iid = parseInt(issueId || '0');

  const gridRef = useRef<AgGridReactType>(null);

  const [issue, setIssue] = useState<ContractIssue | null>(null);
  const [items, setItems] = useState<ContractIssueItem[]>([]);
  const [loading, setLoading] = useState(true);

  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const [inv, itemList] = await Promise.all([
        contractApi.getIssue(cid, iid),
        contractApi.listIssueItems(iid),
      ]);
      setIssue(inv);
      setItems(itemList);
    } catch {
      message.error('載入資料失敗');
    } finally {
      setLoading(false);
    }
  }, [cid, iid]);

  useEffect(() => { fetchData(); }, [fetchData]);

  // ── 重算 ──
  const handleRecalc = async () => {
    try {
      await contractApi.recalcIssue(iid);
      message.success('金額重新計算完成');
      fetchData();
    } catch { message.error('重算失敗'); }
  };

  // ── 提交 ──
  const handleSubmit = () => {
    Modal.confirm({
      title: '提交審核',
      content: '確定提交此期別計價？提交後無法編輯。',
      okText: '確認提交',
      onOk: async () => {
        try {
          await contractApi.submitIssue(cid, iid);
          message.success('已提交');
          fetchData();
        } catch (err: any) { message.error(err?.response?.data?.detail || '提交失敗'); }
      },
    });
  };

  // ── 核准 ──
  const handleApprove = () => {
    Modal.confirm({
      title: '核准',
      content: '確定核准此期別計價？',
      okText: '確認核准',
      onOk: async () => {
        try {
          await contractApi.approveIssue(cid, iid);
          message.success('已核准');
          fetchData();
        } catch (err: any) { message.error(err?.response?.data?.detail || '核准失敗'); }
      },
    });
  };

  // ── 批次導入合約工項 ──
  const handleBatchImport = async () => {
    try {
      const result = await contractApi.batchIssueItemsFromContract(iid);
      message.success(`成功導入 ${result.count} 筆`);
      fetchData();
    } catch (err: any) { message.error(err?.response?.data?.detail || '導入失敗'); }
  };

  // ── 編輯本期完成數量 ──
  const onCellValueChanged = useCallback(async (event: CellValueChangedEvent) => {
    if (event.colDef.field !== 'this_completed_qty') return;
    const row = event.data as ContractIssueItem;
    try {
      await contractApi.updateIssueItem(iid, row.id, { this_completed_qty: row.this_completed_qty });
      fetchData();
    } catch {
      message.error('更新失敗');
      event.api.refreshCells({ rowNodes: [event.node] });
    }
  }, [iid, fetchData]);

  // ── 刪除明細 ──
  const handleDeleteItem = async (itemId: number) => {
    try {
      await contractApi.deleteIssueItem(iid, itemId);
      message.success('明細已刪除');
      fetchData();
    } catch { message.error('刪除失敗'); }
  };

  const columnDefs: ColDef[] = [
    { field: 'c_name', headerName: '項目名稱', width: 200, flex: 1 },
    { field: 'c_unit', headerName: '單位', width: 70 },
    {
      field: 'contract_qty', headerName: '合約數量', width: 100, type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'unit_price', headerName: '單價', width: 110, type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'prev_completed_qty', headerName: '前期完成', width: 100, type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'this_completed_qty', headerName: '本期完成', width: 110, type: 'numericColumn',
      editable: (params) => params.data && issue?.status === 'draft',
      cellEditor: 'agNumberCellEditor', cellEditorParams: { precision: 2, min: 0 },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'total_completed_qty', headerName: '累計完成', width: 100, type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'remain_qty', headerName: '剩餘', width: 90, type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'this_amount', headerName: '本期金額', width: 120, type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'cumulative_amount', headerName: '累計金額', width: 120, type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'progress_rate', headerName: '進度', width: 80,
      valueFormatter: (p) => p.value != null ? `${p.value.toFixed(1)}%` : '',
    },
    {
      headerName: '操作', width: 70,
      cellRenderer: ({ data }: { data: ContractIssueItem }) => {
        if (!data || issue?.status !== 'draft') return null;
        return (
          <Button type="link" size="small" danger onClick={() => handleDeleteItem(data.id)}>刪除</Button>
        );
      },
    },
  ];

  const defaultColDef: ColDef = { resizable: true, sortable: true };

  if (loading) {
    return <div style={{ textAlign: 'center', padding: 80 }}><Spin size="large" /><div style={{ marginTop: 16 }}>載入中...</div></div>;
  }
  if (!issue) {
    return <div style={{ textAlign: 'center', padding: 80 }}><Text type="danger">期別計價單不存在</Text></div>;
  }

  const statusCfg = statusConfig[issue.status] || { color: 'default', label: issue.status };

  return (
    <div>
      {/* 頁首 */}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Space>
          <Button icon={<ArrowLeftOutlined />} onClick={() => navigate(`/projects/${pid}/contracts/${cid}/issues`)}>
            返回列表
          </Button>
          <Title level={4} style={{ margin: 0 }}>
            <DollarOutlined style={{ marginRight: 8 }} />
            {issue.c_name || `第 ${issue.issue_no} 期計價`}
          </Title>
          <Tag color={statusCfg.color}>{statusCfg.label}</Tag>
        </Space>
        <Space>
          {issue.status === 'draft' && (
            <>
              <Button icon={<CalculatorOutlined />} onClick={handleRecalc}>重算金額</Button>
              <Button icon={<PlusOutlined />} onClick={handleBatchImport}>導入合約工項</Button>
              <Button type="primary" icon={<SendOutlined />} onClick={handleSubmit}>提交審核</Button>
            </>
          )}
          {issue.status === 'submitted' && (
            <Button type="primary" icon={<CheckCircleOutlined />} onClick={handleApprove}>核准</Button>
          )}
          <Button icon={<ReloadOutlined />} onClick={fetchData}>重新整理</Button>
        </Space>
      </div>

      {/* 基本資訊 */}
      <Card size="small" style={{ marginBottom: 16 }}>
        <Descriptions size="small" column={4}>
          <Descriptions.Item label="期別">第 {issue.issue_no} 期</Descriptions.Item>
          <Descriptions.Item label="本期金額">
            <Text strong>${(issue.total_amount || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>
          </Descriptions.Item>
          <Descriptions.Item label="累計金額">
            <Text strong>${(issue.cumulative_amount || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>
          </Descriptions.Item>
          <Descriptions.Item label="進度">{issue.progress_rate.toFixed(1)}%</Descriptions.Item>
          <Descriptions.Item label="計價日期">{issue.issue_date || '-'}</Descriptions.Item>
          <Descriptions.Item label="明細數">{items.length}</Descriptions.Item>
        </Descriptions>
        {issue.remark && <div><Text type="secondary">備註：{issue.remark}</Text></div>}
      </Card>

      {/* AG Grid */}
      <Card title={<Space><span>計價明細</span><Tag>{items.length} 筆</Tag></Space>}>
        <div className="ag-theme-alpine" style={{ height: Math.max(300, items.length * 42 + 52), width: '100%' }}>
          <AgGridReact
            ref={gridRef}
            rowData={items}
            columnDefs={columnDefs}
            defaultColDef={defaultColDef}
            onCellValueChanged={onCellValueChanged}
            animateRows
            enableCellTextSelection
            domLayout="autoHeight"
          />
        </div>
      </Card>
    </div>
  );
};

export default IssueDetailPage;
