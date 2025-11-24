//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Metrics;

namespace Grafana.OpenTelemetry
{
    internal sealed class NetRuntimeMetricsInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.NetRuntime;

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            builder.AddRuntimeInstrumentation();
        }
    }
}
