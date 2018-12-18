using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace CoreTrainings.Controllers
{
    [Route("api/[controller]")]
    public class ConcurrentBagController : Controller
    {
        [HttpPost]
        [Route("quickAdd")]
        public async Task AddParticipantWithoutDelayAsync(string participantName)
        {
            await ConcurrentBagService.AddParticipantAsync(participantName, 0);
        }

        [HttpPost]
        [Route("longAdd")]
        public async Task AddParticipantWithDelayAsync(string participantName)
        {
            await ConcurrentBagService.AddParticipantAsync(participantName, CommonService.LongSleepTime);
        }

        [HttpGet]
        public async Task<string> GetParticipantAsync()
        {
            var result = await ConcurrentBagService.GetParticipantAsync(CommonService.LongSleepTime);
            return result;
        }
    }
}