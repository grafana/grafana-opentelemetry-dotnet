//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Resources;

#if NET8_0_OR_GREATER

namespace Grafana.OpenTelemetry
{
    internal class ContainerResourceInitializer : ResourceDetectorInitializer
    {
        public override ResourceDetector Id { get; } = ResourceDetector.Container;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            return builder.AddContainerDetector();
        }
    }
}

#endif
