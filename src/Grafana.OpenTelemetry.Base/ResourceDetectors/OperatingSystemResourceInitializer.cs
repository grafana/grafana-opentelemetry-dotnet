//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

#if !NETSTANDARD

using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal class OperatingSystemResourceInitializer : ResourceDetectorInitializer
    {
        public override ResourceDetector Id { get; } = ResourceDetector.OperatingSystem;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            return builder.AddOperatingSystemDetector();
        }
    }
}

#endif
