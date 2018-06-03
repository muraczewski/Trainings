using System;
using System.Timers;

namespace Trainings
{
    public static class Events
    {
        public static void SecondEvent(object sender, ElapsedEventArgs e)
        {
            if (e.SignalTime.Second % 5 == 0)
            {
                Console.Beep(440, 500);
            }
        }

        public static void PrimaryEvent(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(e.SignalTime);
        }
    }
}
