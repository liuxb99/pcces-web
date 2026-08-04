/* 專案管理頁面 */

import React, { useEffect, useState } from 'react';
import {
  Table, Button, Modal, Form, Input, InputNumber, Space, Card,
  message, Popconfirm, Tag, Typography, Tooltip,
} from 'antd';
import { PlusOutlined, EditOutlined, DeleteOutlined, FolderOpenOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { projectApi } from '../api';
import type { Project, ProjectCreateData } from '../types';

const { Title } = Typography;

const ProjectsPage: React.FC = () => {
  const navigate = useNavigate();
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingProject, setEditingProject] = useState<Project | null>(null);
  const [form] = Form.useForm();

  const fetchProjects = async () => {
    setLoading(true);
    try {
      const data = await projectApi.list();
      setProjects(data);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchProjects(); }, []);

  const handleCreate = () => {
    setEditingProject(null);
    form.resetFields();
    setModalOpen(true);
  };

  const handleEdit = (project: Project) => {
    setEditingProject(project);
    form.setFieldsValue(project);
    setModalOpen(true);
  };

  const handleDelete = async (id: number) => {
    try {
      await projectApi.delete(id);
      message.success('專案已刪除');
      fetchProjects();
    } catch {
      message.error('刪除失敗');
    }
  };

  const handleSubmit = async () => {
    try {
      const values = await form.validateFields();
      if (editingProject) {
        await projectApi.update(editingProject.id, values);
        message.success('專案已更新');
      } else {
        await projectApi.create(values as ProjectCreateData);
        message.success('專案已建立');
      }
      setModalOpen(false);
      fetchProjects();
    } catch {
      message.error('操作失敗');
    }
  };

  const columns = [
    { title: '專案編號', dataIndex: 'code', key: 'code', width: 120,
      render: (v: string) => <Tag color="blue">{v}</Tag>,
    },
    { title: '專案名稱', dataIndex: 'name', key: 'name', ellipsis: true },
    { title: '地點', dataIndex: 'location', key: 'location', width: 150, ellipsis: true },
    { title: '預算總額', dataIndex: 'budget_total', key: 'budget_total', width: 140,
      render: (v: number) => v ? `$${(v).toLocaleString()}` : '-',
    },
    { title: '項目數', dataIndex: 'item_count', key: 'item_count', width: 80 },
    { title: '狀態', dataIndex: 'status', key: 'status', width: 80,
      render: (s: string) => (
        <Tag color={s === 'active' ? 'green' : 'default'}>
          {s === 'active' ? '啟用' : '封存'}
        </Tag>
      ),
    },
    {
      title: '操作', key: 'action', width: 200,
      render: (_: unknown, record: Project) => (
        <Space>
          <Tooltip title="編輯預算">
            <Button type="primary" size="small" icon={<FolderOpenOutlined />}
              onClick={() => navigate(`/app/projects/${record.id}/budget`)}>
              預算
            </Button>
          </Tooltip>
          <Tooltip title="編輯專案">
            <Button size="small" icon={<EditOutlined />} onClick={() => handleEdit(record)} />
          </Tooltip>
          <Popconfirm title="確定刪除此專案？" onConfirm={() => handleDelete(record.id)}>
            <Tooltip title="刪除專案">
              <Button size="small" danger icon={<DeleteOutlined />} />
            </Tooltip>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 24 }}>
        <Title level={4} style={{ margin: 0 }}>專案管理</Title>
        <Button type="primary" icon={<PlusOutlined />} onClick={handleCreate}>
          新增專案
        </Button>
      </div>

      <Card>
        <Table
          dataSource={projects}
          columns={columns}
          rowKey="id"
          loading={loading}
          pagination={{ pageSize: 10, showSizeChanger: true }}
        />
      </Card>

      <Modal
        title={editingProject ? '編輯專案' : '新增專案'}
        open={modalOpen}
        onOk={handleSubmit}
        onCancel={() => setModalOpen(false)}
        width={640}
      >
        <Form form={form} layout="vertical">
          <Form.Item name="code" label="專案編號" rules={[{ required: true, message: '請輸入專案編號' }]}>
            <Input disabled={!!editingProject} />
          </Form.Item>
          <Form.Item name="name" label="專案名稱" rules={[{ required: true, message: '請輸入專案名稱' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="name_en" label="英文名稱">
            <Input />
          </Form.Item>
          <Form.Item name="location" label="地點">
            <Input />
          </Form.Item>
          <Form.Item name="account_code" label="會計科目">
            <Input />
          </Form.Item>
          <Space style={{ width: '100%' }} size={16}>
            <Form.Item name="scope" label="規模">
              <InputNumber min={0} style={{ width: 150 }} />
            </Form.Item>
            <Form.Item name="scope_unit" label="規模單位">
              <Input style={{ width: 120 }} placeholder="式" />
            </Form.Item>
          </Space>
          <Form.Item name="description" label="備註說明">
            <Input.TextArea rows={3} />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default ProjectsPage;
