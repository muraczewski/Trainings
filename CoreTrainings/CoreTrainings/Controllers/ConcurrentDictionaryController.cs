using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace CoreTrainings.Controllers
{
    [Route("api/[controller]")]
    public class ConcurrentDictionaryController : Controller
    {
        [HttpPost]
        [Route("quickAdd")]
        public async Task AddItemWithoutDelayAsync(int key, string value)
        {
            await ConcurrentDictionaryService.AddItemAsync(key, value, 0);
        }

        [HttpPost]
        [Route("longAdd")]
        public async Task AddItemWithDelayAsync(int key, string value)
        {
            await ConcurrentDictionaryService.AddItemAsync(key, value, CommonService.LongSleepTime);
        }

        [HttpPut]
        [Route("quickUpdate")]
        public async Task UpdateItemWihtoutDelayAsync(int key, string oldValue, string newValue)
        {
            await ConcurrentDictionaryService.UpdateItemAsync(key, oldValue, newValue, 0);
        }

        [HttpPut]
        [Route("longUpdate")]
        public async Task UpdateItemWihtDelayAsync(int key, string oldValue, string newValue)
        {
            await ConcurrentDictionaryService.UpdateItemAsync(key, oldValue, newValue, CommonService.LongSleepTime);
        }

        [HttpGet]
        public async Task<string> GetValueAndDeleteAsync(int key)
        {
            var result = await ConcurrentDictionaryService.GetValueAndDeleteAsync(key, CommonService.LongSleepTime);
            return result;
        }
    }
}