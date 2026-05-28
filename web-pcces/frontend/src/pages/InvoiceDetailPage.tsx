/* 計價明細頁面 — AG Grid 可編輯表格 */

import React, { useEffect, useState, useCallback, useRef } from 'react';
import {
  Card, Button, Space, Modal, Form, Input, DatePicker, InputNumber,
  message, Typography, Tag, Descriptions, Spin, Divider, Select, Tooltip,
} from 'antd';
import {
  ReloadOutlined, SendOutlined, CheckCircleOutlined,
  DownloadOutlined, FileTextOutlined, ArrowLeftOutlined,
  PlusOutlined, SaveOutlined, CalculatorOutlined,
} from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';

// AG Grid
import { AgGridReact } from 'ag-grid-react';
import type { AgGridReact as AgGridReactType } from 'ag-grid-react';
import { AllCommunityModule, ModuleRegistry } from 'ag-grid-community';
import type { ColDef, CellValueChangedEvent, ValueSetterParams } from 'ag-grid-community';
// AG Grid 樣式
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';

import { invoiceApi, projectApi, budgetApi } from '../api';
import type { Invoice, InvoiceItem } from '../types';

const { Title, Text } = Typography;

// 註冊 AG Grid 模組
ModuleRegistry.registerModules([AllCommunityModule]);

/** 狀態對應 */
const statusConfig: Record<string, { color: string; label: string }> = {
  draft: { color: 'default', label: '草稿' },
  submitted: { color: 'processing', label: '已提交' },
  approved: { color: 'success', label: '已核准' },
};

const InvoiceDetailPage: React.FC = () => {
  const { id: projectId, invoiceId } = useParams<{ id: string; invoiceId: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');
  const invId = parseInt(invoiceId || '0');

  const gridRef = useRef<AgGridReactType>(null);

  const [invoice, setInvoice] = useState<Invoice | null>(null);
  const [items, setItems] = useState<InvoiceItem[]>([]);
  const [project, setProject] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [batchModalOpen, setBatchModalOpen] = useState(false);
  const [batchLoading, setBatchLoading] = useState(false);

  // ── 載入資料 ──
  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const [proj, inv, itemList] = await Promise.all([
        projectApi.get(pid),
        invoiceApi.get(pid, invId),
        invoiceApi.listItems(invId),
      ]);
      setProject(proj);
      setInvoice(inv);
      setItems(itemList);
    } catch (err) {
      message.error('載入計價資料失敗');
      console.error(err);
    } finally {
      setLoading(false);
    }
  }, [pid, invId]);

  useEffect(() => { fetchData(); }, [fetchData]);

  // ── 重新整理 ──
  const handleRefresh = () => fetchData();

  // ── 重算 ──
  const handleRecalc = async () => {
    try {
      await invoiceApi.recalc(invId);
      message.success('金額重新計算完成');
      fetchData();
    } catch {
      message.error('重算失敗');
    }
  };

  // ── 提交審核 ──
  const handleSubmit = async () => {
    Modal.confirm({
      title: '提交審核',
      content: '確定要提交此計價單進行審核？提交後將無法編輯明細。',
      okText: '確認提交',
      cancelText: '取消',
      onOk: async () => {
        try {
          const updated = await invoiceApi.submit(invId);
          setInvoice(updated);
          message.success('已提交審核');
        } catch (err: any) {
          message.error(err?.response?.data?.detail || '提交失敗');
        }
      },
    });
  };

  // ── 核准 ──
  const handleApprove = async () => {
    Modal.confirm({
      title: '核准計價',
      content: '確定要核准此計價單？核准後將無法修改。',
      okText: '確認核准',
      cancelText: '取消',
      onOk: async () => {
        try {
          const updated = await invoiceApi.approve(invId);
          setInvoice(updated);
          message.success('已核准');
        } catch (err: any) {
          message.error(err?.response?.data?.detail || '核准失敗');
        }
      },
    });
  };

  // ── 明細變更（本期完成數量編輯） ──
  const onCellValueChanged = useCallback(async (event: CellValueChangedEvent) => {
    // 只處理 this_completed_qty 欄位
    if (event.colDef.field !== 'this_completed_qty') return;

    const updatedRow = event.data as InvoiceItem;
    try {
      await invoiceApi.updateItem(invId, updatedRow.id, {
        this_completed_qty: updatedRow.this_completed_qty,
      });
      // 更新後重新載入（讓後端計算的金額同步）
      fetchData();
    } catch (err: any) {
      message.error(err?.response?.data?.detail || '更新明細失敗');
      event.api.refreshCells({ rowNodes: [event.node] });
    }
  }, [invId, fetchData]);

  // ── 批次匯入預算項目 ──
  const handleBatchImport = async () => {
    setBatchLoading(true);
    try {
      const result = await invoiceApi.batchCreateItems(invId, { include_all_leaf: true });
      message.success(`成功匯入 ${result.count} 筆預算項目`);
      setBatchModalOpen(false);
      fetchData();
    } catch (err: any) {
      message.error(err?.response?.data?.detail || '匯入失敗');
    } finally {
      setBatchLoading(false);
    }
  };

  // ── 刪除明細 ──
  const handleDeleteItem = async (itemId: number) => {
    try {
      await invoiceApi.deleteItem(invId, itemId);
      message.success('明細已刪除');
      fetchData();
    } catch (err: any) {
      message.error(err?.response?.data?.detail || '刪除失敗');
    }
  };

  // ── 匯出 Excel ──
  const handleExportExcel = () => {
    const url = invoiceApi.getExportExcelUrl(invId);
    // 用 token 認證，做一個 fetch 然後下載
    const token = localStorage.getItem('pcces_token');
    fetch(url, { headers: { Authorization: `Bearer ${token}` } })
      .then((res) => {
        if (!res.ok) throw new Error('下載失敗');
        return res.blob();
      })
      .then((blob) => {
        const link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = `計價單_${invoice?.invoice_no || invId}.xlsx`;
        link.click();
        URL.revokeObjectURL(link.href);
        message.success('Excel 已下載');
      })
      .catch(() => message.error('匯出失敗'));
  };

  // ── AG Grid 欄位定義 ──
  const columnDefs: ColDef[] = [
    { field: 'item_no', headerName: '編號', width: 100 },
    {
      field: 'c_name',
      headerName: '項目名稱',
      width: 200,
      flex: 1,
    },
    { field: 'c_unit', headerName: '單位', width: 70 },
    {
      field: 'contract_qty',
      headerName: '合約數量',
      width: 100,
      type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'unit_price',
      headerName: '單價',
      width: 110,
      type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'prev_completed_qty',
      headerName: '前期完成',
      width: 100,
      type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'this_completed_qty',
      headerName: '本期完成',
      width: 110,
      type: 'numericColumn',
      editable: (params) => params.data && invoice?.status === 'draft',
      cellEditor: 'agNumberCellEditor',
      cellEditorParams: { precision: 2, min: 0 },
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'total_completed_qty',
      headerName: '累計完成',
      width: 100,
      type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'remain_qty',
      headerName: '剩餘數量',
      width: 100,
      type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'this_amount',
      headerName: '本期金額',
      width: 120,
      type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'cumulative_amount',
      headerName: '累計金額',
      width: 120,
      type: 'numericColumn',
      valueFormatter: (p) => p.value?.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) ?? '',
    },
    {
      field: 'progress_rate',
      headerName: '進度',
      width: 80,
      type: 'numericColumn',
      valueFormatter: (p) => p.value != null ? `${p.value.toFixed(1)}%` : '',
    },
    {
      field: 'remark',
      headerName: '備註',
      width: 120,
      editable: (params) => params.data && invoice?.status === 'draft',
    },
    {
      headerName: '操作',
      width: 70,
      cellRenderer: ({ data }: { data: InvoiceItem }) => {
        if (!data || invoice?.status !== 'draft') return null;
        return (
          <Button
            type="link"
            size="small"
            danger
            onClick={() => handleDeleteItem(data.id)}
          >
            刪除
          </Button>
        );
      },
    },
  ];

  // 預設排序
  const defaultColDef: ColDef = {
    resizable: true,
    sortable: true,
  };

  // ── 狀態標籤 ──
  const statusCfg = statusConfig[invoice?.status || 'draft'];

  if (loading) {
    return (
      <div style={{ textAlign: 'center', padding: 80 }}>
        <Spin size="large" />
        <div style={{ marginTop: 16 }}>載入中...</div>
      </div>
    );
  }

  if (!invoice) {
    return (
      <div style={{ textAlign: 'center', padding: 80 }}>
        <Text type="danger">計價單不存在</Text>
        <br />
        <Button type="link" onClick={() => navigate(`/projects/${pid}/invoices`)}>
          返回列表
        </Button>
      </div>
    );
  }

  return (
    <div>
      {/* 返回按鈕 + 標題 */}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Space>
          <Button
            icon={<ArrowLeftOutlined />}
            onClick={() => navigate(`/projects/${pid}/invoices`)}
          >
            返回列表
          </Button>
          <Title level={4} style={{ margin: 0 }}>
            <FileTextOutlined style={{ marginRight: 8 }} />
            {invoice.c_name || `第 ${invoice.period_no} 期計價`}
          </Title>
          <Tag color={statusCfg.color as any}>{statusCfg.label}</Tag>
        </Space>

        <Space>
          {/* 草稿狀態操作 */}
          {invoice.status === 'draft' && (
            <>
              <Button icon={<CalculatorOutlined />} onClick={handleRecalc}>
                重算金額
              </Button>
              <Button icon={<PlusOutlined />} onClick={() => setBatchModalOpen(true)}>
                匯入預算項目
              </Button>
              <Button type="primary" icon={<SendOutlined />} onClick={handleSubmit}>
                提交審核
              </Button>
            </>
          )}
          {/* 已提交狀態操作 */}
          {invoice.status === 'submitted' && (
            <Button
              type="primary"
              icon={<CheckCircleOutlined />}
              onClick={handleApprove}
            >
              核准
            </Button>
          )}
          {/* 通用操作 */}
          <Button icon={<DownloadOutlined />} onClick={handleExportExcel}>
            匯出 Excel
          </Button>
          <Tooltip title="在瀏覽器中預覽報表（新分頁）">
            <Button
              icon={<FileTextOutlined />}
              onClick={() => window.open(invoiceApi.getReportUrl(invId), '_blank')}
            >
              報表預覽
            </Button>
          </Tooltip>
          <Button icon={<ReloadOutlined />} onClick={handleRefresh}>
            重新整理
          </Button>
        </Space>
      </div>

      {/* 計價基本資訊 */}
      <Card size="small" style={{ marginBottom: 16 }}>
        <Descriptions size="small" column={4}>
          <Descriptions.Item label="計價單號">{invoice.invoice_no || '-'}</Descriptions.Item>
          <Descriptions.Item label="期別">第 {invoice.period_no} 期</Descriptions.Item>
          <Descriptions.Item label="本期金額">
            <Text strong>
              ${invoice.total_amount?.toLocaleString(undefined, { minimumFractionDigits: 2 })}
            </Text>
          </Descriptions.Item>
          <Descriptions.Item label="累計金額">
            <Text strong>
              ${invoice.cumulative_amount?.toLocaleString(undefined, { minimumFractionDigits: 2 })}
            </Text>
          </Descriptions.Item>
          <Descriptions.Item label="完成進度">
            <Text>{invoice.progress_rate.toFixed(1)}%</Text>
          </Descriptions.Item>
          <Descriptions.Item label="計價日期">{invoice.invoice_date || '-'}</Descriptions.Item>
          <Descriptions.Item label="明細筆數">{items.length}</Descriptions.Item>
          <Descriptions.Item label="建立時間">
            {invoice.created_at ? dayjs(invoice.created_at).format('YYYY/MM/DD HH:mm') : '-'}
          </Descriptions.Item>
        </Descriptions>
        {invoice.description && (
          <div style={{ marginTop: 8 }}>
            <Text type="secondary">說明：{invoice.description}</Text>
          </div>
        )}
        {invoice.remark && (
          <div>
            <Text type="secondary">備註：{invoice.remark}</Text>
          </div>
        )}
      </Card>

      {/* AG Grid 明細表格 */}
      <Card
        title={
          <Space>
            <span>計價明細</span>
            <Tag>{items.length} 筆</Tag>
          </Space>
        }
        extra={
          invoice.status === 'draft' && (
            <Space>
              <Button size="small" icon={<PlusOutlined />} onClick={() => setBatchModalOpen(true)}>
                匯入預算項目
              </Button>
            </Space>
          )
        }
      >
        <div
          className="ag-theme-alpine"
          style={{ height: Math.max(300, items.length * 42 + 52), width: '100%' }}
        >
          <AgGridReact
            ref={gridRef}
            rowData={items}
            columnDefs={columnDefs}
            defaultColDef={defaultColDef}
            onCellValueChanged={onCellValueChanged}
            animateRows
            enableCellTextSelection
            ensureDomOrder
            domLayout="autoHeight"
            suppressRowClickSelection
          />
        </div>
      </Card>

      {/* 匯入預算項目 Modal */}
      <Modal
        title="匯入預算項目"
        open={batchModalOpen}
        onOk={handleBatchImport}
        onCancel={() => setBatchModalOpen(false)}
        confirmLoading={batchLoading}
        okText="開始匯入"
        cancelText="取消"
      >
        <div style={{ padding: '16px 0' }}>
          <Text>
            將從專案中自動匯入所有「工作項目 (W)」類型的預算項目作為計價明細。
          </Text>
          <br />
          <Text type="secondary">
            已有對應明細的項目將略過，不會重複匯入。
          </Text>
        </div>
      </Modal>
    </div>
  );
};

export default InvoiceDetailPage;
