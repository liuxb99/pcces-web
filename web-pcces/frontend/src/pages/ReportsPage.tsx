/* 報表與分析頁面 */

import React, { useEffect, useState } from 'react';
import {
  Card, Row, Col, Statistic, Table, Button, Space, Typography,
  Spin, Breadcrumb, message, Tabs, Tag,
} from 'antd';
import { DownloadOutlined, FileExcelOutlined, BarChartOutlined, ReloadOutlined } from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import ReactECharts from 'echarts-for-react';
import { projectApi, reportApi, budgetApi } from '../api';
import type { BudgetItem } from '../types';

const { Title, Text } = Typography;

const ReportsPage: React.FC = () => {
  const { id: projectId } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');

  const [project, setProject] = useState<any>(null);
  const [items, setItems] = useState<BudgetItem[]>([]);
  const [flatItems, setFlatItems] = useState<BudgetItem[]>([]);
  const [summary, setSummary] = useState<any>(null);
  const [loading, setLoading] = useState(true);

  const fetchData = async () => {
    setLoading(true);
    try {
      const [proj, tree, summ] = await Promise.all([
        projectApi.get(pid),
        budgetApi.getTree(pid),
        reportApi.getSummary(pid),
      ]);
      setProject(proj);
      setItems(tree);
      setSummary(summ);

      // 平面化
      const flatten = (nodes: BudgetItem[]): BudgetItem[] => {
        const result: BudgetItem[] = [];
        for (const n of nodes) {
          result.push(n);
          if (n.children?.length) result.push(...flatten(n.children));
        }
        return result;
      };
      setFlatItems(flatten(tree));
    } catch (err) {
      message.error('載入報表資料失敗');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchData(); }, [pid]);

  // 定義 kind 的標籤
  const kindLabels: Record<string, string> = {
    B: '主項', L: '單價', F: '公式', S: '分段', Z: '小計', U: '自訂', W: '工作',
  };

  // Excel 下載（使用 fetch + Blob 以帶入 Authorization header）
  const handleDownloadExcel = async () => {
    const token = localStorage.getItem('pcces_token');
    try {
      const response = await fetch(reportApi.getExcelUrl(pid), {
        headers: { Authorization: `Bearer ${token}` },
      });
      if (!response.ok) {
        message.error('下載失敗：無權限或伺服器錯誤');
        return;
      }
      const blob = await response.blob();
      const url = URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = `PCCES_預算表_${pid}.xlsx`;
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      URL.revokeObjectURL(url);
      message.success('Excel 報表下載完成');
    } catch {
      message.error('下載失敗');
    }
  };

  if (loading) return <Spin size="large" style={{ display: 'block', margin: '100px auto' }} />;

  // 根節點（第一層）
  const rootItems = flatItems.filter(i => !i.parent_id);
  const totalAmount = rootItems.reduce((sum, i) => sum + (i.amount || 0), 0);

  // 圓餅圖
  const pieOption = {
    title: { text: '預算項目分布', left: 'center' },
    tooltip: { trigger: 'item' as const, formatter: '{b}<br/>${c:,.0f} ({d}%)' },
    series: [{
      type: 'pie',
      radius: ['35%', '65%'],
      center: ['50%', '55%'],
      data: rootItems.map(i => ({
        name: i.c_name || '(無名稱)',
        value: i.amount || 0,
      })).filter(d => d.value > 0),
      label: {
        formatter: '{b|{b}}\n{per|{d}%}',
        rich: {
          b: { fontSize: 12, lineHeight: 20 },
          per: { fontSize: 11, color: '#999' },
        },
      },
    }],
  };

  // 長條圖（前 10 大項目）
  const top10 = [...rootItems].sort((a, b) => (b.amount || 0) - (a.amount || 0)).slice(0, 10);
  const barOption = {
    title: { text: '前 10 大預算項目', left: 'center' },
    tooltip: { trigger: 'axis' as const, formatter: '{b}<br/>${c:,.0f}' },
    xAxis: { type: 'category' as const, data: top10.map(i => (i.c_name || '無名').slice(0, 12)),
      axisLabel: { rotate: 30, fontSize: 10 },
    },
    yAxis: { type: 'value' as const, axisLabel: { formatter: '${v}' } },
    series: [{
      type: 'bar',
      data: top10.map(i => i.amount || 0),
      itemStyle: {
        color: {
          type: 'linear',
          x: 0, y: 0, x2: 0, y2: 1,
          colorStops: [
            { offset: 0, color: '#1677ff' },
            { offset: 1, color: '#69b1ff' },
          ],
        },
      },
    }],
  };

  // 報表表格
  const reportColumns = [
    { title: '項次', dataIndex: 'print_no', key: 'print_no', width: 80 },
    { title: '項目名稱', dataIndex: 'c_name', key: 'c_name', ellipsis: true,
      render: (v: string, r: BudgetItem) => (
        <span>{'  '.repeat(r.level_no || 0)}{v || '(無名稱)'}</span>
      ),
    },
    { title: '單位', dataIndex: 'c_unit', key: 'c_unit', width: 60 },
    { title: '數量', dataIndex: 'quantity', key: 'quantity', width: 100, align: 'right' as const,
      render: (v: number) => v ? v.toLocaleString() : '-',
    },
    { title: '單價', dataIndex: 'unit_price', key: 'unit_price', width: 120, align: 'right' as const,
      render: (v: number) => v ? `$${v.toLocaleString()}` : '-',
    },
    { title: '複價', dataIndex: 'amount', key: 'amount', width: 140, align: 'right' as const,
      render: (v: number) => (
        <Text strong>${(v || 0).toLocaleString()}</Text>
      ),
    },
    { title: '備註', dataIndex: 'memo', key: 'memo', ellipsis: true },
  ];

  return (
    <div>
      <Breadcrumb items={[
        { title: <a onClick={() => navigate('/projects')}>專案</a> },
        { title: project?.name || `專案 #${pid}` },
        { title: '報表分析' },
      ]} style={{ marginBottom: 12 }} />

      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 16 }}>
        <Title level={4} style={{ margin: 0 }}>報表分析 — {project?.name}</Title>
        <Space>
          <Button icon={<ReloadOutlined />} onClick={fetchData}>重新整理</Button>
          <Button type="primary" icon={<FileExcelOutlined />} onClick={handleDownloadExcel}>
            下載 Excel
          </Button>
        </Space>
      </div>

      {/* 統計摘要 */}
      <Row gutter={[16, 16]} style={{ marginBottom: 24 }}>
        <Col xs={12} sm={6}>
          <Card size="small">
            <Statistic title="預算總額" value={totalAmount} prefix="$" precision={0} />
          </Card>
        </Col>
        <Col xs={12} sm={6}>
          <Card size="small">
            <Statistic title="項目總數" value={flatItems.length} suffix="項" />
          </Card>
        </Col>
        <Col xs={12} sm={6}>
          <Card size="small">
            <Statistic title="第一層項目" value={rootItems.length} suffix="項" />
          </Card>
        </Col>
        <Col xs={12} sm={6}>
          <Card size="small">
            <Statistic title="資源總數" value={summary?.item_count || 0} suffix="項" />
          </Card>
        </Col>
      </Row>

      {/* 圖表 */}
      <Row gutter={[16, 16]} style={{ marginBottom: 24 }}>
        <Col xs={24} lg={12}>
          <Card title="預算分布圓餅圖" size="small">
            <ReactECharts option={pieOption} style={{ height: 350 }} />
          </Card>
        </Col>
        <Col xs={24} lg={12}>
          <Card title="前十大預算項目長條圖" size="small">
            <ReactECharts option={barOption} style={{ height: 350 }} />
          </Card>
        </Col>
      </Row>

      {/* 詳細報表 */}
      <Card title="預算詳細表" size="small" extra={
        <Space>
          <Tag>{flatItems.length} 項</Tag>
          <Text type="secondary">總額: ${totalAmount.toLocaleString()}</Text>
        </Space>
      }>
        <Table
          dataSource={flatItems}
          columns={reportColumns}
          rowKey="id"
          pagination={{ pageSize: 30, showSizeChanger: true, pageSizeOptions: ['30', '50', '100'] }}
          size="small"
          scroll={{ x: 800 }}
        />
      </Card>
    </div>
  );
};

export default ReportsPage;
