# 01.03-solution-validation: Validate package advisories across the solution

## Objective
Verify all scoped package upgrades together across the solution after the Web and DataLoader changes.

## Scope
Validate `Quote4Ink.slnx`, both projects, and available test projects without expanding package scope beyond SSH.NET, NuGet.Protocol, NuGet.Packaging, and the SQLitePCLRaw bundle override.

## Steps
1. Run a full solution restore/build and resolve warnings or errors caused by the scoped updates.
2. Run available tests.
3. Audit direct and transitive packages and confirm the reported SSH.NET, SQLitePCLRaw.lib.e_sqlite3, and NuGet.Protocol advisories are cleared.

**Done when**: The solution build and available tests pass, target versions resolve as planned, and no scoped advisory appears in the audit.
