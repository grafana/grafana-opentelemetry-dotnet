//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Resources;

#if NET6_0_OR_GREATER

namespace Grafana.OpenTelemetry
{
    internal class ContainerResourceInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.ContainerResource;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            return builder.AddContainerDetector();
        }
    }
}

#endif
