# Changelog

All notable changes to FSharp.MongoDB are documented in this file.

## [Unreleased]

- Updated the test harness projects to target .NET 10 so local and CI test execution stays aligned with the repository SDK baseline.
- Fixed F# map support for `BsonDictionaryOptions`, including `ArrayOfDocuments` coverage.
- Added `Map<int, _>` and `Map<Guid, _>` regression coverage and matched those paths in the C#/F# isomorphic BSON tests.

## [0.5.0-beta]


- Documented the release management flow in the README, including stable and `-beta` release preparation.
- Added GitHub test result publishing across PR, branch, and tag workflows, including Actions job summaries and PR comments.
- Updated the repository and GitHub Actions workflows to use the .NET SDK `10.0.301` baseline.
- Restored the isomorphic serialization test coverage to guard C# and F# BSON parity again.

**Full Changelog**: https://github.com/fsprojects/FSharp.MongoDB/compare/0.4.0-beta...0.5.0-beta

## [0.4.0-beta]

- Refreshed the README to reflect the current package usage and project status.

**Full Changelog**: https://github.com/fsprojects/FSharp.MongoDB/compare/0.3.0-beta...0.4.0-beta

## [0.3.0-beta]

- Updated the supported .NET and MongoDB dependency versions.
- Fixed option serialization behavior.

**Full Changelog**: https://github.com/fsprojects/FSharp.MongoDB/compare/0.2.0-beta...0.3.0-beta

## [0.2.0-beta]

- Migrated repository URLs to the `fsprojects` organization.
- Refined package metadata and description.

**Full Changelog**: https://github.com/fsprojects/FSharp.MongoDB/compare/0.1.0-beta...0.2.0-beta

## [0.1.0-beta]

- Migrated the library to .NET Core and added GitHub Actions CI.
- Added `voption` support, including default value handling for `ValueOption`.
- Ensured C# and F# produce isomorphic BSON and tightened package metadata, docs, and release automation.

**Full Changelog**: https://github.com/fsprojects/FSharp.MongoDB/compare/r0.0.1...0.1.0-beta

## [0.0.1]

- Added support for serializing and deserializing `internal` F# types.
- Changed `MongoCollection<'Document>` to return query results as `AsyncSeq` instances.
- Added helpers to `MongoClient` for executing the `dropDatabase` and `listDatabases` commands.
- Added helpers to `MongoDatabase` for executing the `create`, `drop`, `listCollections`, and `renameCollection` commands.
- Added helpers to `MongoCollection<'Document>` for executing the `aggregate`, `count`, and `distinct` commands.
- Removed support for executing inserts, updates, and deletes against a collection.
- Removed support for executing queries and updates using a `mongo { ... }` computation expression.
- Removed support for specifying queries and updates as code quotations converted to `BsonDocument` values.

**Full Changelog**: https://github.com/fsprojects/FSharp.MongoDB/compare/r0.0.0...r0.0.1

## [0.0.0]

- Added support for serializing and deserializing F# lists, maps, options, records, sets, and discriminated unions.
- Added support for executing basic CRUD operations against a collection.
- Added support for executing queries and updates using a `mongo { ... }` computation expression.
- Added support for specifying queries and updates as code quotations, both type-checked and unchecked, that convert to `BsonDocument` values.
