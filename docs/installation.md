# Installing the Grafana OpenTelemetry distribution for .NET

## Supported .NET Versions

The packages shipped from this repository generally support all the officially
supported versions of [.NET](https://dotnet.microsoft.com/download/dotnet) and
[.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework) (an
older Windows-based .NET implementation), except `.NET Framework 3.5`.

Some instrumentations and instrumentation libraries referenced by the
distribution don't support both .NET and .NET Framework, but only one of them.
For details, refer to the list of [supported instrumentations](./supported-instrumentations.md).

## Install the full package with all available instrumentations

For installing the distribution with the full set of dependencies, add a
reference to the [`Grafana.OpenTelemetry`](https://www.nuget.org/packages/Grafana.OpenTelemetry)
package to your project.

```sh
dotnet add package --prerelease Grafana.OpenTelemetry
```

The list of [supported instrumentations](./supported-instrumentations.md)
specifies what instrumentations are included in the full package.

## Install the base package

For installing the distribution with a minimal set of dependencies, add a
reference to the [`Grafana.OpenTelemetry.Base`](https://www.nuget.org/packages/Grafana.OpenTelemetry.Base)
package to your project.

```sh
dotnet add package --prerelease Grafana.OpenTelemetry.Base
```

The list of [supported instrumentations](./supported-instrumentations.md)
specifies what instrumentations are included in the base package.
