# 01.01-web-advisory-upgrade: Resolve Web project package advisories

## Objective
Clear the scoped NuGet.Protocol and SQLitePCLRaw advisories in the Web project before updating its consumer.

## Scope
Modify `Web/Web.csproj` only. Update NuGet.Packaging from 7.3.1 to 7.9.0, add a direct NuGet.Protocol 7.9.0 reference to override the vulnerable 6.12.1 transitive version, and add `SQLitePCLRaw.bundle_e_sqlite3` 3.0.5 to override the 2.1.11 bundle brought by Microsoft.EntityFrameworkCore.Sqlite.

## Research
`Web` targets `net10.0` and uses project-level PackageReference versions; `get_project_dependencies` confirms NuGet.Packaging 7.3.1 is directly defined in `Web/Web.csproj`, and no `Directory.Packages.props` is used. NuGet.Protocol is currently resolved transitively at 6.12.1 through the Web code-generation dependency chain; a direct 7.9.0 reference will override it. SQLitePCLRaw.lib.e_sqlite3 2.1.11 is reached through Microsoft.EntityFrameworkCore.Sqlite 10.0.7 → SQLitePCLRaw.bundle_e_sqlite3 2.1.11 → SQLitePCLRaw.lib.e_sqlite3; the supported bundle target is 3.0.5. The NuGet.Protocol 7.9.0 API diff reports four removed members, with no matching source usages found.

## Steps
1. Update/add the specified PackageReference entries in Web.csproj.
2. Restore and build Web, fixing relevant compatibility errors or warnings.
3. Audit the Web project to confirm its scoped NuGet.Protocol and SQLitePCLRaw advisories are cleared.

**Done when**: Web restores/builds successfully and no scoped SQLitePCLRaw or NuGet.Protocol advisory remains in its audit.
