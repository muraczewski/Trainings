using System;
using System.Threading;

namespace DesignPatterns.MutexDemo
{
    public class Program
    {
        public static void Main()
        {
            const string mutexName = "MyMutex";

            var isSucceed = Mutex.TryOpenExisting(mutexName, out var myMutex);

            if (isSucceed)
            {
                //myMutex.Close();
                return;
            }

            myMutex = new Mutex(true, mutexName);

            Console.ReadKey(); 
        }
    }
}
