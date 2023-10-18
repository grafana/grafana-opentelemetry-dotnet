//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class SqlClientInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.SqlClient;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            builder.AddSqlClientInstrumentation();
        }
    }
}
