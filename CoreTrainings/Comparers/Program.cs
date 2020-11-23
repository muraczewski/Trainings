
using System;

namespace Comparers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CompareValueTypeDemo();
            CompareStringDemo();
            CompareReferenceTypeDemo();
            Console.ReadKey();
        }

        private static void CompareValueTypeDemo()
        {
            Console.WriteLine("Value types equality");
            var a = 1;
            var b = 1;
            var isEqual = a == b;
            Console.WriteLine($"Is a: {a} equal to b: {b}? {isEqual}");
            Console.WriteLine(Environment.NewLine);
        }

        private static void CompareStringDemo()
        {
            Console.WriteLine("String equality");
            var a1 = "a";
            var a2 = "a";
            var isEqual = a1 == a2;
            Console.WriteLine($"Is a1: {a1} equal to a2: {a2}? {isEqual}");
            Console.WriteLine(Environment.NewLine);
        }

        private static void CompareReferenceTypeDemo()
        {
            Console.WriteLine("Reference types equality");
            var dateNow = DateTime.Now;
            var person1 = new Person(1, dateNow);
            var person2 = new Person(1, dateNow);
            var person3 = person1;

            var areTwoSeparateInstancesEqual = person1 == person2;
            Console.WriteLine($"Is person1 equal to person2? {areTwoSeparateInstancesEqual}");

            /*
            var isCopyEqualToOriginal = person1 == person3;
            Console.WriteLine($"Is person1 equal to own copy in person3? {isCopyEqualToOriginal}");
            Console.WriteLine(Environment.NewLine);
            */

            var areTwoSeparateInstanceEqualWhenIEquatableImplemented = person1.Equals(person2);

            Console.WriteLine($"Is person1 equal to person 2 when IEquatable interface implemented? {areTwoSeparateInstanceEqualWhenIEquatableImplemented}");
        }
    }
}
