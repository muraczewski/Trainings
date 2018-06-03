using System;
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
            MusicPlayerDemo(MusicPlayerMode.Stream);
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
            MusicPlayer musicPlayer = new MusicPlayer();

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
    }
}