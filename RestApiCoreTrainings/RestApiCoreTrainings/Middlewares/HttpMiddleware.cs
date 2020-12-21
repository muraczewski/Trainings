using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RestApiCoreTrainings.Middlewares
{
    public class HttpMiddleware
    {
        private readonly RequestDelegate _next;

        // todo fix this to work swagger
        private List<string> acceptablePaths = new List<string>
        {
            "/swagger/v1/swagger.json", "/api/people", "/swagger/index.html", "/swagger"
        };

        public HttpMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (acceptablePaths.Contains(httpContext.Request.Path.Value))
            {
                return _next(httpContext);
            }

            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HttpMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpMiddleware>();
        }
    }
}
