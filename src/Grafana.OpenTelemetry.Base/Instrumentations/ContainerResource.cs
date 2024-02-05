//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.ResourceDetectors.Container;
using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
    internal class ContainerResourceInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.ContainerResource;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            return builder.AddDetector(new ContainerResourceDetector()); 
        }
    }
}
