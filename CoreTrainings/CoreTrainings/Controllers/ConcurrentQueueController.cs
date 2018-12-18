using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace CoreTrainings.Controllers
{
    [Route("api/[controller]")]
    public class ConcurrentQueueController : Controller
    {
        [HttpPost]
        [Route("quickAdd")]
        public async Task AddElementWithoutDelayAsync(string name)
        {
            await ConcurrentQueueService.AddElementAsync(name, 0);
        }

        [HttpPost]
        [Route("longAdd")]
        public async Task AddElementWithDelayAsync(string name)
        {
            await ConcurrentQueueService.AddElementAsync(name, CommonService.LongSleepTime);
        }

        [HttpGet]
        [Route("FirstElement")]
        public async Task<string> GetFirstElementAsync()
        {
            var result = await ConcurrentQueueService.GetFirstElementAsync(CommonService.LongSleepTime);
            return result;
        }

        [HttpGet]
        [Route("FirstElementWithDeleting")]
        public async Task<string> GetAndDeleteFirstElementAsync()
        {
            var result = await ConcurrentQueueService.GetAndDeleteFirstElementAsync(CommonService.LongSleepTime);
            return result;
        }
    }
}