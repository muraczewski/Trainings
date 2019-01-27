using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RestApiCoreTrainings.Filters
{
    public class LogAsyncExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<LogAsyncExceptionFilter> _logger;

        public LogAsyncExceptionFilter(ILogger<LogAsyncExceptionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            await Task.Run(() => _logger.LogError(context.Exception.Message));
        }
    }
}
