/* 工項比較頁面 — 選取兩個專案，顯示預算項目差異表格 */

import React, { useState, useCallback, useMemo } from 'react';
import {
  Card, Select, Button, Table, Tag, Typography, Spin, Row, Col, Statistic, message, Space, Divider
} from 'antd';
import { SwapOutlined, DownloadOutlined, ReloadOutlined } from '@ant-design/icons';
import type { ColumnsType } from 'antd/es/table';
import { useNavigate } from 'react-router-dom';
import { projectApi, compareApi } from '../api';
import type { Project, CompareItem, CompareResult } from '../types';

const { Title, Text } = Typography;

/** 格式化數值，保留 2 位小數 */
const fmt = (v: number | null | undefined): string => {
  if (v === null || v === undefined) return 'N/A';
  if (typeof v === 'number' && !isFinite(v)) return '∞';
  return v.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
};

/** 格式化差異百分比（含 +/- 符號） */
const fmtPct = (v: number | null | undefined): string => {
  if (v === null || v === undefined) return 'N/A';
  const sign = v >= 0 ? '+' : '';
  return `${sign}${v.toFixed(2)}%`;
};

const ComparePage: React.FC = () => {
  const navigate = useNavigate();

  // 狀態
  const [projects, setProjects] = useState<Project[]>([]);
  const [projectAId, setProjectAId] = useState<number | null>(null);
  const [projectBId, setProjectBId] = useState<number | null>(null);
  const [loading, setLoading] = useState(false);
  const [comparing, setComparing] = useState(false);
  const [result, setResult] = useState<CompareResult | null>(null);
  const [filterStatus, setFilterStatus] = useState<string>('all');

  // 載入專案列表
  const loadProjects = useCallback(async () => {
    setLoading(true);
    try {
      const data = await projectApi.list();
      setProjects(data);
      if (data.length >= 2) {
        setProjectAId(data[0].id);
        setProjectBId(data[1].id);
      } else if (data.length === 1) {
        setProjectAId(data[0].id);
      }
    } catch (err: any) {
      message.error('無法載入專案列表');
    } finally {
      setLoading(false);
    }
  }, []);

  React.useEffect(() => {
    loadProjects();
  }, [loadProjects]);

  // 執行比較
  const handleCompare = useCallback(async () => {
    if (!projectAId || !projectBId) {
      message.warning('請選擇兩個專案進行比較');
      return;
    }
    if (projectAId === projectBId) {
      message.warning('請選擇不同的專案進行比較');
      return;
    }
    setComparing(true);
    setResult(null);
    try {
      const data = await compareApi.compareBudgetItems({
        project_a_id: projectAId,
        project_b_id: projectBId,
        scope: 'leaf',
      });
      setResult(data);
      message.success(`比較完成：共 ${data.items.length} 項`);
    } catch (err: any) {
      message.error('比較失敗：' + (err?.response?.data?.detail || err.message));
    } finally {
      setComparing(false);
    }
  }, [projectAId, projectBId]);

  // 匯出 Excel
  const handleExportExcel = useCallback(async () => {
    if (!projectAId || !projectBId) return;
    try {
      const { data: blob, filename } = await compareApi.exportExcel({
        project_a_id: projectAId,
        project_b_id: projectBId,
        scope: 'leaf',
      });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = filename;
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
      message.success('比較報表已匯出');
    } catch (err: any) {
      message.error('匯出失敗：' + (err?.response?.data?.detail || err.message));
    }
  }, [projectAId, projectBId]);

  // 篩選後資料
  const filteredItems = useMemo(() => {
    if (!result) return [];
    if (filterStatus === 'all') return result.items;
    return result.items.filter((item) => item.status === filterStatus);
  }, [result, filterStatus]);

  // 表格欄位定義
  const columns: ColumnsType<CompareItem> = useMemo(() => [
    {
      title: '項次',
      dataIndex: 'key',
      key: 'key',
      width: 120,
      fixed: 'left',
      render: (val: string) => <Text code>{val}</Text>,
    },
    {
      title: '項目名稱',
      dataIndex: 'c_name',
      key: 'c_name',
      width: 220,
    },
    {
      title: '單位',
      dataIndex: 'c_unit',
      key: 'c_unit',
      width: 60,
    },
    {
      title: 'A 數量',
      key: 'a_qty',
      width: 110,
      align: 'right',
      render: (_: any, record: CompareItem) => (
        <Text>{fmt(record.a.quantity)}</Text>
      ),
    },
    {
      title: 'A 單價',
      key: 'a_price',
      width: 120,
      align: 'right',
      render: (_: any, record: CompareItem) => (
        <Text>{fmt(record.a.unit_price)}</Text>
      ),
    },
    {
      title: 'A 金額',
      key: 'a_amt',
      width: 130,
      align: 'right',
      render: (_: any, record: CompareItem) => (
        <Text strong>{fmt(record.a.amount)}</Text>
      ),
    },
    {
      title: 'B 數量',
      key: 'b_qty',
      width: 110,
      align: 'right',
      render: (_: any, record: CompareItem) => (
        <Text>{fmt(record.b.quantity)}</Text>
      ),
    },
    {
      title: 'B 單價',
      key: 'b_price',
      width: 120,
      align: 'right',
      render: (_: any, record: CompareItem) => (
        <Text>{fmt(record.b.unit_price)}</Text>
      ),
    },
    {
      title: 'B 金額',
      key: 'b_amt',
      width: 130,
      align: 'right',
      render: (_: any, record: CompareItem) => (
        <Text strong>{fmt(record.b.amount)}</Text>
      ),
    },
    {
      title: '數量差異',
      key: 'diff_qty',
      width: 140,
      align: 'right',
      render: (_: any, record: CompareItem) => {
        const v = record.diff.quantity;
        const pct = record.diff_pct.quantity;
        const isSignificant = pct !== null && Math.abs(pct) > 5;
        return (
          <Text
            type={v > 0 ? 'danger' : v < 0 ? 'success' : undefined}
            style={{ fontWeight: isSignificant ? 'bold' : 'normal' }}
          >
            {v > 0 ? '+' : ''}{fmt(v)} ({fmtPct(pct)})
          </Text>
        );
      },
    },
    {
      title: '單價差異',
      key: 'diff_price',
      width: 140,
      align: 'right',
      render: (_: any, record: CompareItem) => {
        const v = record.diff.unit_price;
        const pct = record.diff_pct.unit_price;
        const isSignificant = pct !== null && Math.abs(pct) > 5;
        return (
          <Text
            type={v > 0 ? 'danger' : v < 0 ? 'success' : undefined}
            style={{ fontWeight: isSignificant ? 'bold' : 'normal' }}
          >
            {v > 0 ? '+' : ''}{fmt(v)} ({fmtPct(pct)})
          </Text>
        );
      },
    },
    {
      title: '金額差異',
      key: 'diff_amt',
      width: 140,
      align: 'right',
      render: (_: any, record: CompareItem) => {
        const v = record.diff.amount;
        const pct = record.diff_pct.amount;
        const isSignificant = pct !== null && Math.abs(pct) > 5;
        return (
          <Text
            type={v > 0 ? 'danger' : v < 0 ? 'success' : undefined}
            style={{ fontWeight: isSignificant ? 'bold' : 'normal' }}
          >
            {v > 0 ? '+' : ''}{fmt(v)} ({fmtPct(pct)})
          </Text>
        );
      },
    },
    {
      title: '狀態',
      dataIndex: 'status',
      key: 'status',
      width: 80,
      render: (status: string) => {
        const colorMap: Record<string, string> = {
          added: 'green',
          removed: 'red',
          modified: 'orange',
          unchanged: 'default',
        };
        return <Tag color={colorMap[status] || 'default'}>{status}</Tag>;
      },
    },
  ], []);

  // 狀態篩選選項
  const statusOptions = useMemo(() => {
    if (!result) return [{ value: 'all', label: '全部' }];
    const counts = result.summary;
    return [
      { value: 'all', label: `全部 (${result.items.length})` },
      { value: 'modified', label: `修改 (${counts.modified_count})` },
      { value: 'added', label: `新增 (${counts.added_count})` },
      { value: 'removed', label: `移除 (${counts.removed_count})` },
      { value: 'unchanged', label: `不變 (${counts.unchanged_count})` },
    ];
  }, [result]);

  // 專案選擇器
  const projectOptions = projects.map((p) => ({
    value: p.id,
    label: `${p.code || ''} ${p.name}`,
  }));

  return (
    <div>
      {/* 頁面標題 */}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Title level={4} style={{ margin: 0 }}>
          <SwapOutlined style={{ marginRight: 8 }} />
          工項比較
        </Title>
        <Space>
          <Button icon={<ReloadOutlined />} onClick={loadProjects} loading={loading}>
            重新整理
          </Button>
        </Space>
      </div>

      {/* 專案選取區 */}
      <Card size="small" style={{ marginBottom: 16 }}>
        <Row gutter={16} align="middle">
          <Col span={7}>
            <Text strong>專案 A：</Text>
            <Select
              showSearch
              style={{ width: '100%', marginTop: 4 }}
              placeholder="選擇專案 A"
              value={projectAId}
              onChange={setProjectAId}
              options={projectOptions}
              filterOption={(input, option) =>
                (option?.label as string)?.toLowerCase().includes(input.toLowerCase())
              }
            />
          </Col>
          <Col span={2} style={{ textAlign: 'center' }}>
            <SwapOutlined style={{ fontSize: 24, color: '#1890ff' }} />
          </Col>
          <Col span={7}>
            <Text strong>專案 B：</Text>
            <Select
              showSearch
              style={{ width: '100%', marginTop: 4 }}
              placeholder="選擇專案 B"
              value={projectBId}
              onChange={setProjectBId}
              options={projectOptions}
              filterOption={(input, option) =>
                (option?.label as string)?.toLowerCase().includes(input.toLowerCase())
              }
            />
          </Col>
          <Col span={8}>
            <Space>
              <Button
                type="primary"
                icon={<SwapOutlined />}
                onClick={handleCompare}
                loading={comparing}
                disabled={!projectAId || !projectBId || projectAId === projectBId}
              >
                開始比較
              </Button>
              {result && (
                <Button icon={<DownloadOutlined />} onClick={handleExportExcel}>
                  匯出 Excel
                </Button>
              )}
            </Space>
          </Col>
        </Row>
      </Card>

      {/* 比較結果 */}
      {comparing && (
        <div style={{ textAlign: 'center', padding: 40 }}>
          <Spin size="large" />
          <div style={{ marginTop: 16 }}>正在比較...</div>
        </div>
      )}

      {result && !comparing && (
        <>
          {/* 摘要卡片 */}
          <Row gutter={16} style={{ marginBottom: 16 }}>
            <Col span={6}>
              <Card size="small">
                <Statistic
                  title="專案 A 總額"
                  value={result.summary.total_a}
                  precision={2}
                  prefix="NT$"
                  valueStyle={{ fontSize: 18 }}
                />
              </Card>
            </Col>
            <Col span={6}>
              <Card size="small">
                <Statistic
                  title="專案 B 總額"
                  value={result.summary.total_b}
                  precision={2}
                  prefix="NT$"
                  valueStyle={{ fontSize: 18 }}
                />
              </Card>
            </Col>
            <Col span={6}>
              <Card size="small">
                <Statistic
                  title="差異總額"
                  value={result.summary.diff}
                  precision={2}
                  prefix="NT$"
                  valueStyle={{
                    fontSize: 18,
                    color: result.summary.diff > 0 ? '#cf1322' : '#3f8600',
                  }}
                />
              </Card>
            </Col>
            <Col span={6}>
              <Card size="small">
                <Statistic
                  title="差異百分比"
                  value={result.summary.diff_pct ?? 0}
                  precision={2}
                  suffix="%"
                  valueStyle={{
                    fontSize: 18,
                    color: (result.summary.diff_pct ?? 0) > 0 ? '#cf1322' : '#3f8600',
                  }}
                />
              </Card>
            </Col>
          </Row>

          <Row gutter={16} style={{ marginBottom: 16 }}>
            <Col span={24}>
              <Card size="small">
                <Space size={24}>
                  <Text>
                    新增：<Tag color="green">{result.summary.added_count}</Tag>
                  </Text>
                  <Text>
                    移除：<Tag color="red">{result.summary.removed_count}</Tag>
                  </Text>
                  <Text>
                    修改：<Tag color="orange">{result.summary.modified_count}</Tag>
                  </Text>
                  <Text>
                    不變：<Tag>{result.summary.unchanged_count}</Tag>
                  </Text>
                  <Divider type="vertical" />
                  <Text>
                    專案 A：<Text strong>{result.project_a.name}</Text>
                  </Text>
                  <Text>
                    專案 B：<Text strong>{result.project_b.name}</Text>
                  </Text>
                </Space>
              </Card>
            </Col>
          </Row>

          {/* 篩選 + 匯出 */}
          <div style={{ marginBottom: 12, display: 'flex', justifyContent: 'space-between' }}>
            <Space>
              <Text>篩選狀態：</Text>
              <Select
                value={filterStatus}
                onChange={setFilterStatus}
                options={statusOptions}
                style={{ width: 180 }}
              />
            </Space>
            <Text type="secondary">共 {filteredItems.length} 項</Text>
          </div>

          {/* 差異表格 */}
          <Table<CompareItem>
            columns={columns}
            dataSource={filteredItems}
            rowKey="key"
            scroll={{ x: 1700 }}
            size="small"
            pagination={{
              showSizeChanger: true,
              showTotal: (total) => `共 ${total} 項`,
              defaultPageSize: 50,
              pageSizeOptions: ['20', '50', '100', '200'],
            }}
            rowClassName={(record) => {
              if (record.status === 'added') return 'compare-row-added';
              if (record.status === 'removed') return 'compare-row-removed';
              if (record.status === 'modified') return 'compare-row-modified';
              return '';
            }}
          />
        </>
      )}

      {!result && !comparing && (
        <Card>
          <div style={{ textAlign: 'center', padding: 40, color: '#999' }}>
            <SwapOutlined style={{ fontSize: 48, marginBottom: 16 }} />
            <div>請在上方選擇兩個專案，點擊「開始比較」進行預算項目差異分析。</div>
          </div>
        </Card>
      )}

      {/* 自訂 CSS */}
      <style>{`
        .compare-row-added {
          background-color: #f6ffed !important;
        }
        .compare-row-removed {
          background-color: #fff2f0 !important;
        }
        .compare-row-modified {
          background-color: #fffbe6 !important;
        }
        .compare-row-added:hover td {
          background-color: #d9f7be !important;
        }
        .compare-row-removed:hover td {
          background-color: #ffccc7 !important;
        }
        .compare-row-modified:hover td {
          background-color: #ffe58f !important;
        }
      `}</style>
    </div>
  );
};

export default ComparePage;
