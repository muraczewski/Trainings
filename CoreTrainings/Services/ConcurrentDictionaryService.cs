using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Services
{
    public static class ConcurrentDictionaryService
    {
        private static ConcurrentDictionary<int, string> dictionary;

        public static async Task AddItemAsync(int key, string value, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            dictionary.TryAdd(key, value);
        }

        public static async Task UpdateItemAsync(int key, string oldValue, string newValue, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            dictionary.TryUpdate(key, newValue, oldValue);
        }

        public static async Task<string> GetValueAndDeleteAsync(int key, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            if (dictionary.TryRemove(key, out string value))
            {
                return value;
            }

            return string.Empty;
        }
    }
}
