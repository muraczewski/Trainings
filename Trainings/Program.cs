using System;
using System.Media;
using System.Timers;

namespace Trainings
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExtensionMethodDemo();
            EventsOnTimerDemo();
        }

        static void ExtensionMethodDemo()
        {
            var today = DateTime.Today;

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine($"Today is {today.DayOfWeek} {today.Day} of month. Is it weekend: {today.IsWeekend()}. Is it first part of month: {today.IsFirstPartOfMonth()}");
                today = today.AddDays(8);
            }
            Console.ReadLine();
        }

        static void EventsOnTimerDemo()
        {
            var myTimer = new Timer();
            myTimer.Interval = 1000;
            myTimer.Elapsed += PrimaryEvent;
            myTimer.Elapsed += SecondEvent;
            myTimer.Start();
            Console.ReadLine();
        }

        private static void SecondEvent(object sender, ElapsedEventArgs e)
        {
            if (e.SignalTime.Second % 5 == 0)
            {
                Console.Beep(440, 500);
            }
        }

        private static void PrimaryEvent(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(e.SignalTime);
        }
    }
}
