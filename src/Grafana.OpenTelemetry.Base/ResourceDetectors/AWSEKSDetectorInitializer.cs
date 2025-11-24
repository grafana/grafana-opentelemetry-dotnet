//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

#if !NETSTANDARD && !NETFRAMEWORK
using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
#if NET8_0_OR_GREATER
    [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("Types might be removed")]
#endif
    internal sealed class AWSEKSDetectorInitializer : ResourceDetectorInitializer
    {
        public override ResourceDetector Id { get; } = ResourceDetector.AWSEKS;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Resources.AWS",
                "OpenTelemetry.Resources.AWSResourceBuilderExtensions",
                "AddAWSEKSDetector",
                [builder]);
            return builder;
        }
    }
}
#endif
