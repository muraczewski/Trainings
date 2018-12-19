using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Services
{
    public sealed class BlockingCollectionService
    {
        private static BlockingCollectionService _instance = null;
        private static readonly object m_Sync = new object();
        private static BlockingCollection<string> _participantWithLimit = null;

        private BlockingCollectionService()
        {
            _participantWithLimit = new BlockingCollection<string>(3);
        }

        public static BlockingCollectionService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (m_Sync)
                    {
                        if (_instance == null)
                        {
                            _instance = new BlockingCollectionService();
                        }
                    }
                }

                return _instance;
            }
        }



        public async Task AddParticipantAsync(string participantName, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            _participantWithLimit.Add(participantName);
        }

        public async Task<BlockingCollection<string>> GetParticipantsAsync(int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            return _participantWithLimit;
        }
    }
}