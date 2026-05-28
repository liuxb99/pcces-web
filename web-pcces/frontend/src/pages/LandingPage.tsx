/* 首頁（免登入說明頁面） */

import React from 'react';
import { Button, Typography, Space, Card, Row, Col, Tag, message } from 'antd';
import {
  FileTextOutlined, SafetyOutlined, BarChartOutlined,
  TeamOutlined, CloudOutlined, ThunderboltOutlined,
  ArrowRightOutlined,
} from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { useAppStore } from '../store';

const { Title, Text, Paragraph } = Typography;

const LandingPage: React.FC = () => {
  const navigate = useNavigate();
  const isLoggedIn = useAppStore((s) => !!s.token);

  return (
    <div style={{ minHeight: '100vh', background: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)' }}>
      {/* Header */}
      <div style={{
        display: 'flex', justifyContent: 'space-between', alignItems: 'center',
        padding: '16px 48px', background: 'rgba(255,255,255,0.1)',
      }}>
        <Title level={3} style={{ color: '#fff', margin: 0 }}>
          <FileTextOutlined style={{ marginRight: 8 }} />
          PCCES 網頁版
        </Title>
        <Space>
          {isLoggedIn ? (
            <Button type="primary" ghost onClick={() => navigate('/app/dashboard')}
              icon={<ArrowRightOutlined />}>
              進入系統
            </Button>
          ) : (
            <>
              <Button ghost onClick={() => navigate('/login')}>登入</Button>
              <Button type="primary" ghost onClick={() => navigate('/login?tab=register')}>
                註冊
              </Button>
            </>
          )}
        </Space>
      </div>

      {/* Hero */}
      <div style={{ textAlign: 'center', padding: '80px 24px 60px', color: '#fff' }}>
        <Title style={{ color: '#fff', fontSize: 42, marginBottom: 16 }}>
          公共工程經費估算系統
        </Title>
        <Paragraph style={{ color: 'rgba(255,255,255,0.85)', fontSize: 18, maxWidth: 700, margin: '0 auto 32px' }}>
          從 C# WinForms 重生為現代化網頁應用 — 更快、更直覺、隨時隨地可用
        </Paragraph>

        {/* 示範資料提示卡片 */}
        <div style={{ maxWidth: 520, margin: '0 auto 36px', background: 'rgba(255,255,255,0.15)', borderRadius: 12, padding: '16px 24px' }}>
          <Text style={{ color: '#fff', fontSize: 14 }}>
            💡 系統已內建「OO 大樓新建工程」示範專案，內含完整的預算樹（直接工程費、間接工程費、利潤及營業稅）與 9 筆工/料/機資源資料。
            可直接以訪客身分瀏覽，或使用示範帳號 <Tag color="gold" style={{ cursor: 'pointer' }} onClick={() => navigate('/login')}>demo / demo123</Tag> 登入操作。
          </Text>
        </div>

        <Space size={16}>
          <Button type="primary" size="large" style={{ height: 48, padding: '0 32px', fontSize: 16 }}
            onClick={() => navigate('/app/dashboard')}>
            立即開始使用
            <ArrowRightOutlined />
          </Button>
          <Button size="large" ghost style={{ height: 48, padding: '0 32px', fontSize: 16, color: '#fff', borderColor: '#fff' }}
            onClick={() => navigate('/login')}>
            登入（選用）
          </Button>
        </Space>
      </div>

      {/* 功能特色 */}
      <div style={{ padding: '40px 48px 60px', background: '#fff' }}>
        <Title level={2} style={{ textAlign: 'center', marginBottom: 48 }}>為什麼選擇 PCCES 網頁版？</Title>
        <Row gutter={[24, 24]}>
          <Col xs={24} sm={12} md={8}>
            <Card hoverable>
              <ThunderboltOutlined style={{ fontSize: 36, color: '#1677ff' }} />
              <Title level={4} style={{ marginTop: 16 }}>即時自動計算</Title>
              <Text type="secondary">輸入數量與單價，金額立即更新。不再需要手動點擊重算。</Text>
            </Card>
          </Col>
          <Col xs={24} sm={12} md={8}>
            <Card hoverable>
              <BarChartOutlined style={{ fontSize: 36, color: '#52c41a' }} />
              <Title level={4} style={{ marginTop: 16 }}>互動圖表分析</Title>
              <Text type="secondary">成本分布圓餅圖、前十大項目長條圖，一目瞭然。</Text>
            </Card>
          </Col>
          <Col xs={24} sm={12} md={8}>
            <Card hoverable>
              <SafetyOutlined style={{ fontSize: 36, color: '#faad14' }} />
              <Title level={4} style={{ marginTop: 16 }}>角色權限控管</Title>
              <Text type="secondary">管理員、審核者、編製者 — 權限分明，資料隔離。</Text>
            </Card>
          </Col>
          <Col xs={24} sm={12} md={8}>
            <Card hoverable>
              <TeamOutlined style={{ fontSize: 36, color: '#722ed1' }} />
              <Title level={4} style={{ marginTop: 16 }}>多人協作</Title>
              <Text type="secondary">多使用者同時登入，各自管理自己的專案預算。</Text>
            </Card>
          </Col>
          <Col xs={24} sm={12} md={8}>
            <Card hoverable>
              <CloudOutlined style={{ fontSize: 36, color: '#13c2c2' }} />
              <Title level={4} style={{ marginTop: 16 }}>雲端隨處存取</Title>
              <Text type="secondary">瀏覽器即可使用，不需安裝任何軟體。</Text>
            </Card>
          </Col>
          <Col xs={24} sm={12} md={8}>
            <Card hoverable>
              <FileTextOutlined style={{ fontSize: 36, color: '#eb2f96' }} />
              <Title level={4} style={{ marginTop: 16 }}>Excel 報表匯出</Title>
              <Text type="secondary">一鍵匯出符合公共工程格式的預算總表。</Text>
            </Card>
          </Col>
        </Row>

        {/* 功能列表 */}
        <div style={{ maxWidth: 800, margin: '48px auto 0' }}>
          <Title level={3} style={{ textAlign: 'center', marginBottom: 24 }}>功能一覽</Title>
          <Row gutter={[16, 12]}>
            {[
              ['📋 預算編輯器', '樹狀結構 + 表格編輯，支援 B/L/W/Z 等預算項目類型'],
              ['🌳 樹狀預算', '多層級 WBS 結構，展開/收合，直覺管理'],
              ['🔍 即時搜尋', '快速過濾預算項目'],
              ['📊 互動儀表板', '統計卡片 + 圖表分析 + 最近專案'],
              ['📦 資源管理', '工、料、機分類管理與單價設定'],
              ['📈 Excel 匯出', '含 Authorization 驗證的安全下載'],
              ['🔐 安全認證', 'JWT + PBKDF2-SHA256 加鹽密碼'],
              ['🔒 權限控管', '全部 18 個 API 端點有所有權檢查'],
              ['🧪 測試覆蓋', '38 個自動化測試，持續驗證品質'],
            ].map(([title, desc]) => (
              <Col xs={24} md={12} key={title}>
                <Card size="small">
                  <Text strong>{title}</Text>
                  <br />
                  <Text type="secondary">{desc}</Text>
                </Card>
              </Col>
            ))}
          </Row>
        </div>
      </div>

      {/* Footer */}
      <div style={{ textAlign: 'center', padding: '24px', color: 'rgba(255,255,255,0.6)', background: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)' }}>
        <Text style={{ color: 'rgba(255,255,255,0.6)' }}>
          PCCES 網頁版 · 基於原始 PCCES 4.6 重建 · React + Flask + SQLite
        </Text>
      </div>
    </div>
  );
};

export default LandingPage;
