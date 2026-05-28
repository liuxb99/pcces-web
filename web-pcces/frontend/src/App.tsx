/* 應用程式主元件 — 路由（免登入） */

import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import AppLayout from './components/AppLayout';
import LoginPage from './pages/LoginPage';
import DashboardPage from './pages/DashboardPage';
import ProjectsPage from './pages/ProjectsPage';
import BudgetEditorPage from './pages/BudgetEditorPage';
import ResourcesPage from './pages/ResourcesPage';
import ReportsPage from './pages/ReportsPage';
import LandingPage from './pages/LandingPage';

const App: React.FC = () => {
  return (
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
      </Route>
    </Routes>
  );
};

export default App;
