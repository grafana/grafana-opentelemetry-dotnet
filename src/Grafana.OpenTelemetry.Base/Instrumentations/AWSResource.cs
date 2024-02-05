//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.ResourceDetectors.AWS;
using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal class AWSResourceInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.AWSResource;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            return builder
#if !NETFRAMEWORK && !NETSTANDARD
                .AddDetector(new AWSECSResourceDetector())
                .AddDetector(new AWSEKSResourceDetector())
#endif
                .AddDetector(new AWSEC2ResourceDetector())
                .AddDetector(new AWSEBSResourceDetector());
        }
    }
}
