namespace Grafana.OpenTelemetry
{
    /// <summary>
    /// An enum representing different instrumentation libraries.
    /// </summary>
    public enum Instrumentation
    {
        /// <summary>
        /// .NET runtime metrics (OpenTelemetry.Instrumentation.Runtime)
        /// </summary>
        NetRuntime,

        /// <summary>
        /// .NET process metrics (OpenTelemetry.Instrumentation.Process)
        /// </summary>
        Process,

        /// <summary>
        /// HttpClient metrics and traces (OpenTelemetry.Instrumentation.Http)
        /// </summary>
        HttpClient,

        /// <summary>
        /// ASP.NET Core and traces (OpenTelemetry.Instrumentation.AspNetCore)
        /// </summary>
        AspNetCore
    }
}
