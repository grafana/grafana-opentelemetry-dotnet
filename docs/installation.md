# Installing the Grafana OpenTelemetry distribution for .NET

* [Supported .NET Versions](#supported-net-versions)
* [Install the full package with all available instrumentations](#install-the-full-package-with-all-available-instrumentations)
* [Install the base package](#install-the-base-package)
* [Minimizing unneeded dependencies](#minimizing-unneeded-dependencies)

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

The list of [supported instrumentations](./supported-instrumentations.md) and
[supported resource detectors](./supported-resource-detectors.md)
specify which are included in the base package and enabled by default.

## Minimizing unneeded dependencies

Users might utilize some instrumentation libraries the [full package](#install-the-full-package-with-all-available-instrumentations)
contains, while other contained libraries will not be needed. However, the
[full package](#install-the-full-package-with-all-available-instrumentations)
still pulls in those unneeded instrumentations libraries with their
dependencies.

To mitigate this situation, [base package](#install-the-base-package)
with a built-in lazy-loading mechanism can be used. This mechanism will
initialize known available instrumentation library or resource detectors
assembly, regardless of whether it's added as dependency of the [full package](#install-the-full-package-with-all-available-instrumentations)
or as part of the instrumented project.

For example, if it is desired to use the `AspNetCore` instrumentation without
pulling in any other dependencies from the [full package](#install-the-full-package-with-all-available-instrumentations),
it suffices to install the `AspNetCore` instrumentation library along with the
base package.

```sh
dotnet add package --prerelease Grafana.OpenTelemetry.Base
dotnet add package --prerelease OpenTelemetry.Instrumentation.AspNetCore
```

Then, the `AspNetCore` instrumentation will be lazy-loaded during the
invocation of the `UseGrafana` extension method, no further code changes are
necessary.

```csharp
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .UseGrafana()
    .Build();
```

This way, any other [instrumentation library](./supported-instrumentations.md)
or [resource detector](./supported-resource-detectors.md) supported by the
distribution can be added via lazy loading.
