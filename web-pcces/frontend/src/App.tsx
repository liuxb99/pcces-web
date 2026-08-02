/* 應用程式主元件 — 路由（免登入） */

import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import AppLayout from './components/AppLayout';
import LoginPage from './pages/LoginPage';
import DashboardPage from './pages/DashboardPage';
import ProjectsPage from './pages/ProjectsPage';
import BudgetEditorPage from './pages/BudgetEditorPage';
import BudgetVersionsPage from './pages/BudgetVersionsPage';
import BudgetApprovalPage from './pages/BudgetApprovalPage';
import BudgetValidationPage from './pages/BudgetValidationPage';
import BidLifecyclePage from './pages/BidLifecyclePage';
import ResourcesPage from './pages/ResourcesPage';
import ReportsPage from './pages/ReportsPage';
import InvoiceListPage from './pages/InvoiceListPage';
import InvoiceDetailPage from './pages/InvoiceDetailPage';
import LandingPage from './pages/LandingPage';
import ContractListPage from './pages/ContractListPage';
import MrsBasePage from './pages/MrsBasePage';
import ContractDetailPage from './pages/ContractDetailPage';
import IssueListPage from './pages/IssueListPage';
import IssueDetailPage from './pages/IssueDetailPage';
import SettlementListPage from './pages/SettlementListPage';
import SettlementDetailPage from './pages/SettlementDetailPage';
import AcceptanceListPage from './pages/AcceptanceListPage';
import AcceptanceDetailPage from './pages/AcceptanceDetailPage';
import AdminPage from './pages/AdminPage';
import ComparePage from './pages/ComparePage';
import MrsBasePriceComparePage from './pages/MrsBasePriceComparePage';
import VersionInfoPage from './pages/VersionInfoPage';
import TraceabilityPage from './pages/TraceabilityPage';

const App: React.FC = () => (
  <Routes>
    <Route path="/" element={<LandingPage />} />
    <Route path="/login" element={<LoginPage />} />
    <Route path="/app" element={<AppLayout />}>
      <Route index element={<Navigate to="/app/dashboard" replace />} />
      <Route path="dashboard" element={<DashboardPage />} />
      <Route path="projects" element={<ProjectsPage />} />
      <Route path="projects/:id/budget" element={<BudgetEditorPage />} />
      <Route path="projects/:id/resources" element={<ResourcesPage />} />
      <Route path="projects/:id/reports" element={<ReportsPage />} />
      <Route path="projects/by-code/:projectCode/traceability" element={<TraceabilityPage />} />
      <Route path="projects/by-code/:projectCode/budget-versions" element={<BudgetVersionsPage />} />
      <Route path="projects/by-code/:projectCode/budget-approval" element={<BudgetApprovalPage />} />
      <Route path="projects/by-code/:projectCode/budget-validation" element={<BudgetValidationPage />} />
      <Route path="projects/by-code/:projectCode/bid-lifecycle" element={<BidLifecyclePage />} />
      <Route path="projects/:id/invoices" element={<InvoiceListPage />} />
      <Route path="projects/:id/invoices/:invoiceId" element={<InvoiceDetailPage />} />
      <Route path="projects/:id/contracts" element={<ContractListPage />} />
      <Route path="projects/:id/contracts/:contractId" element={<ContractDetailPage />} />
      <Route path="projects/:id/contracts/:contractId/issues" element={<IssueListPage />} />
      <Route path="projects/:id/contracts/:contractId/issues/:issueId" element={<IssueDetailPage />} />
      <Route path="projects/:id/contracts/:contractId/settlements" element={<SettlementListPage />} />
      <Route path="projects/:id/contracts/:contractId/settlements/:settlementId" element={<SettlementDetailPage />} />
      <Route path="projects/:id/contracts/:contractId/acceptances" element={<AcceptanceListPage />} />
      <Route path="projects/:id/contracts/:contractId/acceptances/:acceptanceId" element={<AcceptanceDetailPage />} />
      <Route path="mrs-base" element={<MrsBasePage />} />
      <Route path="compare/budget-items" element={<ComparePage />} />
      <Route path="compare/mrs-prices" element={<MrsBasePriceComparePage />} />
      <Route path="admin" element={<AdminPage />} />
      <Route path="version" element={<VersionInfoPage />} />
    </Route>
  </Routes>
);

export default App;
