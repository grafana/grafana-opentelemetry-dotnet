//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

#if !NETSTANDARD
using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal class AWSResourceInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.AWSResource;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            return builder
#if !NETFRAMEWORK
                .AddAWSECSDetector()
                .AddAWSEKSDetector()
#endif
                .AddAWSEC2Detector()
                .AddAWSEBSDetector();
        }
    }
}
#endif
