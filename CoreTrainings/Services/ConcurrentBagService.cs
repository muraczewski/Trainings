using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Services
{
    public static class ConcurrentBagService
    {
        private static ConcurrentBag<string> participants;

        public static async Task AddParticipantAsync(string participantName, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);
            participants.Add(participantName);
        }

        public static  async Task<string> GetAndDeleteParticipantAsync(int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            if (participants.TryTake(out string participant))
            {
                return participant;
            }

            return string.Empty;
        }

        public static async Task<string> GetParticipantAsync(int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            if (participants.TryPeek(out string participant))
            {
                return participant;
            }

            return string.Empty;
        }
    }
}
