using System;
using System.Timers;

namespace TimerEvents
{
    class Program
    {
        private static void Main(string[] args)
        {
            EventsOnTimerDemo();
        }

        private static void EventsOnTimerDemo()
        {
            var myTimer = new Timer { Interval = 1000 };
            myTimer.Elapsed += Events.PrimaryEvent;
            myTimer.Elapsed += Events.SecondEvent;
            myTimer.Start();
            Console.ReadLine();
        }
    }
}