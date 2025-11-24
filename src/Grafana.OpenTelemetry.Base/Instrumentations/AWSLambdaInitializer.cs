//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal sealed class AWSLambdaInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.AWSLambda;

#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.DynamicDependency(System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "OpenTelemetry.Instrumentation.AWSLambda.TracerProviderBuilderExtensions", "OpenTelemetry.Instrumentation.AWSLambda")]
        [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage(TrimWarnings.Category, TrimWarnings.CheckId, Justification = TrimWarnings.Justification)]
#endif
        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.AWSLambda",
                "OpenTelemetry.Instrumentation.AWSLambda.TracerProviderBuilderExtensions",
                "AddAWSLambdaConfigurations",
                [builder]);
        }
    }
}
