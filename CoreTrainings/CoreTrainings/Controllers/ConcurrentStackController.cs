using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace CoreTrainings.Controllers
{
    [Route("api/[controller]")]
    public class ConcurrentStackController : Controller
    {
        [HttpPost]
        [Route("quickAdd")]
        public async Task AddElementWithoutDelayAsync(string name)
        {
            await ConcurrentStackService.AddElementAsync(name, 0);
        }

        [HttpPost]
        [Route("longAdd")]
        public async Task AddElementWithDelayAsync(string name)
        {
            await ConcurrentStackService.AddElementAsync(name, CommonService.LongSleepTime);
        }

        [HttpPost]
        [Route("quickMultiAdd")]
        public async Task AddElementsWithoutDelayAsync(string[] names)
        {
            await ConcurrentStackService.AddElementsAsync(names, 0);
        }

        [HttpPost]
        [Route("longMultiAdd")]
        public async Task AddElementsWithDelayAsync(string[] names)
        {
            await ConcurrentStackService.AddElementsAsync(names, CommonService.LongSleepTime);
        }

        [HttpGet]
        [Route("lastElement")]
        public async Task<string> GetLastElementAsync()
        {
            var result = await ConcurrentStackService.GetLastElementAsync(CommonService.LongSleepTime);
            return result;
        }

        [HttpGet]
        [Route("lastElementWithDeleting")]
        public async Task<string> GetAndDeleteLastElementAsync()
        {
            var result = await ConcurrentStackService.GetAndDeleteLastElementAsync(CommonService.LongSleepTime);
            return result;
        }
    }
}