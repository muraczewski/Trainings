using PastebinAPI;
using System.Threading.Tasks;

namespace MessageConsumer
{
    class PastebinWrapper
    {
        public PastebinWrapper()
        {
            Pastebin.DevKey = "8bb7b8fdda2ffda487d119edb9751634";   // TODO  move to config
        }

        public async Task<Paste> CreateBinAsync(string message)
        {
            var user = await Pastebin.LoginAsync("grzegorzmuraczewski", "password"); // TODO move to config

            var paste = await user.CreatePasteAsync(message);

            return paste;
        }
    }
}
