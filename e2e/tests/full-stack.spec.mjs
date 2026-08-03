import { test, expect } from '@playwright/test';

const frontendBase = process.env.PCCES_E2E_BASE_URL || 'http://127.0.0.1:4173';
const goBase = process.env.PCCES_GO_BASE_URL || 'http://127.0.0.1:8080';

test.describe('PCCES full-stack browser gate', () => {
  test('frontend renders and proxies canonical Flask health', async ({ page, request }) => {
    await page.goto('/');
    await expect(page.locator('#root')).toBeVisible();

    const response = await request.get(`${frontendBase}/api/health`);
    expect(response.ok()).toBeTruthy();
    const body = await response.json();
    expect(body.status).toBe('ok');
  });

  test('canonical API rejects unauthenticated business access', async ({ request }) => {
    const response = await request.get(`${frontendBase}/api/projects/`);
    expect(response.status()).toBe(401);
    const body = await response.json();
    expect(body.code).toBe('UNAUTHORIZED');
  });

  test('user can register through the real browser UI and retain JWT session', async ({ page }) => {
    const suffix = `${Date.now()}-${Math.floor(Math.random() * 100000)}`;
    const username = `e2e_${suffix}`;

    await page.goto('/login?tab=register');
    await expect(page.getByText('PCCES 網頁版')).toBeVisible();
    await expect(page.getByRole('tab', { name: '註冊' })).toHaveAttribute('aria-selected', 'true');

    await page.getByPlaceholder('帳號').fill(username);
    await page.getByPlaceholder('顯示名稱').fill('E2E 驗收使用者');
    await page.getByPlaceholder('密碼', { exact: true }).fill('E2ePass123!');
    await page.getByPlaceholder('確認密碼').fill('E2ePass123!');
    await page.getByRole('button', { name: '註冊', exact: true }).click();

    await expect(page).toHaveURL(/\/app\/dashboard/);
    const token = await page.evaluate(() => localStorage.getItem('pcces_token'));
    expect(token).toBeTruthy();

    const health = await page.request.get(`${frontendBase}/api/health`);
    expect(health.ok()).toBeTruthy();

    const capability = await page.request.get(`${frontendBase}/api/capabilities/project.read`, {
      headers: { Authorization: `Bearer ${token}` },
    });
    expect(capability.status()).toBe(200);
    const decision = await capability.json();
    expect(decision.user_id).toBeTruthy();
    expect(decision.action_code).toBe('project.read');
  });

  test('Local Go API is reachable from the same full-stack run', async ({ request }) => {
    const response = await request.get(`${goBase}/api/health`);
    expect(response.ok()).toBeTruthy();
    const body = await response.json();
    expect(body.status).toBe('ok');
  });
});
