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
        /// ASP.NET Core metrics and traces (OpenTelemetry.Instrumentation.AspNetCore)
        /// </summary>
        AspNetCore,

        /// <summary>
        /// gRPC client traces (OpenTelemetry.Instrumentation.GrpcNetClient)
        /// </summary>
        GrpcNetClient,

        /// <summary>
        /// SQL client traces (OpenTelemetry.Instrumentation.SqlClient)
        /// </summary>
        SqlClient,

        /// <summary>
        /// AWS traces (OpenTelemetry.Instrumentation.AWS)
        /// </summary>
        AWS,

        /// <summary>
        /// AWS Lambda traces (OpenTelemetry.Instrumentation.AWSLambda)
        /// </summary>
        AWSLambda,

        /// <summary>
        /// ASP.NET metrics and traces (OpenTelemetry.Instrumentation.AspNetCore)
        /// </summary>
        AspNet,

        /// <summary>
        /// Cassandra metrics (OpenTelemetry.Instrumentation.Cassandra)
        /// </summary>
        Cassandra,

        /// <summary>
        /// Elasticsearch client traces (OpenTelemetry.Instrumentation.ElasticsearchClient)
        /// </summary>
        ElasticsearchClient,

        /// <summary>
        /// EntityFrameworkCore traces (OpenTelemetry.Instrumentation.EntityFrameworkCore)
        /// </summary>
        EntityFrameworkCore,

        /// <summary>
        /// Hangfire traces (OpenTelemetry.Instrumentation.Hangfire)
        /// </summary>
        Hangfire,

        /// <summary>
        /// MySqlData traces (OpenTelemetry.Instrumentation.MySqlData)
        /// </summary>
        MySqlData,

        /// <summary>
        /// Owin metrics and traces (OpenTelemetry.Instrumentation.Owin)
        /// </summary>
        Owin,

        /// <summary>
        /// Quarty traces (OpenTelemetry.Instrumentation.Quartz)
        /// </summary>
        Quartz,

        /// <summary>
        /// StackExchange.Redis traces (OpenTelemetry.Instrumentation.StackExchangeRedis)
        /// </summary>
        StackExchangeRedis,

        /// <summary>
        /// WCF traces (OpenTelemetry.Instrumentation.Wcf)
        /// </summary>
        Wcf
    }
}
