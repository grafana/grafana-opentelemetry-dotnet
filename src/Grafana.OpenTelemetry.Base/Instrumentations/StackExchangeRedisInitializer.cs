//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class StackExchangeRedisInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.StackExchangeRedis;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.StackExchangeRedis",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddRedisInstrumentation",
                [builder]);
        }
    }
}
