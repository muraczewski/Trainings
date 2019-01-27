using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ConcurrentQueueService
    {
        private static ConcurrentQueueService _instance = null;
        private static readonly object m_Sync = new object();
        private static ConcurrentQueue<string> queue;

        private ConcurrentQueueService()
        {
        }

        public static ConcurrentQueueService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (m_Sync)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConcurrentQueueService();
                        }
                    }
                }

                return _instance;
            }
        }

        public async Task AddElementAsync(string name, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);
            queue.Enqueue(name);
        }

        public async Task<string> GetFirstElementAsync(int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            if(queue.TryPeek(out string firstElement))
            {
                return firstElement;
            }

            return string.Empty;
        }

        public async Task<string> GetAndDeleteFirstElementAsync(int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            if (queue.TryDequeue(out string firstElement))
            {
                return firstElement;
            }

            return string.Empty;
        }
    }
}
