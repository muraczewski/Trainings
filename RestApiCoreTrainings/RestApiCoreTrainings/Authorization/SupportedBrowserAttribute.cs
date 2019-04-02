using Microsoft.AspNetCore.Mvc;
using RestApiCoreTrainings.Filters;

namespace RestApiCoreTrainings.Authorization
{
    public class SupportedBrowserAttribute : TypeFilterAttribute
    {
        public SupportedBrowserAttribute() : base(typeof(SupportedBrowserFilter))
        {
        }
    }
}
