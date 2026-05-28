/* 使用者新增/編輯 Modal */

import React, { useEffect } from 'react';
import { Modal, Form, Input, Select, Switch, message } from 'antd';
import type { User } from '../../types';
import { adminApi } from '../../api';

interface Props {
  open: boolean;
  user: User | null;
  onClose: () => void;
  onSuccess: () => void;
}

const UserEditForm: React.FC<Props> = ({ open, user, onClose, onSuccess }) => {
  const [form] = Form.useForm();
  const isEdit = !!user;

  useEffect(() => {
    if (open) {
      if (user) {
        form.setFieldsValue({
          username: user.username,
          display_name: user.display_name,
          email: user.email || '',
          company: user.company || '',
          department: user.department || '',
          phone: user.phone || '',
          role: user.role,
          is_active: user.is_active,
        });
      } else {
        form.resetFields();
        form.setFieldsValue({ role: 'editor', is_active: true });
      }
    }
  }, [open, user, form]);

  const handleOk = async () => {
    try {
      const values = await form.validateFields();
      if (isEdit) {
        // 編輯：僅傳送有修改的欄位
        await adminApi.updateUser(user!.id, values);
        message.success('使用者已更新');
      } else {
        await adminApi.createUser(values);
        message.success('使用者已建立');
      }
      onSuccess();
    } catch (err: any) {
      if (err?.response?.data?.detail) {
        message.error(err.response.data.detail);
      } else if (!err?.errorFields) {
        message.error('操作失敗');
      }
    }
  };

  return (
    <Modal
      title={isEdit ? `編輯使用者：${user?.username}` : '新增使用者'}
      open={open}
      onOk={handleOk}
      onCancel={onClose}
      width={520}
      destroyOnClose
    >
      <Form form={form} layout="vertical">
        <Form.Item name="username" label="帳號" rules={[{ required: true, message: '請輸入帳號' }]}>
          <Input disabled={isEdit} placeholder="帳號" />
        </Form.Item>
        <Form.Item
          name="password"
          label={isEdit ? '密碼（留空則不修改）' : '密碼'}
          rules={isEdit ? [] : [{ required: true, min: 6, message: '密碼至少 6 碼' }]}
        >
          <Input.Password placeholder={isEdit ? '留空則不修改' : '密碼'} />
        </Form.Item>
        <Form.Item name="display_name" label="顯示名稱" rules={[{ required: true, message: '請輸入名稱' }]}>
          <Input placeholder="顯示名稱" />
        </Form.Item>
        <Form.Item name="email" label="Email">
          <Input placeholder="email@example.com" />
        </Form.Item>
        <Form.Item name="company" label="公司/機關">
          <Input placeholder="公司/機關" />
        </Form.Item>
        <Form.Item name="department" label="部門">
          <Input placeholder="部門" />
        </Form.Item>
        <Form.Item name="phone" label="電話">
          <Input placeholder="電話" />
        </Form.Item>
        <Form.Item name="role" label="角色">
          <Select
            options={[
              { value: 'admin', label: '管理員' },
              { value: 'reviewer', label: '審核者' },
              { value: 'editor', label: '編輯者' },
              { value: 'viewer', label: '唯讀' },
            ]}
          />
        </Form.Item>
        <Form.Item name="is_active" label="啟用" valuePropName="checked">
          <Switch />
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default UserEditForm;
