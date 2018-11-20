using System;
using System.IO;
using Trainings;

namespace MusicPlayer
{
    class Program
    {
        private static void Main(string[] args)
        {
            MusicPlayerDemo(MusicPlayerMode.Mp3);
            //MusicPlayerDemo(MusicPlayerMode.Bytes);
            //MusicPlayerDemo(MusicPlayerMode.Stream);
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