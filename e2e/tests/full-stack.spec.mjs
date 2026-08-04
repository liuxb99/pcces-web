import { test, expect } from '@playwright/test';

const frontendBase = process.env.PCCES_E2E_BASE_URL || 'http://127.0.0.1:4173';
const goBase = process.env.PCCES_GO_BASE_URL || 'http://127.0.0.1:8080';

async function registerUser(page, displayName = 'E2E 驗收使用者') {
  const suffix = `${Date.now()}-${Math.floor(Math.random() * 100000)}`;
  const username = `e2e_${suffix}`;
  await page.goto('/login?tab=register');
  await expect(page.getByText('PCCES 網頁版')).toBeVisible();
  await page.getByPlaceholder('帳號').fill(username);
  await page.getByPlaceholder('顯示名稱').fill(displayName);
  await page.getByPlaceholder('密碼', { exact: true }).fill('E2ePass123!');
  await page.getByPlaceholder('確認密碼').fill('E2ePass123!');
  await page.getByRole('button', { name: '註冊', exact: true }).click();
  await expect(page).toHaveURL(/\/app\/dashboard/);
  const token = await page.evaluate(() => localStorage.getItem('pcces_token'));
  expect(token).toBeTruthy();
  return { username, token };
}

test.describe('PCCES full-stack browser gate', () => {
  test('frontend renders and proxies canonical Flask health', async ({ page, request }) => {
    await page.goto('/');
    await expect(page.locator('#root')).toBeVisible();
    const response = await request.get(`${frontendBase}/api/health`);
    expect(response.ok()).toBeTruthy();
    expect((await response.json()).status).toBe('ok');
  });

  test('canonical API rejects unauthenticated business access', async ({ request }) => {
    const response = await request.get(`${frontendBase}/api/projects/`);
    expect(response.status()).toBe(401);
    expect((await response.json()).code).toBe('UNAUTHORIZED');
  });

  test('newly registered user receives canonical capabilities immediately', async ({ page }) => {
    const { token } = await registerUser(page);
    const capability = await page.request.get(`${frontendBase}/api/capabilities/PROJECT_CATALOG`, {
      headers: { Authorization: `Bearer ${token}` },
    });
    expect(capability.status()).toBe(200);
    const decision = await capability.json();
    expect(decision.allowed).toBe(true);
    expect(decision.action_code).toBe('PROJECT_CATALOG');
  });

  test('project and budget lifecycle persists through real browser, API and PostgreSQL', async ({ page }) => {
    const { token } = await registerUser(page, 'E2E 專案預算使用者');
    const suffix = `${Date.now()}-${Math.floor(Math.random() * 100000)}`;
    const projectCode = `E2E-P-${suffix}`;
    const projectName = `E2E 道路改善工程 ${suffix}`;

    await page.goto('/app/projects');
    await expect(page.getByRole('heading', { name: '專案管理' })).toBeVisible();
    await page.getByRole('button', { name: /新增專案/ }).click();
    const modal = page.getByRole('dialog', { name: '新增專案' });
    await modal.getByLabel('專案編號').fill(projectCode);
    await modal.getByLabel('專案名稱').fill(projectName);
    await modal.getByLabel('地點').fill('臺北市');
    await modal.getByRole('button', { name: '確定' }).click();

    const row = page.getByRole('row').filter({ hasText: projectCode });
    await expect(row).toContainText(projectName);
    await expect(row).toContainText('臺北市');

    const projectsResponse = await page.request.get(`${frontendBase}/api/projects/`, {
      headers: { Authorization: `Bearer ${token}` },
    });
    expect(projectsResponse.ok()).toBeTruthy();
    const projects = await projectsResponse.json();
    const project = projects.find((item) => item.code === projectCode);
    expect(project).toBeTruthy();

    const createBudget = await page.request.post(`${frontendBase}/api/projects/${project.id}/budget/`, {
      headers: { Authorization: `Bearer ${token}` },
      data: {
        item_no: 'E2E-001',
        print_no: '1',
        c_name: '预拌混凝土 280kgf/cm2',
        c_unit: 'm3',
        quantity: 2,
        unit_price: 1250,
        kind: 'L',
      },
    });
    expect(createBudget.status()).toBe(201);

    const recalc = await page.request.post(`${frontendBase}/api/projects/${project.id}/budget/recalc`, {
      headers: { Authorization: `Bearer ${token}` },
    });
    expect(recalc.ok()).toBeTruthy();

    await row.getByRole('button', { name: '預算' }).click();
    await expect(page).toHaveURL(new RegExp(`/app/projects/${project.id}/budget`));
    await expect(page.getByRole('heading', { name: projectName })).toBeVisible();
    await expect(page.getByText('预拌混凝土 280kgf/cm2')).toBeVisible();
    await expect(page.getByText('$2,500')).toBeVisible();

    const treeResponse = await page.request.get(`${frontendBase}/api/projects/${project.id}/budget/tree`, {
      headers: { Authorization: `Bearer ${token}` },
    });
    expect(treeResponse.ok()).toBeTruthy();
    const tree = await treeResponse.json();
    expect(tree).toHaveLength(1);
    expect(tree[0].amount).toBe(2500);
    expect(tree[0].item_no).toBe('E2E-001');
  });

  test('Local Go API is reachable from the same full-stack run', async ({ request }) => {
    const response = await request.get(`${goBase}/api/health`);
    expect(response.ok()).toBeTruthy();
    expect((await response.json()).status).toBe('ok');
  });
});
