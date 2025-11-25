# Changelog

## Unreleased version

### BREAKING CHANGES

* Add `net10.0` target.
  ([#219](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/219))
* Use 1.14.0 of OpenTelemetry ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * When targeting net8.0, the package now depends on version 8.0.0 of the
    Microsoft.Extensions.DependencyInjection.Abstractions,
    Microsoft.Extensions.Diagnostics.Abstractions and
    Microsoft.Extensions.Logging.Configuration NuGet packages.
    ([#6667](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6327))
  * Update Microsoft.Extensions.* dependencies to 10.0.0 for .NET Framework and
    .NET Standard.
    ([#6667](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6667))
* Use 1.14.0-rc.1 of OpenTelemetry.Instrumentation.AspNet ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Renamed `MeterProviderBuilderExtensions` and
    `TracerProviderBuilderExtensions` to
    `AspNetInstrumentationMeterProviderBuilderExtensions`
    and `AspNetInstrumentationTracerProviderBuilderExtensions` respectively.
    ([#2910](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2910))
  * Made metrics generation independent from traces.
    Tracing must no longer be enabled to calculate metrics. A compatible version
    of `OpenTelemetry.Instrumentation.AspNet.TelemetryHttpModule` is required.
    ([#2970](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2970))
  * Metrics related option renamed:
    * delegate `AspNetMetricsInstrumentationOptions.EnrichFunc` to
      `AspNetMetricsInstrumentationOptions.EnrichWithHttpContextAction`,
    * property `AspNetMetricsInstrumentationOptions.Enrich` to
      `AspNetMetricsInstrumentationOptions.EnrichWithHttpContext`.
    ([#3070](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3070))
  * Change in public API contract.
    All usages of `HttpRequest`, `HttpResponse` and `HttpContext` replaced by
    `HttpRequestBase`, `HttpResponseBase` and `HttpContextBase` respectively.
    ([#3110](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3110))
  * Updated OpenTelemetry core component version(s) to `1.14.0`.
    ([#3403](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3403))
* Use 1.14.0 of OpenTelemetry.Instrumentation.AspNetCore ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Updated OpenTelemetry core component version(s) to `1.14.0`.
    ([#3403](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3403))
* Use 1.14.0 of OpenTelemetry.Instrumentation.AWSLambda ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Update Amazon.Lambda.Core dependency from 2.2.0 to 2.8.0 which reworks the
    internal logic for determining the trace id created by AWS Lambda.
    ([#3410](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3410))
* Use 1.14.0-beta.2 of OpenTelemetry.Instrumentation.EntityFrameworkCore ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * The `SetDbStatementForStoredProcedure` and
    `SetDbStatementForText` properties have been removed. Behaviors related to this
    option are now always enabled.
    ([#3072](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3072))
  * `db.system.name` now only sets names that are explicitly
    defined in the Semantic Conventions for databases.
    ([#3075](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3075))
  * Updated OpenTelemetry core component version(s) to `1.14.0`.
    ([#3403](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3403))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.Hangfire ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Updated OpenTelemetry core component version(s) to `1.14.0`.
    ([#3403](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3403))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.Owin ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Updated OpenTelemetry core component version(s) to `1.14.0`.
    ([#3403](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3403))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.SqlClient ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * The `SetDbStatementForText` property has been removed.
    Behaviors related to this option are now always enabled.
    ([#3072](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3072))
  * The `Enrich`, `Filter` and `RecordException` properties
    have been removed for .NET Framework where they were non-functional.
    ([#3079](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3079))
  * The `Enrich` property has been renamed to
    `EnrichWithSqlCommand` and no longer passes an event name to the delegate.
    ([#3080](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3080))
  * Updated OpenTelemetry core component version(s) to `1.14.0`.
    ([#3403](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3403))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.StackExchangeRedis ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Introduce `RedisInstrumentationContext` and use it as context for `Filter`
    ([#2977](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2977))
  * Updated OpenTelemetry core component version(s) to `1.14.0`.
    ([#3403](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3403))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.Wcf ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Adjust to breaking changes from `OpenTelemetry.Instrumentation.AspNet` version
    `1.13.0-beta.1`. Fixing span hierarchy when hosted in ASP.NET.
    ([#3151](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3151))
  * Updated OpenTelemetry core component version(s) to `1.14.0`.
    ([#3403](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3403))
* Use 1.14.0-beta.1 of OpenTelemetry.Resources.Host ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Updated OpenTelemetry core component version(s) to `1.14.0`.
    ([#3403](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3403))

### New features

* Use 1.14.0 of OpenTelemetry ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Added a verification to ensure that a `MetricReader` can only be registered
    to a single `MeterProvider`, as required by the OpenTelemetry specification.
    ([#6458](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6458))
  * Added `FormatMessage` configuration option to self-diagnostics feature. When
    set to `true` (default is false), log messages will be formatted by replacing
    placeholders with actual parameter values for improved readability.
  * Add support for .NET 10.0.
    ([#6307](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6307))
* Use 1.14.0 of OpenTelemetry.Exporter.OpenTelemetryProtocol ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * If `EventName` is specified either through `ILogger` or the experimental
    log bridge API, it is exported as `EventName` by default instead of
    `logrecord.event.name` which was previously behind the
    `OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EVENT_LOG_ATTRIBUTES` feature flag.
    Note that exporting `logrecord.event.id` is still behind that same feature
    flag. ([#6306](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6306))
  * Add support for .NET 10.0.
    ([#6307](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6307))
* Use 1.14.0-rc.1 of OpenTelemetry.Instrumentation.AspNet ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Following attributes are available while sampling:
    * `http.request.method`,
    * `server.address`,
    * `server.port`,
    * `url.path`,
    * `url.query`,
    * `url.scheme`,
    * `user_agent.original`.
    ([#3151](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3151))
  * Replace static routing tokens with actual values in the route template.
    ([#3160](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3160))
* Use 1.14.0 of OpenTelemetry.Instrumentation.AspNetCore ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Added support for listening to ASP.NET Core Blazor activities.
    Configurable with the
    `AspNetCoreTraceInstrumentationOptions.EnableRazorComponentsSupport`
    option which defaults to `true`. Only applies to .NET 10.0 or greater.
    ([#3012](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3012))
  * Following metrics will now be enabled by default when targeting `.NET10.0` or
    newer framework:
    * **Meter** : `Microsoft.AspNetCore.MemoryPool`
      * `aspnetcore.memory_pool.pooled`
      * `aspnetcore.memory_pool.evicted`
      * `aspnetcore.memory_pool.allocated`
      * `aspnetcore.memory_pool.rented`
    * **Meter** : `Microsoft.AspNetCore.Authentication`
      * `aspnetcore.authentication.authenticate.duration`
      * `aspnetcore.authentication.challenges`
      * `aspnetcore.authentication.forbids`
      * `aspnetcore.authentication.sign_ins`
      * `aspnetcore.authentication.sign_outs`
    * **Meter** : `Microsoft.AspNetCore.Authorization`
      * `aspnetcore.authorization.attempts`
    * **Meter** : `Microsoft.AspNetCore.Identity`
      * `aspnetcore.identity.user.create.duration`
      * `aspnetcore.identity.user.update.duration`
      * `aspnetcore.identity.user.delete.duration`
      * `aspnetcore.identity.user.check_password_attempts`
      * `aspnetcore.identity.user.generated_tokens`
      * `aspnetcore.identity.user.verify_token_attempts`
      * `aspnetcore.identity.sign_in.authenticate.duration`
      * `aspnetcore.identity.sign_in.check_password_attempts`
      * `aspnetcore.identity.sign_in.sign_ins`
      * `aspnetcore.identity.sign_in.sign_outs`
      * `aspnetcore.identity.sign_in.two_factor_clients_remembered`
      * `aspnetcore.identity.sign_in.two_factor_clients_forgotten`
    * **Meter** : `Microsoft.AspNetCore.Components`
      * `aspnetcore.components.navigate`
      * `aspnetcore.components.handle_event.duration`
    * **Meter** : `Microsoft.AspNetCore.Components.Lifecycle`
      * `aspnetcore.components.update_parameters.duration`
      * `aspnetcore.components.render_diff.duration`
      * `aspnetcore.components.render_diff.size`
    * **Meter** : `Microsoft.AspNetCore.Components.Server.Circuits`
      * `aspnetcore.components.circuit.active`
      * `aspnetcore.components.circuit.connected`
      * `aspnetcore.components.circuit.duration`
    ([#3012](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3012))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet/pull/2822))
* Use 1.14.0 of OpenTelemetry.Instrumentation.AWS ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0 of OpenTelemetry.Instrumentation.AWSLambda ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.0.0-beta.4 of OpenTelemetry.Instrumentation.Cassandra ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.ElasticsearchClient ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0-beta.2 of OpenTelemetry.Instrumentation.EntityFrameworkCore ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Added support for detecting Snowflake for the `db.system`/`db.system.name` attributes
    when using `EFCore.Snowflake`.
    ([#2980](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2980))
  * Add the `server.port` resource attribute when following the new database semantic
    conventions when opted into using the `OTEL_SEMCONV_STABILITY_OPT_IN` environment
    variable.
    ([#3011](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3011))
  * Extend `db.system.name` values to identity additional providers related to Couchbase,
    DB2, MongoDB, MySQL, Oracle, PostgreSQL and SQLite.
    ([#3025](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3025))
  * Add `db.query.parameter.<key>` attribute(s) to query spans if opted into using
    the `OTEL_DOTNET_EXPERIMENTAL_EFCORE_ENABLE_TRACE_DB_QUERY_PARAMETERS`
    environment variable.
    ([#3015](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3015),
    [#3081](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3081))
  * Add the `db.query.summary` attribute and use it for the trace span name when
    opted into using the `OTEL_SEMCONV_STABILITY_OPT_IN` environment variable.
    ([#3022](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3022))
  * The `db.statement` and `db.query.text` attributes are now sanitized when using
    specific SQL-like EFCore providers.
    ([#3022](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3022))
  * Add `net8.0` and `net10.0` targets.
    ([#3519](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3519))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.GrpcNetClient ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0 of OpenTelemetry.Extensions.Hosting ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0 of OpenTelemetry.Instrumentation.Http ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0-beta.1 of OpenTelemetry.Resources.OperatingSystem ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0-beta.2 of OpenTelemetry.Instrumentation.Process ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
  * Add `net8.0` and `net10.0` targets.
    ([#3519](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3519))
* Use 1.14.0-beta.1 of OpenTelemetry.Resources.Process ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add `net8.0` and `net10.0` targets.
    ([#3519](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3519))
* Use 1.14.0-beta.1 of OpenTelemetry.Resources.ProcessRuntime ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add `net8.0` and `net10.0` targets.
    ([#3519](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3519))
* Use 1.14.0-beta.2 of OpenTelemetry.Instrumentation.Quartz ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0 of OpenTelemetry.Instrumentation.Runtime ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.SqlClient ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add `db.query.parameter.<key>` attribute(s) to query spans if opted into using
    the `OTEL_DOTNET_EXPERIMENTAL_SQLCLIENT_ENABLE_TRACE_DB_QUERY_PARAMETERS`
    environment variable. Not supported on .NET Framework.
    ([#3015](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3015),
    [#3081](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3081))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.StackExchangeRedis ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Removed the `db.redis.flags` attribute from the implementation
    as it is not part of the Semantic Conventions for Database Client Calls.
    ([#2982](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2982))
  * The new database semantic conventions can be opted in to by setting
    the `OTEL_SEMCONV_STABILITY_OPT_IN` environment variable. This allows for a
    transition period for users to experiment with the new semantic conventions
    and adapt as necessary. The environment variable supports the following
    values:
    * `database` - emit the new, frozen (proposed for stable) database
      attributes, and stop emitting the old experimental database
      attributes that the instrumentation emitted previously.
    * `database/dup` - emit both the old and the frozen (proposed for stable) database
      attributes, allowing for a more seamless transition.
    * The default behavior (in the absence of one of these values) is to continue
      emitting the same database semantic conventions that were emitted in
      the previous version.
    * Note: this option will be removed after the new database
      semantic conventions are marked stable. At which time this
      instrumentation can receive a stable release, and the old database
      semantic conventions will no longer be supported. Refer to the
      specification for more information regarding the new database
      semantic conventions for
      [spans](https://github.com/open-telemetry/semantic-conventions/blob/v1.28.0/docs/database/database-spans.md).
    ([#3084](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3084))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0-beta.1 of OpenTelemetry.Resources.Container ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0-beta.1 of OpenTelemetry.Resources.Host ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Added support for the `host.arch` resource attribute in HostDetector for .NET
    only.
    ([#3147](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3147))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.Wcf ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Added server instrumentation support for `RecordException` option.
    ([#2880](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2880))
  * Add support for .NET 10.0.
    ([#2822](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2822))
* Add native AoT annotations.
  ([#358](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/358))

### Bug Fixes

* Use 1.14.0 of OpenTelemetry ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Fixed parsing of `OTEL_TRACES_SAMPLER_ARG` decimal values to always use `.`
    as the delimiter when using the `traceidratio` sampler, preventing
    locale-specific parsing issues.
    ([#6444](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6444))
  * Fixed an issue where the Base2 Exponential Bucket Histogram did not reset its
    scale to 20 after each collection cycle when using delta aggregation temporality.
    ([#6557](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6557))
* Use 1.14.0 of OpenTelemetry.Exporter.OpenTelemetryProtocol ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Fixed an issue in .NET Framework where OTLP export of traces, logs, and
    metrics using `OtlpExportProtocol.Grpc` did not correctly set the initial
    write position, resulting in gRPC protocol errors.
    ([#6280](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6280))
  * gRPC calls to export traces, logs, and metrics using `OtlpExportProtocol.Grpc`
    now set the `TE=trailers` HTTP request header to improve interoperability.
    ([#6449](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6449))
  * Improved performance exporting `byte[]` attributes as native binary format
    instead of arrays.
    ([#6534](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6534))
  * Changed histogram protobuf serialization to use packed format for
    `bucket_counts` and `explicit_bounds` to be specification-compliant and fix
    issues with strict OTLP parsers. Lenient parsers should handle both formats.
    ([#6567](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6567))
* Use 1.0.0-beta.4 of OpenTelemetry.Instrumentation.Cassandra ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Updated `CassandraCSharpDriver` to `3.17.0`.
    ([#2836](https://github.com/open-telemetry/opentelemetry-dotnet/pull/2836))
* Use 1.14.0-beta.2 of OpenTelemetry.Instrumentation.EntityFrameworkCore ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Fix diacritic identifier parsing hang and improve FROM clause parsing.
    ([#3368](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3368))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.Hangfire ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Fix `FailedToInjectActivityContext` when no `ActivityContext` exists.
    ([#2990](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2990))
* Use 1.14.0 of OpenTelemetry.Instrumentation.Http ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Do not change activity status set by EnrichWithHttpWebResponse on .NET Framework.
    ([#2988](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2988))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.SqlClient ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Fix activities not being stopped on .NET Framework when using a global activity
    listener.
    ([#3041](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3041))
  * Fix diacritic identifier parsing hang and improve FROM clause parsing.
    ([#3368](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3368))
* Use 1.14.0-beta.1 of OpenTelemetry.Instrumentation.Wcf ([#273](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/273))
  * Fix possible infinite recursion when WCF is hosted in ASP.NET.
    ([#3248](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/3248))
* Address some AoT warnings
  ([#356](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/356))

## 1.3.0

### BREAKING CHANGES

* Remove dependency on `OpenTelemetry.Instrumentation.MySqlData`.
  Add the [MySql.Data.OpenTelemetry](https://www.nuget.org/packages/MySql.Data.OpenTelemetry)
  package to your project if you require MySQL instrumentation. **NOTE**: This
  NuGet package is licensed under the GPL license which may not be suitable for
  all projects.
* Use 1.12.0 of OpenTelemetry.Exporter.OpenTelemetryProtocol ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Now defaults to exporting over OTLP/HTTP instead of OTLP/gRPC. This change
    could result in a failure to export telemetry unless appropriate measures
    are taken. Additionally, if you explicitly configure the exporter to use OTLP/gRPC
    it may result in a `NotSupportedException` without further configuration.
    Please review issue ([#6209](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6209))
    for additional information and workarounds. ([#6229](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6229))
  * Removed the peer service resolver, which was based on earlier experimental
    semantic conventions that are not part of the stable specification. ([#6005](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6005))
* Use 1.12.0 of OpenTelemetry.Instrumentation.AWS ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Change default Semantic Convention to 1.28.
  * Update AWSSDK dependencies to v4. ([#2720](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2720))
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.ElasticsearchClient ([#208](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/208))
  * Replace `db.url` attribute with `url.full` to comply with semantic conventions.
    Redact `username` and `password` part of `url.full`.
    ([#1684](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1684))
* Use 1.12.0-beta.2 of OpenTelemetry.Instrumentation.SqlClient ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * The `peer.service` and `server.socket.address` attributes are no longer emitted.
    Users should rely on the `server.address` attribute for the same information.
    Note that server.address is only included when the `EnableConnectionLevelAttributes`
    option is enabled. ([#2229](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2229))
  * When `EnableConnectionLevelAttributes` is enabled, the `server.port` attribute
    will now be written as an integer to be compliant with the semantic conventions.
    Previously, it was written as a string. ([#2233](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2233))
  * The `EnableConnectionLevelAttributes` option is now enabled by default. ([#2249](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2249))
  * The `SetDbStatementForStoredProcedure` option has been removed. ([#2284](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2284))
  * The `EnableConnectionLevelAttributes` option has been removed. ([#2414](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2414))
  * Enabling `SetDbStatementForText` will no longer capture the raw query text.
    The query is now sanitized. Literal values in the query text are replaced by
    a `?` character. ([#2446](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2446))

### New features

* Include XML documentation in the `Grafana.OpenTelemetry` and
  `Grafana.OpenTelemetry.Base` NuGet packages.
  [#295](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/295)
* Enable metrics for SQL Client instrumentation
  ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
* Use 1.12.0 of OpenTelemetry ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Promoted the MetricPoint reclaim feature for Delta aggregation temporality
    from experimental to stable. ([#5956](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5956))
  * `Meter.Tags` will now be considered when resolving the SDK metric to update
    when measurements are recorded. Meters with the same name and different tags
    will now lead to unique metrics. ([#5982](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5982))
* Use 1.12.0 of OpenTelemetry.Exporter.OpenTelemetryProtocol ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Added support for exporting instrumentation scope attributes from `ActivitySource.Tags`.
    ([#5897](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5897))
  * Removed dependency on `Google.Protobuf`, `Grpc` and `Grpc.Net.Client` packages.
    ([#6005](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6005))
  * Switched from using the `Google.Protobuf` library for serialization to a custom
    manual implementation of protobuf serialization. ([#6005](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6005))
* Use 1.12.0 of OpenTelemetry.Extensions.Hosting ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.AspNet ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Updated registration extension code to retrieve environment variables through
    `IConfiguration`. ([#1976](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1976))
  * The `http.server.request.duration` histogram (measured in seconds) produced
    by the metrics instrumentation in this package now uses the Advice API to set
    default explicit buckets following the OpenTelemetry Specification. ([#2430](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2430))
* Use 1.12.0 of OpenTelemetry.Instrumentation.AspNetCore ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * The `http.server.request.duration` histogram (measured in seconds) produced
    by the metrics instrumentation in this package now uses the Advice API to set
    default explicit buckets following the OpenTelemetry Specification. ([#2430](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2430))
  * Added support for listening to ASP.NET Core SignalR activities. Configurable
    with the AspNetCoreTraceInstrumentationOptions.EnableAspNetCoreSignalRSupport
    option which defaults to true. Only applies to .NET 9.0 or greater. ([#2539](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2539))
* Use 1.12.0 of OpenTelemetry.Instrumentation.AWS ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Introduce `AWSClientInstrumentationOptions.SemanticConventionVersion` which
    provides a mechanism for developers to opt-in to newer versions of the of the
    OpenTelemetry Semantic Conventions. ([#2367](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2367))
  * Context propagation data is always added to SQS and SNS requests regardless of
    sampling decision. This enables downstream services to make consistent sampling
    decisions and prevents incomplete traces. ([#2447](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2447))
* Use 1.12.0 of OpenTelemetry.Instrumentation.AWSLambda ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Introduce `AWSClientInstrumentationOptions.SemanticConventionVersion` which
    provides a mechanism for developers to opt-in to newer versions of the of
    the Open Telemetry Semantic Conventions. ([#2367](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2367))
* Use 1.0.0-beta.2 of OpenTelemetry.Instrumentation.Cassandra ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * `ActivitySource.Version` is set to NuGet package version. ([#1624](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1624))
  * Added direct reference to `Newtonsoft.Json` with minimum version of `13.0.1`
    in response to [CVE-2024-21907](https://github.com/advisories/GHSA-5crp-9r3c-p9vr).
    ([#2058](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2058))
* Use 1.12.0-beta.2 of OpenTelemetry.Instrumentation.EntityFrameworkCore ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * The new database semantic conventions can be opted in to by setting the
    `OTEL_SEMCONV_STABILITY_OPT_IN` environment variable. This allows for a
    transition period for users to experiment with the new semantic conventions
    and adapt as necessary. ([#2130](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2130))
  * Attribute `db.system` reports `oracle` when `Devart.Data.Oracle.Entity.EFCore`
    is used as a provider. ([#2465](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2465))
  * Support use with SqlClient instrumentation.
    ([#2280](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2280),
    [#2829](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2829))
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.ElasticsearchClient ([#208](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/208))
  * Span status is set based on semantic convention for client spans.
    ([#1538](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1538))
  * ActivitySource.Version is set to NuGet package version.
    ([#1624](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1624))
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.GrpcNetClient ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.Hangfire ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * `ActivitySource.Version` is set to NuGet package version. ([#1624](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1624))
  * Added direct reference to `Newtonsoft.Json` with minimum version of `13.0.1`
    for [CVE-2024-21907](https://github.com/advisories/GHSA-5crp-9r3c-p9vr).
    ([#2058](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2058))
* Use 1.12.0 of OpenTelemetry.Instrumentation.Http ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * The `http.client.request.duration` histogram (measured in seconds) produced
    by the metrics instrumentation in this package now uses the Advice API to set
    default explicit buckets following the OpenTelemetry Specification. ([#2430](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2430))
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.Owin ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Updated activity tags to use new semantic conventions attribute schema. ([#2028](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2028))
  * Updated registration extension code to retrieve environment variables through
    `IConfiguration`. ([#1973](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1973))
  * Updated to depend on the `OpenTelemetry.Api.ProviderBuilderExtensions` (API)
    package instead of the OpenTelemetry (SDK) package. ([#1977](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/1977))
  * The `http.server.request.duration` histogram (measured in seconds) produced
    by the metrics instrumentation in this package now uses the Advice API to set
    default explicit buckets following the OpenTelemetry Specification. ([#2430](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2430))
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.Process ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.Quartz ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
* Use 1.12.0 of OpenTelemetry.Instrumentation.Runtime ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Built-in .NET `System.Runtime` metrics are reported for .NET 9 and greater.
    For details about each individual metric check [.NET Runtime metrics docs page](https://learn.microsoft.com/dotnet/core/diagnostics/built-in-metrics-runtime).
    ([#2339](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2339))
* Use 1.12.0-beta.2 of OpenTelemetry.Instrumentation.SqlClient ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * The new database semantic conventions can be opted in to by setting the
    `OTEL_SEMCONV_STABILITY_OPT_IN` environment variable. This allows for a
    transition period for users to experiment with the new semantic conventions
    and adapt as necessary. ([#2229](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2229),
    [#2277](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2277),
    [#2262](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2262),
    [#2279](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2279))
  * The following attributes are now provided when starting an activity for a
    database call: `db.system`, `db.name` (old conventions), `db.namespace` (new
    conventions), `server.address`, and `server.port`. These attributes are now
    available for sampling decisions. ([#2277](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2277))
  * Add support for `metric db.client.operation.duration` from new database
    semantic conventions on .NET 8+. ([#2309](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2309))
  * Add support for metric `db.client.operation.duration` from new database
    semantic conventions on .NET Framework. ([#2311](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2311))
  * The `db.client.operation.duration` histogram (measured in seconds) produced
    by the metrics instrumentation in this package now uses the Advice API to
    set default explicit buckets following the OpenTelemetry Specification. ([#2430](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2430))
  * Add the `db.operation.name` attribute when `CommandType` is `StoredProcedure`
    to conform to the new semantic conventions. This affects you if you have
    opted into the new conventions. ([#2800](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2800))
  * Add the `db.query.summary` attribute. This affects you if you have opted into
    the new conventions. ([#2811](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2811))
  * Added `OTEL_DOTNET_EXPERIMENTAL_SQLCLIENT_ENABLE_TRACE_CONTEXT_PROPAGATION`
    environment variable to propagate trace context to SQL Server databases.
    This will remain experimental while the specification remains in development.
    It is now only available on .NET 8 and newer. ([#2709](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2709))
* Use 1.12.0-beta.2 of OpenTelemetry.Instrumentation.StackExchangeRedis ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * `System.Reflection.Emit.Lightweight` is referenced only by `netstandard2.0`.
    ([#2667](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2667))
  * Add support for early filtering of telemetry collection via a `Filter` option
    ([#2804](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2804))
  * Rename span network attributes to comply with v1.23.0 of Semantic Conventions
    for Database Client Calls ([#2468](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2468))
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.Wcf ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Added a `RecordException` property to specify if exceptions should be recorded
    (defaults to `false`). This is only supported by client instrumentation. ([#2271](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2271))
  * Preserves `IsReadOnly` state of instrumented HTTP Headers. ([#2716](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2716))
* Use 1.12.0-beta.1 of OpenTelemetry.Resources.Container ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
* Use 1.12.0-beta.1 of OpenTelemetry.Resources.Host ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
* Use 1.12.0-beta.1 of OpenTelemetry.Resources.OperatingSystem ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
* Use 1.12.0-beta.1 of OpenTelemetry.Resources.Process ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
* Use 1.12.0-beta.1 of OpenTelemetry.Resources.ProcessRuntime ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
* Use 3.22.0 of CassandraCSharpDriver
  ([#202](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/202))
* Enable tracing for Cassandra instrumentation (requires the
  `CassandraCSharpDriver.OpenTelemetry` NuGet package to be installed in your application)
  ([#202](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/202))

### Bug Fixes

* Remove reference on System.Text.Json for `net8.0` target framework
  ([#136](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/136))
([#146](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/146))
* Use 1.12.0 of OpenTelemetry ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Fixed a bug in tracing where `TraceState` set by a custom `Sampler` is not
    applied when creating propagation-only spans. ([#6058](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6058))
* Use 1.12.0 of OpenTelemetry.Exporter.OpenTelemetryProtocol ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Non-primitive attribute (logs) and tag (traces) values converted using
  * `Convert.ToString` will now format using `CultureInfo.InvariantCulture`. ([#5700](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5700))
  * Fixed an issue causing `NotSupportedException`s to be thrown on startup when
    `AddOtlpExporter` registration extensions are called while using custom dependency
    injection containers which automatically create services (Unity, Grace, etc.).
    ([#5808](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5808))
  * Fixed `PlatformNotSupportedExceptions` being thrown during export when running
  * on mobile platforms which caused telemetry to be dropped silently.
    ([#5821](https://github.com/open-telemetry/opentelemetry-dotnet/pull/5821))
  * Fixed incorrect log serialization of attributes with null values, causing
    some backends to reject logs. ([#6149](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6149))
  * Fixed an issue causing trace exports to fail when Activity.StatusDescription
    exceeds 127 bytes. ([#6119](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6119))
  * Fixed a bug in .NET Framework gRPC export client where the default success export
    response was incorrectly marked as false, now changed to true, ensuring exports
    are correctly marked as successful. ([#6099](https://github.com/open-telemetry/opentelemetry-dotnet/pull/6099))`
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.AspNet ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Fixed an issue in ASP.NET instrumentation where route extraction failed for
    attribute-based routing with multiple HTTP methods sharing the same route template.
    ([#2250](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2250))
  * The `http.server.request.duration` histogram (measured in seconds) produced
    by the metrics instrumentation in this package now uses the Advice API to set
    default explicit buckets following the OpenTelemetry Specification. ([#2430](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2430))
* Use 1.12.0 of OpenTelemetry.Instrumentation.AWS ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Trace instrumentation will now call the `Activity.SetStatus` API instead of
    the deprecated OpenTelemetry API package extension when setting span status.
    ([#2358](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2358))
* Use 1.12.0 of OpenTelemetry.Instrumentation.AWSLambda ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Trace instrumentation will now call the `Activity.SetStatus` API instead of
    the deprecated OpenTelemetry API package extension when setting span status.
    ([#2358](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2358))
  * Trace instrumentation will not fail with an exception if empty `LambdaContext`
    instance is passed. ([#2457](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2457))
* Use 1.12.0-beta.2 of OpenTelemetry.Instrumentation.EntityFrameworkCore ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Trace instrumentation will now call the `Activity.SetStatus` API instead of
    the deprecated OpenTelemetry API package extension when setting span status.
    ([#2358](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2358))
  * Fixed attribute `db.system` for `Devart.Data.SQLite.Entity.EFCore`,
    `Devart.Data.MySql.Entity.EFCore` and `Devart.Data.PostgreSql.Entity.EFCore`.
    ([#2571](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2571))
* Use 1.12.0 of OpenTelemetry.Instrumentation.Http ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Trace instrumentation no longer sets attributes when running on .NET 9 and
    greater because HttpClient now includes native instrumentation which adds
    attributes directly. ([#2314](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2314))
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.Owin ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Trace instrumentation will now call the `Activity.SetStatus` API instead of
    the deprecated OpenTelemetry API package extension when setting span status.
    ([#2358](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2358))
  * Fixed `url.query` value that was incorrectly always set to
    `Microsoft.Owin.ReadableStringCollection`. ([#2732](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2732))
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.Quartz ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Trace instrumentation will now call the `Activity.SetStatus` API instead of
    the deprecated OpenTelemetry API package extension when setting span status.
    ([#2358](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2358))
* Use 1.12.0-beta.2 of OpenTelemetry.Instrumentation.SqlClient ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Fix issue where IPv6 addresses were improperly parsed from the the connection's
    `DataSource` when used to populate the `server.address` attribute. ([#2674](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2674))
  * Fixes an issue that throws `IndexOutOfRangeException` in `SqlProcessor` when
    the SQL statement ends with the beginning of a keyword such as `UPDATE`. ([#2674](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2674))
* Use 1.12.0-beta.1 of OpenTelemetry.Instrumentation.Wcf ([#145](https://github.com/grafana/grafana-opentelemetry-dotnet/pull/145))
  * Trace instrumentation will now call the `Activity.SetStatus` API instead of
    the deprecated OpenTelemetry API package extension when setting span status.
    ([#2358](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2358))
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
