using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfiguringApps.Infrastructure
{
    public class ShortCircuitMiddleware
    {
        RequestDelegate nextDelegate;

        public ShortCircuitMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Items["EdgeBrowser"] as bool? == true)
            {
                httpContext.Response.StatusCode = 403;
            }
            else
                await nextDelegate.Invoke(httpContext);
        }
    }
}
