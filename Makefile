.PHONY: build test verify-changelog release-prepare clean nuget publish-all upgrade

config ?= Debug
version ?= 0.0.0
dryrun ?= false
solution ?= FSharp.MongoDB.sln
project ?= src/FSharp.MongoDB.Bson/FSharp.MongoDB.Bson.fsproj
results ?= TestResults

build:
	dotnet build $(solution) -c $(config)

test:
	dotnet test $(solution) -c $(config) --logger "trx" --results-directory $(results)

verify-changelog:
	REQUIRE_CHANGELOG_ALWAYS=true .github/scripts/check-unreleased-changelog.sh

release-prepare:
	./.github/scripts/release.sh "$(version)" "$(dryrun)"

nuget:
	dotnet pack $(project) -c $(config) -p:Version=$(version) -o $(PWD)/.out

publish-all: build test nuget

clean:
	dotnet clean $(solution) -c $(config)
	find . -type d \( -name bin -o -name obj \) -exec rm -rf {} +

upgrade:
	dotnet restore $(solution) --force-evaluate
