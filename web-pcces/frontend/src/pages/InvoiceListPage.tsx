/* 計價列表頁面 */

import React, { useEffect, useState, useCallback } from 'react';
import {
  Card, Table, Button, Space, Modal, Form, Input, DatePicker,
  message, Typography, Tag, Popconfirm, Tooltip, Badge,
} from 'antd';
import {
  PlusOutlined, EyeOutlined, DeleteOutlined, DollarOutlined,
  ReloadOutlined, FileTextOutlined,
} from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';
import { invoiceApi, projectApi } from '../api';
import type { Invoice, CreateInvoiceRequest } from '../types';
import type { ColumnsType } from 'antd/es/table';

const { Title, Text } = Typography;

/** 狀態標籤顏色對應 */
const statusConfig: Record<string, { color: string; label: string }> = {
  draft: { color: 'default', label: '草稿' },
  submitted: { color: 'processing', label: '已提交' },
  approved: { color: 'success', label: '已核准' },
};

const InvoiceListPage: React.FC = () => {
  const { id: projectId } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const pid = parseInt(projectId || '0');

  const [invoices, setInvoices] = useState<Invoice[]>([]);
  const [project, setProject] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [form] = Form.useForm();

  // 載入資料
  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const [proj, data] = await Promise.all([
        projectApi.get(pid),
        invoiceApi.list(pid),
      ]);
      setProject(proj);
      setInvoices(data);
    } catch (err) {
      message.error('載入計價資料失敗');
    } finally {
      setLoading(false);
    }
  }, [pid]);

  useEffect(() => { fetchData(); }, [fetchData]);

  // 新增計價單
  const handleCreate = () => {
    form.resetFields();
    // 預設計價日期為今天
    form.setFieldsValue({ invoice_date: dayjs() });
    setModalOpen(true);
  };

  const handleSubmitCreate = async () => {
    try {
      const values = await form.validateFields();
      setSubmitting(true);
      const data: CreateInvoiceRequest = {
        ...values,
        invoice_date: values.invoice_date?.format('YYYY-MM-DD'),
      };
      await invoiceApi.create(pid, data);
      message.success('計價單已建立');
      setModalOpen(false);
      fetchData();
    } catch (err: any) {
      if (err?.errorFields) return; // 表單驗證錯誤不提示
      message.error('建立計價單失敗');
    } finally {
      setSubmitting(false);
    }
  };

  // 刪除計價單
  const handleDelete = async (inv: Invoice) => {
    try {
      await invoiceApi.delete(pid, inv.id);
      message.success('計價單已刪除');
      fetchData();
    } catch {
      message.error('刪除失敗，僅草稿狀態可刪除');
    }
  };

  // 表格欄位定義
  const columns: ColumnsType<Invoice> = [
    {
      title: '期別',
      dataIndex: 'period_no',
      key: 'period_no',
      width: 80,
      align: 'center',
      render: (val: number) => <Text strong>第 {val} 期</Text>,
    },
    {
      title: '計價單號',
      dataIndex: 'invoice_no',
      key: 'invoice_no',
      width: 160,
    },
    {
      title: '名稱',
      dataIndex: 'c_name',
      key: 'c_name',
      ellipsis: true,
    },
    {
      title: '狀態',
      dataIndex: 'status',
      key: 'status',
      width: 100,
      align: 'center',
      render: (status: string) => {
        const cfg = statusConfig[status] || { color: 'default', label: status };
        return <Badge status={cfg.color as any} text={cfg.label} />;
      },
    },
    {
      title: '本期金額',
      dataIndex: 'total_amount',
      key: 'total_amount',
      width: 130,
      align: 'right',
      render: (val: number) => (
        <Text>{val.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</Text>
      ),
    },
    {
      title: '累計金額',
      dataIndex: 'cumulative_amount',
      key: 'cumulative_amount',
      width: 130,
      align: 'right',
      render: (val: number) => (
        <Text>{val.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</Text>
      ),
    },
    {
      title: '進度',
      dataIndex: 'progress_rate',
      key: 'progress_rate',
      width: 100,
      align: 'center',
      render: (val: number) => `${val.toFixed(1)}%`,
    },
    {
      title: '明細數',
      dataIndex: 'item_count',
      key: 'item_count',
      width: 70,
      align: 'center',
    },
    {
      title: '建立日期',
      dataIndex: 'created_at',
      key: 'created_at',
      width: 110,
      render: (val: string) => dayjs(val).format('MM/DD'),
    },
    {
      title: '操作',
      key: 'action',
      width: 140,
      align: 'center',
      render: (_: any, record: Invoice) => (
        <Space size="small">
          <Tooltip title="檢視 / 編輯">
            <Button
              type="link"
              size="small"
              icon={<EyeOutlined />}
              onClick={() => navigate(`/projects/${pid}/invoices/${record.id}`)}
            >
              檢視
            </Button>
          </Tooltip>
          {record.status === 'draft' && (
            <Popconfirm
              title="確定刪除此計價單？"
              onConfirm={() => handleDelete(record)}
            >
              <Button type="link" size="small" danger icon={<DeleteOutlined />}>
                刪除
              </Button>
            </Popconfirm>
          )}
        </Space>
      ),
    },
  ];

  return (
    <div>
      {/* 頁首 */}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <div>
          <Title level={4} style={{ margin: 0 }}>
            <DollarOutlined style={{ marginRight: 8 }} />
            計價管理
          </Title>
          {project && (
            <Text type="secondary">
              {project.code} — {project.name}
            </Text>
          )}
        </div>
        <Space>
          <Button icon={<ReloadOutlined />} onClick={fetchData}>
            重新整理
          </Button>
          <Button type="primary" icon={<PlusOutlined />} onClick={handleCreate}>
            新增計價
          </Button>
        </Space>
      </div>

      {/* 表格 */}
      <Card>
        <Table
          dataSource={invoices}
          columns={columns}
          rowKey="id"
          loading={loading}
          pagination={{ pageSize: 20, showSizeChanger: true, showTotal: (t) => `共 ${t} 筆` }}
          locale={{ emptyText: '尚無計價單，點擊「新增計價」開始建立' }}
          onRow={(record) => ({
            style: { cursor: 'pointer' },
            onClick: () => navigate(`/projects/${pid}/invoices/${record.id}`),
          })}
        />
      </Card>

      {/* 新增計價單 Modal */}
      <Modal
        title="新增計價單"
        open={modalOpen}
        onOk={handleSubmitCreate}
        onCancel={() => setModalOpen(false)}
        confirmLoading={submitting}
        okText="建立"
        cancelText="取消"
      >
        <Form form={form} layout="vertical" style={{ marginTop: 16 }}>
          <Form.Item
            name="c_name"
            label="計價名稱"
            rules={[{ required: true, message: '請輸入計價名稱' }]}
          >
            <Input placeholder="例如：第1期計價" />
          </Form.Item>
          <Form.Item
            name="invoice_no"
            label="計價單號"
          >
            <Input placeholder="自動產生的單號" />
          </Form.Item>
          <Form.Item
            name="description"
            label="說明"
          >
            <Input.TextArea rows={3} placeholder="計價說明（選填）" />
          </Form.Item>
          <Form.Item
            name="invoice_date"
            label="計價日期"
          >
            <DatePicker style={{ width: '100%' }} />
          </Form.Item>
          <Form.Item
            name="remark"
            label="備註"
          >
            <Input.TextArea rows={2} placeholder="備註（選填）" />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default InvoiceListPage;
