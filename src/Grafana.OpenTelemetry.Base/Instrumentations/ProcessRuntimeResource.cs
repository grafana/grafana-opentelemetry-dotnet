//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

#if !NETSTANDARD

using OpenTelemetry.Resources;
using OpenTelemetry.ResourceDetectors.ProcessRuntime;

namespace Grafana.OpenTelemetry
{
    internal class ProcessRuntimeResourceInitializer : InstrumentationInitializer
    {
        public override Instrumentation Id { get; } = Instrumentation.ProcessRuntimeResource;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
	    return builder.AddDetector(new ProcessRuntimeDetector()); 
        }
    }
}

#endif
