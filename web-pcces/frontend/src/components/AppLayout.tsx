/* 應用程式佈局 — 側邊欄 + Header + 內容區 */

import React, { useEffect } from 'react';
import { Layout, Menu, Button, Avatar, Dropdown, Typography, theme } from 'antd';
import {
  DashboardOutlined, FolderOutlined, FileTextOutlined,
  ToolOutlined, BarChartOutlined, MenuFoldOutlined,
  MenuUnfoldOutlined, UserOutlined, LogoutOutlined,
  SettingOutlined, DollarOutlined, LinkOutlined, DatabaseOutlined,
  SwapOutlined, InfoCircleOutlined,
} from '@ant-design/icons';
import { Outlet, useNavigate, useLocation, useParams } from 'react-router-dom';
import { useAppStore } from '../store';

const { Header, Sider, Content } = Layout;
const { Text } = Typography;

const AppLayout: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { user, logout, sidebarCollapsed, toggleSidebar, loadFeatureFlags } = useAppStore();
  const displayName = user?.display_name || user?.username || '訪客';
  const { id: projectId } = useParams();
  const { token: themeToken } = theme.useToken();

  // 載入功能開關
  useEffect(() => {
    loadFeatureFlags();
  }, []);

  // 從路徑判斷選中的 menu key
  const getSelectedKey = () => {
    if (location.pathname.includes('/mrs-base')) {
      return 'mrs-base';
    }
    if (location.pathname.includes('/compare/budget-items')) {
      return 'compare-budget-items';
    }
    if (location.pathname.includes('/compare/mrs-prices')) {
      return 'compare-mrs-prices';
    }
    if (location.pathname.includes('/projects') && projectId) {
      if (location.pathname.includes('/budget')) return `budget-${projectId}`;
      if (location.pathname.includes('/resources')) return `resources-${projectId}`;
      if (location.pathname.includes('/invoices')) return `invoices-${projectId}`;
      if (location.pathname.includes('/contracts')) return `contracts-${projectId}`;
      if (location.pathname.includes('/reports')) return `reports-${projectId}`;
      return 'projects';
    }
    return 'dashboard';
  };

  const isAdmin = user?.role === 'admin';

  const menuItems: any[] = [
    { key: 'dashboard', icon: <DashboardOutlined />, label: '儀表板' },
    { key: 'projects', icon: <FolderOutlined />, label: '專案管理' },
    { key: 'mrs-base', icon: <DatabaseOutlined />, label: '公共單價庫' },
    {
      key: 'compare',
      icon: <SwapOutlined />,
      label: '比較分析',
      children: [
        { key: 'compare-budget-items', icon: <FileTextOutlined />, label: '工項比較' },
        { key: 'compare-mrs-prices', icon: <BarChartOutlined />, label: '單價比較' },
      ],
    },
  ];

  // 僅 admin 使用者可看到「系統維護」選單
  if (isAdmin) {
    menuItems.push({ key: 'admin', icon: <SettingOutlined />, label: '系統維護' });
  }

  // 版本資訊（置底）
  menuItems.push({ type: 'divider' });
  menuItems.push({ key: 'version', icon: <InfoCircleOutlined />, label: '版本資訊' });

  // 如果在專案頁面，加上子選項
  if (projectId) {
    menuItems.push(
      { key: 'divider-line', label: '─ 專案功能 ─', disabled: true, style: { opacity: 0.5, fontSize: 11, cursor: 'default' } },
      { key: `budget-${projectId}`, icon: <FileTextOutlined />, label: '預算編輯' },
      { key: `resources-${projectId}`, icon: <ToolOutlined />, label: '資源管理' },
      { key: `invoices-${projectId}`, icon: <DollarOutlined />, label: '計價管理' },
      { key: `contracts-${projectId}`, icon: <LinkOutlined />, label: '分包合約' },
      { key: `reports-${projectId}`, icon: <BarChartOutlined />, label: '報表分析' },
    );
  }

  const handleMenuClick = (info: { key: string }) => {
    switch (info.key) {
      case 'dashboard': navigate('/app/dashboard'); break;
      case 'projects': navigate('/app/projects'); break;
      case 'mrs-base': navigate('/app/mrs-base'); break;
      case 'compare-budget-items': navigate('/app/compare/budget-items'); break;
      case 'compare-mrs-prices': navigate('/app/compare/mrs-prices'); break;
      case 'admin': navigate('/app/admin'); break;
      case 'version': navigate('/app/version'); break;
      default:
        if (info.key.startsWith('budget-')) navigate(`/app/projects/${projectId}/budget`);
        else if (info.key.startsWith('resources-')) navigate(`/app/projects/${projectId}/resources`);
        else if (info.key.startsWith('invoices-')) navigate(`/app/projects/${projectId}/invoices`);
        else if (info.key.startsWith('contracts-')) navigate(`/app/projects/${projectId}/contracts`);
        else if (info.key.startsWith('reports-')) navigate(`/app/projects/${projectId}/reports`);
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
