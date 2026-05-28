/* 結算明細頁面 — AG Grid 可編輯表格 */

import React, { useEffect, useState, useCallback, useRef } from 'react';
import {
  Card, Button, Space, Modal, Form, Input, InputNumber,
  message, Typography, Descriptions, Spin, Tag, Popconfirm,
} from 'antd';
import {
  ReloadOutlined, SendOutlined, CheckCircleOutlined,
  ArrowLeftOutlined, PlusOutlined, CalculatorOutlined,
  FileTextOutlined,
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
import type { ContractSettlement, ContractSettlementItem } from '../types';

const { Title, Text } = Typography;

ModuleRegistry.registerModules([AllCommunityModule]);

const statusConfig: Record<string, { color: string; label: string }> = {
  draft: { color: 'default', label: '草稿' },
  submitted: { color: 'processing', label: '已提交' },
  approved: { color: 'success', label: '已核准' },
};

const SettlementDetailPage: React.FC = () => {
  const { id: projectId, contractId, settlementId } = useParams<{ id: string; contractId: string; settlementId: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');
  const cid = parseInt(contractId || '0');
  const sid = parseInt(settlementId || '0');

  const gridRef = useRef<AgGridReactType>(null);

  const [settlement, setSettlement] = useState<ContractSettlement | null>(null);
  const [items, setItems] = useState<ContractSettlementItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<ContractSettlementItem | null>(null);
  const [itemForm] = Form.useForm();
  const [saving, setSaving] = useState(false);

  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const [st, itemList] = await Promise.all([
        contractApi.getSettlement(cid, sid),
        contractApi.listSettlementItems(sid),
      ]);
      setSettlement(st);
      setItems(itemList);
    } catch {
      message.error('載入資料失敗');
    } finally {
      setLoading(false);
    }
  }, [cid, sid]);

  useEffect(() => { fetchData(); }, [fetchData]);

  const handleRecalc = async () => {
    try {
      await contractApi.recalcSettlement(sid);
      message.success('金額重新計算完成');
      fetchData();
    } catch { message.error('重算失敗'); }
  };

  const handleSubmit = () => {
    Modal.confirm({
      title: '提交審核',
      content: '確定提交此結算？提交後無法編輯。',
      okText: '確認提交',
      onOk: async () => {
        try {
          await contractApi.submitSettlement(cid, sid);
          message.success('已提交');
          fetchData();
        } catch (err: any) { message.error(err?.response?.data?.detail || '提交失敗'); }
      },
    });
  };

  const handleApprove = () => {
    Modal.confirm({
      title: '核准結算',
      content: '確定核准此結算？',
      okText: '確認核准',
      onOk: async () => {
        try {
          await contractApi.approveSettlement(cid, sid);
          message.success('已核准');
          fetchData();
        } catch (err: any) { message.error(err?.response?.data?.detail || '核准失敗'); }
      },
    });
  };

  // 新增/編輯
  const handleAddItem = () => {
    setEditingItem(null);
    itemForm.resetFields();
    setModalOpen(true);
  };

  const handleEditItem = (item: ContractSettlementItem) => {
    setEditingItem(item);
    itemForm.setFieldsValue(item);
    setModalOpen(true);
  };

  const handleSaveItem = async () => {
    try {
      const values = await itemForm.validateFields();
      setSaving(true);
      if (editingItem) {
        await contractApi.updateSettlementItem(sid, editingItem.id, values);
        message.success('明細已更新');
      } else {
        await contractApi.createSettlementItem(sid, values);
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
      await contractApi.deleteSettlementItem(sid, itemId);
      message.success('已刪除');
      fetchData();
    } catch { message.error('刪除失敗'); }
  };

  const onCellValueChanged = useCallback(async (event: CellValueChangedEvent) => {
    const field = event.colDef.field;
    if (!field || !['actual_qty', 'actual_unit_price', 'contract_qty', 'contract_unit_price'].includes(field)) return;
    const row = event.data as ContractSettlementItem;
    try {
      await contractApi.updateSettlementItem(sid, row.id, { [field]: (row as any)[field] });
      fetchData();
    } catch {
      message.error('更新失敗');
      event.api.refreshCells({ rowNodes: [event.node] });
    }
  }, [sid, fetchData]);

  const columnDefs: ColDef[] = [
    { field: 'c_name', headerName: '項目名稱', width: 200, flex: 1 },
    { field: 'c_unit', headerName: '單位', width: 70 },
    {
      field: 'contract_qty', headerName: '合約數量', width: 100, type: 'numericColumn',
      editable: (params) => params.data && settlement?.status === 'draft',
      cellEditor: 'agNumberCellEditor', cellEditorParams: { precision: 2, min: 0 },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'contract_unit_price', headerName: '合約單價', width: 100, type: 'numericColumn',
      editable: (params) => params.data && settlement?.status === 'draft',
      cellEditor: 'agNumberCellEditor', cellEditorParams: { precision: 2, min: 0 },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'contract_amount', headerName: '合約金額', width: 110, type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'actual_qty', headerName: '實作數量', width: 100, type: 'numericColumn',
      editable: (params) => params.data && settlement?.status === 'draft',
      cellEditor: 'agNumberCellEditor', cellEditorParams: { precision: 2, min: 0 },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'actual_unit_price', headerName: '實作單價', width: 100, type: 'numericColumn',
      editable: (params) => params.data && settlement?.status === 'draft',
      cellEditor: 'agNumberCellEditor', cellEditorParams: { precision: 2, min: 0 },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'actual_amount', headerName: '實作金額', width: 110, type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'diff_amount', headerName: '差異', width: 110, type: 'numericColumn',
      cellStyle: (params: any) => {
        const v = params.value || 0;
        if (v > 0) return { color: '#cf1322' };
        if (v < 0) return { color: '#389e0d' };
        return null;
      },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      headerName: '操作', width: 100,
      cellRenderer: ({ data }: { data: ContractSettlementItem }) => {
        if (!data || settlement?.status !== 'draft') return null;
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
  if (!settlement) {
    return <div style={{ textAlign: 'center', padding: 80 }}><Text type="danger">結算單不存在</Text></div>;
  }

  const statusCfg = statusConfig[settlement.status] || { color: 'default', label: settlement.status };

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Space>
          <Button icon={<ArrowLeftOutlined />} onClick={() => navigate(`/projects/${pid}/contracts/${cid}/settlements`)}>
            返回列表
          </Button>
          <Title level={4} style={{ margin: 0 }}>
            <FileTextOutlined style={{ marginRight: 8 }} />
            {settlement.c_name || settlement.settlement_no}
          </Title>
          <Tag color={statusCfg.color}>{statusCfg.label}</Tag>
        </Space>
        <Space>
          {settlement.status === 'draft' && (
            <>
              <Button icon={<CalculatorOutlined />} onClick={handleRecalc}>重算金額</Button>
              <Button icon={<PlusOutlined />} onClick={handleAddItem}>新增明細</Button>
              <Button type="primary" icon={<SendOutlined />} onClick={handleSubmit}>提交審核</Button>
            </>
          )}
          {settlement.status === 'submitted' && (
            <Button type="primary" icon={<CheckCircleOutlined />} onClick={handleApprove}>核准</Button>
          )}
          <Button icon={<ReloadOutlined />} onClick={fetchData}>重新整理</Button>
        </Space>
      </div>

      <Card size="small" style={{ marginBottom: 16 }}>
        <Descriptions size="small" column={4}>
          <Descriptions.Item label="結算編號">{settlement.settlement_no}</Descriptions.Item>
          <Descriptions.Item label="合約金額">
            ${(settlement.contract_amount || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}
          </Descriptions.Item>
          <Descriptions.Item label="結算金額">
            <Text strong>${(settlement.settlement_amount || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>
          </Descriptions.Item>
          <Descriptions.Item label="追加金額">
            <Text style={{ color: '#cf1322' }}>+${(settlement.total_add_amount || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>
          </Descriptions.Item>
          <Descriptions.Item label="扣減金額">
            <Text style={{ color: '#389e0d' }}>-${(settlement.total_deduct_amount || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>
          </Descriptions.Item>
          <Descriptions.Item label="日期">{settlement.settlement_date || '-'}</Descriptions.Item>
          <Descriptions.Item label="明細數">{items.length}</Descriptions.Item>
        </Descriptions>
      </Card>

      <Card title={<Space><span>結算明細</span><Tag>{items.length} 筆</Tag></Space>}>
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
          <Form.Item name="contract_unit_price" label="合約單價">
            <InputNumber style={{ width: '100%' }} min={0} precision={2} />
          </Form.Item>
          <Form.Item name="actual_qty" label="實作數量">
            <InputNumber style={{ width: '100%' }} min={0} precision={2} />
          </Form.Item>
          <Form.Item name="actual_unit_price" label="實作單價">
            <InputNumber style={{ width: '100%' }} min={0} precision={2} />
          </Form.Item>
          <Form.Item name="remark" label="備註"><Input.TextArea rows={2} /></Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default SettlementDetailPage;
