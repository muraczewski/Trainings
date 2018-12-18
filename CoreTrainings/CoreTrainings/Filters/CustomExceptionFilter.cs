using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CoreTrainings.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger logger;

        public CustomExceptionFilter(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<CustomExceptionFilter>();
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception.Message);
        }
    }
}
