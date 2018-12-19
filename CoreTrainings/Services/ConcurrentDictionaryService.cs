using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ConcurrentDictionaryService
    {
        private static ConcurrentDictionaryService _instance = null;
        private static readonly object m_Sync = new object();
        private static ConcurrentDictionary<int, string> dictionary;

        private ConcurrentDictionaryService()
        {
        }

        public static ConcurrentDictionaryService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (m_Sync)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConcurrentDictionaryService();
                        }
                    }
                }

                return _instance;
            }
        }

        public async Task AddItemAsync(int key, string value, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            dictionary.TryAdd(key, value);
        }

        public async Task UpdateItemAsync(int key, string oldValue, string newValue, int sleepTime)
        {
            await CommonService.WaitInThread(sleepTime);

            dictionary.TryUpdate(key, newValue, oldValue);
        }

        public async Task<string> GetValueAndDeleteAsync(int key, int sleepTime)
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
