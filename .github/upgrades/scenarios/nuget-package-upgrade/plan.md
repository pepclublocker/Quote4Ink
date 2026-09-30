# NuGet Package Upgrade Plan

## Overview

**Target**: Resolve the SSH.NET, SQLitePCLRaw, and NuGet.Protocol security advisories by updating the affected package dependency chains.
**Scope**: Two projects, `Web` and `Quote4Ink_DataLoader`.

## Tasks

### 01-upgrade-ssh-net: Upgrade SSH.NET in Quote4Ink_DataLoader

Update the project-level `SSH.NET` reference in `Quote4Ink_DataLoader/Quote4Ink_DataLoader.csproj` from 2025.1.0 to 2026.0.0. The quick assessment found no SSH.NET API breaks. The subsequently expanded NuGet.Protocol and SQLitePCLRaw work is tracked in the child tasks below.

**Done when**: `Quote4Ink_DataLoader` builds and the audit no longer reports SSH.NET advisories GHSA-q939-rpr3-3284 or GHSA-mggc-4xg6-vcxf.

#### 01.01-web-advisory-upgrade: Resolve Web project package advisories

In `Web/Web.csproj`, update NuGet.Packaging to 7.9.0, add a direct NuGet.Protocol 7.9.0 reference to override the vulnerable 6.12.1 transitive dependency, and add `SQLitePCLRaw.bundle_e_sqlite3` 3.0.5 to update the vulnerable transitive native library. The dependency chain is `Microsoft.EntityFrameworkCore.Sqlite` → `SQLitePCLRaw.bundle_e_sqlite3` → `SQLitePCLRaw.lib.e_sqlite3`.

**Done when**: `Web` restores and builds, with no scoped SQLitePCLRaw or NuGet.Protocol advisories reported for this project.

#### 01.02-data-loader-package-updates: Align DataLoader NuGet dependencies

In `Quote4Ink_DataLoader/Quote4Ink_DataLoader.csproj`, update NuGet.Protocol and NuGet.Packaging to 7.9.0 and verify SSH.NET remains at 2026.0.0. The API assessment detected no SSH.NET or NuGet.Packaging breaks; review `apidiff/NuGet.Protocol.apidiff.md` for the four removed members and use build diagnostics if any are referenced.

**Done when**: The project restores and builds with the assessed package versions and no scoped SSH.NET advisory remains.

#### 01.03-solution-validation: Validate package advisories across the solution

Run a full solution build and the available test projects after both project updates. Audit direct and transitive packages to verify the reported SSH.NET, SQLitePCLRaw.lib.e_sqlite3, and NuGet.Protocol advisories are cleared.

**Done when**: The solution build and available tests pass, the target package versions are resolved as planned, and the audit contains none of the scoped advisories.
