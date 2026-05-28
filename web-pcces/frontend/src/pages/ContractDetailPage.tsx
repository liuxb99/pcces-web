/* 分包合約詳細頁面 — 基本資訊 + AG Grid 工項編輯 */

import React, { useEffect, useState, useCallback, useRef } from 'react';
import {
  Card, Button, Space, Modal, Form, Input, InputNumber, message,
  Typography, Descriptions, Spin, Tag, Popconfirm, Tooltip,
} from 'antd';
import {
  ReloadOutlined, ArrowLeftOutlined, PlusOutlined, DeleteOutlined,
  SaveOutlined, LinkOutlined, CheckCircleOutlined, CloseCircleOutlined,
} from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';

import { AgGridReact } from 'ag-grid-react';
import type { AgGridReact as AgGridReactType } from 'ag-grid-react';
import { AllCommunityModule, ModuleRegistry } from 'ag-grid-community';
import type { ColDef, CellValueChangedEvent } from 'ag-grid-community';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';

import { contractApi, projectApi } from '../api';
import type { Contract, ContractItem } from '../types';
import BudgetItemPicker from '../components/BudgetItemPicker';

const { Title, Text } = Typography;

ModuleRegistry.registerModules([AllCommunityModule]);

const statusConfig: Record<string, { color: string; label: string }> = {
  draft: { color: 'default', label: '草稿' },
  active: { color: 'processing', label: '進行中' },
  closed: { color: 'warning', label: '已結案' },
  finalized: { color: 'success', label: '已終驗' },
};

const ContractDetailPage: React.FC = () => {
  const { id: projectId, contractId } = useParams<{ id: string; contractId: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');
  const cid = parseInt(contractId || '0');

  const gridRef = useRef<AgGridReactType>(null);

  const [contract, setContract] = useState<Contract | null>(null);
  const [items, setItems] = useState<ContractItem[]>([]);
  const [project, setProject] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [pickerOpen, setPickerOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<ContractItem | null>(null);
  const [itemForm] = Form.useForm();
  const [saving, setSaving] = useState(false);

  // ── 載入資料 ──
  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const [proj, c, itemList] = await Promise.all([
        projectApi.get(pid),
        contractApi.get(pid, cid),
        contractApi.listItems(cid),
      ]);
      setProject(proj);
      setContract(c);
      setItems(itemList);
    } catch {
      message.error('載入合約資料失敗');
    } finally {
      setLoading(false);
    }
  }, [pid, cid]);

  useEffect(() => { fetchData(); }, [fetchData]);

  // ── 開啟工項編輯 ──
  const handleAddItem = () => {
    setEditingItem(null);
    itemForm.resetFields();
    setEditModalOpen(true);
  };

  const handleEditItem = (item: ContractItem) => {
    setEditingItem(item);
    itemForm.setFieldsValue(item);
    setEditModalOpen(true);
  };

  const handleSaveItem = async () => {
    try {
      const values = await itemForm.validateFields();
      setSaving(true);
      if (editingItem) {
        await contractApi.updateItem(cid, editingItem.id, values);
        message.success('工項已更新');
      } else {
        await contractApi.createItem(cid, values);
        message.success('工項已新增');
      }
      setEditModalOpen(false);
      fetchData();
    } catch (err: any) {
      if (err?.errorFields) return;
      message.error('儲存失敗');
    } finally {
      setSaving(false);
    }
  };

  // ── 刪除工項 ──
  const handleDeleteItem = async (itemId: number) => {
    try {
      await contractApi.deleteItem(cid, itemId);
      message.success('工項已刪除');
      fetchData();
    } catch {
      message.error('刪除失敗');
    }
  };

  // ── 批次匯入預算工項 ──
  const handleBatchImport = async (selectedIds: number[]) => {
    try {
      const result = await contractApi.batchCreateItems(cid, { budget_item_ids: selectedIds });
      message.success(`成功匯入 ${result.count} 筆工項`);
      setPickerOpen(false);
      fetchData();
    } catch (err: any) {
      message.error(err?.response?.data?.detail || '匯入失敗');
    }
  };

  // ── AG Grid Cell Edit ──
  const onCellValueChanged = useCallback(async (event: CellValueChangedEvent) => {
    const field = event.colDef.field;
    if (!field || !['contract_qty', 'unit_price', 'completed_qty'].includes(field)) return;
    const updatedRow = event.data as ContractItem;
    try {
      await contractApi.updateItem(cid, updatedRow.id, { [field]: (updatedRow as any)[field] });
      fetchData();
    } catch {
      message.error('更新失敗');
      event.api.refreshCells({ rowNodes: [event.node] });
    }
  }, [cid, fetchData]);

  // ── AG Grid 欄位 ──
  const columnDefs: ColDef[] = [
    { field: 'item_no', headerName: '編號', width: 90 },
    { field: 'print_no', headerName: '列印編號', width: 100 },
    { field: 'c_name', headerName: '工項名稱', width: 200, flex: 1 },
    { field: 'c_unit', headerName: '單位', width: 70 },
    {
      field: 'contract_qty', headerName: '合約數量', width: 110, type: 'numericColumn',
      editable: (params) => params.data && contract?.status !== 'finalized',
      cellEditor: 'agNumberCellEditor', cellEditorParams: { precision: 2, min: 0 },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'unit_price', headerName: '單價', width: 110, type: 'numericColumn',
      editable: (params) => params.data && contract?.status !== 'finalized',
      cellEditor: 'agNumberCellEditor', cellEditorParams: { precision: 2, min: 0 },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'amount', headerName: '金額', width: 120, type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'completed_qty', headerName: '完成數量', width: 110, type: 'numericColumn',
      editable: (params) => params.data && contract?.status !== 'finalized',
      cellEditor: 'agNumberCellEditor', cellEditorParams: { precision: 2, min: 0 },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'completed_amount', headerName: '完成金額', width: 120, type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'remark', headerName: '備註', width: 120, editable: true,
    },
    {
      headerName: '操作', width: 100,
      cellRenderer: ({ data }: { data: ContractItem }) => {
        if (!data) return null;
        return (
          <Space size="small">
            <Button type="link" size="small" onClick={() => handleEditItem(data)}>編輯</Button>
            <Popconfirm title="確定刪除此工項？" onConfirm={() => handleDeleteItem(data.id)}>
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
  if (!contract) {
    return <div style={{ textAlign: 'center', padding: 80 }}><Text type="danger">合約不存在</Text></div>;
  }

  const statusCfg = statusConfig[contract.status] || { color: 'default', label: contract.status };

  // 已匯入的 budget_item_id 集合
  const importedIds = new Set(items.filter(i => i.budget_item_id).map(i => i.budget_item_id!));

  return (
    <div>
      {/* 頁首 */}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Space>
          <Button icon={<ArrowLeftOutlined />} onClick={() => navigate(`/projects/${pid}/contracts`)}>返回列表</Button>
          <Title level={4} style={{ margin: 0 }}>
            <LinkOutlined style={{ marginRight: 8 }} />
            {contract.c_name}
          </Title>
          <Tag color={statusCfg.color}>{statusCfg.label}</Tag>
        </Space>
        <Space>
          {contract.status === 'draft' && (
            <Button type="primary" icon={<CheckCircleOutlined />} onClick={async () => {
              try {
                await contractApi.update(pid, cid, { status: 'active' } as any);
                message.success('合約已啟用');
                fetchData();
              } catch { message.error('啟用失敗'); }
            }}>啟用合約</Button>
          )}
          <Button icon={<ReloadOutlined />} onClick={fetchData}>重新整理</Button>
        </Space>
      </div>

      {/* 合約基本資訊 */}
      <Card size="small" style={{ marginBottom: 16 }}>
        <Descriptions size="small" column={4}>
          <Descriptions.Item label="合約編號">{contract.contract_no}</Descriptions.Item>
          <Descriptions.Item label="承包商">{contract.contractor || '-'}</Descriptions.Item>
          <Descriptions.Item label="合約金額">
            <Text strong>${(contract.contract_amount || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>
          </Descriptions.Item>
          <Descriptions.Item label="累計計價">
            <Text>${(contract.total_issue_amount || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>
          </Descriptions.Item>
          <Descriptions.Item label="結算金額">
            <Text>${(contract.settlement_amount || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>
          </Descriptions.Item>
          <Descriptions.Item label="開工日期">{contract.start_date || '-'}</Descriptions.Item>
          <Descriptions.Item label="完工日期">{contract.end_date || '-'}</Descriptions.Item>
          <Descriptions.Item label="工項數">{items.length}</Descriptions.Item>
        </Descriptions>
        {contract.remark && <div style={{ marginTop: 8 }}><Text type="secondary">備註：{contract.remark}</Text></div>}
      </Card>

      {/* 工項列表 */}
      <Card
        title={<Space><span>合約工項</span><Tag>{items.length} 筆</Tag></Space>}
        extra={
          contract.status !== 'finalized' && (
            <Space>
              <Button size="small" icon={<PlusOutlined />} onClick={() => setPickerOpen(true)}>
                從預算匯入
              </Button>
              <Button size="small" icon={<PlusOutlined />} onClick={handleAddItem}>
                新增工項
              </Button>
            </Space>
          )
        }
      >
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

      {/* 新增/編輯工項 Modal */}
      <Modal
        title={editingItem ? '編輯工項' : '新增工項'}
        open={editModalOpen}
        onOk={handleSaveItem}
        onCancel={() => setEditModalOpen(false)}
        confirmLoading={saving}
        okText="儲存"
        cancelText="取消"
      >
        <Form form={itemForm} layout="vertical" style={{ marginTop: 16 }}>
          <Form.Item name="c_name" label="工項名稱" rules={[{ required: true, message: '請輸入名稱' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="item_no" label="編號"><Input /></Form.Item>
          <Form.Item name="print_no" label="列印編號"><Input /></Form.Item>
          <Form.Item name="c_unit" label="單位"><Input /></Form.Item>
          <Form.Item name="contract_qty" label="合約數量">
            <InputNumber style={{ width: '100%' }} min={0} precision={2} />
          </Form.Item>
          <Form.Item name="unit_price" label="單價">
            <InputNumber style={{ width: '100%' }} min={0} precision={2} />
          </Form.Item>
          <Form.Item name="remark" label="備註"><Input.TextArea rows={2} /></Form.Item>
        </Form>
      </Modal>

      {/* 預算工項選取器 */}
      <BudgetItemPicker
        open={pickerOpen}
        projectId={pid}
        onCancel={() => setPickerOpen(false)}
        onConfirm={handleBatchImport}
        excludeIds={importedIds}
      />
    </div>
  );
};

export default ContractDetailPage;
