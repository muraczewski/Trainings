using System;

namespace DesignPatterns.Singleton
{
    public class SingletonWithDoubleCheck
    {
        private static SingletonWithDoubleCheck _instance;
        private static readonly object Lock = new object();
        private static int _count;

        public static SingletonWithDoubleCheck GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SingletonWithDoubleCheck();
                        }
                    }
                }

                return _instance;
            }
        }

        private SingletonWithDoubleCheck()
        {
            _count++;
            Console.WriteLine($"Number of instances: {_count}");
        }

        public int GetCount => _count;
    }
}
