#!/bin/bash
set -e
npm --prefix web-pcces/frontend ci
npm --prefix web-pcces/frontend run build
python -c "
import shutil, os
src = 'web-pcces/frontend/dist'
dst = 'api/static'
for f in os.listdir(src):
    p = os.path.join(src, f)
    if os.path.isfile(p):
        shutil.copy2(p, dst)
    elif os.path.isdir(p):
        shutil.copytree(p, os.path.join(dst, f), dirs_exist_ok=True)
"
