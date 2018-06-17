using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

namespace Trainings
{
    class Program
    {
        private static void Main(string[] args)
        {
            //ExtensionMethodDemo();
            //EventsOnTimerDemo();
            //MusicPlayerDemo(MusicPlayerMode.Mp3);
            //MusicPlayerDemo(MusicPlayerMode.Bytes);
            //MusicPlayerDemo(MusicPlayerMode.Stream);
            SerializationDemo();
        }

        private static void ExtensionMethodDemo()
        {
            var today = DateTime.Today;

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine($"Today is {today.DayOfWeek} {today.Day} of month. Is it weekend: {today.IsWeekend()}. Is it first part of month: {today.IsFirstPartOfMonth()}");
                today = today.AddDays(8);
            }
            Console.ReadLine();
        }

        private static void EventsOnTimerDemo()
        {
            var myTimer = new Timer();
            myTimer.Interval = 1000;
            myTimer.Elapsed += Events.PrimaryEvent;
            myTimer.Elapsed += Events.SecondEvent;
            myTimer.Start();
            Console.ReadLine();
        }

        private static void MusicPlayerDemo(MusicPlayerMode musicPlayerMode)
        {
            const string fileName = "simple_audio.mp3";
            IMusicPlayer musicPlayer = new MusicPlayer();

            switch (musicPlayerMode)
            {
                case MusicPlayerMode.Mp3:
                    musicPlayer = new MusicPlayer(fileName);
                    break;
                case MusicPlayerMode.Bytes:
                    var bytes = File.ReadAllBytes(fileName);
                    musicPlayer = new MusicPlayer(bytes);
                    break;
                case MusicPlayerMode.Stream:
                    var stream = File.OpenRead(fileName);
                    musicPlayer = new MusicPlayer(stream);
                    break;
            }

            var delay = 4000;

            musicPlayer.Play();
            System.Threading.Thread.Sleep(delay);
            musicPlayer.Pause();
            System.Threading.Thread.Sleep(delay);
            musicPlayer.Play();
            System.Threading.Thread.Sleep(delay);
            musicPlayer.Stop();
            System.Threading.Thread.Sleep(delay);
            musicPlayer.Play();
            System.Threading.Thread.Sleep(delay);
            musicPlayer.Closed();
            Console.ReadKey();
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

            XmlSerialization.Save(person, "person.xml");
            var savedPerson = (Person)XmlSerialization.Load("person.xml");
            Console.WriteLine($"Are the same objects (by reference)? {person == savedPerson}");
            Console.WriteLine($"Are the same objects (by value)? {person.ToString() == savedPerson.ToString()}" );
            Console.ReadKey();
        }
    }
}