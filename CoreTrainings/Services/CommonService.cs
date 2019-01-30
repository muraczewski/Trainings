using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public static class CommonService
    {
        public const int LongSleepTime = 10000;

        public static Task WaitInThread(int sleepTime)
        {
            return Task.Delay(sleepTime);

        }
    }
}
