/* 應用程式佈局 — 側邊欄 + Header + 內容區 */

import React from 'react';
import { Layout, Menu, Button, Avatar, Dropdown, Typography, theme } from 'antd';
import {
  DashboardOutlined, FolderOutlined, FileTextOutlined,
  ToolOutlined, BarChartOutlined, MenuFoldOutlined,
  MenuUnfoldOutlined, UserOutlined, LogoutOutlined,
  SettingOutlined,
} from '@ant-design/icons';
import { Outlet, useNavigate, useLocation, useParams } from 'react-router-dom';
import { useAppStore } from '../store';

const { Header, Sider, Content } = Layout;
const { Text } = Typography;

const AppLayout: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { user, logout, sidebarCollapsed, toggleSidebar } = useAppStore();
  const displayName = user?.display_name || user?.username || '訪客';
  const { id: projectId } = useParams();
  const { token: themeToken } = theme.useToken();

  // 從路徑判斷選中的 menu key
  const getSelectedKey = () => {
    if (location.pathname.startsWith('/projects') && projectId) {
      if (location.pathname.includes('/budget')) return `budget-${projectId}`;
      if (location.pathname.includes('/resources')) return `resources-${projectId}`;
      if (location.pathname.includes('/reports')) return `reports-${projectId}`;
      return 'projects';
    }
    return location.pathname.split('/')[1] || 'dashboard';
  };

  const menuItems: any[] = [
    { key: 'dashboard', icon: <DashboardOutlined />, label: '儀表板' },
    { key: 'projects', icon: <FolderOutlined />, label: '專案管理' },
  ];

  // 如果在專案頁面，加上子選項
  if (projectId) {
    menuItems.push(
      { key: 'divider-line', label: '─ 專案功能 ─', disabled: true, style: { opacity: 0.5, fontSize: 11, cursor: 'default' } },
      { key: `budget-${projectId}`, icon: <FileTextOutlined />, label: '預算編輯' },
      { key: `resources-${projectId}`, icon: <ToolOutlined />, label: '資源管理' },
      { key: `reports-${projectId}`, icon: <BarChartOutlined />, label: '報表分析' },
    );
  }

  const handleMenuClick = (info: { key: string }) => {
    switch (info.key) {
      case 'dashboard': navigate('/app/dashboard'); break;
      case 'projects': navigate('/app/projects'); break;
      default:
        if (info.key.startsWith('budget-')) navigate(`/projects/${projectId}/budget`);
        else if (info.key.startsWith('resources-')) navigate(`/projects/${projectId}/resources`);
        else if (info.key.startsWith('reports-')) navigate(`/projects/${projectId}/reports`);
        break;
    }
  };

  const userMenu = {
    items: [
      { key: 'profile', icon: <UserOutlined />, label: displayName },
      { type: 'divider' as const },
      { key: 'logout', icon: <LogoutOutlined />, label: '登出', danger: true },
    ],
    onClick: (info: { key: string }) => {
      if (info.key === 'logout') {
        logout();
        navigate('/login');
      }
    },
  };

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Sider
        trigger={null}
        collapsible
        collapsed={sidebarCollapsed}
        theme="light"
        style={{
          borderRight: '1px solid #f0f0f0',
          boxShadow: '2px 0 8px rgba(0,0,0,0.05)',
        }}
      >
        <div style={{
          height: 64,
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          borderBottom: '1px solid #f0f0f0',
        }}>
          <Text strong style={{ fontSize: sidebarCollapsed ? 14 : 18, color: themeToken.colorPrimary }}>
            {sidebarCollapsed ? 'P' : 'PCCES 網頁版'}
          </Text>
        </div>
        <Menu
          mode="inline"
          selectedKeys={[getSelectedKey()]}
          items={menuItems}
          onClick={handleMenuClick}
          style={{ borderRight: 'none' }}
        />
      </Sider>
      <Layout>
        <Header style={{
          padding: '0 24px',
          background: '#fff',
          borderBottom: '1px solid #f0f0f0',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'space-between',
          height: 64,
        }}>
          <Button
            type="text"
            icon={sidebarCollapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
            onClick={toggleSidebar}
          />
          <Dropdown menu={userMenu} placement="bottomRight">
            <div style={{ cursor: 'pointer', display: 'flex', alignItems: 'center', gap: 8 }}>
              <Avatar icon={<UserOutlined />} style={{ backgroundColor: themeToken.colorPrimary }} />
              <Text>{displayName}</Text>
            </div>
          </Dropdown>
        </Header>
        <Content style={{ margin: 24, minHeight: 280 }}>
          <Outlet />
        </Content>
      </Layout>
    </Layout>
  );
};

export default AppLayout;
