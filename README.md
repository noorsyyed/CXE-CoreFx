# Structure of CXE.CoreFx

CXE.CoreFx contains 2 main components at the moment - all of which are contained in the repo:

1. **CXE.CoreFx** <br>
    - **Namespace:** CXE.CoreFx
    - **Content:** This contains base functionality that is useful in all kinds of Dataverse projects. Following is a list of tools:
        - Logger - a Logger-Base with automatic indentation and other advanced features. There are implementations in place as ConsoleLogger as well as FileLogger.
        - Extensions - Added functions for the classes: Entity, EntityReference, Guid, Object, String.
        - String Helper - Some useful functions for handling strings.
    - **Models**
        - **Namespace:** CXE.CoreFx.Models
        - **Content:** This namespace contains ready-to-use model classes for most of the out of the box used entities in Sales, Field Service, Customer Service.
        - **Notes:** 
            - This is compatible with latest dotnet (tested with .NET 8.0) as well as .NET Framework (dotnet full framework).
            - This namespace should only contain entities and fields from our own modules and out-of-the-box entities. For project/customer-specific extensions, create a shared project within the customer solution and inherit from this namespace.
    - **Enums**
        - **Namespace:** CXE.CoreFx.Enums
        - **Content:** Contains a class with some readily used code-choices and alike.
2. **CXE.CoreFx.Plugin** 
    - **Note:** *(requires CXE.CoreFx)*
    - Explained in separate section below...

<br>

# Importing CXE.CoreFx

**CXE.CoreFx requires an existing Visual Studio project that is under GIT version control.**

1. Open a command shell in the root folder of the project (where the solution file is placed) and add the GIT sub-module for CXE.CoreFx like this:
    
    ```bash
    git submodule add https://github.com/noorsyyed/CXE-CoreFx.git "CXE.CoreFx"
    ```

    This will clone the CXE.CoreFx code into your project folder as a sub-module. It will be placed in the folder "CXE.CoreFx".<br>
    
2. Open your project in Visual Studio. Then right click the solution in the very top of your Solution-Explorer and add the projects from the submodule that we just added: <br><br>
    **Add** ⮕ **Existing Project...** ( *.shproj ):

    - **CXE.CoreFx** - *always needed*
    - **CXE.CoreFx.Plugin** - *needed for Plugins or Custom APIs*
    
3. Add the shared project to the references of your original project: <br>
    - Right Click on **References** ⮕ **Add Reference...** ⮕ **Shared Projects** ⮕ Select **CXE.CoreFx** <br>
    - Right Click on **References** ⮕ **Add Reference...** ⮕ **Shared Projects** ⮕ Select **CXE.CoreFx.Plugin** *(for Plugin or Custom API projects)* <br>

<br>

# CXE.CoreFx.Plugin

CXE.CoreFx.Plugin is used for plugin assemblies. This includes Plugins as well as Custom APIs. <br>
Below is a step-by-step guide how to use this toolbox in your project.

1. Include the CXE.CoreFx libraries to your project as follows:
    ```csharp
    using CXE.CoreFx;
    using CXE.CoreFx.Models;
    using CXE.CoreFx.Plugin;
    ```

2. Next inherit PluginBase class for your project class:
    
    ```csharp
    public class YOUR_PROJECT_CLASS : PluginBase
    {
        // ...
    }
    ```

    ..and override the needed Execute function. These are the available functions you can override:<br>

    ```csharp
    // Always executed when plugin is triggered.
    // This is the function you need to use for your Custom APIs
    public override void Execute()                  

    // Only executed on Create
    public override void OnCreatePreOperation()     
    public override void OnCreatePostOperation()
    
    // Only executed on Update
    public override void OnUpdatePreOperation()
    public override void OnUpdatePostOperation()
    
    // Only executed on Delete
    public override void OnDeletePreValidation()
    public override void OnDeletePreOperation()
    public override void OnDeletePostOperation()
    
    // Only executed on SetState
    public override void OnSetStatePreOperation()
    public override void OnSetStatePostOperation()
    ```

    Error handling (try catch) and basic logging are already taken care of. <br>

3. Example: Here is how a typical plugin for the entity 'Account' could look like:

    ```csharp
    // ============================================================================
    // ============================================================================
    // ============================================================================
    public class PluginAccount : PluginBase
    {

        #region Base Functions

        // ============================================================================
        public override void OnCreatePostOperation()
        {
            SetTitle();
        }

        #endregion

        #region Condition Properties

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private bool IsSetTitleCondition
        {
            get
            {
                if (!TargetEntity.Contains(Account.Address1Country) ||
                    !TargetEntity.Contains(Account.AccountNumber))
                {
                    return false;
                }

                return true;
            }
        }

        #endregion

        #region Custom Logic

        // ============================================================================
        private void SetTitle()
        {
            EnterFunction();

            if (!IsSetTitleCondition)
            {
                ExitFunction("Condition not met.");
                return;
            }

            var accountToBeUpdated =
                new Entity(
                    TargetEntity.LogicalName,
                    TargetEntity.Id);

            accountToBeUpdated.AddAttribute(
                Account.Name,
                Account.Address1Country + " - " + Account.AccountNumber);

            ServiceClient.Update(accountToBeUpdated);

            ExitFunction();
        }

        #endregion
    }
    ```

    You should use '**spkl**' (install the nuget package) for deploying plugins. Here is an example **spkl** configuration for the above example plugin:

    ```csharp

    #region SPKL

    [
        CrmPluginRegistration(
            MessageNameEnum.Create,
            "account",
            StageEnum.PostOperation,
            ExecutionModeEnum.Synchronous,
            "",
            "CXE.CoreFx.Plugin.Account - Account Create PostSync",
            1,
            IsolationModeEnum.Sandbox
        )
    ]

    #endregion

    ```


    # Plugins Usage Guide


---

## Structure Overview

- **Controllers**: Business logic for entities (e.g., `CustomerAssetController`, `WorkOrderController`).
- **Models**: Strongly-typed representations of Dataverse entities (e.g., `CustomerAssetModel`, `WorkOrderModel`).
- **Plugin Classes**: Entry points for plugin execution, decorated with registration attributes (e.g., `PluginAssetToDeliveryWorkOrder`).
- **Attributes**: Used in model classes to map properties to Dataverse columns and tables.

---

## How to Add a New Plugin

### 1. Create a Model Class

- Place your model in `Plugins/<CompanyName>.D365.Dataverse.Plugins/Models/`.
- Use attributes like `[DataverseTable]` and `[DataverseColumn]` to map your class and properties to Dataverse tables and columns.
- Example:

    ```csharp
    [DataverseTable('msdyn_customerasset')]
    public class CustomerAssetModel
    {
        [DataverseColumn('customerassetid')]
        public Guid Id { get; set; }

        [DataverseColumn('name')]
        public string Name { get; set; }

        // Add other properties as needed
    }
    ```

    **Attribute Description:**
    - `DataverseTable`: Specifies the logical name of the Dataverse table.
    - `DataverseColumn`: Maps a property to a specific column in the Dataverse table column.

---

### 2. Create a Controller Class

- Place your controller in `Plugins/<CompanyName>.D365.Dataverse.Plugins/Controllers/`.
- Inherit from `ModelControllerBase<TModel>`, where `TModel` is your model class.
- Implement business logic methods (e.g., create, update, custom logic).
- Example:

    ```csharp
    public class CustomerAssetController : ModelControllerBase<CustomerAssetModel>
    {
        public CustomerAssetController(IOrganizationService service, CustomerAssetModel entity, LoggerBase logger)
            : base(service, entity, logger)
        {
            // Initialization logic
        }

        public void CreateWorkOrder(WorkOrderType_WorkOrderTypeEnum workOrderTypeChoice)
        {
            // Business logic for creating a work order
        }
    }
    ```

---

### 3. Create the Plugin Class

- Place your plugin class in `Plugins/<CompanyName>.D365.Dataverse.Plugins/`.
- Inherit from `PluginBase` (from the Plugin Toolbox).
- Annotate the class with `[CrmPluginRegistration]` attributes to register plugin steps.
- Override the relevant methods (`OnCreatePostOperation`, `OnUpdatePostOperation`, etc.).
- Example:

    ```csharp
    [CrmPluginRegistration(
        MessageNameEnum.Create,
        'msdyn_customerasset',
        StageEnum.PostOperation,
        ExecutionModeEnum.Asynchronous,
        '',
        'Swift.D365.Dataverse.Plugins.Asset - Asset Create PostAync - Create Delivery WorkOrder',
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

---

### 4. Conventions to Follow

- **Naming**: Use PascalCase for classes, camelCase for methods, and ALL_CAPS for constants.
- **Controllers**: Encapsulate business logic, keep plugins thin.
- **Models**: Use attributes for mapping, keep them simple and focused.
- **Plugin Classes**: Only handle orchestration and registration, delegate logic to controllers.
- **Attributes**: Always use `[CrmPluginRegistration]` for plugin steps, and `[DataverseTable]`/`[DataverseColumn]` for models.

---

## Example: Adding a New Plugin

Suppose you want to create a plugin for a new entity called `msdyn_newentity`.

1. **Model**: Create `NewEntityModel.cs` with `[DataverseTable('msdyn_newentity')]` and `[DataverseColumn]` attributes.
2. **Controller**: Create `NewEntityController.cs` inheriting from `ModelControllerBase<NewEntityModel>`.
3. **Plugin**: Create `PluginNewEntityHandler.cs`, inherit from `PluginBase`, decorate with `[CrmPluginRegistration]`, and override needed methods.

---

## Attribute Usage in Model Classes

- **`[DataverseTable]`**: Marks the class as representing a Dataverse table.
- **`[DataverseColumn]`**: Maps a property to a Dataverse column.
- These attributes ensure your models are correctly mapped for CRUD operations and make your codebase maintainable and clear.



## Quick Start Checklist

1. Create your model class with proper attributes.
2. Implement a controller for business logic.
3. Create a plugin class, register it, and delegate logic to the controller.
4. Follow naming and structural conventions.
5. Test your plugin thoroughly.

---

By following these steps and conventions, any new developer can quickly start building plugins in this solution with confidence