/* 預算編輯器頁面（核心功能） */

import React, { useEffect, useState, useCallback } from 'react';
import {
  Card, Button, Space, Input, InputNumber, Select, Modal, Form,
  message, Spin, Tree, Table, Typography, Tag, Tabs, Tooltip, Popconfirm,
  Empty, Dropdown, Breadcrumb,
} from 'antd';
import {
  PlusOutlined, DeleteOutlined, EditOutlined, SaveOutlined,
  ReloadOutlined, FolderOpenOutlined, FileAddOutlined,
  DownloadOutlined, BarChartOutlined, ToolOutlined,
  ApartmentOutlined, TableOutlined,
} from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import type { DataNode } from 'antd/es/tree';
import type { ColumnsType } from 'antd/es/table';
import { budgetApi, projectApi, reportApi } from '../api';
import type { BudgetItem, BudgetItemCreateData, BudgetItemUpdateData, BudgetItemKind } from '../types';

const { Title, Text } = Typography;
const { Search } = Input;

/** 預算項目類型對應標籤 */
const kindLabels: Record<BudgetItemKind, { label: string; color: string }> = {
  B: { label: '主項', color: 'blue' },
  L: { label: '單價', color: 'green' },
  F: { label: '公式', color: 'orange' },
  S: { label: '分段', color: 'purple' },
  Z: { label: '小計', color: 'cyan' },
  U: { label: '自訂', color: 'geekblue' },
  W: { label: '工作', color: 'default' },
};

const BudgetEditorPage: React.FC = () => {
  const { id: projectId } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');

  const [project, setProject] = useState<any>(null);
  const [items, setItems] = useState<BudgetItem[]>([]);
  const [flatItems, setFlatItems] = useState<BudgetItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedItem, setSelectedItem] = useState<BudgetItem | null>(null);
  const [selectedKeys, setSelectedKeys] = useState<React.Key[]>([]);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<BudgetItem | null>(null);
  const [viewMode, setViewMode] = useState<'tree' | 'table'>('tree');
  const [searchText, setSearchText] = useState('');
  const [form] = Form.useForm();

  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const [proj, tree] = await Promise.all([
        projectApi.get(pid),
        budgetApi.getTree(pid),
      ]);
      setProject(proj);
      setItems(tree);
      // 平面化樹狀結構
      const flatten = (nodeList: BudgetItem[]): BudgetItem[] => {
        const result: BudgetItem[] = [];
        for (const node of nodeList) {
          result.push(node);
          if (node.children?.length) result.push(...flatten(node.children));
        }
        return result;
      };
      setFlatItems(flatten(tree));
    } catch (err) {
      message.error('載入預算資料失敗');
      console.error(err);
    } finally {
      setLoading(false);
    }
  }, [pid]);

  useEffect(() => { fetchData(); }, [fetchData]);

  // ─── 將 BudgetItem 樹轉為 Ant Design Tree 節點 ───
  const toTreeData = (nodeList: BudgetItem[]): DataNode[] => {
    return nodeList.map((item) => ({
      key: item.id,
      title: (
        <div className="budget-tree-node" style={{ display: 'flex', alignItems: 'center', gap: 6 }}>
          <Tag color={kindLabels[item.kind]?.color} style={{ marginRight: 0, lineHeight: '20px', fontSize: 11 }}>
            {kindLabels[item.kind]?.label || item.kind}
          </Tag>
          <span style={{ flex: 1, overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}>
            {item.print_no ? `[${item.print_no}] ` : ''}{item.c_name || '(無名稱)'}
          </span>
          <Text type="secondary" style={{ fontSize: 12 }}>
            ${(item.amount || 0).toLocaleString()}
          </Text>
        </div>
      ),
      children: item.children?.length ? toTreeData(item.children) : undefined,
      isLeaf: !item.children?.length,
    }));
  };

  // ─── 樹狀節點選擇 ───
  const onTreeSelect = (keys: React.Key[], info: any) => {
    setSelectedKeys(keys);
    const item = flatItems.find(i => i.id === keys[0]);
    setSelectedItem(item || null);
  };

  // ─── 表格欄位 ───
  const tableColumns: ColumnsType<BudgetItem> = [
    { title: '項次', dataIndex: 'print_no', key: 'print_no', width: 100 },
    { title: '編號', dataIndex: 'item_no', key: 'item_no', width: 100 },
    { title: '名稱', dataIndex: 'c_name', key: 'c_name', ellipsis: true,
      render: (v: string, r: BudgetItem) => (
        <Space size={4}>
          <Tag color={kindLabels[r.kind]?.color} style={{ fontSize: 10, lineHeight: '18px' }}>
            {kindLabels[r.kind]?.label || r.kind}
          </Tag>
          <span>{v || '(無名稱)'}</span>
        </Space>
      ),
    },
    { title: '單位', dataIndex: 'c_unit', key: 'c_unit', width: 60 },
    { title: '數量', dataIndex: 'quantity', key: 'quantity', width: 100, align: 'right' as const,
      render: (v: number) => v?.toLocaleString() ?? '-',
    },
    { title: '單價', dataIndex: 'unit_price', key: 'unit_price', width: 120, align: 'right' as const,
      render: (v: number) => v ? `$${v.toLocaleString()}` : '-',
    },
    { title: '複價', dataIndex: 'amount', key: 'amount', width: 140, align: 'right' as const,
      render: (v: number) => (
        <Text strong style={{ color: v ? '#52c41a' : undefined }}>
          ${(v || 0).toLocaleString()}
        </Text>
      ),
    },
    { title: '備註', dataIndex: 'memo', key: 'memo', width: 150, ellipsis: true },
    {
      title: '操作', key: 'action', width: 80, fixed: 'right' as const,
      render: (_: unknown, record: BudgetItem) => (
        <Space size={4}>
          <Tooltip title="編輯">
            <Button size="small" type="link" icon={<EditOutlined />}
              onClick={(e) => { e.stopPropagation(); openEditModal(record); }} />
          </Tooltip>
          <Tooltip title="新增子項">
            <Button size="small" type="link" icon={<FileAddOutlined />}
              onClick={(e) => { e.stopPropagation(); openCreateChild(record); }} />
          </Tooltip>
          <Popconfirm title="確定刪除此項目？" onConfirm={(e) => { e?.stopPropagation(); handleDelete(record); }}>
            <Tooltip title="刪除">
              <Button size="small" type="link" danger icon={<DeleteOutlined />}
                onClick={(e) => e.stopPropagation()} />
            </Tooltip>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  // ─── 新增根節點 ───
  const openCreateRoot = () => {
    setEditingItem(null);
    form.resetFields();
    form.setFieldsValue({ kind: 'B' });
    setModalOpen(true);
  };

  const openCreateChild = (parent: BudgetItem) => {
    setEditingItem(null);
    form.resetFields();
    form.setFieldsValue({ kind: 'W', parent_id: parent.id });
    setModalOpen(true);
  };

  const openEditModal = (item: BudgetItem) => {
    setEditingItem(item);
    form.setFieldsValue(item);
    setModalOpen(true);
  };

  // ─── 儲存預算項目 ───
  const handleSave = async () => {
    try {
      const values = await form.validateFields();
      if (editingItem) {
        await budgetApi.update(pid, editingItem.id, values as BudgetItemUpdateData);
        message.success('預算項目已更新');
      } else {
        await budgetApi.create(pid, values as BudgetItemCreateData);
        message.success('預算項目已建立');
      }
      setModalOpen(false);
      fetchData();
    } catch (err) {
      if (err && typeof err === 'object' && 'errorFields' in err) return; // 表單驗證錯誤
      message.error('儲存失敗');
    }
  };

  // ─── 刪除預算項目 ───
  const handleDelete = async (item: BudgetItem) => {
    try {
      await budgetApi.delete(pid, item.id);
      message.success('預算項目已刪除');
      if (selectedItem?.id === item.id) {
        setSelectedItem(null);
        setSelectedKeys([]);
      }
      fetchData();
    } catch {
      message.error('刪除失敗');
    }
  };

  // ─── 重新計算 ───
  const handleRecalc = async () => {
    try {
      await budgetApi.recalc(pid);
      message.success('預算重新計算完成');
      fetchData();
    } catch {
      message.error('重新計算失敗');
    }
  };

  // ─── 計算總額 ───
  const totalAmount = flatItems
    .filter(i => !i.parent_id)
    .reduce((sum, i) => sum + (i.amount || 0), 0);

  // ─── 總覽統計 ───
  const stats = {
    total: flatItems.length,
    root: flatItems.filter(i => !i.parent_id).length,
    byKind: {} as Record<string, number>,
  };
  flatItems.forEach(i => {
    stats.byKind[i.kind] = (stats.byKind[i.kind] || 0) + 1;
  });

  if (loading) return <Spin size="large" style={{ display: 'block', margin: '100px auto' }} />;

  return (
    <div>
      {/* 麵包屑 + 標題 */}
      <Breadcrumb items={[
        { title: <a onClick={() => navigate('/projects')}>專案</a> },
        { title: project?.name || `專案 #${pid}` },
        { title: '預算編輯' },
      ]} style={{ marginBottom: 12 }} />

      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Space align="center" size={16}>
          <Title level={4} style={{ margin: 0 }}>
            {project?.name || '預算編輯器'}
          </Title>
          <Text type="secondary">
            {flatItems.length} 項目 | 總額: <Text strong style={{ color: '#52c41a', fontSize: 16 }}>${totalAmount.toLocaleString()}</Text>
          </Text>
        </Space>
        <Space>
          <Tooltip title="切換檢視模式">
            <Button
              icon={viewMode === 'tree' ? <TableOutlined /> : <ApartmentOutlined />}
              onClick={() => setViewMode(viewMode === 'tree' ? 'table' : 'tree')}
            >
              {viewMode === 'tree' ? '表格' : '樹狀'}
            </Button>
          </Tooltip>
          <Button icon={<ReloadOutlined />} onClick={fetchData}>重新整理</Button>
          <Button icon={<SaveOutlined />} onClick={handleRecalc}>重新計算</Button>
          <Button type="primary" icon={<PlusOutlined />} onClick={openCreateRoot}>
            新增項目
          </Button>
        </Space>
      </div>

      {/* 統計標籤 */}
      <Card size="small" style={{ marginBottom: 16 }}>
        <Space size={24}>
          <Text>總項目: <strong>{stats.total}</strong></Text>
          <Text>根節點: <strong>{stats.root}</strong></Text>
          {Object.entries(kindLabels).map(([k, v]) => (
            stats.byKind[k] ? (
              <Tag key={k} color={v.color}>{v.label}: {stats.byKind[k]}</Tag>
            ) : null
          ))}
        </Space>
      </Card>

      {/* 搜尋 */}
      <Search
        placeholder="搜尋預算項目名稱..."
        allowClear
        onChange={(e) => setSearchText(e.target.value)}
        style={{ width: 320, marginBottom: 16 }}
      />

      {/* 編輯器主體 */}
      <Card style={{ minHeight: 500 }}>
        {flatItems.length === 0 ? (
          <Empty description="尚無預算項目，點擊「新增項目」開始建立">
            <Button type="primary" icon={<PlusOutlined />} onClick={openCreateRoot}>
              新增第一個項目
            </Button>
          </Empty>
        ) : viewMode === 'tree' ? (
          <div className="budget-editor">
            <div className="budget-tree-panel">
              <Tree
                treeData={toTreeData(items)}
                selectedKeys={selectedKeys}
                onSelect={onTreeSelect}
                defaultExpandAll
                showLine={{ showLeafIcon: false }}
                style={{ fontSize: 13 }}
              />
            </div>
            <div className="budget-table-panel">
              {selectedItem ? (
                <div>
                  <Title level={5} style={{ marginBottom: 16 }}>
                    {selectedItem.c_name || '(無名稱)'}
                    <Text type="secondary" style={{ fontWeight: 'normal', marginLeft: 12, fontSize: 13 }}>
                      ${(selectedItem.amount || 0).toLocaleString()}
                    </Text>
                  </Title>
                  <Table
                    dataSource={selectedItem.children?.length ? selectedItem.children :
                      flatItems.filter(i => i.parent_id === selectedItem.id)}
                    columns={tableColumns}
                    rowKey="id"
                    pagination={false}
                    size="small"
                    scroll={{ x: 900 }}
                  />
                </div>
              ) : (
                <div style={{ textAlign: 'center', padding: 60, color: '#999' }}>
                  <ApartmentOutlined style={{ fontSize: 48, marginBottom: 16 }} />
                  <p>請從左側樹狀結構選擇一個項目</p>
                </div>
              )}
            </div>
          </div>
        ) : (
          <Table
            dataSource={
              searchText
                ? flatItems.filter(i =>
                    i.c_name?.includes(searchText) || i.item_no?.includes(searchText)
                  )
                : flatItems
            }
            columns={tableColumns}
            rowKey="id"
            pagination={{ pageSize: 50, showSizeChanger: true, pageSizeOptions: ['20', '50', '100'] }}
            size="small"
            scroll={{ x: 1000, y: 500 }}
            onRow={(record) => ({
              onDoubleClick: () => openEditModal(record),
              style: { cursor: 'pointer' },
            })}
          />
        )}
      </Card>

      {/* 新增/編輯 Modal */}
      <Modal
        title={editingItem ? '編輯預算項目' : '新增預算項目'}
        open={modalOpen}
        onOk={handleSave}
        onCancel={() => setModalOpen(false)}
        width={700}
      >
        <Form form={form} layout="vertical">
          {!editingItem && (
            <Form.Item name="parent_id" label="上層項目（留空為根節點）">
              <Select
                allowClear
                showSearch
                placeholder="選擇上層項目（選填）"
                filterOption={(input, option) =>
                  (option?.label as string || '').toLowerCase().includes(input.toLowerCase())
                }
                options={flatItems.map(i => ({
                  value: i.id,
                  label: `${i.print_no ? `[${i.print_no}] ` : ''}${i.c_name || '(無名稱)'}`,
                }))}
              />
            </Form.Item>
          )}
          <Space style={{ width: '100%' }} size={16}>
            <Form.Item name="kind" label="項目類型" rules={[{ required: true }]}>
              <Select style={{ width: 120 }}>
                {Object.entries(kindLabels).map(([k, v]) => (
                  <Select.Option key={k} value={k}>{v.label}</Select.Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item name="print_no" label="項次">
              <Input placeholder="e.g. 0001.01" />
            </Form.Item>
            <Form.Item name="item_no" label="項目編號">
              <Input placeholder="e.g. 001" />
            </Form.Item>
          </Space>
          <Space style={{ width: '100%' }} size={16}>
            <Form.Item name="c_name" label="中文名稱" style={{ flex: 1 }}
              rules={[{ required: true, message: '請輸入名稱' }]}>
              <Input />
            </Form.Item>
            <Form.Item name="e_name" label="英文名稱" style={{ flex: 1 }}>
              <Input />
            </Form.Item>
          </Space>
          <Space style={{ width: '100%' }} size={16}>
            <Form.Item name="c_unit" label="單位">
              <Input style={{ width: 80 }} placeholder="式" />
            </Form.Item>
            <Form.Item name="quantity" label="數量">
              <InputNumber min={0} step={0.01} style={{ width: 140 }} />
            </Form.Item>
            <Form.Item name="unit_price" label="單價">
              <InputNumber min={0} step={1} style={{ width: 140 }}
                prefix="$" />
            </Form.Item>
            <Form.Item name="sort_order" label="排序碼">
              <Input placeholder="e.g. 001" style={{ width: 100 }} />
            </Form.Item>
          </Space>
          <Space style={{ width: '100%' }} size={16}>
            <Form.Item name="decimal_qty" label="數量小數位">
              <InputNumber min={0} max={6} style={{ width: 100 }} />
            </Form.Item>
            <Form.Item name="decimal_price" label="單價小數位">
              <InputNumber min={0} max={6} style={{ width: 100 }} />
            </Form.Item>
            <Form.Item name="decimal_amount" label="複價小數位">
              <InputNumber min={0} max={6} style={{ width: 100 }} />
            </Form.Item>
            <Form.Item name="is_fixed_price" label="固定單價">
              <Select style={{ width: 100 }}>
                <Select.Option value={false}>否</Select.Option>
                <Select.Option value={true}>是</Select.Option>
              </Select>
            </Form.Item>
          </Space>
          <Form.Item name="formula" label="公式（F/S/U 類型使用）">
            <Input.TextArea rows={2} placeholder="輸入公式表達式..." />
          </Form.Item>
          <Form.Item name="memo" label="備註">
            <Input.TextArea rows={2} />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default BudgetEditorPage;
