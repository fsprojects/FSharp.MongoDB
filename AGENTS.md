# AGENTS.md

## Contribution

Build and test from the repository root using the solution file `FSharp.MongoDB.sln`.

Common commands:

```sh
make build
make test
```

Equivalent `dotnet` commands:

```sh
dotnet build FSharp.MongoDB.sln -c Debug
dotnet test FSharp.MongoDB.sln -c Debug --logger "trx" --results-directory TestResults
```

Useful supporting commands:

```sh
make clean
make upgrade
```

## Release Management

Prepare a release locally with:

```sh
make release-prepare version=0.5.0
```

Prepare a pre-release with a suffix, for example:

```sh
make release-prepare version=0.5.0-beta
```

This flow validates the changelog, moves the current `Unreleased` notes into a new versioned section, creates the release commit, and creates the corresponding git tag.

After that, push the release commit and tag:

```sh
git push origin main --follow-tags
```

Once the tag is pushed, GitHub Actions builds and validates the release, creates the NuGet package, drafts the GitHub release from the changelog entry, and publishes to NuGet when the GitHub release is published.

## Branch Types

Use these branch prefixes:

- `feature/xxx`: new user-facing functionality or enhancements
- `fix/xxx`: bug fixes and regressions
- `chore/xxx`: maintenance work, tooling, dependency updates, CI, or documentation
- `release/xxx`: release preparation and release-specific changes

## Pull Request Naming

Name pull requests with a conventional prefix matching the work:

- `feat: xxx`
- `fix: xxx`
- `chore: xxx`

Use the same intent in the PR title as in the branch name when possible.
