import unittest

from sqlalchemy import create_engine

from api.migrations import MIGRATIONS, applied_versions, run_migrations


class MigrationRegistryTests(unittest.TestCase):
    def test_clean_database_applies_all_migrations_once(self):
        engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        expected = [version for version, _ in MIGRATIONS]
        self.assertEqual(expected, run_migrations(engine))
        self.assertEqual(expected, applied_versions(engine))
        self.assertEqual([], run_migrations(engine))
        self.assertEqual(expected, applied_versions(engine))


if __name__ == "__main__":
    unittest.main()
