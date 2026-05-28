/* 組織機構管理 — 樹狀顯示 + 編輯面板 */

import React, { useEffect, useState } from 'react';
import {
  Card, Row, Col, Tree, Button, Form, Input, Select, Switch, message,
  Popconfirm, Typography, Empty, Spin, Space, Tag,
} from 'antd';
import { PlusOutlined, DeleteOutlined, ApartmentOutlined } from '@ant-design/icons';
import type { DataNode } from 'antd/es/tree';
import { adminApi } from '../../api';
import type { Organization } from '../../types';

const { Text, Title } = Typography;

/** 將組織陣列轉換為 Ant Design Tree 節點 */
const toTreeData = (orgs: Organization[]): DataNode[] => {
  return orgs.map((org) => ({
    key: `org-${org.id}`,
    title: (
      <Space>
        <ApartmentOutlined />
        <Text strong>{org.code}</Text>
        <Text>{org.c_name}</Text>
        <Tag color="blue" style={{ fontSize: 10 }}>{org.org_type}</Tag>
      </Space>
    ),
    children: org.children ? toTreeData(org.children) : [],
  }));
};

/** 在主陣列中遞迴尋找指定 id 的組織 */
const findOrgById = (orgs: Organization[], id: number): Organization | null => {
  for (const o of orgs) {
    if (o.id === id) return o;
    if (o.children) {
      const found = findOrgById(o.children, id);
      if (found) return found;
    }
  }
  return null;
};

/* ── 主元件 ── */
const OrganizationManage: React.FC = () => {
  const [orgs, setOrgs] = useState<Organization[]>([]);
  const [loading, setLoading] = useState(false);
  const [selectedOrg, setSelectedOrg] = useState<Organization | null>(null);
  const [selectedKey, setSelectedKey] = useState<string>('');
  const [form] = Form.useForm();
  const [saving, setSaving] = useState(false);

  const fetchOrgs = async () => {
    setLoading(true);
    try {
      const res = await adminApi.listOrganizations();
      setOrgs(res);
    } catch {
      message.error('載入組織失敗');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchOrgs();
  }, []);

  // 當選取節點變更時，更新編輯面板
  useEffect(() => {
    if (selectedKey) {
      const id = parseInt(selectedKey.replace('org-', ''), 10);
      const org = findOrgById(orgs, id);
      if (org) {
        setSelectedOrg(org);
        form.setFieldsValue(org);
      }
    } else {
      setSelectedOrg(null);
      form.resetFields();
    }
  }, [selectedKey, orgs, form]);

  const handleSelect = (keys: React.Key[]) => {
    setSelectedKey(keys[0] as string || '');
  };

  const handleSave = async () => {
    try {
      const values = await form.validateFields();
      setSaving(true);
      if (selectedOrg) {
        await adminApi.updateOrganization(selectedOrg.id, values);
        message.success('組織已更新');
      }
      fetchOrgs();
    } catch (err: any) {
      if (err?.response?.data?.detail) {
        message.error(err.response.data.detail);
      }
    } finally {
      setSaving(false);
    }
  };

  const handleCreateRoot = async () => {
    try {
      const values = await form.validateFields();
      if (!values.code || !values.c_name) {
        message.warning('請先填寫代碼與名稱');
        return;
      }
      setSaving(true);
      await adminApi.createOrganization({
        code: values.code,
        c_name: values.c_name,
        org_type: values.org_type || '機關',
      });
      message.success('根組織已建立');
      fetchOrgs();
    } catch (err: any) {
      if (err?.response?.data?.detail) {
        message.error(err.response.data.detail);
      }
    } finally {
      setSaving(false);
    }
  };

  const handleCreateChild = async () => {
    if (!selectedOrg) {
      message.warning('請先選取父組織');
      return;
    }
    try {
      const values = await form.validateFields();
      if (!values.code || !values.c_name) {
        message.warning('請先填寫代碼與名稱');
        return;
      }
      setSaving(true);
      await adminApi.createOrganization({
        parent_id: selectedOrg.id,
        code: values.code,
        c_name: values.c_name,
        org_type: values.org_type || '課室',
      });
      message.success('子組織已建立');
      fetchOrgs();
    } catch (err: any) {
      if (err?.response?.data?.detail) {
        message.error(err.response.data.detail);
      }
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async () => {
    if (!selectedOrg) return;
    try {
      await adminApi.deleteOrganization(selectedOrg.id);
      message.success('組織已刪除');
      setSelectedKey('');
      setSelectedOrg(null);
      fetchOrgs();
    } catch {
      message.error('刪除失敗');
    }
  };

  const treeData = toTreeData(orgs);

  return (
    <Row gutter={[16, 16]} style={{ minHeight: 400 }}>
      {/* 左側：組織樹 */}
      <Col xs={24} md={12}>
        <Card
          title="組織架構"
          size="small"
          extra={
            <Space>
              <Button size="small" type="primary" icon={<PlusOutlined />} onClick={handleCreateRoot}>
                新增根組織
              </Button>
            </Space>
          }
        >
          <Spin spinning={loading}>
            {orgs.length > 0 ? (
              <Tree
                treeData={treeData}
                selectedKeys={[selectedKey]}
                onSelect={handleSelect}
                defaultExpandAll
                showIcon
              />
            ) : (
              <Empty description="尚無組織資料">
                <Button type="primary" onClick={handleCreateRoot}>新增根組織</Button>
              </Empty>
            )}
          </Spin>
        </Card>
      </Col>

      {/* 右側：編輯面板 */}
      <Col xs={24} md={12}>
        <Card
          title={selectedOrg ? `編輯：${selectedOrg.c_name}` : '請選取組織節點'}
          size="small"
          extra={
            selectedOrg ? (
              <Space>
                <Button size="small" icon={<PlusOutlined />} onClick={handleCreateChild}>
                  新增子項
                </Button>
                <Popconfirm title="確定刪除此組織及其子組織？" onConfirm={handleDelete}>
                  <Button size="small" danger icon={<DeleteOutlined />}>刪除</Button>
                </Popconfirm>
              </Space>
            ) : null
          }
        >
          <Form form={form} layout="vertical">
            <Form.Item name="code" label="代碼" rules={[{ required: true, message: '請輸入代碼' }]}>
              <Input placeholder="組織代碼" />
            </Form.Item>
            <Form.Item name="c_name" label="名稱" rules={[{ required: true, message: '請輸入名稱' }]}>
              <Input placeholder="組織名稱" />
            </Form.Item>
            <Form.Item name="org_type" label="類型">
              <Select
                options={[
                  { value: '機關', label: '機關' },
                  { value: '部門', label: '部門' },
                  { value: '課室', label: '課室' },
                ]}
              />
            </Form.Item>
            <Form.Item name="sort_order" label="排序">
              <Input type="number" placeholder="0" />
            </Form.Item>
            <Form.Item name="contact_person" label="聯絡人">
              <Input placeholder="聯絡人" />
            </Form.Item>
            <Form.Item name="contact_phone" label="聯絡電話">
              <Input placeholder="電話" />
            </Form.Item>
            <Form.Item name="address" label="地址">
              <Input placeholder="地址" />
            </Form.Item>
            <Form.Item name="memo" label="備註">
              <Input.TextArea rows={2} />
            </Form.Item>
            <Form.Item name="is_active" label="啟用" valuePropName="checked">
              <Switch />
            </Form.Item>
            <Form.Item>
              <Button type="primary" onClick={handleSave} loading={saving} block disabled={!selectedOrg}>
                儲存變更
              </Button>
            </Form.Item>
          </Form>
        </Card>
      </Col>
    </Row>
  );
};

export default OrganizationManage;
