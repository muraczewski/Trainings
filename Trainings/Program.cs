using System;

namespace Trainings
{
    class Program
    {
        static void Main(string[] args)
        {
            ExtensionMethodDemo();
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
    }
}
