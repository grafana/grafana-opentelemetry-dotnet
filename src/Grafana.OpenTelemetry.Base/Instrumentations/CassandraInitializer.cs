//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class CassandraInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.Cassandra;

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.Cassandra",
                "OpenTelemetry.Metrics.MeterProviderBuilderExtensions",
                "AddCassandraInstrumentation",
                new object[] { builder });
        }

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            // TODO Update upstream to use CassandraCSharpDriver.OpenTelemetry and
            // the this can be changed to .AddSource(CassandraActivitySourceHelper.ActivitySourceName)
            // and then also the user doesn't need to manually install the package.
            // See https://github.com/open-telemetry/opentelemetry-dotnet-contrib/pull/2939.
            builder.AddSource("CassandraCSharpDriver.OpenTelemetry");
        }
    }
}
