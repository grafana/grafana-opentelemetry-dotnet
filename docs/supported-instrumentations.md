# Supported instrumentations

At the moment, the following instrumentations are included in the distribution
packages with [full](./installation.md#install-the-full-package-with-all-available-instrumentations)
and [minimal](./installation.md#install-the-base-package) dependencies:

| Identifier              | Full               | Base               | Library name                                                                                                                                                  |
| ----------------------- | ------------------ | ------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `AspNet`                | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.AspNet](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.AspNet)                                                   |
| `AspNetCore`            | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.AspNetCore](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.AspNetCore)                                           |
| `AWS`                   | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.AWS](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.AWS)                                                         |
| `AWSLambda`             | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.AWSLambda](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.AWSLambda)                                             |
| `AWSResource`           | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Resources.AWS](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Resources.AWS)                       |
| `AzureResource`         | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Resources.Azure](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Resources.Azure)                   |
| `Cassandra`             | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.Cassandra](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Cassandra)                                             |
| `ContainerResource`     | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Resources.Container](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Resources.Container)           |
| `ElasticsearchClient`   | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.ElasticsearchClient](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.ElasticsearchClient)                         |
| `EntityFrameworkCore`   | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.EntityFrameworkCore](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.EntityFrameworkCore)                         |
| `GrpcNetClient`         | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Instrumentation.GrpcNetClient](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.GrpcNetClient)                                     |
| `Hangfire`              | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.Hangfire](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Hangfire)                                               |
| `HttpClient`            | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Instrumentation.Http](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Http)                                                       |
| `HostResource`          | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Resources.Host](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Resources.Host)                     |
| `MySqlData`             |                    |                    | [MySql.Data.OpenTelemetry](https://www.nuget.org/packages/MySql.Data.OpenTelemetry)                                                                           |
|                         |                    |                    | [OpenTelemetry.Instrumentation.MySqlData](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.MySqlData) :warning: **Deprecated**                    |
| `NetRuntime`            | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Instrumentation.Runtime](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Runtime)                                                 |
| `Owin`                  | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.Owin](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Owin)                                                       |
| `Process`               | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Instrumentation.Process](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Process)                                                 |
| `ProcessResource`       | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Resources.Process](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Resources.Process)               |
| `ProcessRuntimeResource`| :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Resources.ProcessRuntime](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Resources.ProcessRuntime) |
| `Quartz`                | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.Quartz](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Quartz)                                                   |
| `SqlClient`             | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Instrumentation.SqlClient](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.SqlClient)                                             |
| `StackExchangeRedis`    | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.StackExchangeRedis](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.StackExchangeRedis)                           |
| `Wcf`                   | :heavy_check_mark: |                    | [OpenTelemetry.Instrumentation.Wcf](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Wcf)                                                         |

* The `AWSLambda` instrumentation is included but needs to be explicitly
  activated, as activating it in non-AWS scenarios causes errors.
* The `ContainerResource` instrumentation is included but needs to be explicitly
  activated, as it currently adds container resource attributes for processes
  running not in containers.
* The `MySqlData` instrumentation is automatically enabled if it is present in
  the application, but requires `MySql.Data.OpenTelemetry` to be manually installed.
