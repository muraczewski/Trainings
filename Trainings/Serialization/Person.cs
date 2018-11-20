using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Serialization
{
    [Serializable]
    [XmlType("AnotherName")]
    public class Person
    {
        public Person()
        {
        }

        public Person(int id, string firstName, string surname, int age, int weight, int height)
        {
            Id = id;
            FirstName = firstName;
            Surname = surname;
            Age = age;
            Weight = weight;
            Height = height;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        [XmlIgnore]
        public int Age { get; set; }

        [XmlIgnore]
        public int Weight { get; set; }

        public int Height { get; set; }

        [XmlAttribute]
        public bool HasEmployment { get; set; }

        [XmlArray("MyCars")]
        [XmlArrayItem("Car")]
        public List<Car> Cars { get; set; }

    }

    public class Car
    {
        public Car()
        {
        }

        public Car(string manufacturer, string model, int year)
        {
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
        }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }
    }
}
