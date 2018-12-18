using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace CoreTrainings.Controllers
{
    [Route("api/[controller]")]
    public class BlockingCollectionController : Controller
    {
        [HttpPost]
        [Route("quickAdd")]
        public async Task AddParticipanWithoutDelaytAsync(string participantName)
        {
            await BlockingCollectionService.AddParticipantAsync(participantName, 0);
        }

        [HttpPost]
        [Route("longAdd")]
        public async Task AddParticipantWithDelayAsync(string participantName)
        {
            await BlockingCollectionService.AddParticipantAsync(participantName, CommonService.LongSleepTime);
        }

        [HttpGet]
        public async Task<BlockingCollection<string>> GetParticipantsAsync()
        {
            var result = await BlockingCollectionService.GetParticipantsAsync(CommonService.LongSleepTime);
            return result;
        }
    }
}