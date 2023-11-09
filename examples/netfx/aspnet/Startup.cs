using System.Net;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AspNetExample.Startup))]

namespace AspNetExample
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
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
