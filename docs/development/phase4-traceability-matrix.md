# Phase 4 Traceability Matrix

Status legend: `VERIFIED` means Web/Python, Local Go and permanent tests exist.

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
| P4-18 | ZMD adapter | formNewProjectWizard | api/legacy_exchange_adapters.py | legacy_exchange_adapters.go | adapter tests | VERIFIED |
| P4-19 | MDB adapter | formNewProjectWizard | api/legacy_exchange_adapters.py | legacy_exchange_adapters.go | CSV bridge adapter tests | VERIFIED |
| P4-20 | PX adapter | formNewProjectWizard | api/legacy_exchange_adapters.py | legacy_exchange_adapters.go | XML adapter tests | VERIFIED |
| P4-21 | Source attachment, filename and downloadable error catalogue | FormBudgetExp_Wzd | api/conversion_source_artifacts.py | conversion_source_artifacts.go | immutable source, SHA-256 and CSV catalogue tests | VERIFIED |
| P4-22 | Long-running progress and cancellation | FormBudgetExp_Wzd | api/conversion_long_jobs.py | conversion_long_jobs.go | progress, optimistic locking and cancellation tests | VERIFIED |

## Completion contracts

- Duplicate combine-bid codes always use an explicit conflict strategy and are never silently overwritten.
- ZMD, MDB bridge and PX inputs normalize into one canonical item model with retained format/version lineage.
- Source files remain immutable and downloadable with original filename, media type, size and SHA-256.
- Long-running jobs use `QUEUED`, `RUNNING`, `COMPLETED`, `FAILED` and `CANCELLED`; progress is monotonic, updates use Row Version, and cancellation clears partial result/error payloads.

## Gate decision

All Phase 4 roadmap capabilities are `VERIFIED`. Phase 4 is complete pending the repository's remote CI result; Phase 5 may begin only after the Phase 4 gate is green.
