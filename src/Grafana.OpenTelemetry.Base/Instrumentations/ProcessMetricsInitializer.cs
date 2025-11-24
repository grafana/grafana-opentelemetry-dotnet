//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Metrics;

namespace Grafana.OpenTelemetry
{
    internal sealed class ProcessMetricsInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.Process;

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            builder.AddProcessInstrumentation();
        }
    }
}
