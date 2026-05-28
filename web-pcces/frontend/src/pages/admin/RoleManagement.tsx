/* 角色/權限管理 — 檢視與變更使用者角色 */

import React, { useEffect, useState } from 'react';
import { Table, Card, Row, Col, Statistic, Tag, Select, message, Spin } from 'antd';
import { UserOutlined, SafetyOutlined, EditOutlined, EyeOutlined } from '@ant-design/icons';
import type { ColumnsType } from 'antd/es/table';
import { adminApi } from '../../api';
import type { User } from '../../types';

const RoleManagement: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(false);

  const fetchUsers = async () => {
    setLoading(true);
    try {
      // 取得所有使用者（不分頁）
      const res = await adminApi.listUsers({ per_page: 200 });
      setUsers(res.users);
    } catch {
      message.error('載入使用者失敗');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  const handleChangeRole = async (id: number, role: string) => {
    try {
      await adminApi.changeUserRole(id, role);
      message.success('角色已變更');
      fetchUsers();
    } catch {
      message.error('角色變更失敗');
    }
  };

  // 統計各角色人數
  const roleCounts = {
    admin: users.filter((u) => u.role === 'admin').length,
    reviewer: users.filter((u) => u.role === 'reviewer').length,
    editor: users.filter((u) => u.role === 'editor').length,
    viewer: users.filter((u) => u.role === 'viewer').length,
  };

  const columns: ColumnsType<User> = [
    { title: '帳號', dataIndex: 'username', key: 'username', width: 120 },
    { title: '姓名', dataIndex: 'display_name', key: 'display_name', width: 120 },
    { title: 'Email', dataIndex: 'email', key: 'email', width: 200, ellipsis: true },
    { title: '公司', dataIndex: 'company', key: 'company', width: 150, ellipsis: true },
    {
      title: '目前角色',
      dataIndex: 'role',
      key: 'role',
      width: 100,
      render: (role: string) => {
        const colors: Record<string, string> = { admin: 'red', reviewer: 'blue', editor: 'green', viewer: 'default' };
        return <Tag color={colors[role] || 'default'}>{role}</Tag>;
      },
    },
    {
      title: '變更角色',
      key: 'change-role',
      width: 160,
      render: (_: any, record: User) => (
        <Select
          value={record.role}
          size="small"
          style={{ width: 130 }}
          onChange={(val) => handleChangeRole(record.id, val)}
          options={[
            { value: 'admin', label: '管理員' },
            { value: 'reviewer', label: '審核者' },
            { value: 'editor', label: '編輯者' },
            { value: 'viewer', label: '唯讀' },
          ]}
        />
      ),
    },
    {
      title: '啟用',
      dataIndex: 'is_active',
      key: 'is_active',
      width: 80,
      render: (active: boolean) => active
        ? <Tag color="green">啟用</Tag>
        : <Tag color="red">停用</Tag>,
    },
  ];

  return (
    <Spin spinning={loading}>
      {/* 角色統計卡片 */}
      <Row gutter={[16, 16]} style={{ marginBottom: 24 }}>
        <Col xs={12} sm={6}>
          <Card hoverable>
            <Statistic
              title={<><SafetyOutlined /> 管理員</>}
              value={roleCounts.admin}
              valueStyle={{ color: '#cf1322' }}
            />
          </Card>
        </Col>
        <Col xs={12} sm={6}>
          <Card hoverable>
            <Statistic
              title={<><UserOutlined /> 審核者</>}
              value={roleCounts.reviewer}
              valueStyle={{ color: '#1677ff' }}
            />
          </Card>
        </Col>
        <Col xs={12} sm={6}>
          <Card hoverable>
            <Statistic
              title={<><EditOutlined /> 編輯者</>}
              value={roleCounts.editor}
              valueStyle={{ color: '#52c41a' }}
            />
          </Card>
        </Col>
        <Col xs={12} sm={6}>
          <Card hoverable>
            <Statistic
              title={<><EyeOutlined /> 唯讀</>}
              value={roleCounts.viewer}
              valueStyle={{ color: '#8c8c8c' }}
            />
          </Card>
        </Col>
      </Row>

      <Table
        dataSource={users}
        columns={columns}
        rowKey="id"
        pagination={{ pageSize: 50, showTotal: (t) => `共 ${t} 人` }}
        size="middle"
        bordered
      />
    </Spin>
  );
};

export default RoleManagement;
