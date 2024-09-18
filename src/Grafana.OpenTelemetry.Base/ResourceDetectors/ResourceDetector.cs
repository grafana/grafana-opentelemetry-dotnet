//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

namespace Grafana.OpenTelemetry
{
    /// <summary>
    /// An enum representing different resource detectors.
    /// </summary>
    public enum ResourceDetector
    {
        /// <summary>
        /// AWS EBS Resource Detector (OpenTelemetry.Resources.AWS)
        /// </summary>
        AWSEBS,
        /// <summary>
        /// AWS EC2 Resource Detector (OpenTelemetry.Resources.AWS)
        /// </summary>
        AWSEC2,
        /// <summary>
        /// AWS ECS Resource Detector (OpenTelemetry.Resources.AWS)
        /// </summary>
        AWSECS,
        /// <summary>
        /// AWS EKS Resource Detector (OpenTelemetry.Resources.AWS)
        /// </summary>
        AWSEKS,

        /// <summary>
        /// Azure App Service Resource Detector (OpenTelemetry.Resources.Azure)
        /// </summary>
        AzureAppService,
        /// <summary>
        /// Azure Virtual Machine Resource Detector (OpenTelemetry.Resources.Azure)
        /// </summary>
        AzureVM,
        /// <summary>
        /// Azure Container Apps Resource Detector (OpenTelemetry.Resources.Azure)
        /// </summary>
        AzureContainerApps,

        /// <summary>
        /// Container Resource Detector (OpenTelemetry.Resources.Container)
        /// </summary>
        Container,

        /// <summary>
        /// Host Resource Detector (OpenTelemetry.Resources.Host)
        /// </summary>
        Host,

        /// <summary>
        /// Operating System Resource Detector (OpenTelemetry.Resources.OperatingSystem)
        /// </summary>
        OperatingSystem,

        /// <summary>
        /// Process Resource Detector (OpenTelemetry.Resources.Process)
        /// </summary>
        Process,

        /// <summary>
        /// Process Runtime Resource Detector (OpenTelemetry.Resources.ProcessRuntime)
        /// </summary>
        ProcessRuntime
    }
}
