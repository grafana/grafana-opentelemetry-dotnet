//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using System.Diagnostics.Tracing;
using System.Text.Json;

#if NET
using System.Text.Json.Serialization;
#endif

namespace Grafana.OpenTelemetry
{
    [EventSource(Name = "OpenTelemetry-Grafana-Distribution")]
    internal sealed partial class GrafanaOpenTelemetryEventSource : EventSource
    {
        public static readonly GrafanaOpenTelemetryEventSource Log = new GrafanaOpenTelemetryEventSource();

        [NonEvent]
        public void EnabledMetricsInstrumentation(string instrumentationLibrary)
        {
            if (Log.IsEnabled(EventLevel.Informational, EventKeywords.All))
            {
                this.EnabledInstrumentation("metrics", instrumentationLibrary);
            }
        }

        [NonEvent]
        public void EnabledTracingInstrumentation(string instrumentationLibrary)
        {
            if (Log.IsEnabled(EventLevel.Informational, EventKeywords.All))
            {
                this.EnabledInstrumentation("tracing", instrumentationLibrary);
            }
        }

        [NonEvent]
        public void FailureEnablingMetricsInstrumentation(string instrumentationLibrary, Exception ex)
        {
            if (Log.IsEnabled(EventLevel.Warning, EventKeywords.All))
            {
                this.FailureEnablingInstrumentation("metrics", instrumentationLibrary, ex.ToString());
            }
        }

        [NonEvent]
        public void FailureEnablingTracingInstrumentation(string instrumentationLibrary, Exception ex)
        {
            if (Log.IsEnabled(EventLevel.Warning, EventKeywords.All))
            {
                this.FailureEnablingInstrumentation("tracing", instrumentationLibrary, ex.ToString());
            }
        }

        [NonEvent]
        public void InitializeDistribution(GrafanaOpenTelemetrySettings settings)
        {
#if NET
            var settingsJson = JsonSerializer.Serialize(settings, GrafanaJsonSerializerContext.Default.GrafanaOpenTelemetrySettings);
#else
            var settingsJson = JsonSerializer.Serialize(settings);
#endif

            InitializeDistribution(settingsJson);
        }

        [Event(1, Message = "Grafana distribution initializing with settings: {0}'", Level = EventLevel.Informational)]
        public void InitializeDistribution(string settings)
        {
            this.WriteEvent(1, settings);
        }

        [Event(2, Message = "Grafana distribution enabling {0} instrumentation '{1}'", Level = EventLevel.Informational)]
        public void EnabledInstrumentation(string signal, string instrumentationLibrary)
        {
            this.WriteEvent(2, signal, instrumentationLibrary);
        }

        [Event(3, Message = "Grafana distribution cannot enable {0} instrumentation '{1}'. Exception: {2}", Level = EventLevel.Warning)]
        public void FailureEnablingInstrumentation(string signal, string instrumentationLibrary, string ex)
        {
            this.WriteEvent(3, signal, instrumentationLibrary, ex);
        }

#if NET
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [JsonSerializable(typeof(GrafanaOpenTelemetrySettings))]
        private sealed partial class GrafanaJsonSerializerContext : JsonSerializerContext;
#endif
    }
}
