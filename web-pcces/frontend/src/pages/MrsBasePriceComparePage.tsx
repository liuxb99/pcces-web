/* MrsBase 單價比較頁面 — 顯示所有公共單價項目，可依分類篩選、搜尋，展開工料機組成 */

import React, { useState, useCallback, useEffect, useMemo } from 'react';
import {
  Card, Select, Table, Typography, Spin, Row, Col, Statistic, Input, Tag, message, Space, Button, Switch
} from 'antd';
import { SearchOutlined, BarChartOutlined, ReloadOutlined, DownOutlined, RightOutlined } from '@ant-design/icons';
import type { ColumnsType } from 'antd/es/table';
import { mrsBaseApi, compareApi } from '../api';
import type { MrsBaseCategory, MrsBaseItem, MrsBaseBreakdownItem } from '../types';

const { Title, Text } = Typography;

/** 格式化金額 */
const fmtMoney = (v: number | null | undefined): string => {
  if (v === null || v === undefined) return '-';
  return `NT$ ${v.toLocaleString(undefined, { minimumFractionDigits: 0, maximumFractionDigits: 2 })}`;
};

const MrsBasePriceComparePage: React.FC = () => {
  // 狀態
  const [categories, setCategories] = useState<MrsBaseCategory[]>([]);
  const [selectedCatId, setSelectedCatId] = useState<number | undefined>(undefined);
  const [searchText, setSearchText] = useState('');
  const [loading, setLoading] = useState(false);
  const [items, setItems] = useState<MrsBaseItem[]>([]);
  const [summary, setSummary] = useState<{ total: number; avg_price: number; max_price: number; min_price: number } | null>(null);
  const [expandedRowKeys, setExpandedRowKeys] = useState<number[]>([]);
  const [showAnalysisOnly, setShowAnalysisOnly] = useState(false);

  // 載入分類
  const loadCategories = useCallback(async () => {
    try {
      const data = await mrsBaseApi.getCategoriesFlat();
      setCategories(data);
    } catch (err) {
      message.error('無法載入分類');
    }
  }, []);

  // 載入項目
  const loadItems = useCallback(async (catId?: number) => {
    setLoading(true);
    try {
      const result = await compareApi.compareMrsBasePrices({
        category_id: catId,
        compare_type: 'all',
      });
      setItems(result.items);
      setSummary(result.summary);
    } catch (err: any) {
      message.error('無法載入單價資料');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    loadCategories();
    loadItems();
  }, [loadCategories, loadItems]);

  // 切換分類
  const handleCategoryChange = useCallback((value: number | undefined) => {
    setSelectedCatId(value);
    loadItems(value);
  }, [loadItems]);

  // 搜尋
  const handleSearch = useCallback(async (value: string) => {
    setSearchText(value);
    if (!value.trim()) {
      loadItems(selectedCatId);
      return;
    }
    setLoading(true);
    try {
      const result = await mrsBaseApi.search({ q: value });
      setItems(result);
      // 從搜尋結果自行計算統計摘要
      const prices = result
        .map((item) => item.unit_price)
        .filter((p): p is number => p !== null && p !== undefined);
      setSummary({
        total: result.length,
        avg_price: prices.length > 0
          ? Math.round((prices.reduce((a, b) => a + b, 0) / prices.length) * 100) / 100
          : 0,
        max_price: prices.length > 0 ? Math.max(...prices) : 0,
        min_price: prices.length > 0 ? Math.min(...prices) : 0,
      });
    } catch (err) {
      message.error('搜尋失敗');
    } finally {
      setLoading(false);
    }
  }, [selectedCatId, loadItems]);

  // 篩選後項目
  const filteredItems = useMemo(() => {
    let result = items;
    if (showAnalysisOnly) {
      result = result.filter((item) => item.is_analysis);
    }
    return result;
  }, [items, showAnalysisOnly]);

  // 分類選項
  const categoryOptions = useMemo(() => [
    { value: undefined, label: '全部分類' },
    ...categories.map((cat) => ({
      value: cat.id,
      label: `${cat.code || ''} ${cat.c_name}`,
    })),
  ], [categories]);

  // 展開明細
  const handleExpand = useCallback(async (expanded: boolean, record: MrsBaseItem) => {
    if (expanded) {
      setExpandedRowKeys((prev) => [...prev, record.id]);
      // 若無 breakdown_items，從 API 載入
      if (!record.breakdown_items || record.breakdown_items.length === 0) {
        try {
          const breakdowns = await mrsBaseApi.getBreakdownItems(record.id);
          setItems((prev) =>
            prev.map((item) =>
              item.id === record.id ? { ...item, breakdown_items: breakdowns } : item
            )
          );
        } catch (err) {
          message.error('無法載入工料機組成');
        }
      }
    } else {
      setExpandedRowKeys((prev) => prev.filter((k) => k !== record.id));
    }
  }, []);

  // 拆卸表格欄位（展開用）
  const expandedRowRender = (record: MrsBaseItem) => {
    const breakdowns = record.breakdown_items || [];
    if (!record.is_analysis || breakdowns.length === 0) {
      return (
        <div style={{ padding: 12, color: '#999', textAlign: 'center' }}>
          此項目無單價分析資料
        </div>
      );
    }

    const breakdownColumns: ColumnsType<MrsBaseBreakdownItem> = [
      { title: '細項名稱', dataIndex: 'c_name', key: 'c_name', width: 200 },
      { title: '單位', dataIndex: 'c_unit', key: 'c_unit', width: 60 },
      { title: '數量', dataIndex: 'quantity', key: 'quantity', width: 100, align: 'right',
        render: (v: number) => v.toFixed(4) },
      { title: '單價', dataIndex: 'unit_price', key: 'unit_price', width: 120, align: 'right',
        render: (v: number) => fmtMoney(v) },
      { title: '金額', dataIndex: 'amount', key: 'amount', width: 130, align: 'right',
        render: (v: number) => fmtMoney(v) },
      { title: '類別', dataIndex: 'category', key: 'category', width: 80,
        render: (v: string) => {
          const colorMap: Record<string, string> = {
            labor: 'blue',
            material: 'orange',
            equipment: 'purple',
            misc: 'default',
          };
          return <Tag color={colorMap[v] || 'default'}>{v}</Tag>;
        },
      },
    ];

    return (
      <div style={{ padding: 12 }}>
        <Table<MrsBaseBreakdownItem>
          columns={breakdownColumns}
          dataSource={breakdowns}
          rowKey="id"
          size="small"
          pagination={false}
          summary={() => (
            <Table.Summary.Row>
              <Table.Summary.Cell index={0}><Text strong>合計</Text></Table.Summary.Cell>
              <Table.Summary.Cell index={1} />
              <Table.Summary.Cell index={2} />
              <Table.Summary.Cell index={3} />
              <Table.Summary.Cell index={4} align="right">
                <Text strong>{fmtMoney(record.unit_price)}</Text>
              </Table.Summary.Cell>
              <Table.Summary.Cell index={5} />
            </Table.Summary.Row>
          )}
        />
      </div>
    );
  };

  // 主表格欄位
  const columns: ColumnsType<MrsBaseItem> = useMemo(() => [
    {
      title: '編碼',
      dataIndex: 'code',
      key: 'code',
      width: 120,
      fixed: 'left',
      render: (val: string) => <Text code>{val}</Text>,
    },
    {
      title: '名稱',
      dataIndex: 'c_name',
      key: 'c_name',
      width: 250,
    },
    {
      title: '單位',
      dataIndex: 'c_unit',
      key: 'c_unit',
      width: 60,
    },
    {
      title: '單價',
      dataIndex: 'unit_price',
      key: 'unit_price',
      width: 130,
      align: 'right',
      sorter: (a, b) => a.unit_price - b.unit_price,
      render: (val: number) => <Text strong>{fmtMoney(val)}</Text>,
    },
    {
      title: '成本類',
      dataIndex: 'cost_kind',
      key: 'cost_kind',
      width: 80,
      render: (val: string) => <Tag>{val}</Tag>,
    },
    {
      title: '單價分析',
      key: 'analysis',
      width: 100,
      align: 'center',
      render: (_: any, record: MrsBaseItem) => (
        record.is_analysis ? (
          <Tag color="blue">有分析</Tag>
        ) : (
          <Text type="secondary">-</Text>
        )
      ),
    },
    {
      title: '審核狀態',
      dataIndex: 'is_approved',
      key: 'is_approved',
      width: 100,
      render: (val: boolean) => (
        val ? <Tag color="green">已審核</Tag> : <Tag>待審核</Tag>
      ),
    },
  ], []);

  return (
    <div>
      {/* 頁面標題 */}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Title level={4} style={{ margin: 0 }}>
          <BarChartOutlined style={{ marginRight: 8 }} />
          單價比較 — 公共單價一覽
        </Title>
        <Space>
          <Button icon={<ReloadOutlined />} onClick={() => loadItems(selectedCatId)} loading={loading}>
            重新整理
          </Button>
        </Space>
      </div>

      {/* 篩選區 */}
      <Card size="small" style={{ marginBottom: 16 }}>
        <Row gutter={16} align="middle">
          <Col span={8}>
            <Text strong>分類：</Text>
            <Select
              style={{ width: '100%', marginTop: 4 }}
              placeholder="全部分類"
              value={selectedCatId}
              onChange={handleCategoryChange}
              options={categoryOptions}
              allowClear
            />
          </Col>
          <Col span={10}>
            <Text strong>搜尋：</Text>
            <Input.Search
              style={{ width: '100%', marginTop: 4 }}
              placeholder="搜尋項目名稱或編碼..."
              value={searchText}
              onChange={(e) => setSearchText(e.target.value)}
              onSearch={handleSearch}
              enterButton={<SearchOutlined />}
              allowClear
            />
          </Col>
          <Col span={6}>
            <Space style={{ marginTop: 20 }}>
              <Text>僅顯示有分析：</Text>
              <Switch checked={showAnalysisOnly} onChange={setShowAnalysisOnly} />
            </Space>
          </Col>
        </Row>
      </Card>

      {/* 統計摘要 */}
      {summary && (
        <Row gutter={16} style={{ marginBottom: 16 }}>
          <Col span={6}>
            <Card size="small">
              <Statistic title="項目總數" value={summary.total} />
            </Card>
          </Col>
          <Col span={6}>
            <Card size="small">
              <Statistic title="平均單價" value={summary.avg_price} precision={2} prefix="NT$" />
            </Card>
          </Col>
          <Col span={6}>
            <Card size="small">
              <Statistic title="最高單價" value={summary.max_price} precision={2} prefix="NT$"
                valueStyle={{ color: '#cf1322' }} />
            </Card>
          </Col>
          <Col span={6}>
            <Card size="small">
              <Statistic title="最低單價" value={summary.min_price} precision={2} prefix="NT$"
                valueStyle={{ color: '#3f8600' }} />
            </Card>
          </Col>
        </Row>
      )}

      {/* 載入中 */}
      {loading && (
        <div style={{ textAlign: 'center', padding: 40 }}>
          <Spin size="large" />
        </div>
      )}

      {/* 項目表格 */}
      {!loading && (
        <Table<MrsBaseItem>
          columns={columns}
          dataSource={filteredItems}
          rowKey="id"
          scroll={{ x: 900 }}
          size="small"
          expandable={{
            expandedRowRender,
            expandedRowKeys,
            onExpand: handleExpand,
            expandIcon: ({ expanded, onExpand, record }) =>
              expanded ? (
                <DownOutlined onClick={(e) => onExpand(record, e)} style={{ cursor: 'pointer' }} />
              ) : (
                <RightOutlined onClick={(e) => onExpand(record, e)} style={{ cursor: 'pointer' }} />
              ),
          }}
          pagination={{
            showSizeChanger: true,
            showTotal: (total) => `共 ${total} 項`,
            defaultPageSize: 50,
            pageSizeOptions: ['20', '50', '100', '200'],
          }}
        />
      )}
    </div>
  );
};

export default MrsBasePriceComparePage;
