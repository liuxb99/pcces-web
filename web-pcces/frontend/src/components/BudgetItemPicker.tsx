/* 共用工項選取元件 — 顯示預算工項樹狀結構供勾選匯入 */

import React, { useEffect, useState } from 'react';
import { Modal, Tree, Spin, Typography, Button, Space, message, Input } from 'antd';
import { budgetApi } from '../api';
import type { BudgetItem } from '../types';
import type { DataNode } from 'antd/es/tree';

const { Text } = Typography;

interface BudgetItemPickerProps {
  open: boolean;
  projectId: number;
  title?: string;
  onCancel: () => void;
  onConfirm: (selectedIds: number[]) => void;
  /** 已選取的 ID 集合（用於排除已匯入的項目） */
  excludeIds?: Set<number>;
  loading?: boolean;
}

/** 將 BudgetItem 樹轉為 Ant Design Tree DataNode */
function toTreeNodes(items: BudgetItem[]): DataNode[] {
  return items.map((item) => ({
    key: item.id,
    title: (
      <span>
        {item.print_no && <Text code style={{ fontSize: 12 }}>{item.print_no}</Text>}
        {' '}
        <Text>{item.c_name || '(無名稱)'}</Text>
        {' '}
        {item.c_unit && <Text type="secondary" style={{ fontSize: 12 }}>({item.c_unit})</Text>}
        {' '}
        <Text type="secondary" style={{ fontSize: 12 }}>
          ${(item.amount || 0).toLocaleString()}
        </Text>
      </span>
    ),
    children: item.children?.length > 0 ? toTreeNodes(item.children) : undefined,
    isLeaf: item.kind === 'W',
    selectable: item.kind === 'W',
  }));
}

const BudgetItemPicker: React.FC<BudgetItemPickerProps> = ({
  open, projectId, title = '選取預算工項', onCancel, onConfirm, excludeIds, loading: externalLoading,
}) => {
  const [treeData, setTreeData] = useState<DataNode[]>([]);
  const [checkedKeys, setCheckedKeys] = useState<React.Key[]>([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (!open) return;
    (async () => {
      setLoading(true);
      try {
        const tree = await budgetApi.getTree(projectId);
        setTreeData(toTreeNodes(tree));
        setCheckedKeys([]);
      } catch {
        message.error('載入預算工項失敗');
      } finally {
        setLoading(false);
      }
    })();
  }, [open, projectId]);

  const handleOk = () => {
    // 只回傳 W 類型的葉節點 ID
    const leafIds = checkedKeys.map(Number).filter((id) => !isNaN(id));
    if (leafIds.length === 0) {
      message.warning('請至少選取一個工項');
      return;
    }
    onConfirm(leafIds);
  };

  return (
    <Modal
      title={title}
      open={open}
      onCancel={onCancel}
      onOk={handleOk}
      confirmLoading={externalLoading || loading}
      okText="匯入選取工項"
      cancelText="取消"
      width={700}
    >
      {loading ? (
        <div style={{ textAlign: 'center', padding: 40 }}>
          <Spin />
        </div>
      ) : treeData.length === 0 ? (
        <div style={{ textAlign: 'center', padding: 40 }}>
          <Text type="secondary">暫無預算工項資料</Text>
        </div>
      ) : (
        <div style={{ maxHeight: 450, overflow: 'auto' }}>
          <div style={{ marginBottom: 8 }}>
            <Space>
              <Button size="small" onClick={() => {
                // 選取所有 W 類型節點
                const allKeys: React.Key[] = [];
                const collect = (nodes: DataNode[]) => {
                  for (const n of nodes) {
                    if (n.selectable !== false && !excludeIds?.has(Number(n.key))) {
                      allKeys.push(n.key);
                    }
                    if (n.children) collect(n.children);
                  }
                };
                collect(treeData);
                setCheckedKeys(allKeys);
              }}>
                全選
              </Button>
              <Button size="small" onClick={() => setCheckedKeys([])}>
                清除
              </Button>
              {excludeIds && excludeIds.size > 0 && (
                <Text type="secondary" style={{ fontSize: 12 }}>
                  （{excludeIds.size} 項已匯入，已略過）
                </Text>
              )}
            </Space>
          </div>
          <Tree
            checkable
            defaultExpandAll
            treeData={treeData}
            checkedKeys={checkedKeys}
            onCheck={(keys) => setCheckedKeys(keys as React.Key[])}
          />
        </div>
      )}
    </Modal>
  );
};

export default BudgetItemPicker;
