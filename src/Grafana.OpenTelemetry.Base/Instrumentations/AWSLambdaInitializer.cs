//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
#if NET8_0_OR_GREATER
    [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("Types might be removed")]
#endif
    internal sealed class AWSLambdaInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.AWSLambda;

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
