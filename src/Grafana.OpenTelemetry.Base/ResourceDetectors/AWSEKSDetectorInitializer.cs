//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

#if !NETSTANDARD && !NETFRAMEWORK
using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal class AWSEKSDetectorInitializer : ResourceDetectorInitializer
    {
        public override ResourceDetector Id { get; } = ResourceDetector.AWSEKS;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Resources.AWS",
                "OpenTelemetry.Resources.AWSResourceBuilderExtensions",
                "AddAWSEKSDetector",
                new object[] { builder });
            return builder;
        }
    }
}
#endif
