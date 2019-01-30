using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace CoreTrainings.Controllers
{
    [Route("api/[controller]")]
    public class ConcurrentBagController : Controller
    {
        private readonly ConcurrentBagService _concurrentBagService = ConcurrentBagService.Instance;

        [HttpPost]
        [Route("quickAdd")]
        public Task AddParticipantWithoutDelayAsync(string participantName)
        {
            return _concurrentBagService.AddParticipantAsync(participantName, 0);
        }

        [HttpPost]
        [Route("longAdd")]
        public async Task AddParticipantWithDelayAsync(string participantName)
        {
            await _concurrentBagService.AddParticipantAsync(participantName, CommonService.LongSleepTime);
        }

        [HttpGet]
        public async Task<string> GetParticipantAsync()
        {
            var result = await _concurrentBagService.GetParticipantAsync(CommonService.LongSleepTime);
            return result;
        }
    }
}