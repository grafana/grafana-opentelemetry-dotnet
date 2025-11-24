//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal sealed class ElasticsearchClientInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.ElasticsearchClient;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.ElasticsearchClient",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddElasticsearchClientInstrumentation",
                [builder]);
        }
    }
}
