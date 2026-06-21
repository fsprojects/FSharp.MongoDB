# Contributing

Thanks for contributing to FSharp.MongoDB.

## Build And Basic Expectations

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

Basic expectations for contributions:

- The goal of this library is to ensure idiomatic F# types are handled correctly by the MongoDB C# driver.
- The project should preserve compatibility with the C# driver rather than introduce F#-specific behavior for its own sake.
- Keep changes focused and scoped to the problem being solved.
- It is recommended to discuss proposed work in an issue before sending a pull request directly.
- Update documentation when behavior, developer workflow, or public usage changes.
- Keep the repository building cleanly before opening a pull request.

## Tests

New features must include tests that cover the added behavior.

When a change introduces new functionality:

- Add or update automated tests for the new behavior.
- Extend regression coverage when fixing bugs.
- Keep C# and F# serialization behavior aligned when the change affects BSON shape or conventions.

## Breaking Changes

Breaking changes must be discussed in an issue before implementation.

Open or reference an issue first when a proposal would:

- Change public APIs
- Change serialization formats or conventions
- Change supported target frameworks or dependency expectations

This keeps design decisions visible before code and release work starts.
