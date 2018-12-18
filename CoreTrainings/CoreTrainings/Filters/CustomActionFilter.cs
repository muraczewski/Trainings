using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CoreTrainings.Filters
{
    public class CustomActionFilter : IActionFilter
    {
        private readonly ILogger logger;

        public CustomActionFilter(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<CustomActionFilter>();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation("On action executed");
           //logger.LogInformation($"On {context.ActionDescriptor.DisplayName} execute");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation("On action executing");
            //logger.LogInformation($"After {context.ActionDescriptor.DisplayName} execute");
        }
    }
}
