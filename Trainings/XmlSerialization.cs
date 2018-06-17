using System;
using System.IO;
using System.Xml.Serialization;

namespace Trainings
{
    public static class XmlSerialization
    {
        public static void Save(Person person, string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof(Person));

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream, person);
            }

            Console.WriteLine("Save succeed");
        }

        public static object Load(string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof(Person));

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                var person = xmlSerializer.Deserialize(fileStream);
                return person;
            }
        }
    }
}
