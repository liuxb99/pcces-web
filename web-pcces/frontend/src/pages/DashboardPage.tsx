/* 儀表板頁面 */

import React, { useEffect, useState } from 'react';
import { Row, Col, Card, Statistic, Table, Spin, Typography, Tag } from 'antd';
import {
  FolderOutlined, FileTextOutlined, DollarOutlined, ToolOutlined,
} from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import ReactECharts from 'echarts-for-react';
import { projectApi } from '../api';
import type { DashboardStats, Project } from '../types';

const { Title } = Typography;

const DashboardPage: React.FC = () => {
  const navigate = useNavigate();
  const [stats, setStats] = useState<DashboardStats | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetch = async () => {
      try {
        const data = await projectApi.getStats();
        setStats(data);
      } finally {
        setLoading(false);
      }
    };
    fetch();
  }, []);

  if (loading || !stats) return <Spin size="large" style={{ display: 'block', margin: '100px auto' }} />;

  // 圓餅圖選項 — 各類別成本分布
  const pieOption = {
    title: { text: '預算分布概觀', left: 'center' },
    tooltip: { trigger: 'item' as const, formatter: '{b}: ${c} ({d}%)' },
    series: [{
      type: 'pie',
      radius: ['40%', '70%'],
      center: ['50%', '55%'],
      data: [
        { value: stats.total_budget_amount * 0.6, name: '直接工程費' },
        { value: stats.total_budget_amount * 0.2, name: '間接工程費' },
        { value: stats.total_budget_amount * 0.12, name: '利潤與管理費' },
        { value: stats.total_budget_amount * 0.08, name: '營業稅' },
      ].filter(d => d.value > 0),
      label: { formatter: '{b}\n{d}%' },
      emphasis: {
        label: { show: true, fontSize: 16, fontWeight: 'bold' },
      },
    }],
  };

  // 最近專案表格欄位
  const columns = [
    { title: '專案編號', dataIndex: 'code', key: 'code', width: 120 },
    { title: '專案名稱', dataIndex: 'name', key: 'name', ellipsis: true },
    { title: '預算總額', dataIndex: 'budget_total', key: 'budget_total',
      render: (v: number) => `$${(v || 0).toLocaleString()}`,
      sorter: (a: Project, b: Project) => (a.budget_total || 0) - (b.budget_total || 0),
    },
    { title: '項目數', dataIndex: 'item_count', key: 'item_count', width: 80 },
    { title: '狀態', dataIndex: 'status', key: 'status', width: 80,
      render: (s: string) => <Tag color={s === 'active' ? 'green' : 'default'}>{s === 'active' ? '啟用' : '封存'}</Tag>,
    },
  ];

  return (
    <div>
      <Title level={4} style={{ marginBottom: 24 }}>儀表板</Title>

      {/* 統計卡片 */}
      <Row gutter={[16, 16]} style={{ marginBottom: 24 }}>
        <Col xs={12} sm={12} md={6}>
          <Card hoverable className="stats-card">
            <Statistic
              title="專案總數"
              value={stats.total_projects}
              prefix={<FolderOutlined />}
              valueStyle={{ color: '#1677ff' }}
            />
          </Card>
        </Col>
        <Col xs={12} sm={12} md={6}>
          <Card hoverable className="stats-card">
            <Statistic
              title="進行中專案"
              value={stats.active_projects}
              prefix={<FileTextOutlined />}
              valueStyle={{ color: '#52c41a' }}
            />
          </Card>
        </Col>
        <Col xs={12} sm={12} md={6}>
          <Card hoverable className="stats-card">
            <Statistic
              title="預算總額"
              value={stats.total_budget_amount}
              prefix={<DollarOutlined />}
              precision={0}
              valueStyle={{ color: '#faad14' }}
            />
          </Card>
        </Col>
        <Col xs={12} sm={12} md={6}>
          <Card hoverable className="stats-card">
            <Statistic
              title="資源項目"
              value={stats.total_resources}
              prefix={<ToolOutlined />}
              valueStyle={{ color: '#722ed1' }}
            />
          </Card>
        </Col>
      </Row>

      {/* 圖表與最近專案 */}
      <Row gutter={[16, 16]}>
        <Col xs={24} lg={12}>
          <Card>
            <ReactECharts option={pieOption} style={{ height: 320 }} />
          </Card>
        </Col>
        <Col xs={24} lg={12}>
          <Card title="最近專案" extra={<a onClick={() => navigate('/projects')}>檢視全部</a>}>
            <Table
              dataSource={stats.recent_projects}
              columns={columns}
              rowKey="id"
              pagination={false}
              size="small"
              onRow={(record) => ({
                onClick: () => navigate(`/projects/${record.id}/budget`),
                style: { cursor: 'pointer' },
              })}
            />
          </Card>
        </Col>
      </Row>
    </div>
  );
};

export default DashboardPage;
