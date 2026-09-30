# 01.02-data-loader-package-updates: Align DataLoader NuGet dependencies

## Objective
Update the DataLoader project's direct package references and confirm the existing SSH.NET fix remains in place.

## Scope
Modify `Quote4Ink_DataLoader/Quote4Ink_DataLoader.csproj` only. Upgrade NuGet.Protocol and NuGet.Packaging from 7.3.1 to the assessed 7.9.0. Verify SSH.NET remains at 2026.0.0 (already changed).

## Research
`Quote4Ink_DataLoader` targets `net10.0`, references `Web`, and uses project-level PackageReference versions; `get_project_dependencies` confirms NuGet.Packaging 7.3.1, NuGet.Protocol 7.3.1, and SSH.NET 2026.0.0 are directly defined in its csproj, with no `Directory.Packages.props`. The Web project now resolves NuGet.Packaging and NuGet.Protocol at 7.9.0, so align the DataLoader references to prevent version divergence in the application dependency graph. The assessment reports no SSH.NET or NuGet.Packaging breaks and four removed NuGet.Protocol members; no matching source usages were found.

## Steps
1. Update the NuGet.Protocol and NuGet.Packaging references to 7.9.0.
2. Verify SSH.NET remains at 2026.0.0.
3. Restore/build the project and audit the resolved package versions and scoped SSH.NET advisory.

**Done when**: The project builds successfully with the assessed versions and the SSH.NET advisories remain cleared.
