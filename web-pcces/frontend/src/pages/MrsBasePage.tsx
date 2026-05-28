/* MrsBase 公共單價庫主頁 */

import React, { useState, useCallback } from 'react';
import { Card, Button, Space, message, Input, Splitter, Tooltip } from 'antd';
import { PlusOutlined, EditOutlined, DeleteOutlined, ReloadOutlined, SearchOutlined, CheckOutlined, CloseOutlined, BookOutlined } from '@ant-design/icons';
import MrsBaseCategoryTree from '../components/mrs-base/MrsBaseCategoryTree';
import MrsBaseItemGrid from '../components/mrs-base/MrsBaseItemGrid';
import MrsBaseItemEditModal from '../components/mrs-base/MrsBaseItemEditModal';
import { mrsBaseApi } from '../api';
import type { MrsBaseItem, MrsBaseCategory } from '../types';

const MrsBasePage: React.FC = () => {
  const [selectedCategoryId, setSelectedCategoryId] = useState<number | null>(null);
  const [selectedItems, setSelectedItems] = useState<MrsBaseItem[]>([]);
  const [searchKeyword, setSearchKeyword] = useState('');
  const [editModalVisible, setEditModalVisible] = useState(false);
  const [editingItem, setEditingItem] = useState<MrsBaseItem | null>(null);
  const [categories, setCategories] = useState<MrsBaseCategory[]>([]);
  const [refreshKey, setRefreshKey] = useState(0);

  // 觸發刷新
  const triggerRefresh = useCallback(() => {
    setRefreshKey((k) => k + 1);
  }, []);

  // 載入分類（傳給編輯 Modal 用）
  const loadCategories = useCallback(async () => {
    try {
      const data = await mrsBaseApi.getCategories();
      setCategories(data);
    } catch {
      // 忽略
    }
  }, []);

  // 開啟新增視窗
  const handleAdd = () => {
    loadCategories();
    setEditingItem(null);
    setEditModalVisible(true);
  };

  // 開啟編輯視窗
  const handleEdit = (item?: MrsBaseItem) => {
    const target = item || (selectedItems.length === 1 ? selectedItems[0] : null);
    if (!target) {
      message.warning('請選取一個項目進行編輯');
      return;
    }
    loadCategories();
    setEditingItem(target);
    setEditModalVisible(true);
  };

  // 在表格雙擊時編輯
  const handleDoubleClick = (item: MrsBaseItem) => {
    loadCategories();
    setEditingItem(item);
    setEditModalVisible(true);
  };

  // 刪除
  const handleDelete = async () => {
    if (selectedItems.length === 0) {
      message.warning('請選取要刪除的項目');
      return;
    }
    const ids = selectedItems.map((i) => i.id);
    try {
      await mrsBaseApi.batchDeleteItems(ids);
      message.success(`已刪除 ${ids.length} 筆項目`);
      triggerRefresh();
      setSelectedItems([]);
    } catch (err: any) {
      message.error(err?.response?.data?.detail || '刪除失敗');
    }
  };

  // 審核
  const handleApprove = async () => {
    if (selectedItems.length === 0) {
      message.warning('請選取要審核的項目');
      return;
    }
    try {
      for (const item of selectedItems) {
        if (!item.is_approved) {
          await mrsBaseApi.approveItem(item.id);
        }
      }
      message.success(`已審核 ${selectedItems.length} 筆項目`);
      triggerRefresh();
    } catch (err: any) {
      message.error(err?.response?.data?.detail || '審核失敗');
    }
  };

  // 取消審核
  const handleUnapprove = async () => {
    if (selectedItems.length === 0) {
      message.warning('請選取項目');
      return;
    }
    try {
      for (const item of selectedItems) {
        if (item.is_approved) {
          await mrsBaseApi.unapproveItem(item.id);
        }
      }
      message.success(`已取消 ${selectedItems.length} 筆審核`);
      triggerRefresh();
    } catch (err: any) {
      message.error(err?.response?.data?.detail || '取消審核失敗');
    }
  };

  // 儲存後回呼
  const handleSaved = () => {
    triggerRefresh();
  };

  return (
    <div style={{ height: 'calc(100vh - 120px)', display: 'flex', flexDirection: 'column' }}>
      {/* 頁面標題 */}
      <div style={{ marginBottom: 12, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <h2 style={{ margin: 0 }}>公共單價庫</h2>
        <Space>
          <Input.Search
            placeholder="搜尋代碼/名稱…"
            style={{ width: 250 }}
            value={searchKeyword}
            onChange={(e) => setSearchKeyword(e.target.value)}
            onSearch={(value) => setSearchKeyword(value)}
            allowClear
          />
          <Button type="primary" icon={<PlusOutlined />} onClick={handleAdd}>
            新增項目
          </Button>
          <Button icon={<EditOutlined />} onClick={() => handleEdit()} disabled={selectedItems.length !== 1}>
            編輯
          </Button>
          <Button icon={<DeleteOutlined />} onClick={handleDelete} disabled={selectedItems.length === 0} danger>
            刪除
          </Button>
          <Button icon={<CheckOutlined />} onClick={handleApprove} disabled={selectedItems.length === 0}>
            審核
          </Button>
          <Button icon={<CloseOutlined />} onClick={handleUnapprove} disabled={selectedItems.length === 0}>
            取消審核
          </Button>
          <Button icon={<ReloadOutlined />} onClick={triggerRefresh}>
            刷新
          </Button>
        </Space>
      </div>

      {/* 左右分割：分類樹 + 項目表格 */}
      <div style={{ flex: 1, display: 'flex', gap: 12, overflow: 'hidden' }}>
        {/* 左側：分類樹 */}
        <Card
          title="分類"
          size="small"
          style={{ width: 280, minWidth: 200, overflow: 'auto' }}
          bodyStyle={{ padding: 0 }}
        >
          <MrsBaseCategoryTree
            selectedCategoryId={selectedCategoryId}
            onSelect={setSelectedCategoryId}
          />
        </Card>

        {/* 右側：項目表格 */}
        <Card
          title={`項目列表${selectedCategoryId ? ` (分類篩選中)` : ''}${searchKeyword ? ` (搜尋: ${searchKeyword})` : ''}`}
          size="small"
          style={{ flex: 1, overflow: 'hidden' }}
          bodyStyle={{ padding: 0, height: 'calc(100% - 38px)' }}
        >
          <MrsBaseItemGrid
            key={refreshKey}
            categoryId={selectedCategoryId}
            searchKeyword={searchKeyword}
            onSelectionChange={setSelectedItems}
            onDoubleClick={handleDoubleClick}
          />
        </Card>
      </div>

      {/* 編輯/新增對話框 */}
      <MrsBaseItemEditModal
        visible={editModalVisible}
        editingItem={editingItem}
        categoryId={selectedCategoryId}
        categories={categories}
        onClose={() => setEditModalVisible(false)}
        onSaved={handleSaved}
      />
    </div>
  );
};

export default MrsBasePage;
