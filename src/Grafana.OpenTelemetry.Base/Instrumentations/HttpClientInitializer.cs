//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class HttpClientInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.HttpClient;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            builder.AddHttpClientInstrumentation();
        }

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            builder.AddHttpClientInstrumentation();
        }
    }
}
