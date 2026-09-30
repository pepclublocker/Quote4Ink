# 01-upgrade-ssh-net: Upgrade SSH.NET in Quote4Ink_DataLoader

Update the project-level `SSH.NET` PackageReference in `Quote4Ink_DataLoader/Quote4Ink_DataLoader.csproj` from 2025.1.0 to 2026.0.0. This original SSH.NET work is independently complete; the additional NuGet.Protocol and SQLitePCLRaw remediation requested later is tracked in the child tasks.

## Research Findings

- Both projects target `net10.0` and use project-level package versions; no `Directory.Packages.props` was found.
- `Quote4Ink_DataLoader` directly references SSH.NET 2025.1.0, NuGet.Protocol 7.3.1, and NuGet.Packaging 7.3.1. SSH.NET is already updated to the assessment-recommended 2026.0.0.
- `Web` directly references NuGet.Packaging 7.3.1 and receives NuGet.Protocol 6.12.1 and SQLitePCLRaw.bundle_e_sqlite3 2.1.11 transitively. The SQLite vulnerability path is `Microsoft.EntityFrameworkCore.Sqlite` → `SQLitePCLRaw.bundle_e_sqlite3` → `SQLitePCLRaw.lib.e_sqlite3`.
- The solution assessment recommends NuGet.Protocol 7.9.0 and NuGet.Packaging 7.9.0; it detects four removed NuGet.Protocol members. A source search found no matching usages. The supported stable bundle version is 3.0.5.
- The baseline audit reported two High SSH.NET advisories, one High SQLitePCLRaw advisory, and one Low NuGet.Protocol advisory. Keep the Web dependency update before DataLoader because DataLoader references Web.

**Done when**: `Quote4Ink_DataLoader` builds and NuGet audit no longer reports SSH.NET advisories GHSA-q939-rpr3-3284 or GHSA-mggc-4xg6-vcxf.
