namespace DesignPatterns.Singleton
{
    public class Singleton
    {
        private static Singleton _instance;
        private static int _count;

        public static Singleton GetInstance => _instance ?? (_instance = new Singleton());

        private Singleton()
        {
            _count++;
        }

        public int GetCount => _count;
    }
}
