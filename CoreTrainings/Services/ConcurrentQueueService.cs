using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Services
{
    public static class ConcurrentQueueService
    {
        private static ConcurrentQueue<string> queue;

        public static async Task AddElementAsync(string name, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);
            queue.Enqueue(name);
        }

        public static async Task<string> GetFirstElementAsync(int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            if(queue.TryPeek(out string firstElement))
            {
                return firstElement;
            }

            return string.Empty;
        }

        public static async Task<string> GetAndDeleteFirstElementAsync(int sleepTime)
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
