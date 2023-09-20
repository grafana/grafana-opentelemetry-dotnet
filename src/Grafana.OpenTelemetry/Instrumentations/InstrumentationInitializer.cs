using System;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal abstract class InstrumentationInitializer
    {
        public static InstrumentationInitializer[] Initializers = new InstrumentationInitializer[]
        {
            new AspNetCoreInitializer(),
            new NetRuntimeMetricsInitializer(),
            new ProcessMetricsInitializer(),
            new HttpClientInitializer()
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

        abstract protected void InitializeTracing(TracerProviderBuilder builder);
        abstract protected void InitializeMetrics(MeterProviderBuilder builder);
    }
}
