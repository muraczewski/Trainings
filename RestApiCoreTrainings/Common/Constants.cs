using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Common
{
    public static class Constants
    {

        public static ICollection<char> ConsoleCorrectAnswers => new Collection<char> { 'y', 'Y', 'n', 'N' };

        public static ICollection<char> ConsolePositiveAnswers => new Collection<char> { 'y', 'Y' };

        public static ICollection<char> ConsoleNegativeAnswers => new Collection<char> { 'n', 'N' };

        public static int MaximumSqsMessageSize => 262144;

        public static List<string> Recipients = new List<string>
        {
            "grzegorz.muraczewski@gmail.com"
        };
    }
}
