using System.Net;
using Grafana.OpenTelemetry;
using Microsoft.Owin;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Owin;

[assembly: OwinStartup(typeof(AspNetExample.Startup))]

namespace AspNetExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseOpenTelemetry();

            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Path.Value == "/")
                {
                    ctx.Response.StatusCode = (int)HttpStatusCode.Redirect;
                    ctx.Response.Headers.Set("Location", ctx.Request.Uri + "swagger");
                }
                else
                {
                    await next();
                }
            });
        }
    }
}
