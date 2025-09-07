# Quick Start Checklist

Use this checklist to get productive quickly.

- [ ] Add the CXE.CoreFx submodule to your repository.
- [ ] Add CXE.CoreFx and CXE.CoreFx.Plugin shared projects to your solution.
- [ ] Reference the shared projects from your main project(s).
- [ ] Create a model with [DataverseTable] and [DataverseColumn] attributes.
- [ ] Create a controller inheriting ModelControllerBase<T>.
- [ ] Create a plugin that inherits PluginBase.
- [ ] Register plugin steps using [CrmPluginRegistration] (consider spkl).
- [ ] Keep plugin logic minimal; delegate to controllers.
- [ ] Test thoroughly in a sandbox environment.
