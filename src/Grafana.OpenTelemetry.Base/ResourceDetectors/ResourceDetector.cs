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
        AWSEBSDetector,
        /// <summary>
        /// AWS EC2 Resource Detector (OpenTelemetry.ResourceDetectors.AWS)
        /// </summary>
        AWSEC2Detector,
        /// <summary>
        /// AWS ECS Resource Detector (OpenTelemetry.ResourceDetectors.AWS)
        /// </summary>
        AWSECSDetector,
        /// <summary>
        /// AWS EKS Resource Detector (OpenTelemetry.ResourceDetectors.AWS)
        /// </summary>
        AWSEKSDetector,

        /// <summary>
        /// Azure App Service Resource Detector (OpenTelemetry.ResourceDetectors.Azure)
        /// </summary>
        AzureAppServiceDetector,
        /// <summary>
        /// Azure Virtual Machine Resource Detector (OpenTelemetry.ResourceDetectors.Azure)
        /// </summary>
        AzureVMDetector,
        /// <summary>
        /// Azure Container Apps Resource Detector (OpenTelemetry.ResourceDetectors.Azure)
        /// </summary>
        AzureContainerAppsDetector,

        /// <summary>
        /// Container Resource Detector (OpenTelemetry.ResourceDetectors.Container)
        /// </summary>
        Container,

        /// <summary>
        /// Host Resource Detector (OpenTelemetry.ResourceDetectors.Host)
        /// </summary>
        Host,

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
