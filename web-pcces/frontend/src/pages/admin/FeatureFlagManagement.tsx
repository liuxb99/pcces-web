/* 功能開關管理（Admin Tab） */

import React, { useEffect, useState } from 'react';
import {
  Button, Table, Switch, Tag, Space, message, Popconfirm,
  Modal, Form, Input, Select, InputNumber, Typography, Alert,
} from 'antd';
import {
  PlusOutlined, EditOutlined, DeleteOutlined, StopOutlined,
  CheckCircleOutlined,
} from '@ant-design/icons';
import { adminApi } from '../../api';
import type { FeatureFlag, FeatureFlagCreateData, FeatureFlagUpdateData } from '../../types';

const { Text } = Typography;

// 分類標籤顏色對照
const CATEGORY_COLORS: Record<string, string> = {
  general: 'blue',
  budget: 'green',
  mrs: 'orange',
  invoice: 'purple',
  contract: 'cyan',
  compare: 'geekblue',
  report: 'magenta',
  admin: 'red',
};

const CATEGORY_LABELS: Record<string, string> = {
  general: '一般',
  budget: '預算',
  mrs: '單價庫',
  invoice: '計價',
  contract: '合約',
  compare: '比較',
  report: '報表',
  admin: '管理',
};

// 所有分類（用於篩選按鈕）
const CATEGORIES = [
  { key: 'all', label: '全部' },
  ...Object.entries(CATEGORY_LABELS).map(([k, v]) => ({ key: k, label: v })),
];

const FeatureFlagManagement: React.FC = () => {
  const [flags, setFlags] = useState<FeatureFlag[]>([]);
  const [loading, setLoading] = useState(false);
  const [categoryFilter, setCategoryFilter] = useState('all');
  const [modalVisible, setModalVisible] = useState(false);
  const [editingFlag, setEditingFlag] = useState<FeatureFlag | null>(null);
  const [form] = Form.useForm();

  /** 載入功能開關列表 */
  const loadFlags = async () => {
    setLoading(true);
    try {
      const params: { category?: string; per_page?: number } = { per_page: 200 };
      if (categoryFilter !== 'all') {
        params.category = categoryFilter;
      }
      const data = await adminApi.featureFlags.list(params);
      setFlags(data.flags);
    } catch (err: any) {
      message.error('載入功能開關失敗');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadFlags();
  }, [categoryFilter]);

  /** 切換啟用/停用 */
  const handleToggle = async (flag: FeatureFlag) => {
    if (flag.is_system) {
      message.warning('系統核心功能不可停用');
      return;
    }
    try {
      const updated = await adminApi.featureFlags.toggle(flag.id);
      setFlags((prev) => prev.map((f) => (f.id === flag.id ? updated : f)));
      message.success(`已${updated.is_enabled ? '啟用' : '停用'}「${updated.display_name}」`);
    } catch (err: any) {
      const errMsg = err?.response?.data?.error || '操作失敗';
      message.error(errMsg);
      loadFlags(); // 復原
    }
  };

  /** 開啟新增 Modal */
  const handleAdd = () => {
    setEditingFlag(null);
    form.resetFields();
    form.setFieldsValue({ is_enabled: true, is_system: false, category: 'general', sort_order: 0 });
    setModalVisible(true);
  };

  /** 開啟編輯 Modal */
  const handleEdit = (flag: FeatureFlag) => {
    setEditingFlag(flag);
    form.setFieldsValue({
      display_name: flag.display_name,
      description: flag.description,
      category: flag.category,
      is_enabled: flag.is_enabled,
      sort_order: flag.sort_order,
    });
    setModalVisible(true);
  };

  /** 送出表單（新增或編輯） */
  const handleSubmit = async () => {
    try {
      const values = await form.validateFields();
      if (editingFlag) {
        // 編輯
        const updateData: FeatureFlagUpdateData = {
          display_name: values.display_name,
          description: values.description,
          category: values.category,
          is_enabled: values.is_enabled,
          sort_order: values.sort_order,
        };
        const updated = await adminApi.featureFlags.update(editingFlag.id, updateData);
        setFlags((prev) => prev.map((f) => (f.id === updated.id ? updated : f)));
        message.success('已更新功能開關');
      } else {
        // 新增
        const createData: FeatureFlagCreateData = {
          flag_key: values.flag_key,
          display_name: values.display_name,
          description: values.description,
          category: values.category,
          is_enabled: values.is_enabled,
          is_system: values.is_system || false,
          sort_order: values.sort_order || 0,
        };
        const created = await adminApi.featureFlags.create(createData);
        setFlags((prev) => [...prev, created]);
        message.success('已新增功能開關');
      }
      setModalVisible(false);
    } catch (err: any) {
      if (err?.errorFields) return; // 表單驗證錯誤
      const errMsg = err?.response?.data?.error || '操作失敗';
      message.error(errMsg);
    }
  };

  /** 刪除功能開關 */
  const handleDelete = async (flag: FeatureFlag) => {
    try {
      await adminApi.featureFlags.delete(flag.id);
      setFlags((prev) => prev.filter((f) => f.id !== flag.id));
      message.success('已刪除功能開關');
    } catch (err: any) {
      const errMsg = err?.response?.data?.error || '刪除失敗';
      message.error(errMsg);
    }
  };

  // 表格欄位定義
  const columns = [
    {
      title: '功能代號',
      dataIndex: 'flag_key',
      key: 'flag_key',
      width: 180,
      render: (text: string, record: FeatureFlag) => (
        <Space>
          <Text code>{text}</Text>
          {record.is_system && <Tag color="red">系統</Tag>}
        </Space>
      ),
    },
    {
      title: '功能名稱',
      dataIndex: 'display_name',
      key: 'display_name',
      width: 200,
    },
    {
      title: '分類',
      dataIndex: 'category',
      key: 'category',
      width: 100,
      render: (cat: string) => (
        <Tag color={CATEGORY_COLORS[cat] || 'default'}>
          {CATEGORY_LABELS[cat] || cat}
        </Tag>
      ),
    },
    {
      title: '排序',
      dataIndex: 'sort_order',
      key: 'sort_order',
      width: 60,
      align: 'center' as const,
    },
    {
      title: '狀態',
      dataIndex: 'is_enabled',
      key: 'is_enabled',
      width: 120,
      render: (enabled: boolean, record: FeatureFlag) => (
        <Space>
          <Switch
            checked={enabled}
            disabled={record.is_system}
            onChange={() => handleToggle(record)}
            checkedChildren={<CheckCircleOutlined />}
            unCheckedChildren={<StopOutlined />}
          />
          <Text type={enabled ? 'success' : 'danger'}>
            {enabled ? '啟用' : '停用'}
          </Text>
        </Space>
      ),
    },
    {
      title: '操作',
      key: 'actions',
      width: 180,
      render: (_: any, record: FeatureFlag) => (
        <Space>
          <Button
            type="link"
            icon={<EditOutlined />}
            onClick={() => handleEdit(record)}
            size="small"
          >
            編輯
          </Button>
          {!record.is_system ? (
            <Popconfirm
              title="確認刪除此功能開關？"
              onConfirm={() => handleDelete(record)}
              okText="確認"
              cancelText="取消"
            >
              <Button
                type="link"
                danger
                icon={<DeleteOutlined />}
                size="small"
              >
                刪除
              </Button>
            </Popconfirm>
          ) : (
            <Text type="secondary" style={{ fontSize: 12 }}>系統核心</Text>
          )}
        </Space>
      ),
    },
  ];

  return (
    <div>
      <div style={{ marginBottom: 16, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <Space wrap>
          {CATEGORIES.map((cat) => (
            <Button
              key={cat.key}
              type={categoryFilter === cat.key ? 'primary' : 'default'}
              size="small"
              onClick={() => setCategoryFilter(cat.key)}
            >
              {cat.label}
            </Button>
          ))}
        </Space>
        <Button type="primary" icon={<PlusOutlined />} onClick={handleAdd}>
          新增功能開關
        </Button>
      </div>

      <Alert
        message="系統核心功能（標示「系統」者）不可停用或刪除。功能開關變更後，使用者需重新整理頁面才會生效。"
        type="info"
        showIcon
        style={{ marginBottom: 16 }}
      />

      <Table
        dataSource={flags}
        columns={columns}
        rowKey="id"
        loading={loading}
        pagination={false}
        size="middle"
      />

      {/* 新增/編輯 Modal */}
      <Modal
        title={editingFlag ? '編輯功能開關' : '新增功能開關'}
        open={modalVisible}
        onOk={handleSubmit}
        onCancel={() => setModalVisible(false)}
        okText="儲存"
        cancelText="取消"
        width={520}
      >
        <Form form={form} layout="vertical">
          {!editingFlag && (
            <Form.Item
              name="flag_key"
              label="功能代號"
              rules={[
                { required: true, message: '請輸入功能代號' },
                { pattern: /^[a-z_][a-z0-9_]*$/, message: '僅接受小寫英文、數字和底線' },
              ]}
            >
              <Input placeholder="例如：budget_compare" />
            </Form.Item>
          )}

          <Form.Item
            name="display_name"
            label="功能名稱"
            rules={[{ required: true, message: '請輸入功能名稱' }]}
          >
            <Input placeholder="例如：工項比較" />
          </Form.Item>

          <Form.Item name="description" label="功能說明">
            <Input.TextArea rows={2} placeholder="功能用途描述" />
          </Form.Item>

          <Form.Item name="category" label="分類" rules={[{ required: true }]}>
            <Select>
              {Object.entries(CATEGORY_LABELS).map(([k, v]) => (
                <Select.Option key={k} value={k}>{v}</Select.Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item name="sort_order" label="排序">
            <InputNumber min={0} style={{ width: '100%' }} />
          </Form.Item>

          <Form.Item name="is_enabled" label="啟用狀態" valuePropName="checked">
            <Switch checkedChildren="啟用" unCheckedChildren="停用" />
          </Form.Item>

          {!editingFlag && (
            <Form.Item name="is_system" label="系統核心" valuePropName="checked">
              <Switch checkedChildren="是" unCheckedChildren="否" />
            </Form.Item>
          )}
        </Form>
      </Modal>
    </div>
  );
};

export default FeatureFlagManagement;
