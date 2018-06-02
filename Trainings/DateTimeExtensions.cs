using System;

namespace Trainings
{
    public static class DateTimeExtensions
    {
        public static bool IsWeekend(this DateTime @dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsFirstPartOfMonth(this DateTime @dateTime)
        {
            var numberOfDaysInCurrentMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

            return dateTime.Day < (numberOfDaysInCurrentMonth / 2);
        }
    }
}
