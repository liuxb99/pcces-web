/* 版本資訊頁面 — 顯示系統版本、更新日誌、技術棧與健康狀態 */

import React, { useEffect, useState, useRef } from 'react';
import {
  Card, Typography, Descriptions, Tag, Timeline, Button, Space,
  Row, Col, Statistic, Spin, Alert,
} from 'antd';
import {
  InfoCircleOutlined, GithubOutlined, LinkOutlined,
  CheckCircleOutlined, CloseCircleOutlined, ClockCircleOutlined,
  ReloadOutlined,
} from '@ant-design/icons';
import { systemApi } from '../api';
import type { VersionInfo, HealthStatus } from '../types';

const { Title, Text, Paragraph } = Typography;

/** 格式化 uptime */
const formatUptime = (seconds: number): string => {
  const h = Math.floor(seconds / 3600);
  const m = Math.floor((seconds % 3600) / 60);
  const s = seconds % 60;
  const parts: string[] = [];
  if (h > 0) parts.push(`${h} 小時`);
  if (m > 0) parts.push(`${m} 分鐘`);
  parts.push(`${s} 秒`);
  return parts.join(' ');
};

const VersionInfoPage: React.FC = () => {
  const [version, setVersion] = useState<VersionInfo | null>(null);
  const [health, setHealth] = useState<HealthStatus | null>(null);
  const [loading, setLoading] = useState(true);
  const [uptimeSeconds, setUptimeSeconds] = useState(0);
  const intervalRef = useRef<number | null>(null);

  /** 載入版本資訊 */
  const loadVersion = async () => {
    try {
      const data = await systemApi.getVersion();
      setVersion(data);
    } catch {
      // 忽略錯誤
    }
  };

  /** 載入健康狀態 */
  const loadHealth = async () => {
    try {
      const data = await systemApi.getHealth();
      setHealth(data);
      setUptimeSeconds(data.uptime_seconds);
    } catch {
      setHealth({ status: 'down', database: 'disconnected', uptime_seconds: 0, timestamp: '' });
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    setLoading(true);
    loadVersion();
    loadHealth();

    // 每秒更新 uptime
    intervalRef.current = window.setInterval(() => {
      setUptimeSeconds((prev) => prev + 1);
    }, 1000);

    // 每 60 秒重新檢查健康狀態
    const healthInterval = window.setInterval(loadHealth, 60000);

    return () => {
      if (intervalRef.current) clearInterval(intervalRef.current);
      clearInterval(healthInterval);
    };
  }, []);

  const healthColor = health?.status === 'healthy' ? 'green' : health?.status === 'degraded' ? 'orange' : 'red';
  const healthIcon = health?.status === 'healthy' ? <CheckCircleOutlined /> : <CloseCircleOutlined />;
  const dbColor = health?.database === 'connected' ? 'green' : 'red';

  return (
    <div style={{ maxWidth: 900, margin: '0 auto' }}>
      <Title level={3}>
        <InfoCircleOutlined style={{ marginRight: 8 }} />
        版本資訊
      </Title>

      {loading ? (
        <div style={{ textAlign: 'center', padding: 60 }}>
          <Spin size="large" />
        </div>
      ) : (
        <>
          {/* 版本資訊主卡片 */}
          <Card style={{ marginBottom: 24, textAlign: 'center' }}>
            <Title level={2} style={{ marginBottom: 8 }}>
              {version?.app_name || 'PCCES 公共工程經費估算系統'}
            </Title>
            <Space size="large" style={{ marginBottom: 16 }}>
              <Text type="secondary">
                版本：<Tag color="blue" style={{ fontSize: 14 }}>{version?.app_version || '-'}</Tag>
              </Text>
              <Text type="secondary">
                建置日期：{version?.build_date || '-'}
              </Text>
            </Space>
            <br />
            <Tag
              icon={healthIcon}
              color={healthColor}
              style={{ fontSize: 14, padding: '4px 16px', borderRadius: 16 }}
            >
              系統狀態：
              {health?.status === 'healthy' ? '正常運作'
                : health?.status === 'degraded' ? '部分異常'
                : '無法連接'}
            </Tag>
          </Card>

          {/* 系統狀態 */}
          <Row gutter={16} style={{ marginBottom: 24 }}>
            <Col span={8}>
              <Card size="small">
                <Statistic
                  title="資料庫"
                  value={health?.database === 'connected' ? '已連線' : '未連線'}
                  valueStyle={{ color: dbColor }}
                  prefix={health?.database === 'connected' ? <CheckCircleOutlined /> : <CloseCircleOutlined />}
                />
              </Card>
            </Col>
            <Col span={8}>
              <Card size="small">
                <Statistic
                  title="上線時間"
                  value={formatUptime(uptimeSeconds)}
                  valueStyle={{ fontSize: 16 }}
                  prefix={<ClockCircleOutlined />}
                />
              </Card>
            </Col>
            <Col span={8}>
              <Card size="small">
                <Statistic
                  title="最後更新"
                  value={health?.timestamp ? new Date(health.timestamp).toLocaleString('zh-TW') : '-'}
                  valueStyle={{ fontSize: 14 }}
                />
              </Card>
            </Col>
          </Row>

          {/* 更新日誌 */}
          {version?.changelog && version.changelog.length > 0 && (
            <Card title="更新日誌" style={{ marginBottom: 24 }}>
              <Timeline
                items={version.changelog.map((entry) => ({
                  color: entry.version === version?.app_version ? 'blue' : 'gray',
                  children: (
                    <>
                      <Text strong>
                        v{entry.version}
                        <Text type="secondary" style={{ marginLeft: 8 }}>({entry.date})</Text>
                      </Text>
                      <ul style={{ margin: '4px 0 0 0', paddingLeft: 20 }}>
                        {entry.changes.map((change, idx) => (
                          <li key={idx}>
                            <Text>{change}</Text>
                          </li>
                        ))}
                      </ul>
                    </>
                  ),
                }))}
              />
            </Card>
          )}

          {/* 技術棧 */}
          {version?.dependencies && (
            <Card title="技術棧" style={{ marginBottom: 24 }}>
              <Row gutter={24}>
                {Object.entries(version.dependencies).map(([layer, deps]) => (
                  <Col span={12} key={layer}>
                    <Descriptions
                      title={layer === 'backend' ? '後端' : '前端'}
                      column={1}
                      size="small"
                      bordered
                    >
                      {Object.entries(deps).map(([name, ver]) => (
                        <Descriptions.Item key={name} label={name}>
                          <Tag>{ver}</Tag>
                        </Descriptions.Item>
                      ))}
                    </Descriptions>
                  </Col>
                ))}
              </Row>
            </Card>
          )}

          {/* 外部連結 */}
          <Card size="small">
            <Space size="large">
              {version?.release_notes_url && (
                <Button
                  type="link"
                  icon={<GithubOutlined />}
                  href={version.release_notes_url}
                  target="_blank"
                  rel="noopener noreferrer"
                >
                  查看 GitHub Release
                </Button>
              )}
              {version?.repo_url && (
                <Button
                  type="link"
                  icon={<LinkOutlined />}
                  href={version.repo_url}
                  target="_blank"
                  rel="noopener noreferrer"
                >
                  查看原始碼
                </Button>
              )}
              <Button
                type="link"
                icon={<ReloadOutlined />}
                onClick={() => { setLoading(true); loadVersion(); loadHealth(); }}
              >
                重新整理
              </Button>
            </Space>
          </Card>
        </>
      )}
    </div>
  );
};

export default VersionInfoPage;
