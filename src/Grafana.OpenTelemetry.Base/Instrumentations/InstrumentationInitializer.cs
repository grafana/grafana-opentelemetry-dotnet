//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
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
            new WcfInitializer()
        };

        abstract public Instrumentation Id { get; }

        public void Initialize(TracerProviderBuilder builder)
        {
            try
            {
                InitializeTracing(builder);

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

                GrafanaOpenTelemetryEventSource.Log.EnabledMetricsInstrumentation(Id.ToString());
            }
            catch (Exception ex)
            {
                GrafanaOpenTelemetryEventSource.Log.FailureEnablingMetricsInstrumentation(Id.ToString(), ex);
            }
        }

        protected virtual void InitializeTracing(TracerProviderBuilder builder)
        { }

        protected virtual void InitializeMetrics(MeterProviderBuilder builder)
        { }
    }
}
