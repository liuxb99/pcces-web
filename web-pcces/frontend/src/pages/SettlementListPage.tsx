/* 結算列表頁面 — 分包合約結算管理 */

import React, { useEffect, useState, useCallback } from 'react';
import {
  Card, Table, Button, Space, Modal, Form, Input, DatePicker,
  message, Typography, Tag, Popconfirm, Tooltip, Badge,
} from 'antd';
import {
  PlusOutlined, EyeOutlined, DeleteOutlined, FileTextOutlined,
  ReloadOutlined, ArrowLeftOutlined,
} from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';
import { contractApi } from '../api';
import type { ContractSettlement, ContractSettlementCreateData } from '../types';
import type { ColumnsType } from 'antd/es/table';

const { Title, Text } = Typography;

const statusConfig: Record<string, { color: string; label: string }> = {
  draft: { color: 'default', label: '草稿' },
  submitted: { color: 'processing', label: '已提交' },
  approved: { color: 'success', label: '已核准' },
};

const SettlementListPage: React.FC = () => {
  const { id: projectId, contractId } = useParams<{ id: string; contractId: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');
  const cid = parseInt(contractId || '0');

  const [settlements, setSettlements] = useState<ContractSettlement[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [form] = Form.useForm();

  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const data = await contractApi.listSettlements(cid);
      setSettlements(data);
    } catch {
      message.error('載入結算資料失敗');
    } finally {
      setLoading(false);
    }
  }, [cid]);

  useEffect(() => { fetchData(); }, [fetchData]);

  const handleCreate = () => {
    form.resetFields();
    setModalOpen(true);
  };

  const handleSubmitCreate = async () => {
    try {
      const values = await form.validateFields();
      setSubmitting(true);
      const data: ContractSettlementCreateData = {
        ...values,
        settlement_date: values.settlement_date?.format('YYYY-MM-DD'),
      };
      await contractApi.createSettlement(cid, data);
      message.success('結算單已建立');
      setModalOpen(false);
      fetchData();
    } catch (err: any) {
      if (err?.errorFields) return;
      message.error('建立失敗');
    } finally {
      setSubmitting(false);
    }
  };

  const handleDelete = async (id: number) => {
    try {
      await contractApi.deleteSettlement(cid, id);
      message.success('已刪除');
      fetchData();
    } catch {
      message.error('刪除失敗，僅草稿可刪除');
    }
  };

  const columns: ColumnsType<ContractSettlement> = [
    { title: '結算編號', dataIndex: 'settlement_no', key: 'settlement_no', width: 120 },
    { title: '名稱', dataIndex: 'c_name', key: 'c_name', ellipsis: true },
    {
      title: '狀態', dataIndex: 'status', key: 'status', width: 100, align: 'center',
      render: (s: string) => {
        const cfg = statusConfig[s] || { color: 'default', label: s };
        return <Badge status={cfg.color as any} text={cfg.label} />;
      },
    },
    {
      title: '合約金額', dataIndex: 'contract_amount', key: 'contract_amount', width: 130, align: 'right',
      render: (v: number) => <Text>${(v || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>,
    },
    {
      title: '結算金額', dataIndex: 'settlement_amount', key: 'settlement_amount', width: 130, align: 'right',
      render: (v: number) => <Text strong>${(v || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>,
    },
    {
      title: '日期', dataIndex: 'settlement_date', key: 'settlement_date', width: 110,
      render: (v: string) => v || '-',
    },
    {
      title: '建立', dataIndex: 'created_at', key: 'created_at', width: 110,
      render: (v: string) => dayjs(v).format('MM/DD'),
    },
    {
      title: '操作', key: 'action', width: 120, align: 'center',
      render: (_: any, record: ContractSettlement) => (
        <Space size="small">
          <Button type="link" size="small" icon={<EyeOutlined />}
            onClick={() => navigate(`/projects/${pid}/contracts/${cid}/settlements/${record.id}`)}>
            檢視
          </Button>
          {record.status === 'draft' && (
            <Popconfirm title="確定刪除？" onConfirm={() => handleDelete(record.id)}>
              <Button type="link" size="small" danger icon={<DeleteOutlined />} />
            </Popconfirm>
          )}
        </Space>
      ),
    },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Space>
          <Button icon={<ArrowLeftOutlined />} onClick={() => navigate(`/projects/${pid}/contracts`)}>返回合約</Button>
          <Title level={4} style={{ margin: 0 }}>
            <FileTextOutlined style={{ marginRight: 8 }} />
            結算管理
          </Title>
        </Space>
        <Space>
          <Button icon={<ReloadOutlined />} onClick={fetchData}>重新整理</Button>
          <Button type="primary" icon={<PlusOutlined />} onClick={handleCreate}>新增結算</Button>
        </Space>
      </div>

      <Card>
        <Table
          dataSource={settlements}
          columns={columns}
          rowKey="id"
          loading={loading}
          pagination={{ pageSize: 20, showSizeChanger: true, showTotal: (t) => `共 ${t} 筆` }}
          locale={{ emptyText: '尚無結算資料' }}
          onRow={(record) => ({
            style: { cursor: 'pointer' },
            onClick: () => navigate(`/projects/${pid}/contracts/${cid}/settlements/${record.id}`),
          })}
        />
      </Card>

      <Modal title="新增結算單" open={modalOpen} onOk={handleSubmitCreate} onCancel={() => setModalOpen(false)}
        confirmLoading={submitting} okText="建立" cancelText="取消">
        <Form form={form} layout="vertical" style={{ marginTop: 16 }}>
          <Form.Item name="c_name" label="結算名稱"><Input placeholder="例如：工程結算" /></Form.Item>
          <Form.Item name="settlement_no" label="結算編號"><Input placeholder="自動產生" /></Form.Item>
          <Form.Item name="settlement_date" label="結算日期"><DatePicker style={{ width: '100%' }} /></Form.Item>
          <Form.Item name="remark" label="備註"><Input.TextArea rows={3} /></Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default SettlementListPage;
