using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RestApiCoreTrainings.Filters
{
    public class LogAsyncActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<LogAsyncActionFilter> _logger;

        public LogAsyncActionFilter(ILogger<LogAsyncActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"Start of async action {context.HttpContext.Request.Path}");

            var resultContext = await next();

            _logger.LogInformation($"End of async action {context.HttpContext.Request.Path}");
        }
    }
}
