using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCoreTrainings.Middlewares
{
    public class HttpMiddleware
    {
        private readonly RequestDelegate _next;

        private List<string> acceptablePaths = new List<string>
        {
            "/swagger/v1/swagger.json", "/api/people", "/swagger/index.html"
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

            return null;
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
