# Changelog

## Unreleased version

### Bug Fixes

* Remove reference on System.Text.Json for `net8.0` target framework
  ([#136](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/136))
* Replace dependency on `OpenTelemetry.Instrumentation.MySqlData`
  with `MySql.Data.OpenTelemetry`
  ([#146](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/146))
* Use 1.0.0-rc.18 of OpenTelemetry.Instrumentation.Wcf
  ([#146](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/146))

## 1.2.0

### BREAKING CHANGES

* Drop dependency on MySQL.Data.OpenTelemetry
  ([#131](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/131))
* Drop support for .NET 6. EOL was November 12 2024
  ([#131](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/131))

### New features

* Use 8.0.1 of Microsoft.Extensions.Logging
  ([#128](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/128))
* Use 1.10.0-beta.1 of OpenTelemetry.Instrumentation.AWS
  ([#127](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/127))
* Use 1.10.0-beta.1 of OpenTelemetry.Instrumentation.AWSLambda
  ([#127](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/127))
* Use 0.5.0-beta.7 of OpenTelemetry.Instrumentation.Process
  ([#128](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/128))
* Use 0.1.0-beta.3 of OpenTelemetry.Resources.Process
  ([#128](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/128))

## 1.1.0

### Bug Fixes

* Upgrade reference to System.Text.Json to version 8.0.5, resolving
  [CVE-2024-43485](https://github.com/advisories/GHSA-8g4q-xg66-9fp4)
  ([#123](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/123))

## 1.0.1

This is the first GA release of this distribution.

### BREAKING CHANGES

* Removes AWS and Azure resource detectors.
  ([#114](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/114))
* Drops supports for .NET 7. EOL was May 24 2024
  ([#116](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/116))
* Separates resource detectors and instrumentations. Removes resource detector
  names from `Grafana.OpenTelemetry.Instrumentation` enumeration. Adds new
  `Grafana.OpenTelemetry.ResourceDetectors` enumeration.
  ([#111](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/111))
  ([#119](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/119))

### Bug Fixes

* Remove workaround for stable `service.instance.id` across signals
  ([#108](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/108))
* Use 0.1.0-beta.3 of OpenTelemetry.Resources.Host
  * Fix the bug where macOS was detected as Linux
  ([#1985](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1985))

### New features

* Adds Operating System resource detector.
  ([#113](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/113))
  ([#121](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/121))
* Add new environment variable `GRAFANA_DOTNET_DISABLE_RESOURCE_DETECTORS`.
  Setting this will cause the provided resource detectors to be disabled.
  ([#111](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/111))
  ([#118](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/118))
  ([#119](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/119))
* Add new environment variable `GRAFANA_DOTNET_RESOURCE_DETECTORS`. Setting this
  will cause only the provided resource detectors to be enabled.
  ([#111](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/111))
  ([#118](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/118))
  ([#119](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/119))

## 0.9.0-beta.1

### BREAKING CHANGES

* Use 0.5.0-beta.6 of OpenTelemetry.Instrumentation.Process
  * **Breaking Change**: Renamed package from `OpenTelemetry.ResourceDetectors.Process`
    to `OpenTelemetry.Resources.Process`.
    ([#1717](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1717))
  * **Breaking Change**: `ProcessDetector` type is now internal, use `ResourceBuilder`
    extension method `AddProcessDetector` to enable the detector.
    ([#1717](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1717))
* Use 1.5.0-beta.1 of OpenTelemetry.Resources.AWS
  * **Breaking Change**: Renamed package from `OpenTelemetry.ResourceDetectors.AWS`
    to `OpenTelemetry.Resources.AWS`.
    ([#1839](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1839))
  * **Breaking Change**: `AWSEBSResourceDetector`, `AWSEC2ResourceDetector`,
  `AWSECSResourceDetector` and `AWSEKSResourceDetector` types are now internal,
  use `ResourceBuilder` extension methods `AddAWSEBSDetector`,
  `AddAWSEC2Detector`, `AddAWSECSDetector`
  and `AddAWSEKSDetector` respectively to enable the detectors.
    ([#1839](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1839))
  * **Breaking Change**: Renamed EventSource
  from `OpenTelemetry-ResourceDetectors-AWS`
  to `OpenTelemetry-Resources-AWS`.
    ([#1839](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1839))
* Use 1.0.0-beta.8 of OpenTelemetry.Resources.Azure
  * **Breaking Change**: Renamed method from `AddAppServiceDetector`
    to `AddAzureAppServiceDetector`.
    ([#1883](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1883))
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
  * **Breaking Change**: Renamed package from `OpenTelemetry.ResourceDetectors.Azure`
  to `OpenTelemetry.Resources.Azure`.
  ([#1840](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1840))
  * **Breaking Change**: `AppServiceResourceDetector` type is now internal, use `ResourceBuilder`
    extension method `AddAppServiceDetector` to enable the detector.
    ([#1840](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1840))
  * **Breaking Change**: `AzureVMResourceDetector` type is now internal, use `ResourceBuilder`
    extension method `AddAzureVMResourceDetector` to enable the detector.
    ([#1840](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1840))
  * **Breaking Change**: `AzureContainerAppsResourceDetector` type is now
    internal, use `ResourceBuilder` extension method `AddAzureContainerAppsResourceDetector`
    to enable the detector.
    ([#1840](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1840))
* Use 1.0.0-beta.9 of OpenTelemetry.Resources.Container
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
  * **Breaking Change**: Renamed package from `OpenTelemetry.ResourceDetectors.Container`
    to `OpenTelemetry.Resources.Container`.
    ([#1849](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1849))
  * **Breaking Change**: `ContainerResourceDetector` type is now internal,
  use `ResourceBuilder` extension method `AddContainerDetector`
  to enable the detector.
    ([#1849](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1849))
  * **Breaking Change**: Renamed EventSource
  from `OpenTelemetry-ResourceDetectors-Container`
  to `OpenTelemetry-Resources-Container`.
    ([#1849](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1849))
* Use 0.1.0-beta.2 of OpenTelemetry.Resources.Host
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
  * **Breaking Change**: Renamed package from `OpenTelemetry.ResourceDetectors.Host`
    to `OpenTelemetry.Resources.Host`.
    ([#1820](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1820))
  * **Breaking Change**: `HostDetector` type is now internal, use `ResourceBuilder`
    extension method `AddHostDetector` to enable the detector.
    ([#1820](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1820))
* Use 0.1.0-beta.2 of OpenTelemetry.Resources.Process
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
  * **Breaking Change**: Renamed package from `OpenTelemetry.ResourceDetectors.Process`
  to `OpenTelemetry.Resources.Process`.
  ([#1717](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1717))
  * **Breaking Change**: `ProcessDetector` type is now internal, use `ResourceBuilder`
  extension method `AddProcessDetector` to enable the detector.
  ([#1717](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1717))
* Use 0.1.0-beta.2 of OpenTelemetry.Resources.ProcessRuntime
  * **Breaking Change**: Renamed package from `OpenTelemetry.ResourceDetectors.ProcessRuntime`
    to `OpenTelemetry.Resources.ProcessRuntime`.
    ([#1767](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1767))
  * **Breaking Change**: `ProcessRuntimeDetector` type is now internal, use `ResourceBuilder`
    extension method `AddProcessRuntimeDetector` to enable the detector.
    ([#1767](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1767))

### Bug Fixes

* Use 1.9.0 of OpenTelemetry
  * Fixed a race condition for the experimental MetricPoint reclaim scenario
    (enabled via `OTEL_DOTNET_EXPERIMENTAL_METRICS_RECLAIM_UNUSED_METRIC_POINTS`)
    which could have led to a measurement being dropped.
    ([#5546](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5546))
  * Fixed the nullable annotations for the `SamplingResult` constructors
    to allow `null` being supplied as `attributes` or `traceStateString`
    which has always been supported.
    ([#5614](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5614))
* Use 1.0.0-rc.6 of OpenTelemetry.Instrumentation.Owin
  * Massive memory leak in OwinInstrumentationMetrics addressed.
    Made both Meter and Histogram singletons.
    ([#1655](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1655))
* Use 1.9.0-beta.1 of OpenTelemetry.Instrumentation.StackExchangeRedis
  * Update `StackExchange.Redis` version to `2.6.122`, resolving warnings about
    [CVE-2021-24112](https://github.com/advisories/GHSA-rxg9-xrhp-64gj).
    ([#1961](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1961))

### New features

* Use 1.9.0 of OpenTelemetry.Exporter.OpenTelemetryProtocol
  * `User-Agent` header format changed from
    `OTel-OTLP-Exporter-Dotnet/{NuGet Package Version}+{Commit Hash}`
    to `OTel-OTLP-Exporter-Dotnet/{NuGet Package Version}`.
    ([#5528](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5528))
  * Implementation of [OTLP
    specification](https://github.com/open-telemetry/opentelemetry-proto/blob/v1.2.0/opentelemetry/proto/trace/v1/trace.proto#L112-L133)
    for propagating `Span` and `SpanLink` flags containing W3C trace flags and
    `parent_is_remote` information.
    ([#5563](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5563))
* Use 1.9.0 of OpenTelemetry.Extensions.Hosting
* Use 1.9.0-beta.1 of OpenTelemetry.Instrumentation.AspNet
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
* Use 1.9.0 of OpenTelemetry.Instrumentation.AspNetCore
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
* Use 1.1.0-beta.4 of OpenTelemetry.Instrumentation.AWS
* Use 1.3.0-beta.1 of OpenTelemetry.Instrumentation.AWSLambda
* Use 1.0.0-beta.12 of OpenTelemetry.Instrumentation.EntityFrameworkCore
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
* Use 1.9.0-beta.1 of OpenTelemetry.Instrumentation.GrpcNetClient
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
* Use 1.6.0-beta.1 of OpenTelemetry.Instrumentation.Hangfire
* Use 1.9.0 of OpenTelemetry.Instrumentation.Http
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
* Use 0.5.0-beta.6 of OpenTelemetry.Instrumentation.Process
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
* Use 1.0.0-beta.3 of OpenTelemetry.Instrumentation.Quartz
* Use 1.9.0 of OpenTelemetry.Instrumentation.Runtime
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
* Use 1.9.0-beta.1 of OpenTelemetry.Instrumentation.StackExchangeRedis
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
  * Add support for instrumenting `IConnectionMultiplexer`
    which is added with service key.
    ([#1885](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1885))
* Use 1.9.0-beta.1 of OpenTelemetry.Instrumentation.SqlClient
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
* Use 1.0.0-rc.17 of OpenTelemetry.Instrumentation.Wcf
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
* Use 1.5.0-beta.1 of OpenTelemetry.Resources.AWS
  * Implement support for cloud.{account.id,availability_zone,region} attributes
    in AWS ECS detector.
    ([#1552](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1552))
  * Implement support for `cloud.resource_id` attribute in AWS ECS detector.
    ([#1576](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1576))
  * Update OpenTelemetry SDK version to `1.8.1`.
    ([#1668](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1668))
* Use 1.0.0-beta.9 of OpenTelemetry.Resources.Container
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
* Use 0.1.0-beta.2 of OpenTelemetry.Resources.Host
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
  * Adds support for `host.id` resource attribute on non-containerized systems.
  `host.id` will be set per [semantic convention rules](https://github.com/open-telemetry/semantic-conventions/blob/v1.24.0/docs/resource/host.md)
    ([#1631](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1631))
* Use 0.1.0-beta.2 of OpenTelemetry.Resources.Process
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))
  * Updated OpenTelemetry core component version(s) to `1.9.0`.
    ([#1888](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1888))

## 0.8.2-beta.1

### Bug Fixes

* Use 8.0.4 of System.Text.Json to remediate CVE-2024-30105
  ([#102](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/102))

## 0.8.1-beta.1

### BREAKING CHANGES

* Use 1.8.1 of OpenTelemetry.Instrumentation.Http
  * **Breaking Change**: Fixed tracing instrumentation so that by default any
  values detected in the query string component of requests are replaced with
  the text `Redacted` when building the `url.full` tag. For example,
  `?key1=value1&key2=value2` becomes `?key1=Redacted&key2=Redacted`. You can
  disable this redaction by setting the environment variable
  `OTEL_DOTNET_EXPERIMENTAL_HTTPCLIENT_DISABLE_URL_QUERY_REDACTION` to `true`.
  ([#5532](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5532))

* Use 1.8.1 of OpenTelemetry.Instrumentation.AspNetCore
  * **Breaking Change**: Fixed tracing instrumentation so that by default any
  values detected in the query string component of requests are replaced with
  the text `Redacted` when building the `url.full` tag. For example,
  `?key1=value1&key2=value2` becomes `?key1=Redacted&key2=Redacted`. You can
  disable this redaction by setting the environment variable
  `OTEL_DOTNET_EXPERIMENTAL_HTTPCLIENT_DISABLE_URL_QUERY_REDACTION` to `true`.
  ([#5532](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5532))

### Bug Fixes

* Use 1.8.1 of OpenTelemetry
  * Fixed an issue in Logging where unwanted objects (processors, exporters, etc.)
  could be created inside delegates automatically executed by the Options API
  during configuration reload.
  ([#5514](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5514))
  * Include changes from the following versions:
    * [1.8.0](https://github.com/open-telemetry/opentelemetry-dotnet/releases/tag/core-1.8.0)

* Use 1.8.1 of OpenTelemetry.Exporter.OpenTelemetryProtocol
  * Fix native AoT warnings in `OpenTelemetry.Exporter.OpenTelemetryProtocol`.
  ([#5520](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5520))

* Use 1.8.1 of OpenTelemetry.Instrumentation.AspNetCore
  * Includes fixes from [1.8.0](https://github.com/open-telemetry/opentelemetry-dotnet/releases/tag/Instrumentation.AspNetCore-1.8.0)

* Use 1.8.1 of OpenTelemetry.Instrumentation.Http
  * Includes fixes from [1.8.0](https://github.com/open-telemetry/opentelemetry-dotnet/releases/tag/Instrumentation.Http-1.8.0)

### New features

* Use 1.8.0 of OpenTelemetry.Exporter.Console
  * Added support for `ActivitySource.Version` property. ([#5472](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5472))

* Use 1.8.0-beta.1 of OpenTelemetry.Instrumentation.GrpcNetClient

* Use 1.8.0 of OpenTelemetry.Instrumentation.Runtime
  * `Meter.Version` is set to NuGet package version. ([#1624](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1624))
  * Update `OpenTelemetry.Api` to `1.8.0`. ([#1624](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1624))

* Use 1.8.0-beta.1 of OpenTelemetry.Instrumentation.SqlClient

* Use 0.5.0-beta.5 of OpenTelemetry.Instrumentation.Process
  * `Meter.Version` is set to NuGet package version. ([#1624](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1624))
  * Update `OpenTelemetry.Api` to `1.8.0`. ([#1624](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1624))

* Use 1.0.0-beta.6 of OpenTelemetry.ResourceDetectors.Azure
  * Update `OpenTelemetry.Api` to `1.8.0`. ([#1635](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1635))

* Use 0.1.0-alpha.3 of OpenTelemetry.ResourceDetectors.Host
  * Update `OpenTelemetry.Api` to `1.8.0`. ([#1635](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1635))

* Use 0.1.0-alpha.3 of OpenTelemetry.ResourceDetectors.ProcessRuntime
  * Update `OpenTelemetry.Api` to `1.8.0`. ([#1635](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1635))

## 0.7.0-beta.4

### Bug fixes

* Use 1.7.1 of ASP.NET Core instrumentation.
  * Fixed issue
  [#4466](https://github.com/open-telemetry/opentelemetry-dotnet/issues/4466)
  where the activity instance returned by `Activity.Current` was different than
  instance obtained from `IHttpActivityFeature.Activity`.
  ([#5136](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5136))

  * Fixed an issue where the `http.route` attribute was not set on either the
  `Activity` or `http.server.request.duration` metric generated from a
  request when an exception handling middleware is invoked. One caveat is that
  this fix does not address the problem for the `http.server.request.duration`
  metric when running ASP.NET Core 8. ASP.NET Core 8 contains an equivalent fix
  which should ship in version 8.0.2
  (see: [dotnet/aspnetcore#52652](https://github.com/dotnet/aspnetcore/pull/52652)).
  ([#5135](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5135))

  * Fixes scenario when the `net6.0` target of this library is loaded into a
  .NET 7+ process and the instrumentation does not behave as expected. This
  is an unusual scenario that does not affect users consuming this package
  normally. This fix is primarily to support the
  [opentelemetry-dotnet-instrumentation](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5252)
  project.
  ([#5252](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5252))

* Use 1.7.1 of HTTP instrumentation.
  * .NET Framework - fix description for `http.client.request.duration` metric.
  ([#5234](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5234))

### New features

* [#81](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/81)
  Adds a .NET 8 test project and integrates it into the OATS test matrix.
* [#85](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/85)
  Adds resource detectors for Azure, host, process, process runtime, and
  container resource attributes.
* [#87](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/87)
  Add a new `OtlpExporter` class supporting specifying OTLP protocol, endpoint,
  and header
* Use 1.7.1 of Runtime instrumentation.
  * Update `OpenTelemetry.Api` to `1.7.0`.
  ([#1486](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1486))

### Deprecation

* [#87](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/87)
  Deprecate use of `GRAFANA_CLOUD_*` environment variables in code and documentation

  The use of `CloudOtlpExporter` and `GRAFANA_CLOUD_*` environment variables is
  deprecated. Instead use `OtlpExporter` for code-level configuration, and
  `OTEL_EXPORTER_OTLP_PROTOCOL`, `OTEL_EXPORTER_OTLP_ENDPOINT`, and
  `OTEL_EXPORTER_OTLP_HEADERS` for configuration via environment variables.

## 0.7.0-beta.3

### Bug fixes

* Sets `OTEL_DOTNET_EXPERIMENTAL_ASPNETCORE_ENABLE_GRPC_INSTRUMENTATION`
  to `true` when enabling tracing. [gRPC instrumentation is experimental](https://github.com/open-telemetry/opentelemetry-dotnet/releases/tag/Instrumentation.AspNetCore-1.6.0),
  while HTTP is stable.

## 0.7.0-beta.2

### Bug fixes

* [#71](https://github.com/grafana/grafana-opentelemetry-dotnet/issues/71):
  Lazy-loading of ASP.NET Core instrumentation was broken. This was fixed by
  updateing changed class names of ASP.NET Core instrumentation library
  extension classes.

## 0.7.0-beta.1

### BREAKING CHANGES

* Use 1.7.0 of ASP.NET Core instrumentation.
  * Removes support for `OTEL_SEMCONV_STABILITY_OPT_IN`.
    [Instrumentation will only emit stable conventions](https://github.com/open-telemetry/semantic-conventions/tree/v1.23.0/docs/http).
  * Defaults `OTEL_DOTNET_EXPERIMENTAL_ASPNETCORE_ENABLE_GRPC_INSTRUMENTATION`
    to `true`. [gRPC instrumentation is experimental](https://github.com/open-telemetry/opentelemetry-dotnet/releases/tag/Instrumentation.AspNetCore-1.6.0),
    while HTTP is stable.
* Use 1.7.0 of HTTP instrumentation.
  * [`http.user_agent` Activity tag removed from HTTP instrumentation](https://github.com/open-telemetry/opentelemetry-dotnet/releases/tag/1.6.0-rc.1).
  * Removes support for `OTEL_SEMCONV_STABILITY_OPT_IN`.
    [Instrumentation will only emit stable conventions](https://github.com/open-telemetry/semantic-conventions/tree/v1.23.0/docs/http).

### New features

* [Use 1.7.0 of upstream SDK](https://github.com/open-telemetry/opentelemetry-dotnet/releases/tag/Instrumentation.AspNetCore-1.7.0).

## 0.6.0-beta.3

### Bug fixes

* [#65](https://github.com/grafana/grafana-opentelemetry-dotnet/issues/65):
  Using the distribution broke self-contained applications. This has been fixed
  by using a different way to determine the distribution version.
* [#64](https://github.com/grafana/grafana-opentelemetry-dotnet/issues/64): The
  version in `telemetry.distro.version` contained a trailing commit hash. This
  commit hash is removed now before populating the attribute.

## 0.6.0-beta.2

### New features

* Use 1.6.0-beta.3 of upstream instrumentation libraries.
* Allow specifying custom resource attributes via `GrafanaOpenTelemetrySettings`.
* Run unit tests on .NET 8.
* Use libraries released with .NET 8.
* Improve accuracy of resource attributes `telemetry.distro.name` and `telemetry.distro.version`.

### Bug fixes

* Make unit tests runnable for non-net462 targets.

## 0.6.0-beta.1

Released 2023-11-09

* Initial public preview release
