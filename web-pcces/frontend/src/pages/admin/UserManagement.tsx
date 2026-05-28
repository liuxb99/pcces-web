/* 使用者管理列表 */

import React, { useEffect, useState } from 'react';
import {
  Table, Button, Input, Select, Space, Tag, Switch, Popconfirm, message, Card, Row, Col,
} from 'antd';
import { PlusOutlined, SearchOutlined, ReloadOutlined } from '@ant-design/icons';
import type { ColumnsType } from 'antd/es/table';
import { adminApi } from '../../api';
import type { User } from '../../types';
import UserEditForm from './UserEditForm';

const UserManagement: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [total, setTotal] = useState(0);
  const [loading, setLoading] = useState(false);
  const [page, setPage] = useState(1);
  const [perPage] = useState(20);
  const [q, setQ] = useState('');
  const [roleFilter, setRoleFilter] = useState<string>('');
  const [activeFilter, setActiveFilter] = useState<string>('');
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [editingUser, setEditingUser] = useState<User | null>(null);

  const fetchUsers = async () => {
    setLoading(true);
    try {
      const res = await adminApi.listUsers({
        q: q || undefined,
        role: roleFilter || undefined,
        is_active: activeFilter || undefined,
        page,
        per_page: perPage,
      });
      setUsers(res.users);
      setTotal(res.total);
    } catch {
      message.error('載入使用者列表失敗');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUsers();
  }, [page, roleFilter, activeFilter]);

  const handleSearch = () => {
    setPage(1);
    fetchUsers();
  };

  const handleToggleActive = async (id: number) => {
    try {
      await adminApi.toggleUserActive(id);
      message.success('狀態已更新');
      fetchUsers();
    } catch {
      message.error('更新失敗');
    }
  };

  const handleChangeRole = async (id: number, role: string) => {
    try {
      await adminApi.changeUserRole(id, role);
      message.success('角色已變更');
      fetchUsers();
    } catch {
      message.error('角色變更失敗');
    }
  };

  const handleDelete = async (id: number) => {
    try {
      await adminApi.deleteUser(id);
      message.success('使用者已刪除');
      fetchUsers();
    } catch {
      message.error('刪除失敗');
    }
  };

  const openEditModal = (user?: User) => {
    setEditingUser(user || null);
    setEditModalOpen(true);
  };

  const columns: ColumnsType<User> = [
    { title: '帳號', dataIndex: 'username', key: 'username', width: 120 },
    { title: '姓名', dataIndex: 'display_name', key: 'display_name', width: 120 },
    { title: 'Email', dataIndex: 'email', key: 'email', width: 180, ellipsis: true },
    { title: '公司', dataIndex: 'company', key: 'company', width: 150, ellipsis: true },
    { title: '部門', dataIndex: 'department', key: 'department', width: 120, ellipsis: true },
    {
      title: '角色',
      dataIndex: 'role',
      key: 'role',
      width: 140,
      render: (role: string, record: User) => (
        <Select
          value={role}
          size="small"
          style={{ width: 110 }}
          onChange={(val) => handleChangeRole(record.id, val)}
          options={[
            { value: 'admin', label: <Tag color="red">admin</Tag> },
            { value: 'reviewer', label: <Tag color="blue">reviewer</Tag> },
            { value: 'editor', label: <Tag color="green">editor</Tag> },
            { value: 'viewer', label: <Tag color="default">viewer</Tag> },
          ]}
        />
      ),
    },
    {
      title: '啟用',
      dataIndex: 'is_active',
      key: 'is_active',
      width: 80,
      render: (active: boolean, record: User) => (
        <Switch
          checked={active}
          size="small"
          onChange={() => handleToggleActive(record.id)}
        />
      ),
    },
    {
      title: '建立時間',
      dataIndex: 'created_at',
      key: 'created_at',
      width: 160,
      render: (v: string) => v ? new Date(v).toLocaleString('zh-TW') : '',
    },
    {
      title: '操作',
      key: 'actions',
      width: 140,
      render: (_: any, record: User) => (
        <Space>
          <Button type="link" size="small" onClick={() => openEditModal(record)}>
            編輯
          </Button>
          <Popconfirm
            title="確定刪除此使用者？"
            onConfirm={() => handleDelete(record.id)}
            okText="確定"
            cancelText="取消"
          >
            <Button type="link" size="small" danger disabled={record.role === 'admin'}>
              刪除
            </Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <div>
      <Card style={{ marginBottom: 16 }}>
        <Row gutter={[16, 16]} align="middle">
          <Col>
            <Input
              placeholder="搜尋帳號/姓名/Email"
              prefix={<SearchOutlined />}
              value={q}
              onChange={(e) => setQ(e.target.value)}
              onPressEnter={handleSearch}
              style={{ width: 260 }}
              allowClear
            />
          </Col>
          <Col>
            <Select
              placeholder="角色篩選"
              value={roleFilter || undefined}
              onChange={(val) => setRoleFilter(val || '')}
              allowClear
              style={{ width: 130 }}
              options={[
                { value: 'admin', label: '管理員' },
                { value: 'reviewer', label: '審核者' },
                { value: 'editor', label: '編輯者' },
                { value: 'viewer', label: '唯讀' },
              ]}
            />
          </Col>
          <Col>
            <Select
              placeholder="狀態篩選"
              value={activeFilter || undefined}
              onChange={(val) => setActiveFilter(val || '')}
              allowClear
              style={{ width: 120 }}
              options={[
                { value: 'true', label: '啟用' },
                { value: 'false', label: '停用' },
              ]}
            />
          </Col>
          <Col>
            <Space>
              <Button onClick={handleSearch}>查詢</Button>
              <Button icon={<ReloadOutlined />} onClick={fetchUsers}>重新整理</Button>
            </Space>
          </Col>
          <Col flex="auto" style={{ textAlign: 'right' }}>
            <Button type="primary" icon={<PlusOutlined />} onClick={() => openEditModal()}>
              新增使用者
            </Button>
          </Col>
        </Row>
      </Card>

      <Table
        dataSource={users}
        columns={columns}
        rowKey="id"
        loading={loading}
        pagination={{
          current: page,
          pageSize: perPage,
          total,
          onChange: (p) => setPage(p),
          showSizeChanger: false,
          showTotal: (t) => `共 ${t} 人`,
        }}
        size="middle"
        bordered
      />

      <UserEditForm
        open={editModalOpen}
        user={editingUser}
        onClose={() => {
          setEditModalOpen(false);
          setEditingUser(null);
        }}
        onSuccess={() => {
          setEditModalOpen(false);
          setEditingUser(null);
          fetchUsers();
        }}
      />
    </div>
  );
};

export default UserManagement;
