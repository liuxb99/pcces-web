import os,tempfile,unittest
from sqlalchemy import create_engine
from api.admin_console import AdminConsoleService

class AdminConsoleTest(unittest.TestCase):
 def setUp(self):
  self.tmp=tempfile.NamedTemporaryFile(suffix=".db",delete=False);self.tmp.close()
  self.engine=create_engine("sqlite:///"+self.tmp.name);self.s=AdminConsoleService(self.engine,"sqlite:///"+self.tmp.name)
 def tearDown(self):
  self.engine.dispose();os.unlink(self.tmp.name)
 def test_typed_setting_and_version(self):
  item=self.s.set_setting("autosave.interval_seconds",{"value":60,"row_version":0},"u")
  self.assertEqual(item["value"],60);self.assertEqual(item["row_version"],1)
  with self.assertRaises(RuntimeError):self.s.set_setting("autosave.interval_seconds",{"value":70,"row_version":0},"u")
  with self.assertRaises(ValueError):self.s.set_setting("autosave.interval_seconds",{"value":"bad","row_version":1},"u")
 def test_backup_has_hash_and_smoke(self):
  run=self.s.backup("u");self.assertEqual(run["status"],"COMPLETED");self.assertEqual(len(run["sha256"]),64);self.assertIsNotNone(self.s.backup_artifact(run["id"]))

if __name__=="__main__":unittest.main()
