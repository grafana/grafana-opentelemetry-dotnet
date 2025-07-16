//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using System.Collections.Generic;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

#if NET
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
#endif

namespace Grafana.OpenTelemetry
{
    internal abstract class InstrumentationInitializer
    {
        public static InstrumentationInitializer[] Initializers = GetDefaults();

        public abstract Instrumentation Id { get; }

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

#if NET
        [UnconditionalSuppressMessage(
            "Trimming",
            "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code",
            Justification = "Usage of is SqlClientInitializer guarded.")]
#endif
        private static InstrumentationInitializer[] GetDefaults()
        {
            var initializers = new List<InstrumentationInitializer>
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
                new StackExchangeRedisInitializer(),
                new WcfInitializer(),
            };

#if NET
            if (RuntimeFeature.IsDynamicCodeSupported)
            {
                initializers.Add(new SqlClientInitializer());
            }
#else
            initializers.Add(new SqlClientInitializer());
#endif

            return initializers.ToArray();
        }
    }
}
