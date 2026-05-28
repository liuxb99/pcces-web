/* 分包合約列表頁面 */

import React, { useEffect, useState, useCallback } from 'react';
import {
  Card, Table, Button, Space, Modal, Form, Input, InputNumber,
  message, Typography, Tag, Popconfirm, Tooltip,
} from 'antd';
import {
  PlusOutlined, EyeOutlined, DeleteOutlined, LinkOutlined,
  ReloadOutlined, EditOutlined,
} from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';
import { contractApi, projectApi } from '../api';
import type { Contract, ContractCreateData } from '../types';
import type { ColumnsType } from 'antd/es/table';

const { Title, Text } = Typography;

/** 合約狀態標籤 */
const statusConfig: Record<string, { color: string; label: string }> = {
  draft: { color: 'default', label: '草稿' },
  active: { color: 'processing', label: '進行中' },
  closed: { color: 'warning', label: '已結案' },
  finalized: { color: 'success', label: '已終驗' },
};

const ContractListPage: React.FC = () => {
  const { id: projectId } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');

  const [contracts, setContracts] = useState<Contract[]>([]);
  const [project, setProject] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [form] = Form.useForm();

  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const [proj, data] = await Promise.all([
        projectApi.get(pid),
        contractApi.list(pid),
      ]);
      setProject(proj);
      setContracts(data);
    } catch {
      message.error('載入合約資料失敗');
    } finally {
      setLoading(false);
    }
  }, [pid]);

  useEffect(() => { fetchData(); }, [fetchData]);

  // 新增合約
  const handleCreate = () => {
    form.resetFields();
    setModalOpen(true);
  };

  const handleSubmitCreate = async () => {
    try {
      const values = await form.validateFields();
      setSubmitting(true);
      await contractApi.create(pid, values as ContractCreateData);
      message.success('合約已建立');
      setModalOpen(false);
      fetchData();
    } catch (err: any) {
      if (err?.errorFields) return;
      message.error('建立合約失敗');
    } finally {
      setSubmitting(false);
    }
  };

  // 刪除合約
  const handleDelete = async (c: Contract) => {
    try {
      await contractApi.delete(pid, c.id);
      message.success('合約已刪除');
      fetchData();
    } catch {
      message.error('刪除失敗，僅草稿可刪除');
    }
  };

  // 結案
  const handleClose = async (c: Contract) => {
    try {
      await contractApi.close(pid, c.id);
      message.success('合約已結案');
      fetchData();
    } catch {
      message.error('結案失敗');
    }
  };

  const columns: ColumnsType<Contract> = [
    {
      title: '合約編號',
      dataIndex: 'contract_no',
      key: 'contract_no',
      width: 140,
    },
    {
      title: '合約名稱',
      dataIndex: 'c_name',
      key: 'c_name',
      ellipsis: true,
    },
    {
      title: '承包商',
      dataIndex: 'contractor',
      key: 'contractor',
      width: 150,
      render: (v: string | null) => v || '-',
    },
    {
      title: '合約金額',
      dataIndex: 'contract_amount',
      key: 'contract_amount',
      width: 130,
      align: 'right',
      render: (val: number) => (
        <Text>${(val || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>
      ),
    },
    {
      title: '累計計價',
      dataIndex: 'total_issue_amount',
      key: 'total_issue_amount',
      width: 130,
      align: 'right',
      render: (val: number) => (
        <Text>${(val || 0).toLocaleString(undefined, { minimumFractionDigits: 2 })}</Text>
      ),
    },
    {
      title: '狀態',
      dataIndex: 'status',
      key: 'status',
      width: 100,
      align: 'center',
      render: (s: string) => {
        const cfg = statusConfig[s] || { color: 'default', label: s };
        return <Tag color={cfg.color}>{cfg.label}</Tag>;
      },
    },
    {
      title: '建立日期',
      dataIndex: 'created_at',
      key: 'created_at',
      width: 110,
      render: (v: string) => dayjs(v).format('MM/DD'),
    },
    {
      title: '操作',
      key: 'action',
      width: 200,
      align: 'center',
      render: (_: any, record: Contract) => (
        <Space size="small">
          <Tooltip title="檢視/編輯合約">
            <Button
              type="link" size="small" icon={<EyeOutlined />}
              onClick={() => navigate(`/projects/${pid}/contracts/${record.id}`)}
            >
              檢視
            </Button>
          </Tooltip>
          <Tooltip title="期別計價">
            <Button
              type="link" size="small"
              onClick={() => navigate(`/projects/${pid}/contracts/${record.id}/issues`)}
            >
              計價
            </Button>
          </Tooltip>
          <Tooltip title="結算管理">
            <Button
              type="link" size="small"
              onClick={() => navigate(`/projects/${pid}/contracts/${record.id}/settlements`)}
            >
              結算
            </Button>
          </Tooltip>
          <Tooltip title="終驗管理">
            <Button
              type="link" size="small"
              onClick={() => navigate(`/projects/${pid}/contracts/${record.id}/acceptances`)}
            >
              終驗
            </Button>
          </Tooltip>
          {record.status === 'draft' && (
            <>
              <Popconfirm title="確定刪除？" onConfirm={() => handleDelete(record)}>
                <Button type="link" size="small" danger icon={<DeleteOutlined />} />
              </Popconfirm>
            </>
          )}
          {(record.status === 'active' || record.status === 'draft') && (
            <Popconfirm title="確定結案？" onConfirm={() => handleClose(record)}>
              <Button type="link" size="small">結案</Button>
            </Popconfirm>
          )}
        </Space>
      ),
    },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <div>
          <Title level={4} style={{ margin: 0 }}>
            <LinkOutlined style={{ marginRight: 8 }} />
            分包合約管理
          </Title>
          {project && (
            <Text type="secondary">{project.code} — {project.name}</Text>
          )}
        </div>
        <Space>
          <Button icon={<ReloadOutlined />} onClick={fetchData}>重新整理</Button>
          <Button type="primary" icon={<PlusOutlined />} onClick={handleCreate}>新增合約</Button>
        </Space>
      </div>

      <Card>
        <Table
          dataSource={contracts}
          columns={columns}
          rowKey="id"
          loading={loading}
          pagination={{ pageSize: 20, showSizeChanger: true, showTotal: (t) => `共 ${t} 筆` }}
          locale={{ emptyText: '尚無分包合約，點擊「新增合約」開始建立' }}
          onRow={(record) => ({
            style: { cursor: 'pointer' },
            onClick: () => navigate(`/projects/${pid}/contracts/${record.id}`),
          })}
        />
      </Card>

      {/* 新增合約 Modal */}
      <Modal
        title="新增分包合約"
        open={modalOpen}
        onOk={handleSubmitCreate}
        onCancel={() => setModalOpen(false)}
        confirmLoading={submitting}
        okText="建立"
        cancelText="取消"
      >
        <Form form={form} layout="vertical" style={{ marginTop: 16 }}>
          <Form.Item name="c_name" label="合約名稱" rules={[{ required: true, message: '請輸入合約名稱' }]}>
            <Input placeholder="例如：結構體工程分包合約" />
          </Form.Item>
          <Form.Item name="contract_no" label="合約編號">
            <Input placeholder="自動產生" />
          </Form.Item>
          <Form.Item name="contractor" label="承包商">
            <Input placeholder="承包商名稱" />
          </Form.Item>
          <Form.Item name="contract_amount" label="合約金額">
            <InputNumber style={{ width: '100%' }} min={0} precision={2} placeholder="0" />
          </Form.Item>
          <Form.Item name="start_date" label="開工日期">
            <Input placeholder="YYYY-MM-DD" />
          </Form.Item>
          <Form.Item name="end_date" label="完工日期">
            <Input placeholder="YYYY-MM-DD" />
          </Form.Item>
          <Form.Item name="remark" label="備註">
            <Input.TextArea rows={3} />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default ContractListPage;
