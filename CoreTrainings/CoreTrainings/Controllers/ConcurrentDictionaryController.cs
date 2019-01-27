using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace CoreTrainings.Controllers
{
    [Route("api/[controller]")]
    public class ConcurrentDictionaryController : Controller
    {
        private readonly ConcurrentDictionaryService _concurrentDictionaryService = ConcurrentDictionaryService.Instance;

        [HttpPost]
        [Route("quickAdd")]
        public async Task AddItemWithoutDelayAsync(int key, string value)
        {
            await _concurrentDictionaryService.AddItemAsync(key, value, 0);
        }

        [HttpPost]
        [Route("longAdd")]
        public async Task AddItemWithDelayAsync(int key, string value)
        {
            await _concurrentDictionaryService.AddItemAsync(key, value, CommonService.LongSleepTime);
        }

        [HttpPut]
        [Route("quickUpdate")]
        public async Task UpdateItemWithoutDelayAsync(int key, string oldValue, string newValue)
        {
            await _concurrentDictionaryService.UpdateItemAsync(key, oldValue, newValue, 0);
        }

        [HttpPut]
        [Route("longUpdate")]
        public async Task UpdateItemWithDelayAsync(int key, string oldValue, string newValue)
        {
            await _concurrentDictionaryService.UpdateItemAsync(key, oldValue, newValue, CommonService.LongSleepTime);
        }

        [HttpGet]
        public async Task<string> GetValueAndDeleteAsync(int key)
        {
            var result = await _concurrentDictionaryService.GetValueAndDeleteAsync(key, CommonService.LongSleepTime);
            return result;
        }
    }
}