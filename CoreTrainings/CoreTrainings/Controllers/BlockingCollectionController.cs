using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace CoreTrainings.Controllers
{
    [Route("api/[controller]")]
    public class BlockingCollectionController : Controller
    {
        private readonly BlockingCollectionService _blockingCollectionService = BlockingCollectionService.Instance;

        [HttpPost]
        [Route("quickAdd")]
        public async Task AddParticipantWithoutDelayAsync(string participantName)
        {
            await _blockingCollectionService.AddParticipantAsync(participantName, 0);
        }

        [HttpPost]
        [Route("longAdd")]
        public async Task<IActionResult> AddParticipantWithDelayAsync(string participantName)
        {
            await _blockingCollectionService.AddParticipantAsync(participantName, CommonService.LongSleepTime);
            return NoContent();
        }

        [HttpGet]
        public async Task<BlockingCollection<string>> GetParticipantsAsync()
        {
            var result = await _blockingCollectionService.GetParticipantsAsync(CommonService.LongSleepTime);
            return result;
        }
    }
}