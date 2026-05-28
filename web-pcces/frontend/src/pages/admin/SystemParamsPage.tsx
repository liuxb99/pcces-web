/* 系統參數設定 — 分類 E/F/G 切換，表格編輯 */

import React, { useEffect, useState } from 'react';
import {
  Tabs, Table, Button, Input, Modal, Form, Select, Switch, message, Popconfirm, Space,
} from 'antd';
import { PlusOutlined, EditOutlined, DeleteOutlined } from '@ant-design/icons';
import type { ColumnsType } from 'antd/es/table';
import { adminApi } from '../../api';
import type { SystemParameter } from '../../types';

const CATEGORIES = [
  { key: 'E', label: '系統參數 E' },
  { key: 'F', label: '系統參數 F' },
  { key: 'G', label: '系統參數 G' },
];

/* ── 新增/編輯 Modal ── */
const ParamEditModal: React.FC<{
  open: boolean;
  param: SystemParameter | null;
  category: string;
  onClose: () => void;
  onSuccess: () => void;
}> = ({ open, param, category, onClose, onSuccess }) => {
  const [form] = Form.useForm();
  const isEdit = !!param;
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    if (open && param) {
      form.setFieldsValue(param);
    } else if (open) {
      form.resetFields();
      form.setFieldsValue({ category, is_active: true });
    }
  }, [open, param, category, form]);

  const handleOk = async () => {
    try {
      const values = await form.validateFields();
      setSaving(true);
      if (isEdit) {
        await adminApi.updateParam(param!.id, values);
        message.success('參數已更新');
      } else {
        await adminApi.createParam(values);
        message.success('參數已建立');
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
    <Modal title={isEdit ? '編輯參數' : '新增參數'} open={open} onOk={handleOk} onCancel={onClose} confirmLoading={saving} destroyOnClose>
      <Form form={form} layout="vertical">
        <Form.Item name="category" label="分類">
          <Select
            options={CATEGORIES.map((c) => ({ value: c.key, label: c.label }))}
          />
        </Form.Item>
        <Form.Item name="code" label="代碼" rules={[{ required: true, message: '請輸入代碼' }]}>
          <Input placeholder="參數代碼" disabled={isEdit} />
        </Form.Item>
        <Form.Item name="c_name" label="名稱">
          <Input placeholder="參數名稱" />
        </Form.Item>
        <Form.Item name="c_value" label="值">
          <Input placeholder="參數值" />
        </Form.Item>
        <Form.Item name="c_default" label="預設值">
          <Input placeholder="預設值" />
        </Form.Item>
        <Form.Item name="sort_order" label="排序">
          <Input type="number" placeholder="0" />
        </Form.Item>
        <Form.Item name="memo" label="備註">
          <Input.TextArea rows={2} />
        </Form.Item>
        <Form.Item name="is_active" label="啟用" valuePropName="checked">
          <Switch />
        </Form.Item>
      </Form>
    </Modal>
  );
};

/* ── 主元件 ── */
const SystemParamsPage: React.FC = () => {
  const [params, setParams] = useState<SystemParameter[]>([]);
  const [loading, setLoading] = useState(false);
  const [activeCategory, setActiveCategory] = useState('E');
  const [modalOpen, setModalOpen] = useState(false);
  const [editingParam, setEditingParam] = useState<SystemParameter | null>(null);

  const fetchParams = async (category: string) => {
    setLoading(true);
    try {
      const res = await adminApi.listParams(category);
      setParams(res);
    } catch {
      message.error('載入系統參數失敗');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchParams(activeCategory);
  }, [activeCategory]);

  const handleDelete = async (id: number) => {
    try {
      await adminApi.deleteParam(id);
      message.success('參數已刪除');
      fetchParams(activeCategory);
    } catch {
      message.error('刪除失敗');
    }
  };

  const handleCellSave = async (id: number, field: string, value: any) => {
    try {
      await adminApi.updateParam(id, { [field]: value });
      message.success('已更新');
      fetchParams(activeCategory);
    } catch {
      message.error('更新失敗');
    }
  };

  const columns: ColumnsType<SystemParameter> = [
    { title: '代碼', dataIndex: 'code', key: 'code', width: 120 },
    { title: '名稱', dataIndex: 'c_name', key: 'c_name', width: 200, ellipsis: true },
    {
      title: '值',
      dataIndex: 'c_value',
      key: 'c_value',
      width: 200,
      render: (val: string | null, record: SystemParameter) => (
        <Input
          size="small"
          defaultValue={val || ''}
          onBlur={(e) => {
            if (e.target.value !== (val || '')) {
              handleCellSave(record.id, 'c_value', e.target.value);
            }
          }}
          style={{ width: '100%' }}
        />
      ),
    },
    {
      title: '預設值',
      dataIndex: 'c_default',
      key: 'c_default',
      width: 120,
      render: (v: string | null) => v || '-',
    },
    {
      title: '啟用',
      dataIndex: 'is_active',
      key: 'is_active',
      width: 80,
      render: (active: boolean, record: SystemParameter) => (
        <Switch
          checked={active}
          size="small"
          onChange={(val) => handleCellSave(record.id, 'is_active', val)}
        />
      ),
    },
    {
      title: '操作',
      key: 'actions',
      width: 100,
      render: (_: any, record: SystemParameter) => (
        <Space>
          <Button type="link" size="small" icon={<EditOutlined />}
            onClick={() => { setEditingParam(record); setModalOpen(true); }} />
          <Popconfirm title="確定刪除？" onConfirm={() => handleDelete(record.id)}>
            <Button type="link" size="small" danger icon={<DeleteOutlined />} />
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <div>
      <Tabs
        activeKey={activeCategory}
        onChange={(key) => { setActiveCategory(key); setEditingParam(null); }}
        tabBarExtraContent={
          <Button type="primary" icon={<PlusOutlined />} onClick={() => { setEditingParam(null); setModalOpen(true); }}>
            新增參數
          </Button>
        }
        items={CATEGORIES.map((c) => ({
          key: c.key,
          label: c.label,
          children: (
            <Table
              dataSource={params}
              columns={columns}
              rowKey="id"
              loading={loading}
              pagination={false}
              size="middle"
              bordered
            />
          ),
        }))}
      />

      <ParamEditModal
        open={modalOpen}
        param={editingParam}
        category={activeCategory}
        onClose={() => { setModalOpen(false); setEditingParam(null); }}
        onSuccess={() => { setModalOpen(false); setEditingParam(null); fetchParams(activeCategory); }}
      />
    </div>
  );
};

export default SystemParamsPage;
