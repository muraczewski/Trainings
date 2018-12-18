using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public static class CommonService
    {
        public const int LongSleepTime = 20000;

        public static async Task WaitInThread(int sleepTime)
        {
            await Task.Run(() => Thread.Sleep(sleepTime));
        }
    }
}
