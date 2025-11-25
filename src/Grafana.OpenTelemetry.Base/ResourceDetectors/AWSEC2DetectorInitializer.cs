//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

#if !NETSTANDARD
using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal sealed class AWSEC2DetectorInitializer : ResourceDetectorInitializer
    {
        public override ResourceDetector Id { get; } = ResourceDetector.AWSEC2;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Resources.AWS",
                "OpenTelemetry.Resources.AWSResourceBuilderExtensions",
                "AddAWSEC2Detector",
                [builder]);
            return builder;
        }
    }
}
#endif
