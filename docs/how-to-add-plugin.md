# How to Add a New Plugin

This guide walks through adding a new plugin end-to-end.

## 1. Create a Model Class

- Location: Plugins/<CompanyName>.D365.Dataverse.Plugins/Models/
- Decorate with attributes like [DataverseTable] and [DataverseColumn].

Example:
```
[DataverseTable('msdyn_customerasset')]
public class CustomerAssetModel
{
    [DataverseColumn('customerassetid')]
    public Guid Id { get; set; }

    [DataverseColumn('name')]
    public string Name { get; set; }
}
```

## 2. Create a Controller Class

- Location: Plugins/<CompanyName>.D365.Dataverse.Plugins/Controllers/
- Inherit from ModelControllerBase<TModel>.
```
Example:

public class CustomerAssetController : ModelControllerBase<CustomerAssetModel>
{
    public CustomerAssetController(IOrganizationService service, CustomerAssetModel entity, LoggerBase logger)
        : base(service, entity, logger)
    {
    }

    public void CreateWorkOrder(WorkOrderType_WorkOrderTypeEnum workOrderTypeChoice)
    {
        // Business logic here
    }
}
```

## 3. Create the Plugin Class

- Location: Plugins/<CompanyName>.D365.Dataverse.Plugins/
- Inherit from PluginBase and decorate with CrmPluginRegistration.

Example:
```
[CrmPluginRegistration(
    MessageNameEnum.Create,
    'msdyn_customerasset',
    StageEnum.PostOperation,
    ExecutionModeEnum.Asynchronous,
    '',
    '<CompanyName>.D365.Dataverse.Plugins.Asset - Asset Create PostAync - Create Delivery WorkOrder',
    1,
    IsolationModeEnum.Sandbox
)]
public class PluginAssetToDeliveryWorkOrder : PluginBase
{
    private CustomerAssetController _customerAssetController;

    public override void OnCreatePostOperation() => _customerAssetController.CreateWorkOrder(WorkOrderType_WorkOrderTypeEnum.Delivery);

    public override void InitializeController() =>
        _customerAssetController = new CustomerAssetController(
            this.ServiceClient,
            new CustomerAssetModel
            {
                Id = this.TargetEntity.Id,
                LogicalName = this.TargetEntity.LogicalName,
                Attributes = this.TargetEntity.Attributes
            },
            this.Logger
        );
}
```