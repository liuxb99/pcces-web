# IFC Integration Strategy

## Role in PCCES workflow

pcces-web should not use IFC as a price database. IFC supplies BIM objects, identity, geometry and quantities; PCCES remains the source of truth for work items, resource analysis, unit prices and estimates.

```text
IFC / Engineering IR
        ↓
Quantity and Classification Adapter
        ↓
PCCES work-item mapping
        ↓
Estimate / budget / variance
```

## Required IFC data

The adapter should read:

- IFC GUID and class;
- facility, storey, bridge part, zone and location;
- material and section properties;
- base quantities and calculated quantities;
- classification codes and custom engineering properties;
- provenance and review status.

## Mapping model

One BIM object may map to multiple PCCES work items. For example, one reinforced-concrete column may generate:

- concrete placement;
- reinforcing steel fabrication and installation;
- formwork;
- scaffolding or temporary works;
- finishing and inspection items.

Mappings should therefore be rule-based and many-to-many, not a single IFC-class-to-price-code lookup.

## Identity and traceability

Each quantity line should retain:

- semantic object ID;
- IFC GUID;
- source IFC revision;
- quantity formula and unit;
- mapping rule/version;
- approval status.

This enables recalculation when geometry changes and provides an audit trail from budget item back to the BIM component.

## Recommended boundaries

| Responsibility | Owner |
|---|---|
| BIM geometry and component identity | IFC / Engineering IR |
| Quantity formulas | quantity engine |
| Work-item classification | PCCES mapping rules / knowledge graph |
| Unit prices and resource analysis | pcces-web |
| Review and approval | pcces-web workflow |

## Delivery phases

1. Import IFC GUID, class, location and base quantities.
2. Map beams, columns, slabs, walls, piles and footings to PCCES items.
3. Add bridge parts and civil-infrastructure classifications.
4. Add revision diff and automatic quantity-impact reporting.
5. Add approved budget feedback to the shared knowledge graph.
