# 01.01-web-package-advisories: Resolve Web project package advisories

## Objective
Clear the scoped NuGet.Protocol and SQLitePCLRaw advisories in the Web project before updating its consumer.

## Scope
Modify `Web/Web.csproj` only. Update NuGet.Packaging from 7.3.1 to 7.9.0, add a direct NuGet.Protocol 7.9.0 reference to override the vulnerable 6.12.1 transitive version, and add `SQLitePCLRaw.bundle_e_sqlite3` 3.0.5 to override the 2.1.11 bundle brought by Microsoft.EntityFrameworkCore.Sqlite.

## Research
Both projects target net10.0 and use project-level PackageReference versions. No Directory.Packages.props exists. The SQLite advisory path is Microsoft.EntityFrameworkCore.Sqlite → SQLitePCLRaw.bundle_e_sqlite3 → SQLitePCLRaw.lib.e_sqlite3. NuGet.Protocol's assessed 7.9.0 diff reports four removed members; no matching source usages were found.

## Steps
1. Update/add the specified PackageReference entries in Web.csproj.
2. Restore and build Web, fixing any relevant compatibility errors or warnings.
3. Audit the Web project to confirm its scoped NuGet.Protocol and SQLitePCLRaw advisories are cleared.

**Done when**: Web restores/builds successfully and no scoped SQLitePCLRaw or NuGet.Protocol advisory remains in its audit.
