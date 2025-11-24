#if NET8_0_OR_GREATER

namespace Grafana.OpenTelemetry;

internal static class TrimWarnings
{
    internal const string Category = "ReflectionAnalysis";
    internal const string CheckId = "IL2026:RequiresUnreferencedCode";
    internal const string Justification = "[DynamicDependency] is used to preserve members.";
}

#endif
