using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializationDemo();
        }

        private static void SerializationDemo()
        {
            var person = new Person(1, "Adam", "Nowak", 30, 80, 180);
            var cars = new List<Car>
            {
                new Car("ford", "mondeo", 2003),
                new Car("ford", "focus", 2006)
            };
            person.Cars = cars;

            XmlSerialization.SerializeToFile(person, "person.xml");
            var savedPerson = (Person)XmlSerialization.DeserializeFromFile("person.xml");
            Console.WriteLine($"Are the same objects (by reference)? {person == savedPerson}");
            Console.WriteLine($"Are the same objects (by value)? {person.ToString() == savedPerson.ToString()}");
            Console.ReadKey();
        }
    }
}
