/* MrsBase 項目表格（AG Grid） */

import React, { useEffect, useState, useCallback, useRef } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { Spin, Tag } from 'antd';
import type { ColDef, GridOptions } from 'ag-grid-community';
import { mrsBaseApi } from '../../api';
import type { MrsBaseItem } from '../../types';

interface Props {
  categoryId: number | null;
  searchKeyword?: string;
  onSelectionChange?: (items: MrsBaseItem[]) => void;
  onDoubleClick?: (item: MrsBaseItem) => void;
}

const MrsBaseItemGrid: React.FC<Props> = ({ categoryId, searchKeyword, onSelectionChange, onDoubleClick }) => {
  const [items, setItems] = useState<MrsBaseItem[]>([]);
  const [loading, setLoading] = useState(false);
  const gridRef = useRef<AgGridReact>(null);

  const loadItems = useCallback(async () => {
    setLoading(true);
    try {
      const params: Record<string, any> = { per_page: 200 };
      if (categoryId) params.category_id = categoryId;
      if (searchKeyword) params.q = searchKeyword;
      const result = await mrsBaseApi.listItems(params);
      setItems(result.items);
    } catch (err: any) {
      console.error('載入項目失敗', err);
    } finally {
      setLoading(false);
    }
  }, [categoryId, searchKeyword]);

  useEffect(() => {
    loadItems();
  }, [loadItems]);

  // 欄位定義
  const columnDefs: ColDef[] = [
    { field: 'code', headerName: '編碼', width: 120, sortable: true, filter: true },
    { field: 'c_name', headerName: '名稱', width: 260, sortable: true, filter: true },
    { field: 'c_unit', headerName: '單位', width: 70, sortable: true },
    {
      field: 'unit_price',
      headerName: '單價',
      width: 120,
      sortable: true,
      cellStyle: { textAlign: 'right' },
      valueFormatter: (params) => (params.value || 0).toLocaleString(),
    },
    {
      field: 'cost_kind',
      headerName: '成本種類',
      width: 100,
      sortable: true,
      cellRenderer: (params: any) => {
        const map: Record<string, { label: string; color: string }> = {
          '1': { label: '工', color: 'blue' },
          '2': { label: '料', color: 'green' },
          '3': { label: '機', color: 'orange' },
          '4': { label: '雜', color: 'default' },
        };
        const info = map[params.value] || { label: params.value, color: 'default' };
        return <Tag color={info.color}>{info.label}</Tag>;
      },
    },
    {
      field: 'is_analysis',
      headerName: '分析',
      width: 70,
      sortable: true,
      cellRenderer: (params: any) => params.value ? <Tag color="purple">啟用</Tag> : '',
    },
    {
      field: 'is_approved',
      headerName: '審核',
      width: 70,
      sortable: true,
      cellRenderer: (params: any) => params.value
        ? <Tag color="green">通過</Tag>
        : <Tag color="default">草稿</Tag>,
    },
    {
      field: 'created_at',
      headerName: '建立時間',
      width: 160,
      sortable: true,
      valueFormatter: (params) => params.value ? new Date(params.value).toLocaleString() : '',
    },
  ];

  const gridOptions: GridOptions = {
    defaultColDef: {
      resizable: true,
    },
    rowSelection: 'multiple',
    onSelectionChanged: () => {
      if (onSelectionChange && gridRef.current) {
        const selected = gridRef.current.api.getSelectedRows() as MrsBaseItem[];
        onSelectionChange(selected);
      }
    },
    onRowDoubleClicked: (event) => {
      if (onDoubleClick && event.data) {
        onDoubleClick(event.data);
      }
    },
  };

  return (
    <Spin spinning={loading}>
      <div className="ag-theme-alpine" style={{ height: 'calc(100vh - 280px)', width: '100%' }}>
        <AgGridReact
          ref={gridRef}
          rowData={items}
          columnDefs={columnDefs}
          gridOptions={gridOptions}
          animateRows
          enableCellTextSelection
        />
      </div>
    </Spin>
  );
};

export default MrsBaseItemGrid;
