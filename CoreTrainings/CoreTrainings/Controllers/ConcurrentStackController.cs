using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace CoreTrainings.Controllers
{
    [Route("api/[controller]")]
    public class ConcurrentStackController : Controller
    {
        private readonly ConcurrentStackService _concurrentStackService = ConcurrentStackService.Instance;

        [HttpPost]
        [Route("quickAdd")]
        public async Task AddElementWithoutDelayAsync(string name)
        {
            await _concurrentStackService.AddElementAsync(name, 0);
        }

        [HttpPost]
        [Route("longAdd")]
        public async Task AddElementWithDelayAsync(string name)
        {
            await _concurrentStackService.AddElementAsync(name, CommonService.LongSleepTime);
        }

        [HttpPost]
        [Route("quickMultiAdd")]
        public async Task AddElementsWithoutDelayAsync(string[] names)
        {
            await _concurrentStackService.AddElementsAsync(names, 0);
        }

        [HttpPost]
        [Route("longMultiAdd")]
        public async Task AddElementsWithDelayAsync(string[] names)
        {
            await _concurrentStackService.AddElementsAsync(names, CommonService.LongSleepTime);
        }

        [HttpGet]
        [Route("lastElement")]
        public async Task<string> GetLastElementAsync()
        {
            var result = await _concurrentStackService.GetLastElementAsync(CommonService.LongSleepTime);
            return result;
        }

        [HttpGet]
        [Route("lastElementWithDeleting")]
        public async Task<string> GetAndDeleteLastElementAsync()
        {
            var result = await _concurrentStackService.GetAndDeleteLastElementAsync(CommonService.LongSleepTime);
            return result;
        }
    }
}