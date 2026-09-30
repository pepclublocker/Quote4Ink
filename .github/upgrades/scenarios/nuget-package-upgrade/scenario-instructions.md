# NuGet Package Upgrade

## Strategy
Upgrade the affected package dependency chains to assessment-recommended stable versions across the solution, validating restore, build, tests, and vulnerability audit.

## Preferences
- **Flow Mode**: Automatic
- **Commit Strategy**: After Each Task
- **Scope**: Entire solution (`Quote4Ink.slnx`)
- **Packages**: `SSH.NET` 2026.0.0; `NuGet.Protocol` 7.9.0; `NuGet.Packaging` 7.9.0; and `SQLitePCLRaw.bundle_e_sqlite3` 3.0.5 to remediate the transitive `SQLitePCLRaw.lib.e_sqlite3` advisory.
- **Reason**: Resolve the reported SSH.NET, SQLitePCLRaw, and NuGet.Protocol security advisories.

## Decisions
- User expanded the original SSH.NET-only scope to include the SQLitePCLRaw and NuGet.Protocol advisories across the solution.
- Use project-level package versions; no `Directory.Packages.props` was found.
- Update `NuGet.Packaging` alongside `NuGet.Protocol` to keep their package dependency family aligned.
- Add `SQLitePCLRaw.bundle_e_sqlite3` 3.0.5 in `Web` to update its transitive `SQLitePCLRaw.lib.e_sqlite3` dependency.
- The quick assessment found four removed NuGet.Protocol members but no source matches; validate with solution builds and a vulnerability audit.
- User approved finalizing the original SSH.NET fix as complete while continuing the newly added advisory tasks.
- User approved replacing the stale, unmodified Web task entry with a fresh task entry and continuing the package updates.

## Source Control
- **Source Branch**: `master`
- **Working Branch**: `nuget-package-upgrade`
- **Pending Changes**: Stashed before creating the working branch; restore after the package work is complete.
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)
