/* 終驗明細頁面 — AG Grid 可編輯表格 */

import React, { useEffect, useState, useCallback, useRef } from 'react';
import {
  Card, Button, Space, Modal, Form, Input, InputNumber, Select,
  message, Typography, Descriptions, Spin, Tag, Popconfirm,
} from 'antd';
import {
  ReloadOutlined, SendOutlined, CheckCircleOutlined,
  ArrowLeftOutlined, PlusOutlined, CalculatorOutlined,
  CheckSquareOutlined,
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
import type { ContractFinalAcceptance, ContractFinalAcceptanceItem } from '../types';

const { Title, Text } = Typography;

ModuleRegistry.registerModules([AllCommunityModule]);

const statusConfig: Record<string, { color: string; label: string }> = {
  draft: { color: 'default', label: '草稿' },
  submitted: { color: 'processing', label: '已提交' },
  approved: { color: 'success', label: '已核准' },
};

const resultConfig: Record<string, { color: string; label: string }> = {
  pass: { color: 'success', label: '合格' },
  conditional_pass: { color: 'warning', label: '附條件合格' },
  fail: { color: 'error', label: '不合格' },
};

const AcceptanceDetailPage: React.FC = () => {
  const { id: projectId, contractId, acceptanceId } = useParams<{ id: string; contractId: string; acceptanceId: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');
  const cid = parseInt(contractId || '0');
  const aid = parseInt(acceptanceId || '0');

  const gridRef = useRef<AgGridReactType>(null);

  const [acceptance, setAcceptance] = useState<ContractFinalAcceptance | null>(null);
  const [items, setItems] = useState<ContractFinalAcceptanceItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<ContractFinalAcceptanceItem | null>(null);
  const [itemForm] = Form.useForm();
  const [saving, setSaving] = useState(false);

  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const [ac, itemList] = await Promise.all([
        contractApi.getAcceptance(cid, aid),
        contractApi.listAcceptanceItems(aid),
      ]);
      setAcceptance(ac);
      setItems(itemList);
    } catch {
      message.error('載入資料失敗');
    } finally {
      setLoading(false);
    }
  }, [cid, aid]);

  useEffect(() => { fetchData(); }, [fetchData]);

  const handleRecalc = async () => {
    try {
      await contractApi.recalcAcceptance(aid);
      message.success('重新計算完成');
      fetchData();
    } catch { message.error('重算失敗'); }
  };

  const handleSubmit = () => {
    Modal.confirm({
      title: '提交審核',
      content: '確定提交此終驗？提交後無法編輯。',
      okText: '確認提交',
      onOk: async () => {
        try {
          await contractApi.submitAcceptance(cid, aid);
          message.success('已提交');
          fetchData();
        } catch (err: any) { message.error(err?.response?.data?.detail || '提交失敗'); }
      },
    });
  };

  const handleApprove = () => {
    Modal.confirm({
      title: '核准終驗',
      content: '確定核准此終驗？',
      okText: '確認核准',
      onOk: async () => {
        try {
          await contractApi.approveAcceptance(cid, aid);
          message.success('已核准');
          fetchData();
        } catch (err: any) { message.error(err?.response?.data?.detail || '核准失敗'); }
      },
    });
  };

  // 批次導入合約工項
  const handleBatchImport = async () => {
    try {
      const result = await contractApi.batchAcceptanceItemsFromContract(aid);
      message.success(`成功導入 ${result.count} 筆`);
      fetchData();
    } catch (err: any) { message.error(err?.response?.data?.detail || '導入失敗'); }
  };

  // 新增/編輯
  const handleAddItem = () => {
    setEditingItem(null);
    itemForm.resetFields();
    setModalOpen(true);
  };

  const handleEditItem = (item: ContractFinalAcceptanceItem) => {
    setEditingItem(item);
    itemForm.setFieldsValue(item);
    setModalOpen(true);
  };

  const handleSaveItem = async () => {
    try {
      const values = await itemForm.validateFields();
      setSaving(true);
      if (editingItem) {
        await contractApi.updateAcceptanceItem(aid, editingItem.id, values);
        message.success('明細已更新');
      } else {
        await contractApi.createAcceptanceItem(aid, values);
        message.success('明細已新增');
      }
      setModalOpen(false);
      fetchData();
    } catch (err: any) {
      if (err?.errorFields) return;
      message.error('儲存失敗');
    } finally {
      setSaving(false);
    }
  };

  const handleDeleteItem = async (itemId: number) => {
    try {
      await contractApi.deleteAcceptanceItem(aid, itemId);
      message.success('已刪除');
      fetchData();
    } catch { message.error('刪除失敗'); }
  };

  const onCellValueChanged = useCallback(async (event: CellValueChangedEvent) => {
    const field = event.colDef.field;
    if (!field || !['actual_qty', 'accepted_qty', 'rejected_qty'].includes(field)) return;
    const row = event.data as ContractFinalAcceptanceItem;
    try {
      await contractApi.updateAcceptanceItem(aid, row.id, { [field]: (row as any)[field] });
      fetchData();
    } catch {
      message.error('更新失敗');
      event.api.refreshCells({ rowNodes: [event.node] });
    }
  }, [aid, fetchData]);

  const columnDefs: ColDef[] = [
    { field: 'c_name', headerName: '項目名稱', width: 200, flex: 1 },
    { field: 'c_unit', headerName: '單位', width: 70 },
    {
      field: 'contract_qty', headerName: '合約數量', width: 100, type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'actual_qty', headerName: '實作數量', width: 100, type: 'numericColumn',
      editable: (params) => params.data && acceptance?.status === 'draft',
      cellEditor: 'agNumberCellEditor', cellEditorParams: { precision: 2, min: 0 },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'accepted_qty', headerName: '驗收合格', width: 100, type: 'numericColumn',
      editable: (params) => params.data && acceptance?.status === 'draft',
      cellEditor: 'agNumberCellEditor', cellEditorParams: { precision: 2, min: 0 },
      cellStyle: { color: '#389e0d' },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'rejected_qty', headerName: '不合格', width: 90, type: 'numericColumn',
      editable: (params) => params.data && acceptance?.status === 'draft',
      cellEditor: 'agNumberCellEditor', cellEditorParams: { precision: 2, min: 0 },
      cellStyle: { color: '#cf1322' },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    { field: 'remark', headerName: '備註', width: 120, editable: true },
    {
      headerName: '操作', width: 100,
      cellRenderer: ({ data }: { data: ContractFinalAcceptanceItem }) => {
        if (!data || acceptance?.status !== 'draft') return null;
        return (
          <Space size="small">
            <Button type="link" size="small" onClick={() => handleEditItem(data)}>編輯</Button>
            <Popconfirm title="確定刪除？" onConfirm={() => handleDeleteItem(data.id)}>
              <Button type="link" size="small" danger>刪除</Button>
            </Popconfirm>
          </Space>
        );
      },
    },
  ];

  const defaultColDef: ColDef = { resizable: true, sortable: true };

  if (loading) {
    return <div style={{ textAlign: 'center', padding: 80 }}><Spin size="large" /><div style={{ marginTop: 16 }}>載入中...</div></div>;
  }
  if (!acceptance) {
    return <div style={{ textAlign: 'center', padding: 80 }}><Text type="danger">終驗單不存在</Text></div>;
  }

  const statusCfg = statusConfig[acceptance.status] || { color: 'default', label: acceptance.status };
  const resultCfg = acceptance.result ? (resultConfig[acceptance.result] || { color: 'default', label: acceptance.result }) : null;

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Space>
          <Button icon={<ArrowLeftOutlined />} onClick={() => navigate(`/projects/${pid}/contracts/${cid}/acceptances`)}>
            返回列表
          </Button>
          <Title level={4} style={{ margin: 0 }}>
            <CheckSquareOutlined style={{ marginRight: 8 }} />
            {acceptance.c_name || acceptance.acceptance_no}
          </Title>
          <Tag color={statusCfg.color}>{statusCfg.label}</Tag>
          {resultCfg && <Tag color={resultCfg.color}>{resultCfg.label}</Tag>}
        </Space>
        <Space>
          {acceptance.status === 'draft' && (
            <>
              <Button icon={<CalculatorOutlined />} onClick={handleRecalc}>重算</Button>
              <Button icon={<PlusOutlined />} onClick={handleBatchImport}>導入合約工項</Button>
              <Button icon={<PlusOutlined />} onClick={handleAddItem}>新增明細</Button>
              <Button type="primary" icon={<SendOutlined />} onClick={handleSubmit}>提交審核</Button>
            </>
          )}
          {acceptance.status === 'submitted' && (
            <Button type="primary" icon={<CheckCircleOutlined />} onClick={handleApprove}>核准</Button>
          )}
          <Button icon={<ReloadOutlined />} onClick={fetchData}>重新整理</Button>
        </Space>
      </div>

      <Card size="small" style={{ marginBottom: 16 }}>
        <Descriptions size="small" column={4}>
          <Descriptions.Item label="終驗編號">{acceptance.acceptance_no}</Descriptions.Item>
          <Descriptions.Item label="檢驗人員">{acceptance.inspector || '-'}</Descriptions.Item>
          <Descriptions.Item label="終驗日期">{acceptance.acceptance_date || '-'}</Descriptions.Item>
          <Descriptions.Item label="明細數">{items.length}</Descriptions.Item>
        </Descriptions>
        {acceptance.defect_description && (
          <div><Text type="warning">缺失說明：{acceptance.defect_description}</Text></div>
        )}
        {acceptance.remark && <div><Text type="secondary">備註：{acceptance.remark}</Text></div>}
      </Card>

      <Card title={<Space><span>終驗明細</span><Tag>{items.length} 筆</Tag></Space>}>
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

      <Modal title={editingItem ? '編輯明細' : '新增明細'} open={modalOpen}
        onOk={handleSaveItem} onCancel={() => setModalOpen(false)} confirmLoading={saving}
        okText="儲存" cancelText="取消">
        <Form form={itemForm} layout="vertical" style={{ marginTop: 16 }}>
          <Form.Item name="c_name" label="項目名稱"><Input /></Form.Item>
          <Form.Item name="c_unit" label="單位"><Input /></Form.Item>
          <Form.Item name="contract_qty" label="合約數量">
            <InputNumber style={{ width: '100%' }} min={0} precision={2} />
          </Form.Item>
          <Form.Item name="actual_qty" label="實作數量">
            <InputNumber style={{ width: '100%' }} min={0} precision={2} />
          </Form.Item>
          <Form.Item name="accepted_qty" label="驗收合格數量">
            <InputNumber style={{ width: '100%' }} min={0} precision={2} />
          </Form.Item>
          <Form.Item name="rejected_qty" label="不合格數量">
            <InputNumber style={{ width: '100%' }} min={0} precision={2} />
          </Form.Item>
          <Form.Item name="remark" label="備註"><Input.TextArea rows={2} /></Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default AcceptanceDetailPage;
