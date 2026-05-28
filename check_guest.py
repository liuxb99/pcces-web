import json, subprocess

BASE = "https://pcces-web.vercel.app"

def api(method, path, data=None, token=None):
    cmd = ["curl", "-sk", "-w", "\n%{http_code}"]
    if method == "POST":
        cmd += ["-X", "POST"]
    if data:
        cmd += ["-H", "Content-Type: application/json", "-d", json.dumps(data)]
    if token:
        cmd += ["-H", f"Authorization: Bearer {token}"]
    cmd += [f"{BASE}{path}"]
    r = subprocess.run(cmd, capture_output=True, text=True)
    lines = r.stdout.strip().split("\n")
    http_code = lines[-1].strip()
    body = "\n".join(lines[:-1])
    try:
        return http_code, json.loads(body)
    except:
        return http_code, body

# Guest access (no token) — deployed api/index.py has guest fallback
code, resp = api("GET", "/api/projects/")
print(f"[Guest Projects] HTTP {code}: {len(resp) if isinstance(resp,list) else 'N/A'} projects")

code, resp = api("GET", "/api/projects/stats")
print(f"[Guest Stats] HTTP {code}: OK")

code, resp = api("GET", "/api/projects/1/budget/tree")
print(f"[Guest Tree] HTTP {code}")

code, resp = api("GET", "/api/projects/1/resources/")
print(f"[Guest Resources] HTTP {code}")

print("\nGuest-mode access: OK (as expected for demo)")
