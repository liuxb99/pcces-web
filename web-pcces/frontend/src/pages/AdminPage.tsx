/* 系統維護主頁面（Tabs 切換各子功能） */

import React from 'react';
import { Tabs, Typography, Alert } from 'antd';
import { UserOutlined, TeamOutlined, DatabaseOutlined, SettingOutlined, ApartmentOutlined, ControlOutlined } from '@ant-design/icons';
import { Navigate } from 'react-router-dom';
import { useAppStore } from '../store';
import UserManagement from './admin/UserManagement';
import RoleManagement from './admin/RoleManagement';
import CodeTableManagement from './admin/CodeTableManagement';
import SystemParamsPage from './admin/SystemParamsPage';
import OrganizationManage from './admin/OrganizationManage';
import FeatureFlagManagement from './admin/FeatureFlagManagement';

const { Title } = Typography;

const AdminPage: React.FC = () => {
  const user = useAppStore((s) => s.user);

  // 非管理員禁止進入
  if (!user || user.role !== 'admin') {
    return <Navigate to="/app/dashboard" replace />;
  }

  return (
    <div>
      <div style={{ marginBottom: 24 }}>
        <Title level={3} style={{ margin: 0 }}>系統維護中心</Title>
        <Alert
          message="管理員專區 — 所有操作均會即時生效，請謹慎操作。"
          type="info"
          showIcon
          style={{ marginTop: 12 }}
        />
      </div>

      <Tabs
        defaultActiveKey="users"
        tabPosition="top"
        items={[
          {
            key: 'users',
            label: <span><UserOutlined /> 使用者管理</span>,
            children: <UserManagement />,
          },
          {
            key: 'roles',
            label: <span><TeamOutlined /> 角色權限</span>,
            children: <RoleManagement />,
          },
          {
            key: 'codes',
            label: <span><DatabaseOutlined /> 代碼管理</span>,
            children: <CodeTableManagement />,
          },
          {
            key: 'params',
            label: <span><SettingOutlined /> 系統參數</span>,
            children: <SystemParamsPage />,
          },
          {
            key: 'org',
            label: <span><ApartmentOutlined /> 組織機構</span>,
            children: <OrganizationManage />,
          },
          {
            key: 'feature-flags',
            label: <span><ControlOutlined /> 功能開關</span>,
            children: <FeatureFlagManagement />,
          },
        ]}
      />
    </div>
  );
};

export default AdminPage;
