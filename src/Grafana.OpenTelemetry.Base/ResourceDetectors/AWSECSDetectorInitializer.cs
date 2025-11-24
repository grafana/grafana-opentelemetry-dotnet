//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

#if !NETSTANDARD && !NETFRAMEWORK
using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal sealed class AWSECSDetectorInitializer : ResourceDetectorInitializer
    {
        public override ResourceDetector Id { get; } = ResourceDetector.AWSECS;

#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.DynamicDependency(System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.All, "OpenTelemetry.Resources.AWSResourceBuilderExtensions", "OpenTelemetry.Resources.AWS")]
        [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage(TrimWarnings.Category, TrimWarnings.CheckId, Justification = TrimWarnings.Justification)]
#endif
        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Resources.AWS",
                "OpenTelemetry.Resources.AWSResourceBuilderExtensions",
                "AddAWSECSDetector",
                [builder]);
            return builder;
        }
    }
}
#endif
