/* MrsBase 工料機組成面板 */

import React, { useEffect, useState, useCallback } from 'react';
import { Table, Button, Space, message, Modal, Input, InputNumber, Select, Popconfirm } from 'antd';
import { PlusOutlined, EditOutlined, DeleteOutlined, ReloadOutlined } from '@ant-design/icons';
import { mrsBaseApi } from '../../api';
import type { MrsBaseBreakdownItem } from '../../types';

interface Props {
  itemId: number;
  onPriceChange?: (newPrice: number) => void;
}

const categoryOptions = [
  { value: 'labor', label: '工' },
  { value: 'material', label: '料' },
  { value: 'equipment', label: '機' },
  { value: 'misc', label: '雜' },
];

const MrsBaseBreakdownPanel: React.FC<Props> = ({ itemId, onPriceChange }) => {
  const [items, setItems] = useState<MrsBaseBreakdownItem[]>([]);
  const [loading, setLoading] = useState(false);
  const [editModalVisible, setEditModalVisible] = useState(false);
  const [editingItem, setEditingItem] = useState<MrsBaseBreakdownItem | null>(null);
  const [formData, setFormData] = useState({
    code: '',
    c_name: '',
    c_unit: '式',
    quantity: 0,
    unit_price: 0,
    category: 'material',
    remark: '',
  });

  const loadItems = useCallback(async () => {
    setLoading(true);
    try {
      const data = await mrsBaseApi.getBreakdownItems(itemId);
      setItems(data);
    } catch (err: any) {
      message.error('載入工料機組成失敗');
    } finally {
      setLoading(false);
    }
  }, [itemId]);

  useEffect(() => {
    loadItems();
  }, [loadItems]);

  // 新增/編輯對話框開啟
  const openEditModal = (item?: MrsBaseBreakdownItem) => {
    if (item) {
      setEditingItem(item);
      setFormData({
        code: item.code,
        c_name: item.c_name,
        c_unit: item.c_unit,
        quantity: item.quantity,
        unit_price: item.unit_price,
        category: item.category,
        remark: item.remark || '',
      });
    } else {
      setEditingItem(null);
      setFormData({ code: '', c_name: '', c_unit: '式', quantity: 0, unit_price: 0, category: 'material', remark: '' });
    }
    setEditModalVisible(true);
  };

  // 儲存
  const handleSave = async () => {
    if (!formData.c_name.trim()) {
      message.warning('請輸入名稱');
      return;
    }
    try {
      if (editingItem) {
        await mrsBaseApi.updateBreakdownItem(itemId, editingItem.id, formData);
        message.success('細項已更新');
      } else {
        await mrsBaseApi.createBreakdownItem(itemId, formData);
        message.success('細項已新增');
      }
      setEditModalVisible(false);
      loadItems();
      // 觸發重新計算
      const result = await mrsBaseApi.recalcBreakdown(itemId);
      if (onPriceChange) onPriceChange(result.unit_price);
    } catch (err: any) {
      message.error(err?.response?.data?.detail || '儲存失敗');
    }
  };

  // 刪除
  const handleDelete = async (bdId: number) => {
    try {
      await mrsBaseApi.deleteBreakdownItem(itemId, bdId);
      message.success('細項已刪除');
      loadItems();
      const result = await mrsBaseApi.recalcBreakdown(itemId);
      if (onPriceChange) onPriceChange(result.unit_price);
    } catch (err: any) {
      message.error(err?.response?.data?.detail || '刪除失敗');
    }
  };

  // 重新計算
  const handleRecalc = async () => {
    try {
      const result = await mrsBaseApi.recalcBreakdown(itemId);
      message.success(`重新計算完成，單價: ${result.unit_price}`);
      if (onPriceChange) onPriceChange(result.unit_price);
    } catch (err: any) {
      message.error('重新計算失敗');
    }
  };

  // 計算各類別金額與佔比
  const categories = ['labor', 'material', 'equipment', 'misc'];
  const catTotal = (cat: string) =>
    items.filter((i) => i.category === cat).reduce((sum, i) => sum + (i.amount || 0), 0);
  const grandTotal = items.reduce((sum, i) => sum + (i.amount || 0), 0);

  const columns = [
    { title: '代碼', dataIndex: 'code', key: 'code', width: 120 },
    { title: '名稱', dataIndex: 'c_name', key: 'c_name', width: 200 },
    { title: '單位', dataIndex: 'c_unit', key: 'c_unit', width: 80 },
    { title: '數量', dataIndex: 'quantity', key: 'quantity', width: 100, render: (v: number) => v.toFixed(2) },
    { title: '單價', dataIndex: 'unit_price', key: 'unit_price', width: 120, render: (v: number) => v.toLocaleString() },
    { title: '金額', dataIndex: 'amount', key: 'amount', width: 120, render: (v: number) => v.toLocaleString() },
    {
      title: '類別',
      dataIndex: 'category',
      key: 'category',
      width: 80,
      render: (v: string) => {
        const map: Record<string, string> = { labor: '工', material: '料', equipment: '機', misc: '雜' };
        return map[v] || v;
      },
    },
    {
      title: '操作',
      key: 'action',
      width: 120,
      render: (_: any, record: MrsBaseBreakdownItem) => (
        <Space>
          <Button size="small" icon={<EditOutlined />} onClick={() => openEditModal(record)} />
          <Popconfirm title="確認刪除？" onConfirm={() => handleDelete(record.id)} okText="刪除" cancelText="取消">
            <Button size="small" danger icon={<DeleteOutlined />} />
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <div>
      {/* 工具列 */}
      <div style={{ marginBottom: 12, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <Space>
          <Button type="primary" icon={<PlusOutlined />} onClick={() => openEditModal()}>
            新增細項
          </Button>
          <Button icon={<ReloadOutlined />} onClick={handleRecalc}>
            重新計算
          </Button>
        </Space>
        <div style={{ fontWeight: 500 }}>
          總金額：<span style={{ color: '#1890ff' }}>{grandTotal.toLocaleString()}</span>
        </div>
      </div>

      {/* 各類別佔比 */}
      <div style={{ marginBottom: 12, display: 'flex', gap: 16, fontSize: 13 }}>
        {categories.map((cat) => {
          const total = catTotal(cat);
          const pct = grandTotal > 0 ? ((total / grandTotal) * 100).toFixed(1) : '0.0';
          const label = { labor: '工', material: '料', equipment: '機', misc: '雜' }[cat];
          return (
            <div key={cat} style={{ background: '#fafafa', padding: '6px 12px', borderRadius: 4, border: '1px solid #f0f0f0' }}>
              {label}: {total.toLocaleString()} ({pct}%)
            </div>
          );
        })}
      </div>

      {/* 表格 */}
      <Table
        dataSource={items}
        columns={columns}
        rowKey="id"
        loading={loading}
        pagination={false}
        size="small"
        scroll={{ y: 300 }}
      />

      {/* 新增/編輯對話框 */}
      <Modal
        title={editingItem ? '編輯工料機細項' : '新增工料機細項'}
        open={editModalVisible}
        onOk={handleSave}
        onCancel={() => setEditModalVisible(false)}
        okText="儲存"
        cancelText="取消"
      >
        <div style={{ display: 'flex', flexDirection: 'column', gap: 12 }}>
          <div>
            <div style={{ marginBottom: 4, fontWeight: 500 }}>名稱 *</div>
            <Input value={formData.c_name} onChange={(e) => setFormData({ ...formData, c_name: e.target.value })} placeholder="請輸入名稱" />
          </div>
          <div style={{ display: 'flex', gap: 12 }}>
            <div style={{ flex: 1 }}>
              <div style={{ marginBottom: 4, fontWeight: 500 }}>代碼</div>
              <Input value={formData.code} onChange={(e) => setFormData({ ...formData, code: e.target.value })} />
            </div>
            <div style={{ flex: 1 }}>
              <div style={{ marginBottom: 4, fontWeight: 500 }}>單位</div>
              <Input value={formData.c_unit} onChange={(e) => setFormData({ ...formData, c_unit: e.target.value })} />
            </div>
          </div>
          <div style={{ display: 'flex', gap: 12 }}>
            <div style={{ flex: 1 }}>
              <div style={{ marginBottom: 4, fontWeight: 500 }}>數量</div>
              <InputNumber
                style={{ width: '100%' }}
                value={formData.quantity}
                onChange={(v) => setFormData({ ...formData, quantity: v || 0 })}
                min={0}
                step={0.01}
              />
            </div>
            <div style={{ flex: 1 }}>
              <div style={{ marginBottom: 4, fontWeight: 500 }}>單價</div>
              <InputNumber
                style={{ width: '100%' }}
                value={formData.unit_price}
                onChange={(v) => setFormData({ ...formData, unit_price: v || 0 })}
                min={0}
                step={1}
              />
            </div>
          </div>
          <div>
            <div style={{ marginBottom: 4, fontWeight: 500 }}>類別</div>
            <Select
              style={{ width: '100%' }}
              value={formData.category}
              onChange={(v) => setFormData({ ...formData, category: v })}
              options={categoryOptions}
            />
          </div>
          <div>
            <div style={{ marginBottom: 4, fontWeight: 500 }}>備註</div>
            <Input.TextArea
              value={formData.remark}
              onChange={(e) => setFormData({ ...formData, remark: e.target.value })}
              rows={2}
            />
          </div>
        </div>
      </Modal>
    </div>
  );
};

export default MrsBaseBreakdownPanel;
