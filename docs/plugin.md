# CXE.CoreFx.Plugin

CXE.CoreFx.Plugin is used for plugin assemblies (Plugins and Custom APIs). It provides a PluginBase and helpers that handle logging, error handling, and structured lifecycle methods.

## Usage

1. Include namespaces:
```
using CXE.CoreFx;
using CXE.CoreFx.Models;
using CXE.CoreFx.Plugin;
```

2. Inherit from PluginBase:
```
public class YOUR_PROJECT_CLASS : PluginBase
{
    // ...
}
```
3. Override lifecycle methods as needed:

#### Custom APIs should use Execute()
public override void Execute()

#### Create
public override void OnCreatePreOperation()
public override void OnCreatePostOperation()

#### Update
public override void OnUpdatePreOperation()
public override void OnUpdatePostOperation()

#### Delete
public override void OnDeletePreValidation()
public override void OnDeletePreOperation()
public override void OnDeletePostOperation()

#### Set State
public override void OnSetStatePreOperation()
public override void OnSetStatePostOperation()

## Example Plugin (Account)
```
public class PluginAccount : PluginBase
{
    public override void OnCreatePostOperation()
    {
        SetTitle();
    }

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

    private void SetTitle()
    {
        EnterFunction();

        if (!IsSetTitleCondition)
        {
            ExitFunction("Condition not met.");
            return;
        }

        var accountToBeUpdated = new Entity(TargetEntity.LogicalName, TargetEntity.Id);
        accountToBeUpdated.AddAttribute(Account.Name, Account.Address1Country + " - " + Account.AccountNumber);
        ServiceClient.Update(accountToBeUpdated);

        ExitFunction();
    }
}
```
## Registration with spkl
```
// Add nuget package `spkl` to the project and use attributes like:

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
```