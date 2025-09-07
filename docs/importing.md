# Importing CXE.CoreFx

CXE.CoreFx requires an existing Visual Studio project that is under Git version control.

## Steps

1. Add the CXE.CoreFx repository as a submodule from your solution root (where the .sln lives):

   git submodule add https://github.com/noorsyyed/CXE-CoreFx.git "CXE.CoreFx"

   This clones the code into the folder "CXE.CoreFx".

2. In Visual Studio, right-click the solution and choose Add ? Existing Project... and add:
   - CXE.CoreFx (always needed)
   - CXE.CoreFx.Plugin (needed for Plugins or Custom APIs)

3. Add shared project references in your original project:
   - References ? Add Reference... ? Shared Projects ? CXE.CoreFx
   - References ? Add Reference... ? Shared Projects ? CXE.CoreFx.Plugin (for Plugin or Custom API projects)
