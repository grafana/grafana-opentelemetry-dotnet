//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal abstract class ResourceDetectorInitializer
    {
        public static ResourceDetectorInitializer[] Initializers = new ResourceDetectorInitializer[]
        {
#if !NETSTANDARD
            new AWSEBSDetectorInitializer(),
            new AWSEC2DetectorInitializer(),
#endif
#if !NETSTANDARD && !NETFRAMEWORK
            new AWSECSDetectorInitializer(),
            new AWSEKSDetectorInitializer(),
#endif
            new AzureAppServiceDetectorInitializer(),
            new AzureVMDetectorInitializer(),
            new AzureContainerAppsDetectorInitializer(),
#if NET8_0_OR_GREATER
            new ContainerResourceInitializer(),
#endif
#if !NETSTANDARD
            new HostResourceInitializer(),
            new OperatingSystemResourceInitializer(),
            new ProcessResourceInitializer(),
            new ProcessRuntimeResourceInitializer()
#endif
        };

        abstract public ResourceDetector Id { get; }

        public void Initialize(TracerProviderBuilder builder)
        {
            try
            {
                builder.ConfigureResource(resourceBuilder => InitializeResourceDetector(resourceBuilder));

                GrafanaOpenTelemetryEventSource.Log.EnabledTracingInstrumentation(Id.ToString());
            }
            catch (Exception ex)
            {
                GrafanaOpenTelemetryEventSource.Log.FailureEnablingTracingInstrumentation(Id.ToString(), ex);
            }
        }

        public void Initialize(MeterProviderBuilder builder)
        {
            try
            {
                builder.ConfigureResource(resourceBuilder => InitializeResourceDetector(resourceBuilder));

                GrafanaOpenTelemetryEventSource.Log.EnabledMetricsInstrumentation(Id.ToString());
            }
            catch (Exception ex)
            {
                GrafanaOpenTelemetryEventSource.Log.FailureEnablingMetricsInstrumentation(Id.ToString(), ex);
            }
        }

        protected virtual ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            return builder;
        }
    }
}
