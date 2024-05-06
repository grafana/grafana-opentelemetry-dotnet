//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal abstract class InstrumentationInitializer
    {
        public static InstrumentationInitializer[] Initializers = new InstrumentationInitializer[]
        {
#if NETFRAMEWORK
            new AspNetInitializer(),
            new OwinInitializer(),
#endif
            new AspNetCoreInitializer(),
            new AWSInitializer(),
            new AWSLambdaInitializer(),
            new CassandraInitializer(),
            new ElasticsearchClientInitializer(),
            new EntityFrameworkCoreInitializer(),
            new GrpcNetClientInitializer(),
            new HangfireInitializer(),
            new HttpClientInitializer(),
            new MySqlDataInitializer(),
            new NetRuntimeMetricsInitializer(),
            new ProcessMetricsInitializer(),
            new QuartzInitializer(),
            new SqlClientInitializer(),
            new StackExchangeRedisInitializer(),
            new WcfInitializer(),
            new AzureResourceInitializer(),
            new ContainerResourceInitializer(),
#if !NETSTANDARD
            new AWSResourceInitializer(),
            new HostResourceInitializer(),
            new ProcessResourceInitializer(),
            new ProcessRuntimeResourceInitializer()
#endif
        };

        abstract public Instrumentation Id { get; }

        public void Initialize(TracerProviderBuilder builder)
        {
            try
            {
                InitializeTracing(builder);
                InitializeResource(builder);

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
                InitializeMetrics(builder);
                InitializeResource(builder);

                GrafanaOpenTelemetryEventSource.Log.EnabledMetricsInstrumentation(Id.ToString());
            }
            catch (Exception ex)
            {
                GrafanaOpenTelemetryEventSource.Log.FailureEnablingMetricsInstrumentation(Id.ToString(), ex);
            }
        }

        protected void InitializeResource(TracerProviderBuilder builder)
        {
            builder.ConfigureResource(resourceBuilder => InitializeResourceDetector(resourceBuilder));
        }

        protected void InitializeResource(MeterProviderBuilder builder)
        {
            builder.ConfigureResource(resourceBuilder => InitializeResourceDetector(resourceBuilder));
        }

        protected virtual void InitializeTracing(TracerProviderBuilder builder)
        { }

        protected virtual void InitializeMetrics(MeterProviderBuilder builder)
        { }

        protected virtual ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            return builder;
        }
    }
}
