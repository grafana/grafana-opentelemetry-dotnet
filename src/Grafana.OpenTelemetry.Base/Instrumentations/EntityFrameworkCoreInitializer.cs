//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class EntityFrameworkCoreInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.EntityFrameworkCore;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.EntityFrameworkCore",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddEntityFrameworkCoreInstrumentation",
                new object[] { builder });
        }
    }
}
