import unittest
from sqlalchemy import create_engine, select

from api.budget_calculation_trace import BudgetTraceService, budget_calculation_traces


class BudgetCalculationTraceTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        self.service = BudgetTraceService(self.engine)

    def test_persists_and_reads_trace(self):
        trace = self.service.calculate("P1", "I1", "F", 2, {"base":"1000", "rate":"0.075"})
        self.assertEqual("75.00", trace["result"])
        loaded = self.service.get(trace["id"])
        self.assertEqual("MULTIPLY_BASE_RATE", loaded["steps"][0]["operation"])
        self.assertEqual(1, len(self.service.list_project("P1")))

    def test_trace_is_append_only(self):
        trace = self.service.calculate("P1", None, "L", 2, {"quantity":"2", "unit_price":"3"})
        with self.engine.connect() as conn:
            rows = conn.execute(select(budget_calculation_traces.c.id)).all()
        self.assertEqual("6.00", trace["result"])
        self.assertEqual(1, len(rows))

    def test_invalid_kind_does_not_persist(self):
        with self.assertRaises(ValueError):
            self.service.calculate("P1", None, "X", 2, {})
        self.assertEqual([], self.service.list_project("P1"))


if __name__ == "__main__":
    unittest.main()
