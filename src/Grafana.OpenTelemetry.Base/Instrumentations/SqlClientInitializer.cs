//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
#if NET
    [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("Trimming is not yet supported with SqlClient instrumentation.")]
#endif
    internal class SqlClientInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.SqlClient;

        protected override void InitializeMetrics(MeterProviderBuilder builder)
        {
            builder.AddSqlClientInstrumentation();
        }

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            builder.AddSqlClientInstrumentation();
        }
    }
}
