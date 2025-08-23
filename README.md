# Structure of CXE.CoreFx

CXE.CoreFx contains 2 main components at the moment - all of which are contained in the repo:

1. **CXE.CoreFx.Base** <br>
    - **Namespace:** CXE.CoreFx.Base
    - **Content:** This contains base functionality that is useful in all kinds of Dataverse projects. Following is a list of tools:
        - Logger - a Logger-Base with automatic indentation and other advanced features. There are implementations in place as ConsoleLogger as well as FileLogger.
        - Extensions - Added functions for the classes: Entity, EntityReference, Guid, Object, String.
        - String Helper - Some useful functions for handling strings.
    - **Models**
        - **Namespace:** CXE.CoreFx.Base.Models
        - **Content:** This namespace contains ready-to-use model classes for most of the out of the box used entities in Sales, Field Service, Customer Service.
        - **Notes:** 
            - This is compatible with latest dotnet (tested with .NET 8.0) as well as .NET Framework (dotnet full framework).
            - This namespace should only contain entities and fields from our own modules and out-of-the-box entities. For project/customer-specific extensions, create a shared project within the customer solution and inherit from this namespace.
    - **Enums**
        - **Namespace:** CXE.CoreFx.Base.Enums
        - **Content:** Contains a class with some readily used code-choices and alike.
2. **CXE.CoreFx.Plugin** 
    - **Note:** *(requires CXE.CoreFx.Base)*
    - Explained in separate section below...

<br>

# Importing CXE.CoreFx

**CXE.CoreFx requires an existing Visual Studio project that is under GIT version control.**

1. Open a command shell in the root folder of the project (where the solution file is placed) and add the GIT sub-module for CXE.CoreFx like this:
    
    ```bash
    git submodule add https://swiftleisure@dev.azure.com/swiftleisure/Dynamics%20365/_git/DataverseToolbox "CXE.CoreFx"
    ```

    This will clone the CXE.CoreFx code into your project folder as a sub-module. It will be placed in the folder "CXE.CoreFx".<br>
    
2. Open your project in Visual Studio. Then right click the solution in the very top of your Solution-Explorer and add the projects from the submodule that we just added: <br><br>
    **Add** ⮕ **Existing Project...** ( *.shproj ):

    - **CXE.CoreFx.Base** - *always needed*
    - **CXE.CoreFx.Plugin** - *needed for Plugins or Custom APIs*
    
3. Add the shared project to the references of your original project: <br>
    - Right Click on **References** ⮕ **Add Reference...** ⮕ **Shared Projects** ⮕ Select **CXE.CoreFx.Base** <br>
    - Right Click on **References** ⮕ **Add Reference...** ⮕ **Shared Projects** ⮕ Select **CXE.CoreFx.Plugin** *(for Plugin or Custom API projects)* <br>

<br>

# CXE.CoreFx.Plugin

CXE.CoreFx.Plugin is used for plugin assemblies. This includes Plugins as well as Custom APIs. <br>
Below is a step-by-step guide how to use this toolbox in your project.

1. Include the CXE.CoreFx libraries to your project as follows:
    ```csharp
    using CXE.CoreFx.Base;
    using CXE.CoreFx.Base.Models;
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
