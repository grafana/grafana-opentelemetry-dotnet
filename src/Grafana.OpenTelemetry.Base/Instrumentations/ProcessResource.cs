//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

#if !NETSTANDARD

using OpenTelemetry.Resources;
using OpenTelemetry.ResourceDetectors.Process;

namespace Grafana.OpenTelemetry
{
    internal class ProcessResourceInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.ProcessResource;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
	    return builder.AddDetector(new ProcessDetector()); 
        }
    }
}

#endif
