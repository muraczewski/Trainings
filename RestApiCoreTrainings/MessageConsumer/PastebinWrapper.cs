using PastebinAPI;

namespace MessageConsumer
{
    class PastebinWrapper
    {
        public PastebinWrapper()
        {
            Pastebin.DevKey = "8bb7b8fdda2ffda487d119edb9751634";   // TODO  move to config
        }

        public Paste CreateBin(string message)
        {
            var user = Pastebin.LoginAsync("grzegorzmuraczewski", "password").Result; // TODO move to config

            var paste = user.CreatePasteAsync(message).Result;

            return paste;
        }
    }
}
