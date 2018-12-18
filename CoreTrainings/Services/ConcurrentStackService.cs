using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Services
{
    public static class ConcurrentStackService
    {
        private static ConcurrentStack<string> stack;

        public static async Task AddElementAsync(string name, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            stack.Push(name);
        }

        public static async Task AddElementsAsync(string[] names, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            stack.PushRange(names);
        }

        public static async Task<string> GetLastElementAsync(int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            if (stack.TryPeek(out string lastElement))
            {
                return lastElement;
            }
            return string.Empty;
        }

        public static async Task<string> GetAndDeleteLastElementAsync(int sleepTime)
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
