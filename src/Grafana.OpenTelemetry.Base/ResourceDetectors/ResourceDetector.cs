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
        /// AWS EBS Resource Detector (OpenTelemetry.ResourceDetectors.AWS)
        /// </summary>
        AWSEBS,
        /// <summary>
        /// AWS EC2 Resource Detector (OpenTelemetry.ResourceDetectors.AWS)
        /// </summary>
        AWSEC2,
        /// <summary>
        /// AWS ECS Resource Detector (OpenTelemetry.ResourceDetectors.AWS)
        /// </summary>
        AWSECS,
        /// <summary>
        /// AWS EKS Resource Detector (OpenTelemetry.ResourceDetectors.AWS)
        /// </summary>
        AWSEKS,

        /// <summary>
        /// Azure App Service Resource Detector (OpenTelemetry.ResourceDetectors.Azure)
        /// </summary>
        AzureAppService,
        /// <summary>
        /// Azure Virtual Machine Resource Detector (OpenTelemetry.ResourceDetectors.Azure)
        /// </summary>
        AzureVM,
        /// <summary>
        /// Azure Container Apps Resource Detector (OpenTelemetry.ResourceDetectors.Azure)
        /// </summary>
        AzureContainerApps,

        /// <summary>
        /// Container Resource Detector (OpenTelemetry.ResourceDetectors.Container)
        /// </summary>
        Container,

        /// <summary>
        /// Host Resource Detector (OpenTelemetry.ResourceDetectors.Host)
        /// </summary>
        Host,

        /// <summary>
        /// Operating System Resource Detector (OpenTelemetry.ResourceDetectors.OperatingSystem)
        /// </summary>
        OperatingSystem,

        /// <summary>
        /// Process Resource Detector (OpenTelemetry.ResourceDetectors.Process)
        /// </summary>
        Process,

        /// <summary>
        /// Process Runtime Resource Detector (OpenTelemetry.ResourceDetectors.ProcessRuntime)
        /// </summary>
        ProcessRuntime
    }
}
