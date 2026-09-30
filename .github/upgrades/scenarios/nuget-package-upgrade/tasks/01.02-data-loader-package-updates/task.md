# 01.02-data-loader-package-updates: Align DataLoader NuGet dependencies

## Objective
Update the DataLoader project's direct package references and confirm the existing SSH.NET fix remains in place.

## Scope
Modify `Quote4Ink_DataLoader/Quote4Ink_DataLoader.csproj` only. Upgrade NuGet.Protocol and NuGet.Packaging from 7.3.1 to the assessed 7.9.0. Verify SSH.NET remains at 2026.0.0 (already changed).

## Research
The project targets net10.0, references Web, and uses project-level PackageReference versions with no Directory.Packages.props. The quick API assessment reports no SSH.NET or NuGet.Packaging breaks and four removed NuGet.Protocol members; a source search found no matching usages.

## Steps
1. Update the NuGet.Protocol and NuGet.Packaging references to 7.9.0.
2. Verify SSH.NET remains at 2026.0.0.
3. Restore/build the project and audit the resolved package versions and scoped SSH.NET advisory.

**Done when**: The project builds successfully with the assessed versions and the SSH.NET advisories remain cleared.
