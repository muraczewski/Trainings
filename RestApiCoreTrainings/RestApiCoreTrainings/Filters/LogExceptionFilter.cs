using Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace RestApiCoreTrainings.Filters
{
    public class LogExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<LogExceptionFilter> _logger;

        public LogExceptionFilter(ILogger<LogExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is HttpCustomException exception)
            {
                var a = exception.ToString();
                _logger.LogError(exception, $"{exception.Message} Status code: {exception.StatusCode}", exception.StatusCode);
            }
            else
            {
                _logger.LogError(context.Exception, context.Exception.Message);
            }
        }
    }
}