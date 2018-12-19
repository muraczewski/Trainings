using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ConcurrentStackService
    {
        private static ConcurrentStackService _instance = null;
        private static readonly object m_Sync = new object();
        private static ConcurrentStack<string> stack;

        private ConcurrentStackService()
        {
        }

        public static ConcurrentStackService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (m_Sync)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConcurrentStackService();
                        }
                    }
                }

                return _instance;
            }
        }

        public async Task AddElementAsync(string name, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            stack.Push(name);
        }

        public async Task AddElementsAsync(string[] names, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            stack.PushRange(names);
        }

        public async Task<string> GetLastElementAsync(int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            if (stack.TryPeek(out string lastElement))
            {
                return lastElement;
            }
            return string.Empty;
        }

        public async Task<string> GetAndDeleteLastElementAsync(int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            if (stack.TryPop(out string lastElement))
            {
                return lastElement;
            }

            return string.Empty;
        }
    }
}
