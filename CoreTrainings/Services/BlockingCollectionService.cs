using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Services
{
    public static class BlockingCollectionService
    {
        private static BlockingCollection<string> participantWithLimit = new BlockingCollection<string>(3);

        public static async Task AddParticipantAsync(string participantName, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            participantWithLimit.Add(participantName);
        }

        public static async Task<BlockingCollection<string>> GetParticipantsAsync(int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            return participantWithLimit;
        }
    }
}