# Supported resource detectors

The following resource detectors are recognized:

| Identifier            | Enabled by default | Pre-installed      | Library name |
| --------------------- | ------------------ | ------------------ | ------------ |
| `AWSEBS`              |                    |                    | [OpenTelemetry.ResourceDetectors.AWS](https://www.nuget.org/packages/OpenTelemetry.ResourceDetectors.AWS) |
| `AWSEC2`              |                    |                    | [OpenTelemetry.ResourceDetectors.AWS](https://www.nuget.org/packages/OpenTelemetry.ResourceDetectors.AWS) |
| `AWSECS`              |                    |                    | [OpenTelemetry.ResourceDetectors.AWS](https://www.nuget.org/packages/OpenTelemetry.ResourceDetectors.AWS) |
| `AWSEKS`              |                    |                    | [OpenTelemetry.ResourceDetectors.AWS](https://www.nuget.org/packages/OpenTelemetry.ResourceDetectors.AWS) |
| `AzureAppService`     |                    |                    | [OpenTelemetry.ResourceDetectors.Azure](https://www.nuget.org/packages/OpenTelemetry.ResourceDetectors.Azure) |
| `AzureVM`             |                    |                    | [OpenTelemetry.ResourceDetectors.Azure](https://www.nuget.org/packages/OpenTelemetry.ResourceDetectors.Azure) |
| `AzureContainerApps`  |                    |                    | [OpenTelemetry.ResourceDetectors.Azure](https://www.nuget.org/packages/OpenTelemetry.ResourceDetectors.Azure) |
| `Container`           |                    | :heavy_check_mark: | [OpenTelemetry.ResourceDetectors.Container](https://www.nuget.org/packages/OpenTelemetry.ResourceDetectors.Container) |
| `Host`                | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.ResourceDetectors.Host](https://www.nuget.org/packages/OpenTelemetry.ResourceDetectors.Host) |
| `OperatingSystem`     |                    |                    | [OpenTelemetry.ResourceDetectors.OperatingSystem](https://www.nuget.org/packages/OpenTelemetry.ResourceDetectors.OperatingSystem) |
| `Process`             | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.ResourceDetectors.Process](https://www.nuget.org/packages/OpenTelemetry.ResourceDetectors.Process) |
| `ProcessRuntime`      | :heavy_check_mark: | :heavy_check_mark: | [OpenTelemetry.ResourceDetectors.ProcessRuntime](https://www.nuget.org/packages/OpenTelemetry.ResourceDetectors.ProcessRuntime) |

* The `Container` resource detector is included but needs to be explicitly
  activated, as activating it in non-container environments can causes erroneous
  attributes. A future release may activate it by default.
