# Release Process

This project relies on manual steps to publish the libraries in this repo as
NuGet packages. Package versioning is determined by git tags, and implemented
using [the MinVer package](https://github.com/adamralph/minver). This package
is also used by the OpenTelemetry .NET SDK.

MinVer finds the latest tag in the repo commit history and uses the tag as
the version when building the libaries and packages.

NuGet treats any package version containing `-alpha`, `-beta`, or `-rc` as a
*pre-release* package.

To publish to the Grafana Labs NuGet organization, you must be added as a
member by an existing organization administrator.

## Publication Steps

1. Determine the version to be released, set a tag on `main` via git:
    * `git checkout main`
    * `git tag VERSION` where `VERSION` is an appropriate SemVer version
2. Build both packages:
    * `dotnet build ./src/Grafana.OpenTelemetry.Base --configuration Release
-p:Deterministic=true`
    * `dotnet build ./src/Grafana.OpenTelemetry --configuration Release
-p:Deterministic=true`
3. [Open the NuGet package upload page](
https://www.nuget.org/packages/manage/upload)
4. Upload the two packages, checking that fields are populated as expected.
The packages will be located at:
    * `./src/Grafana.OpenTelemetry.Base/bin/Release/
Grafana.OpenTelemetry.Base.VERSION.nupkg`
    * `./src/Grafana.OpenTelemetry/bin/Release/
Grafana.OpenTelemetry.VERSION.nupkg`
