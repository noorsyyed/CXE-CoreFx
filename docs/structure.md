# Structure Overview and Conventions

## Structure Overview

- Controllers: Business logic for entities (e.g., CustomerAssetController, WorkOrderController).
- Models: Strongly-typed representations of Dataverse entities (e.g., CustomerAssetModel, WorkOrderModel).
- Plugin Classes: Entry points for plugin execution, decorated with registration attributes (e.g., PluginAssetToDeliveryWorkOrder).
- Attributes: Used in model classes to map properties to Dataverse columns and tables.

## Conventions

- Naming: Use PascalCase for classes, camelCase for methods, and ALL_CAPS for constants.
- Controllers: Encapsulate business logic; keep plugins thin.
- Models: Use attributes for mapping; keep them focused.
- Plugin Classes: Orchestrate and delegate to controllers; minimal logic.
- Attributes: Always use CrmPluginRegistration for plugin steps and DataverseTable/DataverseColumn for model mapping.
