/* 期別計價列表頁面 — 分包合約期別計價管理 */

import React, { useEffect, useState, useCallback } from 'react';
import {
  Card, Table, Button, Space, Modal, Form, Input, DatePicker,
  message, Typography, Tag, Popconfirm, Tooltip, Badge,
} from 'antd';
import {
  PlusOutlined, EyeOutlined, DeleteOutlined, DollarOutlined,
  ReloadOutlined, ArrowLeftOutlined,
} from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';
import { contractApi } from '../api';
import type { ContractIssue, ContractIssueCreateData } from '../types';
import type { ColumnsType } from 'antd/es/table';

const { Title, Text } = Typography;

const statusConfig: Record<string, { color: string; label: string }> = {
  draft: { color: 'default', label: '草稿' },
  submitted: { color: 'processing', label: '已提交' },
  approved: { color: 'success', label: '已核准' },
};

const IssueListPage: React.FC = () => {
  const { id: projectId, contractId } = useParams<{ id: string; contractId: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');
  const cid = parseInt(contractId || '0');

  const [issues, setIssues] = useState<ContractIssue[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [form] = Form.useForm();

  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const data = await contractApi.listIssues(cid);
      setIssues(data);
    } catch {
      message.error('載入期別計價資料失敗');
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
      const data: ContractIssueCreateData = {
        ...values,
        issue_date: values.issue_date?.format('YYYY-MM-DD'),
      };
      await contractApi.createIssue(cid, data);
      message.success('期別計價單已建立');
      setModalOpen(false);
      fetchData();
    } catch (err: any) {
      if (err?.errorFields) return;
      message.error('建立失敗');
    } finally {
      setSubmitting(false);
    }
  };

  const handleDelete = async (issueId: number) => {
    try {
      await contractApi.deleteIssue(cid, issueId);
      message.success('已刪除');
      fetchData();
    } catch {
      message.error('刪除失敗，僅草稿可刪除');
    }
  };

  const columns: ColumnsType<ContractIssue> = [
    {
      title: '期別', dataIndex: 'issue_no', key: 'issue_no', width: 80, align: 'center',
      render: (v: number) => <Text strong>第 {v} 期</Text>,
    },
    {
      title: '名稱', dataIndex: 'c_name', key: 'c_name', ellipsis: true,
    },
    {
      title: '狀態', dataIndex: 'status', key: 'status', width: 100, align: 'center',
      render: (s: string) => {
        const cfg = statusConfig[s] || { color: 'default', label: s };
        return <Badge status={cfg.color as any} text={cfg.label} />;
      },
    },
    {
      title: '本期金額', dataIndex: 'total_amount', key: 'total_amount', width: 130, align: 'right',
      render: (v: number) => <Text>${(v || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>,
    },
    {
      title: '累計金額', dataIndex: 'cumulative_amount', key: 'cumulative_amount', width: 130, align: 'right',
      render: (v: number) => <Text>${(v || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>,
    },
    {
      title: '進度', dataIndex: 'progress_rate', key: 'progress_rate', width: 90, align: 'center',
      render: (v: number) => `${(v || 0).toFixed(1)}%`,
    },
    {
      title: '建立日期', dataIndex: 'created_at', key: 'created_at', width: 110,
      render: (v: string) => dayjs(v).format('MM/DD'),
    },
    {
      title: '操作', key: 'action', width: 140, align: 'center',
      render: (_: any, record: ContractIssue) => (
        <Space size="small">
          <Tooltip title="檢視/編輯">
            <Button type="link" size="small" icon={<EyeOutlined />}
              onClick={() => navigate(`/projects/${pid}/contracts/${cid}/issues/${record.id}`)}>
              檢視
            </Button>
          </Tooltip>
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
          <Button icon={<ArrowLeftOutlined />} onClick={() => navigate(`/projects/${pid}/contracts`)}>
            返回合約
          </Button>
          <Title level={4} style={{ margin: 0 }}>
            <DollarOutlined style={{ marginRight: 8 }} />
            期別計價管理
          </Title>
        </Space>
        <Space>
          <Button icon={<ReloadOutlined />} onClick={fetchData}>重新整理</Button>
          <Button type="primary" icon={<PlusOutlined />} onClick={handleCreate}>新增期別計價</Button>
        </Space>
      </div>

      <Card>
        <Table
          dataSource={issues}
          columns={columns}
          rowKey="id"
          loading={loading}
          pagination={{ pageSize: 20, showSizeChanger: true, showTotal: (t) => `共 ${t} 筆` }}
          locale={{ emptyText: '尚無期別計價資料' }}
          onRow={(record) => ({
            style: { cursor: 'pointer' },
            onClick: () => navigate(`/projects/${pid}/contracts/${cid}/issues/${record.id}`),
          })}
        />
      </Card>

      <Modal
        title="新增期別計價"
        open={modalOpen}
        onOk={handleSubmitCreate}
        onCancel={() => setModalOpen(false)}
        confirmLoading={submitting}
        okText="建立"
        cancelText="取消"
      >
        <Form form={form} layout="vertical" style={{ marginTop: 16 }}>
          <Form.Item name="c_name" label="計價名稱">
            <Input placeholder="例如：第1期計價" />
          </Form.Item>
          <Form.Item name="issue_date" label="計價日期">
            <DatePicker style={{ width: '100%' }} />
          </Form.Item>
          <Form.Item name="remark" label="備註">
            <Input.TextArea rows={3} />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default IssueListPage;
