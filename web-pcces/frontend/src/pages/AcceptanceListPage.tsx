/* 終驗列表頁面 — 分包合約終驗管理 */

import React, { useEffect, useState, useCallback } from 'react';
import {
  Card, Table, Button, Space, Modal, Form, Input, DatePicker, Select,
  message, Typography, Tag, Popconfirm, Tooltip, Badge,
} from 'antd';
import {
  PlusOutlined, EyeOutlined, DeleteOutlined, CheckSquareOutlined,
  ReloadOutlined, ArrowLeftOutlined,
} from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';
import { contractApi } from '../api';
import type { ContractFinalAcceptance, ContractFinalAcceptanceCreateData } from '../types';
import type { ColumnsType } from 'antd/es/table';

const { Title, Text } = Typography;

const statusConfig: Record<string, { color: string; label: string }> = {
  draft: { color: 'default', label: '草稿' },
  submitted: { color: 'processing', label: '已提交' },
  approved: { color: 'success', label: '已核准' },
};

const resultConfig: Record<string, { color: string; label: string }> = {
  pass: { color: 'success', label: '合格' },
  conditional_pass: { color: 'warning', label: '附條件合格' },
  fail: { color: 'error', label: '不合格' },
};

const AcceptanceListPage: React.FC = () => {
  const { id: projectId, contractId } = useParams<{ id: string; contractId: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');
  const cid = parseInt(contractId || '0');

  const [acceptances, setAcceptances] = useState<ContractFinalAcceptance[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [form] = Form.useForm();

  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const data = await contractApi.listAcceptances(cid);
      setAcceptances(data);
    } catch {
      message.error('載入終驗資料失敗');
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
      const data: ContractFinalAcceptanceCreateData = {
        ...values,
        acceptance_date: values.acceptance_date?.format('YYYY-MM-DD'),
      };
      await contractApi.createAcceptance(cid, data);
      message.success('終驗單已建立');
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
      await contractApi.deleteAcceptance(cid, id);
      message.success('已刪除');
      fetchData();
    } catch {
      message.error('刪除失敗，僅草稿可刪除');
    }
  };

  const columns: ColumnsType<ContractFinalAcceptance> = [
    { title: '終驗編號', dataIndex: 'acceptance_no', key: 'acceptance_no', width: 120 },
    { title: '名稱', dataIndex: 'c_name', key: 'c_name', ellipsis: true },
    {
      title: '狀態', dataIndex: 'status', key: 'status', width: 100, align: 'center',
      render: (s: string) => {
        const cfg = statusConfig[s] || { color: 'default', label: s };
        return <Badge status={cfg.color as any} text={cfg.label} />;
      },
    },
    {
      title: '結果', dataIndex: 'result', key: 'result', width: 120, align: 'center',
      render: (r: string | null) => {
        if (!r) return <Text type="secondary">-</Text>;
        const cfg = resultConfig[r] || { color: 'default', label: r };
        return <Tag color={cfg.color}>{cfg.label}</Tag>;
      },
    },
    { title: '檢驗人員', dataIndex: 'inspector', key: 'inspector', width: 120, render: (v: string | null) => v || '-' },
    { title: '日期', dataIndex: 'acceptance_date', key: 'acceptance_date', width: 110, render: (v: string) => v || '-' },
    {
      title: '建立', dataIndex: 'created_at', key: 'created_at', width: 110,
      render: (v: string) => dayjs(v).format('MM/DD'),
    },
    {
      title: '操作', key: 'action', width: 120, align: 'center',
      render: (_: any, record: ContractFinalAcceptance) => (
        <Space size="small">
          <Button type="link" size="small" icon={<EyeOutlined />}
            onClick={() => navigate(`/projects/${pid}/contracts/${cid}/acceptances/${record.id}`)}>
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
            <CheckSquareOutlined style={{ marginRight: 8 }} />
            終驗管理
          </Title>
        </Space>
        <Space>
          <Button icon={<ReloadOutlined />} onClick={fetchData}>重新整理</Button>
          <Button type="primary" icon={<PlusOutlined />} onClick={handleCreate}>新增終驗</Button>
        </Space>
      </div>

      <Card>
        <Table
          dataSource={acceptances}
          columns={columns}
          rowKey="id"
          loading={loading}
          pagination={{ pageSize: 20, showSizeChanger: true, showTotal: (t) => `共 ${t} 筆` }}
          locale={{ emptyText: '尚無終驗資料' }}
          onRow={(record) => ({
            style: { cursor: 'pointer' },
            onClick: () => navigate(`/projects/${pid}/contracts/${cid}/acceptances/${record.id}`),
          })}
        />
      </Card>

      <Modal title="新增終驗單" open={modalOpen} onOk={handleSubmitCreate} onCancel={() => setModalOpen(false)}
        confirmLoading={submitting} okText="建立" cancelText="取消">
        <Form form={form} layout="vertical" style={{ marginTop: 16 }}>
          <Form.Item name="c_name" label="終驗名稱"><Input placeholder="例如：工程終驗" /></Form.Item>
          <Form.Item name="acceptance_no" label="終驗編號"><Input placeholder="自動產生" /></Form.Item>
          <Form.Item name="acceptance_date" label="終驗日期"><DatePicker style={{ width: '100%' }} /></Form.Item>
          <Form.Item name="inspector" label="檢驗人員"><Input /></Form.Item>
          <Form.Item name="result" label="結果">
            <Select allowClear placeholder="選擇結果">
              <Select.Option value="pass">合格</Select.Option>
              <Select.Option value="conditional_pass">附條件合格</Select.Option>
              <Select.Option value="fail">不合格</Select.Option>
            </Select>
          </Form.Item>
          <Form.Item name="defect_description" label="缺失說明"><Input.TextArea rows={3} /></Form.Item>
          <Form.Item name="remark" label="備註"><Input.TextArea rows={2} /></Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default AcceptanceListPage;
