# Supported resource detectors

The following resource detectors are recognized:

| Identifier           | Enabled by default | Pre-installed      | Library name                                                                                                      |
| -------------------- | ------------------ | ------------------ | ----------------------------------------------------------------------------------------------------------------- |
| `AWSEBS`             |                    |                    | [OpenTelemetry.Resources.AWS](https://www.nuget.org/packages/OpenTelemetry.Resources.AWS)                         |
| `AWSEC2`             |                    |                    | [OpenTelemetry.Resources.AWS](https://www.nuget.org/packages/OpenTelemetry.Resources.AWS)                         |
| `AWSECS`             |                    |                    | [OpenTelemetry.Resources.AWS](https://www.nuget.org/packages/OpenTelemetry.Resources.AWS)                         |
| `AWSEKS`             |                    |                    | [OpenTelemetry.Resources.AWS](https://www.nuget.org/packages/OpenTelemetry.Resources.AWS)                         |
| `AzureAppService`    |                    |                    | [OpenTelemetry.Resources.Azure](https://www.nuget.org/packages/OpenTelemetry.Resources.Azure)                     |
| `AzureVM`            |                    |                    | [OpenTelemetry.Resources.Azure](https://www.nuget.org/packages/OpenTelemetry.Resources.Azure)                     |
| `AzureContainerApps` |                    |                    | [OpenTelemetry.Resources.Azure](https://www.nuget.org/packages/OpenTelemetry.Resources.Azure)                     |
| `Container`          |                    | :heavy_check_mark: | [OpenTelemetry.Resources.Container](https://www.nuget.org/packages/OpenTelemetry.Resources.Container)             |
| `Host`               | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Resources.Host](https://www.nuget.org/packages/OpenTelemetry.Resources.Host)                       |
| `OperatingSystem`    |                    |                    | [OpenTelemetry.Resources.OperatingSystem](https://www.nuget.org/packages/OpenTelemetry.Resources.OperatingSystem) |
| `Process`            | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Resources.Process](https://www.nuget.org/packages/OpenTelemetry.Resources.Process)                 |
| `ProcessRuntime`     | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.Resources.ProcessRuntime](https://www.nuget.org/packages/OpenTelemetry.Resources.ProcessRuntime)   |

* The `Container` resource detector is included but needs to be explicitly
  activated, as activating it in non-container environments can causes erroneous
  attributes. A future release may activate it by default.
