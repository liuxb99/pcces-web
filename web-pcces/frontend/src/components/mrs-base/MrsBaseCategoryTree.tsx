/* MrsBase 分類樹元件 */

import React, { useEffect, useState, useCallback } from 'react';
import { Tree, Input, Button, Space, message, Modal, Dropdown } from 'antd';
import { PlusOutlined, FolderOutlined, ReloadOutlined } from '@ant-design/icons';
import type { DataNode } from 'antd/es/tree';
import { mrsBaseApi } from '../../api';
import type { MrsBaseCategory } from '../../types';

interface Props {
  selectedCategoryId: number | null;
  onSelect: (categoryId: number | null) => void;
  onRefresh?: () => void;
}

const MrsBaseCategoryTree: React.FC<Props> = ({ selectedCategoryId, onSelect, onRefresh }) => {
  const [categories, setCategories] = useState<MrsBaseCategory[]>([]);
  const [loading, setLoading] = useState(false);
  const [expandedKeys, setExpandedKeys] = useState<React.Key[]>([]);

  // 載入分類樹
  const loadCategories = useCallback(async () => {
    setLoading(true);
    try {
      const data = await mrsBaseApi.getCategories();
      setCategories(data);
    } catch (err: any) {
      message.error('載入分類失敗');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    loadCategories();
  }, [loadCategories]);

  // 將分類轉換為 antd Tree 需要的 DataNode 格式
  const toTreeData = (nodes: MrsBaseCategory[]): DataNode[] => {
    return nodes.map((node) => ({
      key: node.id,
      title: (
        <span>
          <FolderOutlined style={{ marginRight: 6 }} />
          {node.c_name}
          {node.item_count !== undefined && (
            <span style={{ color: '#999', marginLeft: 6, fontSize: 12 }}>
              ({node.item_count})
            </span>
          )}
        </span>
      ),
      children: node.children ? toTreeData(node.children) : undefined,
    }));
  };

  // 遞迴展開所有節點
  const expandAll = (nodes: MrsBaseCategory[]): React.Key[] => {
    const keys: React.Key[] = [];
    nodes.forEach((node) => {
      keys.push(node.id);
      if (node.children) {
        keys.push(...expandAll(node.children));
      }
    });
    return keys;
  };

  // 點選節點
  const handleSelect = (selectedKeys: React.Key[]) => {
    const id = selectedKeys.length > 0 ? Number(selectedKeys[0]) : null;
    onSelect(id);
  };

  // 刷新
  const handleRefresh = () => {
    loadCategories();
    if (onRefresh) onRefresh();
  };

  // 新增分類對話框
  const [addModalVisible, setAddModalVisible] = useState(false);
  const [newCatName, setNewCatName] = useState('');
  const [newCatCode, setNewCatCode] = useState('');

  const handleAddCategory = async () => {
    if (!newCatName.trim()) {
      message.warning('請輸入分類名稱');
      return;
    }
    try {
      await mrsBaseApi.createCategory({
        c_name: newCatName.trim(),
        code: newCatCode.trim() || newCatName.trim(),
        parent_id: selectedCategoryId,
      });
      message.success('分類已建立');
      setAddModalVisible(false);
      setNewCatName('');
      setNewCatCode('');
      loadCategories();
    } catch (err: any) {
      message.error(err?.response?.data?.detail || '建立分類失敗');
    }
  };

  // 刪除分類
  const handleDeleteCategory = (catId: number) => {
    Modal.confirm({
      title: '確認刪除此分類？',
      content: '僅在分類下無子分類及項目時才能刪除',
      okText: '刪除',
      cancelText: '取消',
      okButtonProps: { danger: true },
      onOk: async () => {
        try {
          await mrsBaseApi.deleteCategory(catId);
          message.success('分類已刪除');
          if (selectedCategoryId === catId) {
            onSelect(null);
          }
          loadCategories();
        } catch (err: any) {
          message.error(err?.response?.data?.detail || '刪除失敗');
        }
      },
    });
  };

  return (
    <div style={{ height: '100%', display: 'flex', flexDirection: 'column' }}>
      {/* 工具列 */}
      <div style={{ padding: '8px 12px', borderBottom: '1px solid #f0f0f0', display: 'flex', gap: 6 }}>
        <Button size="small" type="primary" icon={<PlusOutlined />} onClick={() => setAddModalVisible(true)}>
          新增分類
        </Button>
        <Button size="small" icon={<ReloadOutlined />} onClick={handleRefresh} />
      </div>

      {/* 樹狀結構 */}
      <div style={{ flex: 1, overflow: 'auto', padding: '4px 0' }}>
        <Tree
          treeData={toTreeData(categories)}
          selectedKeys={selectedCategoryId ? [selectedCategoryId] : []}
          onSelect={handleSelect}
          expandedKeys={expandedKeys.length > 0 ? expandedKeys : expandAll(categories)}
          onExpand={(keys) => setExpandedKeys(keys)}
          showLine
          style={{ padding: '0 12px' }}
        />
      </div>

      {/* 新增分類對話框 */}
      <Modal
        title="新增分類"
        open={addModalVisible}
        onOk={handleAddCategory}
        onCancel={() => {
          setAddModalVisible(false);
          setNewCatName('');
          setNewCatCode('');
        }}
        okText="建立"
        cancelText="取消"
      >
        <div style={{ display: 'flex', flexDirection: 'column', gap: 12 }}>
          {selectedCategoryId && (
            <div style={{ color: '#666', fontSize: 13 }}>
              上層分類 ID: {selectedCategoryId}
            </div>
          )}
          <div>
            <div style={{ marginBottom: 4, fontWeight: 500 }}>分類名稱 *</div>
            <Input
              value={newCatName}
              onChange={(e) => setNewCatName(e.target.value)}
              placeholder="請輸入分類名稱"
            />
          </div>
          <div>
            <div style={{ marginBottom: 4, fontWeight: 500 }}>分類代碼</div>
            <Input
              value={newCatCode}
              onChange={(e) => setNewCatCode(e.target.value)}
              placeholder="留空則與名稱相同"
            />
          </div>
        </div>
      </Modal>
    </div>
  );
};

export default MrsBaseCategoryTree;
