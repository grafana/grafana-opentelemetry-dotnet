//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
#if NET8_0_OR_GREATER
    [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("Types might be removed")]
#endif
    internal sealed class CassandraInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.Cassandra;

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.Cassandra",
                "OpenTelemetry.Metrics.MeterProviderBuilderExtensions",
                "AddCassandraInstrumentation",
                [builder]);
        }

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            builder.AddSource("CassandraCSharpDriver.OpenTelemetry");
        }
    }
}
