//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Trace;

namespace Grafana.OpenTelemetry
{
    internal class AWSLambdaInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.AWSLambda;

        protected override void InitializeTracing(TracerProviderBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Instrumentation.AWSLambda",
                "OpenTelemetry.Instrumentation.AWSLambda.TracerProviderBuilderExtensions",
                "AddAWSLambdaConfigurations",
                new object[] { builder });
        }
    }
}
