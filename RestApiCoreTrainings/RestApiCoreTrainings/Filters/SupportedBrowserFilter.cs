using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestApiCoreTrainings.Filters
{
    public class SupportedBrowserFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userAgent = context.HttpContext.Request.Headers["User-Agent"];
            //TODO replace with better checking e.g. https://github.com/ua-parser/uap-csharp
            if (!userAgent[0].Contains("Chrome"))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
