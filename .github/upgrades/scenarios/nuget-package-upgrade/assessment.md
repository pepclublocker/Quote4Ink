# NuGet package upgrade assessment

_Mode: **quick assessment** — package API diffs only; no per-project source scan was run._

## Recommended versions

- **NuGet.Packaging**: **7.9.0** (unified across 2 project(s)).
- **NuGet.Protocol**: **7.9.0** (unified across 1 project(s)).
- **SSH.NET**: **2026.0.0** (unified across 1 project(s)).

## Public API changes

> **Types moved (namespace changed) are not removals.** A moved type keeps its name and members;
> the fix is a `using`-directive change, not a rewrite. Do not treat a moved type as deleted.

- **NuGet.Packaging**: no source-breaking public API changes detected — see [`apidiff/NuGet.Packaging.apidiff.md`](apidiff/NuGet.Packaging.apidiff.md).
- **NuGet.Protocol**: 4 member(s) removed — see [`apidiff/NuGet.Protocol.apidiff.md`](apidiff/NuGet.Protocol.apidiff.md).

## Breaking-change findings

- Version divergence findings (Pkg.0003): 0
- Requested-version-unsupported findings (Pkg.0002): 0

- Quick mode does not scan source, so there are no per-line `PkgApi` usage findings. Review the
  per-package API diffs above and rely on build errors during execution to pinpoint affected code.
- A full code scan can locate the exact source location of every breaking-change usage across the repo.
  It is opt-in and slower — re-run the assessment with `fullScan=true` only if the user requests it.

## Next steps

1. Proceed to planning to triage the API changes above and plan the code fixes (if any).

