/* 代碼表管理 — 左側代碼表列表 + 右側代碼項樹狀管理 */

import React, { useEffect, useState } from 'react';
import {
  Card, Row, Col, List, Button, Modal, Form, Input, message, Typography, Space,
  Tree, Popconfirm, Tag, Empty, Spin,
} from 'antd';
import { PlusOutlined, EditOutlined, DeleteOutlined, FolderOutlined } from '@ant-design/icons';
import type { DataNode } from 'antd/es/tree';
import { adminApi } from '../../api';
import type { CodeTable, CodeItem } from '../../types';

const { Text, Title } = Typography;

/** 將代碼項陣列轉換為 Ant Design Tree 節點 */
const toTreeData = (items: CodeItem[]): DataNode[] => {
  return items.map((item) => ({
    key: `item-${item.id}`,
    title: `${item.code} ${item.c_name}`,
    icon: <FolderOutlined />,
    children: item.children ? toTreeData(item.children) : [],
  }));
};

/* ── 代碼表編輯 Modal ── */
const CodeTableEditModal: React.FC<{
  open: boolean;
  table: CodeTable | null;
  onClose: () => void;
  onSuccess: () => void;
}> = ({ open, table, onClose, onSuccess }) => {
  const [form] = Form.useForm();
  const isEdit = !!table;
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    if (open && table) {
      form.setFieldsValue(table);
    } else if (open) {
      form.resetFields();
    }
  }, [open, table, form]);

  const handleOk = async () => {
    try {
      const values = await form.validateFields();
      setSaving(true);
      if (isEdit) {
        await adminApi.updateCodeTable(table!.id, values);
        message.success('代碼表已更新');
      } else {
        await adminApi.createCodeTable(values);
        message.success('代碼表已建立');
      }
      onSuccess();
    } catch (err: any) {
      if (err?.response?.data?.detail) {
        message.error(err.response.data.detail);
      }
    } finally {
      setSaving(false);
    }
  };

  return (
    <Modal title={isEdit ? '編輯代碼表' : '新增代碼表'} open={open} onOk={handleOk} onCancel={onClose} confirmLoading={saving} destroyOnClose>
      <Form form={form} layout="vertical">
        <Form.Item name="table_code" label="代碼表識別碼" rules={[{ required: true, message: '請輸入識別碼' }]}>
          <Input placeholder="例如 DEPT, ASSET" disabled={isEdit} />
        </Form.Item>
        <Form.Item name="table_name" label="代碼表名稱">
          <Input placeholder="例如 部門編碼, 公物編碼" />
        </Form.Item>
        <Form.Item name="memo" label="備註">
          <Input.TextArea rows={2} />
        </Form.Item>
      </Form>
    </Modal>
  );
};

/* ── 代碼項編輯 Modal ── */
const CodeItemEditModal: React.FC<{
  open: boolean;
  parentId: number | null;
  item: CodeItem | null;
  onClose: () => void;
  onSuccess: () => void;
  tableId: number;
}> = ({ open, parentId, item, onClose, onSuccess, tableId }) => {
  const [form] = Form.useForm();
  const isEdit = !!item;
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    if (open && item) {
      form.setFieldsValue(item);
    } else if (open) {
      form.resetFields();
    }
  }, [open, item, form]);

  const handleOk = async () => {
    try {
      const values = await form.validateFields();
      setSaving(true);
      if (isEdit) {
        await adminApi.updateCodeItem(item!.id, values);
        message.success('代碼項已更新');
      } else {
        await adminApi.createCodeItem(tableId, { ...values, parent_id: parentId });
        message.success('代碼項已建立');
      }
      onSuccess();
    } catch (err: any) {
      if (err?.response?.data?.detail) {
        message.error(err.response.data.detail);
      }
    } finally {
      setSaving(false);
    }
  };

  return (
    <Modal title={isEdit ? '編輯代碼項' : '新增代碼項'} open={open} onOk={handleOk} onCancel={onClose} confirmLoading={saving} destroyOnClose>
      <Form form={form} layout="vertical">
        {!isEdit && parentId && <Text type="secondary">父項目 ID: {parentId}</Text>}
        <Form.Item name="code" label="代碼" rules={[{ required: true, message: '請輸入代碼' }]}>
          <Input placeholder="代碼" />
        </Form.Item>
        <Form.Item name="c_name" label="名稱" rules={[{ required: true, message: '請輸入名稱' }]}>
          <Input placeholder="名稱" />
        </Form.Item>
        <Form.Item name="sort_order" label="排序">
          <Input type="number" placeholder="0" />
        </Form.Item>
        <Form.Item name="memo" label="備註">
          <Input.TextArea rows={2} />
        </Form.Item>
      </Form>
    </Modal>
  );
};

/* ── 主元件 ── */
const CodeTableManagement: React.FC = () => {
  const [tables, setTables] = useState<CodeTable[]>([]);
  const [selectedTable, setSelectedTable] = useState<CodeTable | null>(null);
  const [codeItems, setCodeItems] = useState<CodeItem[]>([]);
  const [loading, setLoading] = useState(false);
  const [itemsLoading, setItemsLoading] = useState(false);

  // Modal 狀態
  const [tableModalOpen, setTableModalOpen] = useState(false);
  const [editingTable, setEditingTable] = useState<CodeTable | null>(null);
  const [itemModalOpen, setItemModalOpen] = useState(false);
  const [editingItem, setEditingItem] = useState<CodeItem | null>(null);
  const [itemParentId, setItemParentId] = useState<number | null>(null);

  const fetchTables = async () => {
    setLoading(true);
    try {
      const res = await adminApi.listCodeTables();
      setTables(res);
    } catch {
      message.error('載入代碼表失敗');
    } finally {
      setLoading(false);
    }
  };

  const fetchItems = async (tableId: number) => {
    setItemsLoading(true);
    try {
      const res = await adminApi.listCodeItems(tableId);
      setCodeItems(res);
    } catch {
      message.error('載入代碼項失敗');
    } finally {
      setItemsLoading(false);
    }
  };

  useEffect(() => {
    fetchTables();
  }, []);

  useEffect(() => {
    if (selectedTable) {
      fetchItems(selectedTable.id);
    } else {
      setCodeItems([]);
    }
  }, [selectedTable]);

  const openTableEdit = (t?: CodeTable) => {
    setEditingTable(t || null);
    setTableModalOpen(true);
  };

  const openItemEdit = (item?: CodeItem, parentId?: number | null) => {
    setEditingItem(item || null);
    setItemParentId(parentId ?? null);
    setItemModalOpen(true);
  };

  const handleDeleteTable = async (id: number) => {
    try {
      await adminApi.deleteCodeTable(id);
      message.success('代碼表已刪除');
      if (selectedTable?.id === id) setSelectedTable(null);
      fetchTables();
    } catch {
      message.error('刪除失敗');
    }
  };

  const handleDeleteItem = async (id: number) => {
    try {
      await adminApi.deleteCodeItem(id);
      message.success('代碼項已刪除');
      if (selectedTable) fetchItems(selectedTable.id);
    } catch {
      message.error('刪除失敗');
    }
  };

  const treeData = toTreeData(codeItems);

  return (
    <Row gutter={[16, 16]} style={{ minHeight: 400 }}>
      {/* 左側：代碼表列表 */}
      <Col xs={24} md={8}>
        <Card
          title="代碼表"
          size="small"
          extra={<Button type="primary" size="small" icon={<PlusOutlined />} onClick={() => openTableEdit()}>新增</Button>}
        >
          <Spin spinning={loading}>
            <List
              dataSource={tables}
              renderItem={(t) => (
                <List.Item
                  onClick={() => setSelectedTable(t)}
                  style={{
                    cursor: 'pointer',
                    background: selectedTable?.id === t.id ? '#e6f4ff' : undefined,
                    padding: '8px 12px',
                  }}
                  actions={[
                    <Button type="link" size="small" icon={<EditOutlined />} onClick={(e) => { e.stopPropagation(); openTableEdit(t); }} />,
                    <Popconfirm title="確定刪除？" onConfirm={() => handleDeleteTable(t.id)} key="del">
                      <Button type="link" size="small" danger icon={<DeleteOutlined />} onClick={(e) => e.stopPropagation()} />
                    </Popconfirm>,
                  ]}
                >
                  <List.Item.Meta
                    title={<Text strong>{t.table_code}</Text>}
                    description={<Text type="secondary">{t.table_name || '（無名稱）'}</Text>}
                  />
                </List.Item>
              )}
              locale={{ emptyText: <Empty description="尚無代碼表" /> }}
            />
          </Spin>
        </Card>
      </Col>

      {/* 右側：代碼項樹狀管理 */}
      <Col xs={24} md={16}>
        <Card
          title={selectedTable ? `代碼項 — ${selectedTable.table_code}` : '請選擇代碼表'}
          size="small"
          extra={
            selectedTable ? (
              <Space>
                <Button size="small" icon={<PlusOutlined />} onClick={() => openItemEdit(undefined, null)}>
                  新增根項
                </Button>
              </Space>
            ) : null
          }
        >
          {selectedTable ? (
            <Spin spinning={itemsLoading}>
              {codeItems.length > 0 ? (
                <Tree
                  treeData={treeData}
                  defaultExpandAll
                  showIcon
                  titleRender={(node: any) => {
                    const itemId = parseInt(node.key.replace('item-', ''), 10);
                    return (
                      <Space>
                        <span>{node.title}</span>
                        <Button type="link" size="small" icon={<EditOutlined />}
                          onClick={() => {
                            const item = codeItems.find((ci) => ci.id === itemId);
                            if (item) openItemEdit(item);
                          }} />
                        <Button type="link" size="small" icon={<PlusOutlined />}
                          onClick={() => openItemEdit(undefined, itemId)} />
                        <Popconfirm title="確定刪除此項目？" onConfirm={() => handleDeleteItem(itemId)}>
                          <Button type="link" size="small" danger icon={<DeleteOutlined />} />
                        </Popconfirm>
                      </Space>
                    );
                  }}
                />
              ) : (
                <Empty description="尚無代碼項，請新增">
                  <Button type="primary" onClick={() => openItemEdit(undefined, null)}>
                    新增根項
                  </Button>
                </Empty>
              )}
            </Spin>
          ) : (
            <Empty description="請從左側選擇一個代碼表" />
          )}
        </Card>
      </Col>

      {/* Modals */}
      <CodeTableEditModal
        open={tableModalOpen}
        table={editingTable}
        onClose={() => { setTableModalOpen(false); setEditingTable(null); }}
        onSuccess={() => { setTableModalOpen(false); setEditingTable(null); fetchTables(); }}
      />
      <CodeItemEditModal
        open={itemModalOpen}
        parentId={itemParentId}
        item={editingItem}
        tableId={selectedTable?.id || 0}
        onClose={() => { setItemModalOpen(false); setEditingItem(null); }}
        onSuccess={() => { setItemModalOpen(false); setEditingItem(null); if (selectedTable) fetchItems(selectedTable.id); }}
      />
    </Row>
  );
};

export default CodeTableManagement;
