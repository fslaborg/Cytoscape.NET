# Cyjs.NET

[![Discord](https://img.shields.io/discord/836161044501889064?color=purple&label=Join%20our%20Discord%21&logo=discord&logoColor=white)](https://discord.gg/qbN5v8WvfR)

.NET interface for Cytoscape.js written in F#

## Development

_Note:_ The `release` and `prerelease` build targets assume that there is a `NUGET_KEY` environment variable that contains a valid Nuget.org API key.

### build

Check the [build project](https://github.com/fslaborg/Cyjs.NET/blob/main/build) to take a look at the  build targets. Here are some examples:

```shell
# Windows

# Build only
./build.cmd

# Full release buildchain: build, test, pack, build the docs, push a git tag, publish thze nuget package, release the docs
./build.cmd release

# The same for prerelease versions:
./build.cmd prerelease


# Linux/mac

# Build only
build.sh

# Full release buildchain: build, test, pack, build the docs, push a git tag, publísh the nuget package, release the docs
build.sh release

# The same for prerelease versions:
build.sh prerelease

```