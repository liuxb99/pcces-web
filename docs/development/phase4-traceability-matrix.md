# Phase 4 Traceability Matrix

Status legend: `VERIFIED` means Web/Python, Local Go and permanent tests exist. `OPEN` means required by the roadmap but not yet complete.

| ID | Capability | Legacy entry | Web/Python | Local Go | Permanent evidence | Status |
|---|---|---|---|---|---|---|
| P4-01 | Cost structure type and project selection | CostStructureTypePicker / FormBudgetCostStructurePicker | api/cost_structure.py | cost_structure_repository.go | Phase 4 cost structure tests | VERIFIED |
| P4-02 | Cost category import and item properties | CostStructureImport / FormBudgetCostProperty | api/cost_structure_details.py | cost_structure_detail_repository.go | detail import tests | VERIFIED |
| P4-03 | Fee, tax, signed adjustment calculation | DomainModule.CostStructure | api/cost_structure_calculation.py | cost_structure_calculation.go | calculation tests | VERIFIED |
| P4-04 | Project initialization and persisted recalculation | DomainModule.CostStructure | api/cost_structure_project_run.py | project cost structure run repository | run tests | VERIFIED |
| P4-05 | Budget-version linkage and run diff | Conversion.cs | api/cost_structure_run_versions.py | cost structure run version repository | version tests | VERIFIED |
| P4-06 | Budget-to-bid conversion session and lineage | Conversion.cs | api/budget_bid_conversion.py | budget_bid_conversion_repository.go | conversion tests | VERIFIED |
| P4-07 | Conversion preflight and wizard options | FormBudgetExp_Wzd / Option | api/conversion_wizard.py | conversion_wizard_repository.go | wizard tests | VERIFIED |
| P4-08 | Export job and immutable artifact metadata | FormBudgetExp_Wzd | api/conversion_export_jobs.py | conversion_export_jobs.go | export tests | VERIFIED |
| P4-09 | New and legacy XML serialization/validation | Conversion.cs | conversion_export_lifecycle.py | conversion_export_lifecycle.go | XML contract tests | VERIFIED |
| P4-10 | XLSX export | FormBudgetExp_Wzd | conversion_export_lifecycle.py | conversion_export_lifecycle.go | XLSX ZIP tests | VERIFIED |
| P4-11 | Export retry and artifact versions | FormBudgetExp_Wzd | conversion_export_lifecycle.py | conversion_export_lifecycle.go | lifecycle tests | VERIFIED |
| P4-12 | Electronic bid reverse import and format detection | formNewProjectWizard / Conversion.cs | bid_budget_roundtrip.py | bid_import_roundtrip.go | reverse-import tests | VERIFIED |
| P4-13 | Import session persistence and round-trip lineage | formNewProjectWizard | bid_budget_roundtrip.py | bid_import_sessions.go | canonical contract tests | VERIFIED |
| P4-14 | CREATE/REPLACE/APPEND atomic budget apply | formNewProjectWizard | bid_import_apply.py | bid_import_apply.go | apply and rollback tests | VERIFIED |
| P4-15 | Approved/frozen/archived write protection | FormBudgetExp_Wzd | bid_import_apply.py | bid_import_apply.go | read-only tests | VERIFIED |
| P4-16 | Post-import numeric and lineage consistency audit | Conversion.cs | phase4_roundtrip_audit.py | phase4_roundtrip_audit.go | round-trip audit tests | VERIFIED |
| P4-17 | Budget combine-bid conflict strategy | FormBudgetCombineBid / ucBudgetCombineBid | api/budget_combine_bid.py | combine_bid.go | strategy, persistence and canonical contract tests | VERIFIED |
| P4-18 | ZMD adapter | formNewProjectWizard | not implemented | not implemented | none | OPEN |
| P4-19 | MDB adapter | formNewProjectWizard | not implemented | not implemented | none | OPEN |
| P4-20 | PX adapter | formNewProjectWizard | not implemented | not implemented | none | OPEN |
| P4-21 | Source attachment, filename and downloadable error catalogue | FormBudgetExp_Wzd | partial metadata only | partial metadata only | partial | OPEN |
| P4-22 | Long-running progress and cancellation | FormBudgetExp_Wzd | not implemented | not implemented | none | OPEN |

## Combine-bid conflict contract

The combine-bid engine never silently overwrites duplicate item codes. Every collision is recorded with the existing and incoming source project and one explicit strategy:

- `BLOCK`: keep the first item and block the session.
- `KEEP_FIRST`: retain the existing item and record the decision.
- `KEEP_LAST`: replace with the incoming item and record the decision.
- `SUM_QUANTITY`: combine only when name, unit and unit price are compatible; otherwise block.
- `RENAME`: preserve both items by generating a deterministic suffix.

## Gate decision

Phase 4 is **not complete** while any row is `OPEN`. The next ordered batch is ZMD adapter parity, followed by MDB/PX adapters, source attachment/error catalogue, and long-running progress/cancellation.
