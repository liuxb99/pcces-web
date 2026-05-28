import json, subprocess, sys

BASE = "https://pcces-web.vercel.app"

def api(method, path, data=None, token=None):
    cmd = ["curl", "-sk"]
    if method == "POST":
        cmd += ["-X", "POST"]
    if data:
        cmd += ["-H", "Content-Type: application/json", "-d", json.dumps(data)]
    if token:
        cmd += ["-H", f"Authorization: Bearer {token}"]
    cmd += [f"{BASE}{path}"]
    r = subprocess.run(cmd, capture_output=True, text=True)
    try:
        return r.returncode, json.loads(r.stdout)
    except json.JSONDecodeError:
        return r.returncode, r.stdout[:200]

# 1. Health
rc, health = api("GET", "/api/health")
print(f"[1] Health: {health.get('status','FAIL')} (rc={rc})")
assert health.get("status") == "ok"

# 2. Login as demo
rc, login = api("POST", "/api/auth/login", {"username": "demo", "password": "demo123"})
print(f"[2] Login: {'OK' if 'access_token' in login else 'FAIL'}")
assert "access_token" in login
token = login["access_token"]
print(f"    user: {login['user']['display_name']} / {login['user']['company']}")

# 3. List projects
rc, projects = api("GET", "/api/projects/", token=token)
print(f"[3] Projects: {len(projects)} project(s)")
for p in projects:
    print(f"    - {p['code']}: {p['name']}")
assert len(projects) >= 1
assert any(p["code"] == "DEMO001" for p in projects)

# 4. Budget tree
pid = projects[0]["id"]
rc, tree = api("GET", f"/api/projects/{pid}/budget/tree", token=token)
print(f"[4] Budget tree roots: {len(tree)}")
for root in tree:
    print(f"    Root: {root['c_name']} (kind={root['kind']}, amount={root['amount']}, children={len(root.get('children',[]))})")
    for child in root.get("children", []):
        print(f"      Child: {child['c_name']} (kind={child['kind']}, amount={child['amount']}, children={len(child.get('children',[]))})")
        for gc in child.get("children", []):
            print(f"        Grandchild: {gc['c_name']} (amount={gc['amount']})")

# 5. Resources
rc, resources = api("GET", f"/api/projects/{pid}/resources/", token=token)
print(f"[5] Resources: {len(resources)}")

# 6. Dashboard stats
rc, stats = api("GET", "/api/projects/stats", token=token)
print(f"[6] Stats: {stats.get('total_projects')} projects, {stats.get('total_budget_items')} items")

print("\nALL CHECKS PASSED")
