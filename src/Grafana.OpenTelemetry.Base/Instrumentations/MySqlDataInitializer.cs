//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal sealed class MySqlDataInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.MySqlData;

#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.DynamicDependency(System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "OpenTelemetry.Trace.TracerProviderBuilderExtensions", "MySQL.Data.OpenTelemetry")]
        [System.Diagnostics.CodeAnalysis.DynamicDependency(System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "OpenTelemetry.Trace.TracerProviderBuilderExtensions", "OpenTelemetry.Instrumentation.MySqlData")]
        [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage(TrimWarnings.Category, TrimWarnings.CheckId, Justification = TrimWarnings.Justification)]
#endif
        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            // MySQL.Data.OpenTelemetry
            ReflectionHelper.CallStaticMethod(
                "MySQL.Data.OpenTelemetry",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddConnectorNet",
                [builder]);

            // OpenTelemetry.Instrumentation.MySqlData
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.MySqlData",
                "OpenTelemetry.Trace.TracerProviderBuilderExtensions",
                "AddMySqlDataInstrumentation",
                [builder]);
        }
    }
}
