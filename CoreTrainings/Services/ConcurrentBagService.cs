using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ConcurrentBagService
    {
        private static ConcurrentBagService _instance = null;
        private static readonly object m_Sync = new object();
        private static ConcurrentBag<string> _participants;

        private ConcurrentBagService()
        {
        }

        public static ConcurrentBagService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (m_Sync)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConcurrentBagService();
                        }
                    }
                }

                return _instance;
            }
        }

        public async Task AddParticipantAsync(string participantName, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);
            _participants.Add(participantName);
        }

        public async Task<string> GetAndDeleteParticipantAsync(int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            if (_participants.TryTake(out string participant))
            {
                return participant;
            }

            return string.Empty;
        }

        public async Task<string> GetParticipantAsync(int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            if (_participants.TryPeek(out string participant))
            {
                return participant;
            }

            return string.Empty;
        }
    }
}
