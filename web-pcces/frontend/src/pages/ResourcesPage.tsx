/* 資源管理頁面 — 含單價分析功能 */

import React, { useEffect, useState, useCallback } from 'react';
import {
  Card, Table, Button, Modal, Form, Input, InputNumber, Select, Space,
  message, Tag, Typography, Tooltip, Breadcrumb, Tabs, Switch,
  Progress, Popconfirm, Empty, Descriptions,
} from 'antd';
import {
  PlusOutlined, EditOutlined, DeleteOutlined, DownOutlined, RightOutlined,
  PercentageOutlined, CalculatorOutlined, ReloadOutlined, DownloadOutlined,
} from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import { resourceApi, projectApi } from '../api';
import type { Resource, ResourceBreakdownItem, ResourceWithAnalysis } from '../types';

const { Title, Text } = Typography;

/* ── 類別標籤對照 ── */
const categoryLabels: Record<string, { label: string; color: string }> = {
  labor: { label: '工', color: 'blue' },
  material: { label: '料', color: 'green' },
  equipment: { label: '機', color: 'orange' },
  other: { label: '其他', color: 'default' },
};

/* ── 輔助：金額格式化 ── */
const fmt = (v: number | null | undefined) =>
  `$${(v || 0).toLocaleString(undefined, { minimumFractionDigits: 0, maximumFractionDigits: 2 })}`;

/* ── 輔助：百分化 ── */
const fmtPct = (v: number | null | undefined) =>
  `${(v || 0).toFixed(1)}%`;

/* ════════════════════════════════════════════════
   單價分析細項管理子元件
   ════════════════════════════════════════════════ */
const BreakdownManager: React.FC<{
  pid: number;
  resource: ResourceWithAnalysis;
  onRefresh: () => void;
}> = ({ pid, resource, onRefresh }) => {
  const [items, setItems] = useState<ResourceBreakdownItem[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [form] = Form.useForm();

  const fetchItems = useCallback(async () => {
    setLoading(true);
    try {
      const data = await resourceApi.getBreakdown(pid, resource.id);
      setItems(data);
    } finally {
      setLoading(false);
    }
  }, [pid, resource.id]);

  useEffect(() => { fetchItems(); }, [fetchItems]);

  /* 新增細項 */
  const handleAdd = async () => {
    try {
      const values = await form.validateFields();
      const qty = values.quantity || 0;
      const up = values.unit_price || 0;
      await resourceApi.createBreakdown(pid, resource.id, {
        ...values,
        quantity: qty,
        unit_price: up,
      });
      message.success('細項已新增');
      setModalOpen(false);
      form.resetFields();
      fetchItems();
      onRefresh(); // 刷新上層列表
    } catch {
      message.error('新增失敗');
    }
  };

  /* 刪除細項 */
  const handleDelete = async (bid: number) => {
    try {
      await resourceApi.deleteBreakdown(pid, resource.id, bid);
      message.success('細項已刪除');
      fetchItems();
      onRefresh();
    } catch {
      message.error('刪除失敗');
    }
  };

  /* 細項表格欄位 */
  const itemColumns = [
    { title: '編碼', dataIndex: 'code', key: 'code', width: 90 },
    { title: '名稱', dataIndex: 'c_name', key: 'c_name', ellipsis: true },
    { title: '單位', dataIndex: 'c_unit', key: 'c_unit', width: 60 },
    { title: '數量', dataIndex: 'quantity', key: 'quantity', width: 80, align: 'right' as const,
      render: (v: number) => v.toLocaleString(),
    },
    { title: '單價', dataIndex: 'unit_price', key: 'unit_price', width: 100, align: 'right' as const,
      render: (v: number) => fmt(v),
    },
    { title: '金額', dataIndex: 'amount', key: 'amount', width: 120, align: 'right' as const,
      render: (v: number) => <Text strong>{fmt(v)}</Text>,
    },
    { title: '備註', dataIndex: 'remark', key: 'remark', ellipsis: true,
      render: (v: string | null) => v || '-',
    },
    {
      title: '操作', key: 'action', width: 60,
      render: (_: any, record: ResourceBreakdownItem) => (
        <Popconfirm title="確定刪除此細項？" onConfirm={() => handleDelete(record.id)}>
          <Button type="link" danger size="small" icon={<DeleteOutlined />} />
        </Popconfirm>
      ),
    },
  ];

  /* 比率顯示 */
  const rates = [
    { label: '人工', value: resource.labor_rate, color: '#1677ff' },
    { label: '材料', value: resource.material_rate, color: '#52c41a' },
    { label: '設備', value: resource.equipment_rate, color: '#fa8c16' },
    { label: '雜項', value: resource.misc_rate, color: '#722ed1' },
  ];
  const totalRate = rates.reduce((s, r) => s + (r.value || 0), 0);

  return (
    <div style={{ padding: '8px 0' }}>
      {/* 比率條 */}
      <Descriptions size="small" column={4} style={{ marginBottom: 8 }}>
        {rates.map(r => (
          <Descriptions.Item key={r.label} label={
            <span style={{ color: r.color }}>{r.label}</span>
          }>
            <span style={{ fontWeight: 600 }}>{fmtPct(r.value)}</span>
          </Descriptions.Item>
        ))}
        <Descriptions.Item label="合計">
          <Tag color={Math.abs(totalRate - 100) < 0.01 ? 'success' : 'warning'}>
            {fmtPct(totalRate)}
          </Tag>
        </Descriptions.Item>
      </Descriptions>

      {/* 細項列表 */}
      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 8 }}>
        <Text type="secondary">細項明細（{items.length} 項）</Text>
        <Space>
          <Text type="secondary">
            分析總額：<Text strong>{fmt(items.reduce((s, i) => s + (i.amount || 0), 0))}</Text>
            {' | '}原單價：<Text strong>{fmt(resource.unit_price)}</Text>
          </Text>
          <Button size="small" icon={<PlusOutlined />} onClick={() => { form.resetFields(); setModalOpen(true); }}>
            新增細項
          </Button>
        </Space>
      </div>

      <Table dataSource={items} columns={itemColumns} rowKey="id"
        loading={loading} pagination={false} size="small"
        locale={{ emptyText: <Empty description="尚無細項資料，請點擊「新增細項」建立" /> }}
      />

      {/* 新增細項 Modal */}
      <Modal title="新增單價分析細項" open={modalOpen} onOk={handleAdd}
        onCancel={() => setModalOpen(false)} width={520}>
        <Form form={form} layout="vertical">
          <Space style={{ width: '100%' }} size={12}>
            <Form.Item name="code" label="編碼" rules={[{ required: true }]}>
              <Input placeholder="e.g. L001" style={{ width: 130 }} />
            </Form.Item>
            <Form.Item name="c_name" label="名稱" rules={[{ required: true }]}>
              <Input placeholder="細項名稱" style={{ width: 200 }} />
            </Form.Item>
          </Space>
          <Space style={{ width: '100%' }} size={12}>
            <Form.Item name="c_unit" label="單位" rules={[{ required: true }]}>
              <Input placeholder="式" style={{ width: 80 }} />
            </Form.Item>
            <Form.Item name="quantity" label="數量" rules={[{ required: true }]}>
              <InputNumber min={0} precision={2} style={{ width: 120 }} />
            </Form.Item>
            <Form.Item name="unit_price" label="單價" rules={[{ required: true }]}>
              <InputNumber min={0} precision={2} style={{ width: 140 }} prefix="$" />
            </Form.Item>
          </Space>
          <Form.Item name="remark" label="備註">
            <Input.TextArea rows={2} />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

/* ════════════════════════════════════════════════
   主頁面
   ════════════════════════════════════════════════ */
const ResourcesPage: React.FC = () => {
  const { id: projectId } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');

  const [project, setProject] = useState<any>(null);
  const [resources, setResources] = useState<Resource[]>([]);
  const [analysisResources, setAnalysisResources] = useState<ResourceWithAnalysis[]>([]);
  const [loading, setLoading] = useState(true);
  const [analysisLoading, setAnalysisLoading] = useState(false);

  /* 新增 / 編輯資源 Modal */
  const [modalOpen, setModalOpen] = useState(false);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [editingResource, setEditingResource] = useState<ResourceWithAnalysis | null>(null);
  const [form] = Form.useForm();
  const [editForm] = Form.useForm();

  /* 展開控制 */
  const [expandedRows, setExpandedRows] = useState<number[]>([]);

  /* 載入基本資源列表 */
  const fetchResources = useCallback(async () => {
    setLoading(true);
    try {
      const [proj, res] = await Promise.all([
        projectApi.get(pid),
        resourceApi.list(pid),
      ]);
      setProject(proj);
      setResources(res);
    } finally {
      setLoading(false);
    }
  }, [pid]);

  /* 載入分析資源列表 */
  const fetchAnalysis = useCallback(async () => {
    setAnalysisLoading(true);
    try {
      const data = await resourceApi.listAnalysis(pid);
      setAnalysisResources(data);
    } finally {
      setAnalysisLoading(false);
    }
  }, [pid]);

  useEffect(() => {
    fetchResources();
    fetchAnalysis();
  }, [fetchResources, fetchAnalysis]);

  /* ── 新增資源 ── */
  const handleCreate = async () => {
    try {
      const values = await form.validateFields();
      await resourceApi.create(pid, values);
      message.success('資源已建立');
      setModalOpen(false);
      form.resetFields();
      fetchResources();
      fetchAnalysis();
    } catch {
      message.error('建立失敗');
    }
  };

  /* ── 開啟編輯 Modal ── */
  const openEdit = (r: ResourceWithAnalysis) => {
    setEditingResource(r);
    editForm.setFieldsValue({
      is_analysis: r.is_analysis,
      labor_rate: r.labor_rate,
      material_rate: r.material_rate,
      equipment_rate: r.equipment_rate,
      misc_rate: r.misc_rate,
      unit_price: r.unit_price,
    });
    setEditModalOpen(true);
  };

  /* ── 儲存編輯 ── */
  const handleEditSave = async () => {
    if (!editingResource) return;
    try {
      const values = await editForm.validateFields();
      await resourceApi.update(pid, editingResource.id, values);
      message.success('資源已更新');
      setEditModalOpen(false);
      setEditingResource(null);
      fetchResources();
      fetchAnalysis();
    } catch {
      message.error('更新失敗');
    }
  };

  /* ── 切換啟用分析 ── */
  const toggleAnalysis = async (r: ResourceWithAnalysis, checked: boolean) => {
    try {
      await resourceApi.update(pid, r.id, { is_analysis: checked });
      message.success(checked ? '已啟用單價分析' : '已停用單價分析');
      fetchResources();
      fetchAnalysis();
    } catch {
      message.error('操作失敗');
    }
  };

  /* ── 重新計算所有分析資源 ── */
  const handleRecalcAll = async () => {
    try {
      await resourceApi.recalcAnalysis(pid);
      message.success('所有分析資源已重新計算');
      fetchAnalysis();
    } catch {
      message.error('重新計算失敗');
    }
  };

  /* ── 展開／收合 ── */
  const toggleExpand = (id: number) => {
    setExpandedRows(prev =>
      prev.includes(id) ? prev.filter(x => x !== id) : [...prev, id]
    );
  };

  /* ══════════════════════════════════════
     基本資源表格欄位（與原相同）
     ══════════════════════════════════════ */
  const columns = [
    { title: '編碼', dataIndex: 'code', key: 'code', width: 100 },
    { title: '中文名稱', dataIndex: 'c_name', key: 'c_name', ellipsis: true },
    { title: '英文名稱', dataIndex: 'e_name', key: 'e_name', ellipsis: true,
      render: (v: string) => v || '-',
    },
    { title: '單位', dataIndex: 'c_unit', key: 'c_unit', width: 60 },
    { title: '類別', dataIndex: 'category', key: 'category', width: 80,
      render: (v: string) => {
        const cfg = categoryLabels[v] || categoryLabels.other;
        return <Tag color={cfg.color}>{cfg.label}</Tag>;
      },
    },
    { title: '單價', dataIndex: 'unit_price', key: 'unit_price', width: 120, align: 'right' as const,
      render: (v: number) => fmt(v),
    },
    { title: '分析', dataIndex: 'is_analysis', key: 'is_analysis', width: 70,
      render: (v: boolean) => v ? <Tag color="purple">啟用</Tag> : <Tag>—</Tag>,
    },
    { title: '公開', dataIndex: 'is_public', key: 'is_public', width: 60,
      render: (v: boolean) => v ? <Tag color="green">是</Tag> : <Tag>否</Tag>,
    },
    {
      title: '操作', key: 'action', width: 80,
      render: (_: any, r: any) => (
        <Tooltip title="編輯分析設定">
          <Button type="link" size="small" icon={<EditOutlined />}
            onClick={() => openEdit(r as ResourceWithAnalysis)} />
        </Tooltip>
      ),
    },
  ];

  /* ══════════════════════════════════════
     分析資源表格欄位
     ══════════════════════════════════════ */
  const analysisColumns = [
    {
      title: '', key: 'expand', width: 30,
      render: (_: any, r: ResourceWithAnalysis) => (
        <Button type="text" size="small"
          icon={expandedRows.includes(r.id) ? <DownOutlined /> : <RightOutlined />}
          onClick={() => toggleExpand(r.id)} />
      ),
    },
    { title: '編碼', dataIndex: 'code', key: 'code', width: 90 },
    { title: '名稱', dataIndex: 'c_name', key: 'c_name', ellipsis: true },
    { title: '單位', dataIndex: 'c_unit', key: 'c_unit', width: 50 },
    { title: '單價', dataIndex: 'unit_price', key: 'unit_price', width: 110, align: 'right' as const,
      render: (v: number) => <Text strong>{fmt(v)}</Text>,
    },
    {
      title: '分析總額', dataIndex: 'breakdown_total', key: 'breakdown_total',
      width: 110, align: 'right' as const,
      render: (v: number | undefined) => {
        if (v === undefined || v === null) return <Text type="secondary">—</Text>;
        return <Text>{fmt(v)}</Text>;
      },
    },
    {
      title: '比率', key: 'rates', width: 250,
      render: (_: any, r: ResourceWithAnalysis) => {
        const total = (r.labor_rate || 0) + (r.material_rate || 0) +
                      (r.equipment_rate || 0) + (r.misc_rate || 0);
        if (total === 0) return <Text type="secondary">未設定</Text>;
        return (
          <Space size={4} style={{ width: '100%' }}>
            <Tooltip title={`人工 ${fmtPct(r.labor_rate)}`}>
              <div style={{ width: `${Math.max((r.labor_rate || 0) / total * 100, 2)}%`,
                minWidth: 4, height: 14, background: '#1677ff', borderRadius: 2 }} />
            </Tooltip>
            <Tooltip title={`材料 ${fmtPct(r.material_rate)}`}>
              <div style={{ width: `${Math.max((r.material_rate || 0) / total * 100, 2)}%`,
                minWidth: 4, height: 14, background: '#52c41a', borderRadius: 2 }} />
            </Tooltip>
            <Tooltip title={`設備 ${fmtPct(r.equipment_rate)}`}>
              <div style={{ width: `${Math.max((r.equipment_rate || 0) / total * 100, 2)}%`,
                minWidth: 4, height: 14, background: '#fa8c16', borderRadius: 2 }} />
            </Tooltip>
            <Tooltip title={`雜項 ${fmtPct(r.misc_rate)}`}>
              <div style={{ width: `${Math.max((r.misc_rate || 0) / total * 100, 2)}%`,
                minWidth: 4, height: 14, background: '#722ed1', borderRadius: 2 }} />
            </Tooltip>
          </Space>
        );
      },
    },
    {
      title: '啟用', dataIndex: 'is_analysis', key: 'is_analysis', width: 70,
      render: (_: boolean, r: ResourceWithAnalysis) => (
        <Switch checked={r.is_analysis} size="small"
          onChange={(v) => toggleAnalysis(r, v)} />
      ),
    },
    {
      title: '操作', key: 'action', width: 100,
      render: (_: any, r: ResourceWithAnalysis) => (
        <Space>
          <Tooltip title="編輯分析設定">
            <Button type="link" size="small" icon={<EditOutlined />}
              onClick={() => openEdit(r)} />
          </Tooltip>
        </Space>
      ),
    },
  ];

  /* 分組 */
  const grouped = {
    labor: resources.filter(r => r.category === 'labor'),
    material: resources.filter(r => r.category === 'material'),
    equipment: resources.filter(r => r.category === 'equipment'),
    other: resources.filter(r => r.category === 'other'),
  };

  return (
    <div>
      <Breadcrumb items={[
        { title: <a onClick={() => navigate('/projects')}>專案</a> },
        { title: project?.name || `專案 #${pid}` },
        { title: '資源管理' },
      ]} style={{ marginBottom: 12 }} />

      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 16 }}>
        <Title level={4} style={{ margin: 0 }}>資源管理 — {project?.name}</Title>
        <Space>
          <Button icon={<DownloadOutlined />}>匯入</Button>
          <Button type="primary" icon={<PlusOutlined />}
            onClick={() => { form.resetFields(); setModalOpen(true); }}>
            新增資源
          </Button>
        </Space>
      </div>

      <Tabs defaultActiveKey="all" items={[
        /* ── 全部 ── */
        {
          key: 'all',
          label: `全部 (${resources.length})`,
          children: (
            <Card>
              <Table dataSource={resources} columns={columns} rowKey="id"
                loading={loading} pagination={{ pageSize: 20 }} size="small" />
            </Card>
          ),
        },
        /* ── 工 ── */
        {
          key: 'labor',
          label: `工 (${grouped.labor.length})`,
          children: (
            <Card>
              <Table dataSource={grouped.labor} columns={columns} rowKey="id"
                loading={loading} pagination={{ pageSize: 20 }} size="small" />
            </Card>
          ),
        },
        /* ── 料 ── */
        {
          key: 'material',
          label: `料 (${grouped.material.length})`,
          children: (
            <Card>
              <Table dataSource={grouped.material} columns={columns} rowKey="id"
                loading={loading} pagination={{ pageSize: 20 }} size="small" />
            </Card>
          ),
        },
        /* ── 機 ── */
        {
          key: 'equipment',
          label: `機 (${grouped.equipment.length})`,
          children: (
            <Card>
              <Table dataSource={grouped.equipment} columns={columns} rowKey="id"
                loading={loading} pagination={{ pageSize: 20 }} size="small" />
            </Card>
          ),
        },
        /* ── 單價分析 ── */
        {
          key: 'analysis',
          label: (
            <span>
              <CalculatorOutlined style={{ marginRight: 4 }} />
              單價分析 ({analysisResources.length})
            </span>
          ),
          children: (
            <Card>
              <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 12 }}>
                <Text type="secondary">
                  已啟用單價分析的資源列表。點擊展開可檢視細項明細。
                </Text>
                <Space>
                  <Button size="small" icon={<ReloadOutlined />}
                    onClick={handleRecalcAll}>
                    重新計算
                  </Button>
                </Space>
              </div>

              {analysisResources.length === 0 ? (
                <Empty description={
                  <span>
                    尚無啟用單價分析的資源。<br />
                    請在資源列表中點擊編輯按鈕，啟用「單價分析」功能。
                  </span>
                } />
              ) : (
                <Table dataSource={analysisResources} columns={analysisColumns} rowKey="id"
                  loading={analysisLoading} pagination={{ pageSize: 20 }} size="small"
                  expandable={{
                    expandedRowKeys: expandedRows,
                    onExpandedRowsChange: (keys: readonly React.Key[]) => setExpandedRows([...keys] as number[]),
                    expandedRowRender: (r: ResourceWithAnalysis) => (
                      <BreakdownManager pid={pid} resource={r}
                        onRefresh={fetchAnalysis} />
                    ),
                    rowExpandable: () => true,
                    showExpandColumn: false,
                  }} />
              )}
            </Card>
          ),
        },
      ]} />

      {/* ── 新增資源 Modal ── */}
      <Modal title="新增資源" open={modalOpen} onOk={handleCreate}
        onCancel={() => setModalOpen(false)}>
        <Form form={form} layout="vertical">
          <Space style={{ width: '100%' }} size={16}>
            <Form.Item name="code" label="編碼" rules={[{ required: true }]}>
              <Input placeholder="e.g. M001" />
            </Form.Item>
            <Form.Item name="category" label="類別" rules={[{ required: true }]} initialValue="material">
              <Select style={{ width: 120 }}>
                <Select.Option value="labor">工</Select.Option>
                <Select.Option value="material">料</Select.Option>
                <Select.Option value="equipment">機</Select.Option>
                <Select.Option value="other">其他</Select.Option>
              </Select>
            </Form.Item>
          </Space>
          <Form.Item name="c_name" label="中文名稱" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="e_name" label="英文名稱">
            <Input />
          </Form.Item>
          <Space style={{ width: '100%' }} size={16}>
            <Form.Item name="c_unit" label="單位" rules={[{ required: true }]}>
              <Input style={{ width: 100 }} />
            </Form.Item>
            <Form.Item name="e_unit" label="英文單位">
              <Input style={{ width: 100 }} />
            </Form.Item>
            <Form.Item name="unit_price" label="單價" rules={[{ required: true }]}>
              <InputNumber min={0} style={{ width: 140 }} prefix="$" />
            </Form.Item>
          </Space>
          <Form.Item name="remark" label="備註">
            <Input.TextArea rows={2} />
          </Form.Item>
        </Form>
      </Modal>

      {/* ── 編輯資源 Modal（分析設定） ── */}
      <Modal title={`編輯分析設定 — ${editingResource?.c_name || ''}`}
        open={editModalOpen} onOk={handleEditSave}
        onCancel={() => { setEditModalOpen(false); setEditingResource(null); }}
        width={520}>
        <Form form={editForm} layout="vertical">
          <Form.Item name="is_analysis" label="啟用單價分析" valuePropName="checked">
            <Switch />
          </Form.Item>
          <Form.Item name="unit_price" label="單價">
            <InputNumber min={0} precision={2} style={{ width: 200 }} prefix="$" />
          </Form.Item>
          <Text type="secondary" style={{ display: 'block', marginBottom: 8 }}>
            比率設定（加總應為 100%）
          </Text>
          <Space style={{ width: '100%' }} size={12}>
            <Form.Item name="labor_rate" label="人工比率 %">
              <InputNumber min={0} max={100} precision={1} style={{ width: 100 }}
                formatter={(v) => `${v}%`}
                parser={(v) => parseFloat(v?.replace('%', '') || '0') as any} />
            </Form.Item>
            <Form.Item name="material_rate" label="材料比率 %">
              <InputNumber min={0} max={100} precision={1} style={{ width: 100 }}
                formatter={(v) => `${v}%`}
                parser={(v) => parseFloat(v?.replace('%', '') || '0') as any} />
            </Form.Item>
          </Space>
          <Space style={{ width: '100%' }} size={12}>
            <Form.Item name="equipment_rate" label="設備比率 %">
              <InputNumber min={0} max={100} precision={1} style={{ width: 100 }}
                formatter={(v) => `${v}%`}
                parser={(v) => parseFloat(v?.replace('%', '') || '0') as any} />
            </Form.Item>
            <Form.Item name="misc_rate" label="雜項比率 %">
              <InputNumber min={0} max={100} precision={1} style={{ width: 100 }}
                formatter={(v) => `${v}%`}
                parser={(v) => parseFloat(v?.replace('%', '') || '0') as any} />
            </Form.Item>
          </Space>
        </Form>
      </Modal>
    </div>
  );
};

export default ResourcesPage;