//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry
{
#if NET8_0_OR_GREATER
    [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("Types might be removed")]
#endif
    internal sealed class AzureAppServiceDetectorInitializer : ResourceDetectorInitializer
    {
        public override ResourceDetector Id { get; } = ResourceDetector.AzureAppService;

        protected override ResourceBuilder InitializeResourceDetector(ResourceBuilder builder)
        {
            ReflectionHelper.CallStaticMethod(
                "OpenTelemetry.Resources.Azure",
                "OpenTelemetry.Resources.AzureResourceBuilderExtensions",
                "AddAzureAppServiceDetector",
                [builder]);
            return builder;
        }
    }
}
