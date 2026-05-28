/* MrsBase 項目編輯視窗 */

import React, { useEffect, useState } from 'react';
import { Modal, Form, Input, InputNumber, Select, Tabs, message, Switch } from 'antd';
import { mrsBaseApi } from '../../api';
import type { MrsBaseItem, MrsBaseCategory } from '../../types';
import MrsBaseBreakdownPanel from './MrsBaseBreakdownPanel';

interface Props {
  visible: boolean;
  editingItem: MrsBaseItem | null;   // null = 新增模式
  categoryId: number | null;        // 預設分類
  categories: MrsBaseCategory[];    // 分類選項（平面列表）
  onClose: () => void;
  onSaved: () => void;
}

const costKindOptions = [
  { value: '工', label: '工' },
  { value: '料', label: '料' },
  { value: '機', label: '機' },
  { value: '雜', label: '雜' },
];

const itemTypeOptions = [
  { value: 'B', label: '主要項目 (B)' },
  { value: 'L', label: '單價項目 (L)' },
  { value: 'W', label: '工作項目 (W)' },
];

const MrsBaseItemEditModal: React.FC<Props> = ({
  visible, editingItem, categoryId, categories, onClose, onSaved,
}) => {
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);
  const [activeTab, setActiveTab] = useState('basic');
  const [unitPrice, setUnitPrice] = useState(0);

  // 從分類樹中提取平面選項
  const flattenCategories = (nodes: MrsBaseCategory[]): { value: number; label: string }[] => {
    const result: { value: number; label: string }[] = [];
    const walk = (list: MrsBaseCategory[], prefix = '') => {
      for (const node of list) {
        result.push({ value: node.id, label: `${prefix}${node.c_name}` });
        if (node.children) walk(node.children, `${prefix}  `);
      }
    };
    walk(nodes);
    return result;
  };

  const flatCatOptions = flattenCategories(categories);

  // 表單值變更時更新 unitPrice
  useEffect(() => {
    const price = form.getFieldValue('unit_price');
    if (price !== undefined) setUnitPrice(Number(price) || 0);
  }, [form.getFieldValue('unit_price')]);

  // 開啟時填入表單
  useEffect(() => {
    if (visible && editingItem) {
      form.setFieldsValue({
        category_id: editingItem.category_id,
        code: editingItem.code,
        pub_code: editingItem.pub_code,
        c_name: editingItem.c_name,
        e_name: editingItem.e_name,
        c_unit: editingItem.c_unit,
        e_unit: editingItem.e_unit,
        unit_price: editingItem.unit_price,
        cost_kind: editingItem.cost_kind,
        item_type: editingItem.item_type,
        is_analysis: editingItem.is_analysis,
        labor_rate: editingItem.labor_rate,
        material_rate: editingItem.material_rate,
        equipment_rate: editingItem.equipment_rate,
        misc_rate: editingItem.misc_rate,
        decimal_qty: editingItem.decimal_qty,
        decimal_price: editingItem.decimal_price,
        decimal_amount: editingItem.decimal_amount,
        memo: editingItem.memo,
      });
      setUnitPrice(editingItem.unit_price);
    } else if (visible) {
      form.resetFields();
      if (categoryId) form.setFieldValue('category_id', categoryId);
      form.setFieldValue('cost_kind', '料');
      form.setFieldValue('item_type', 'W');
      form.setFieldValue('c_unit', '式');
      setUnitPrice(0);
    }
  }, [visible, editingItem, categoryId, form]);

  const handleSave = async () => {
    try {
      const values = await form.validateFields();
      setLoading(true);
      if (editingItem) {
        await mrsBaseApi.updateItem(editingItem.id, values);
        message.success('項目已更新');
      } else {
        await mrsBaseApi.createItem(values);
        message.success('項目已建立');
      }
      onSaved();
      onClose();
    } catch (err: any) {
      if (err?.response?.data?.detail) {
        message.error(err.response.data.detail);
      } else if (err?.errorFields) {
        // 表單驗證錯誤，不另行提示
      } else {
        message.error('儲存失敗');
      }
    } finally {
      setLoading(false);
    }
  };

  // 工料機組成價格變更回呼
  const handleBreakdownPriceChange = (newPrice: number) => {
    setUnitPrice(newPrice);
    form.setFieldValue('unit_price', newPrice);
  };

  return (
    <Modal
      title={editingItem ? `編輯項目：${editingItem.code}` : '新增公共單價項目'}
      open={visible}
      onOk={handleSave}
      onCancel={onClose}
      okText="儲存"
      cancelText="取消"
      confirmLoading={loading}
      width={760}
    >
      <Tabs activeKey={activeTab} onChange={setActiveTab}>
        {/* Tab 1: 基本資料 */}
        <Tabs.TabPane tab="基本資料" key="basic">
          <Form form={form} layout="vertical" style={{ marginTop: 12 }}>
            <Form.Item name="category_id" label="所屬分類" rules={[{ required: true, message: '請選擇分類' }]}>
              <Select options={flatCatOptions} placeholder="請選擇分類" />
            </Form.Item>
            <div style={{ display: 'flex', gap: 12 }}>
              <Form.Item name="code" label="編碼" rules={[{ required: true, message: '請輸入編碼' }]} style={{ flex: 1 }}>
                <Input placeholder="唯一編碼" />
              </Form.Item>
              <Form.Item name="pub_code" label="公共工程代碼" style={{ flex: 1 }}>
                <Input placeholder="選填" />
              </Form.Item>
            </div>
            <Form.Item name="c_name" label="中文名稱" rules={[{ required: true, message: '請輸入名稱' }]}>
              <Input placeholder="請輸入中文名稱" />
            </Form.Item>
            <Form.Item name="e_name" label="英文名稱">
              <Input placeholder="選填" />
            </Form.Item>
            <div style={{ display: 'flex', gap: 12 }}>
              <Form.Item name="c_unit" label="中文單位" style={{ flex: 1 }}>
                <Input placeholder="如：式、m³、噸" />
              </Form.Item>
              <Form.Item name="e_unit" label="英文單位" style={{ flex: 1 }}>
                <Input placeholder="選填" />
              </Form.Item>
            </div>
            <div style={{ display: 'flex', gap: 12 }}>
              <Form.Item name="unit_price" label="單價" style={{ flex: 1 }}>
                <InputNumber style={{ width: '100%' }} min={0} step={1} />
              </Form.Item>
              <Form.Item name="cost_kind" label="成本種類" style={{ flex: 1 }}>
                <Select options={costKindOptions} />
              </Form.Item>
              <Form.Item name="item_type" label="項目類型" style={{ flex: 1 }}>
                <Select options={itemTypeOptions} />
              </Form.Item>
            </div>
            <div style={{ display: 'flex', gap: 12 }}>
              <Form.Item name="decimal_qty" label="數量小數位" style={{ flex: 1 }}>
                <InputNumber min={0} max={6} />
              </Form.Item>
              <Form.Item name="decimal_price" label="單價小數位" style={{ flex: 1 }}>
                <InputNumber min={0} max={6} />
              </Form.Item>
              <Form.Item name="decimal_amount" label="金額小數位" style={{ flex: 1 }}>
                <InputNumber min={0} max={6} />
              </Form.Item>
            </div>
            <Form.Item name="memo" label="備註">
              <Input.TextArea rows={2} />
            </Form.Item>
          </Form>
        </Tabs.TabPane>

        {/* Tab 2: 分析設定 */}
        <Tabs.TabPane tab="分析設定" key="analysis">
          <Form form={form} layout="vertical" style={{ marginTop: 12 }}>
            <Form.Item name="is_analysis" label="啟用單價分析" valuePropName="checked">
              <Switch />
            </Form.Item>
            <div style={{ display: 'flex', gap: 12 }}>
              <Form.Item name="labor_rate" label="人工比率 (%)" style={{ flex: 1 }}>
                <InputNumber min={0} max={100} step={0.1} />
              </Form.Item>
              <Form.Item name="material_rate" label="材料比率 (%)" style={{ flex: 1 }}>
                <InputNumber min={0} max={100} step={0.1} />
              </Form.Item>
            </div>
            <div style={{ display: 'flex', gap: 12 }}>
              <Form.Item name="equipment_rate" label="設備比率 (%)" style={{ flex: 1 }}>
                <InputNumber min={0} max={100} step={0.1} />
              </Form.Item>
              <Form.Item name="misc_rate" label="雜項比率 (%)" style={{ flex: 1 }}>
                <InputNumber min={0} max={100} step={0.1} />
              </Form.Item>
            </div>
          </Form>
        </Tabs.TabPane>

        {/* Tab 3: 工料機組成 */}
        {editingItem && (
          <Tabs.TabPane tab="工料機組成" key="breakdown">
            <div style={{ marginTop: 12 }}>
              <div style={{ marginBottom: 8, color: '#666' }}>
                目前單價：<strong style={{ color: '#1890ff' }}>{unitPrice.toLocaleString()}</strong>
              </div>
              <MrsBaseBreakdownPanel
                itemId={editingItem.id}
                onPriceChange={handleBreakdownPriceChange}
              />
            </div>
          </Tabs.TabPane>
        )}

        {/* Tab 4: 備註 */}
        <Tabs.TabPane tab="備註" key="memo">
          <Form form={form} layout="vertical" style={{ marginTop: 12 }}>
            <Form.Item name="memo" label="備註">
              <Input.TextArea rows={6} placeholder="輸入備註文字…" />
            </Form.Item>
          </Form>
        </Tabs.TabPane>
      </Tabs>
    </Modal>
  );
};

export default MrsBaseItemEditModal;
