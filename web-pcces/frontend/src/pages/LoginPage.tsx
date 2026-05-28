/* 登入頁面 */

import React, { useState } from 'react';
import { Form, Input, Button, message, Tabs, Typography, Space } from 'antd';
import { UserOutlined, LockOutlined, MailOutlined, BugOutlined } from '@ant-design/icons';
import { useNavigate, useSearchParams, Navigate } from 'react-router-dom';
import { useAppStore } from '../store';
import { authApi } from '../api';

const { Title, Text } = Typography;

const LoginPage: React.FC = () => {
  const navigate = useNavigate();
  const setAuth = useAppStore((s) => s.setAuth);
  const [loading, setLoading] = useState(false);
  const [searchParams] = useSearchParams();
  const defaultTab = searchParams.get('tab') === 'register' ? 'register' : 'login';
  const token = useAppStore((s) => s.token);
  const [loginForm] = Form.useForm();

  // 已登入則跳轉到儀表板
  if (token) {
    return <Navigate to="/app/dashboard" replace />;
  }

  const handleLogin = async (values: { username: string; password: string }) => {
    setLoading(true);
    try {
      const res = await authApi.login(values);
      setAuth(res.user, res.access_token);
      message.success(`歡迎回來，${res.user.display_name}！`);
      navigate('/app/dashboard');
    } catch {
      message.error('帳號或密碼錯誤');
    } finally {
      setLoading(false);
    }
  };

  const handleRegister = async (values: {
    username: string; password: string; confirmPassword: string;
    display_name: string; email?: string; company?: string;
  }) => {
    if (values.password !== values.confirmPassword) {
      message.error('密碼不一致');
      return;
    }
    setLoading(true);
    try {
      const res = await authApi.register({
        username: values.username,
        password: values.password,
        display_name: values.display_name,
        email: values.email,
        company: values.company,
      });
      setAuth(res.user, res.access_token);
      message.success('註冊成功！');
      navigate('/app/dashboard');
    } catch {
      message.error('註冊失敗，帳號可能已存在');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-container">
      <div className="login-card">
        <Title level={3} style={{ textAlign: 'center', marginBottom: 4 }}>
          PCCES 網頁版
        </Title>
        <Text type="secondary" style={{ display: 'block', textAlign: 'center', marginBottom: 32 }}>
          公共工程經費估算系統
        </Text>

        <Tabs centered defaultActiveKey={defaultTab} items={[
          {
            key: 'login',
            label: '登入',
            children: (
              <Form form={loginForm} onFinish={handleLogin} layout="vertical" size="large">
                <Form.Item name="username" rules={[{ required: true, message: '請輸入帳號' }]}>
                  <Input prefix={<UserOutlined />} placeholder="帳號" />
                </Form.Item>
                <Form.Item name="password" rules={[{ required: true, message: '請輸入密碼' }]}>
                  <Input.Password prefix={<LockOutlined />} placeholder="密碼" />
                </Form.Item>
                <Form.Item>
                  <Space style={{ width: '100%' }} direction="vertical">
                    <Button type="primary" htmlType="submit" loading={loading} block>
                      登入
                    </Button>
                    <Button
                      type="dashed"
                      block
                      icon={<BugOutlined />}
                      onClick={() => loginForm.setFieldsValue({ username: 'demo', password: 'demo123' })}
                    >
                      使用示範帳號
                    </Button>
                  </Space>
                </Form.Item>
              </Form>
            ),
          },
          {
            key: 'register',
            label: '註冊',
            children: (
              <Form onFinish={handleRegister} layout="vertical" size="large">
                <Form.Item name="username" rules={[{ required: true, message: '請輸入帳號' }]}>
                  <Input prefix={<UserOutlined />} placeholder="帳號" />
                </Form.Item>
                <Form.Item name="display_name" rules={[{ required: true, message: '請輸入顯示名稱' }]}>
                  <Input prefix={<UserOutlined />} placeholder="顯示名稱" />
                </Form.Item>
                <Form.Item name="email">
                  <Input prefix={<MailOutlined />} placeholder="Email（選填）" />
                </Form.Item>
                <Form.Item name="company">
                  <Input prefix={<UserOutlined />} placeholder="公司/機關（選填）" />
                </Form.Item>
                <Form.Item name="password" rules={[{ required: true, min: 6, message: '密碼至少 6 碼' }]}>
                  <Input.Password prefix={<LockOutlined />} placeholder="密碼" />
                </Form.Item>
                <Form.Item name="confirmPassword" rules={[{ required: true, message: '請確認密碼' }]}>
                  <Input.Password prefix={<LockOutlined />} placeholder="確認密碼" />
                </Form.Item>
                <Form.Item>
                  <Button type="primary" htmlType="submit" loading={loading} block>
                    註冊
                  </Button>
                </Form.Item>
              </Form>
            ),
          },
        ]} />
      </div>
    </div>
  );
};

export default LoginPage;
