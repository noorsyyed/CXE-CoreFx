# CXE.CoreFx – Overview

This documentation set introduces the CXE.CoreFx libraries and how to use them in Dataverse projects. It is split into focused pages for quick navigation.

## Components

1. CXE.CoreFx
   - Namespace: CXE.CoreFx
   - Content:
     - Logger: Logger base with automatic indentation and advanced features. Includes ConsoleLogger and FileLogger.
     - Extensions: Added helpers for Entity, EntityReference, Guid, Object, and String.
     - String Helper: Utility functions for handling strings.
   - Models
     - Namespace: CXE.CoreFx.Models
     - Ready-to-use model classes for out-of-the-box entities used in Sales, Field Service, and Customer Service.
     - Notes:
       - Compatible with .NET 8.0 and .NET Framework (full framework).
       - Only contains entities/fields from our own modules and OOB entities. For customer-specific extensions, create a shared project in the customer solution and inherit from this namespace.
   - Enums
     - Namespace: CXE.CoreFx.Enums
     - Contains commonly used code choices and related constants.

2. CXE.CoreFx.Plugin
   - A toolbox for plugin assemblies (Plugins and Custom APIs). See the Plugin page for step-by-step usage and examples.

## Next Steps

- Import the library into your solution: see Importing CXE.CoreFx.
- Learn how to write and register plugins using PluginBase: see CXE.CoreFx.Plugin.
- Follow the conventions and structure used across this repository: see Structure Overview and Conventions.

## Links

- [Importing CXE.CoreFx](./importing.md)
- [CXE.CoreFx.Plugin](./plugin.md)
- [Structure Overview and Conventions](./structure.md)
- [How to Add a New Plugin](./how-to-add-plugin.md)
- [Attribute Usage (Models)](./attributes.md)
- [Quick Start Checklist](./checklist.md)
