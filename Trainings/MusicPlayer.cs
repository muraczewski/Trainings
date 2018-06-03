using System;
using System.IO;
using NAudio.Wave;
using NLayer.NAudioSupport;

namespace Trainings
{
    public class MusicPlayer : IMusicPlayer, IDisposable
    {
        private readonly Mp3FileReader _mp3FileReader;
        private readonly WaveOut _waveOut;
        private readonly Stream _stream;

        public MusicPlayer()
        {
        }

        public MusicPlayer(string fileName)
        {
            var builder = new Mp3FileReader.FrameDecompressorBuilder(wf => new Mp3FrameDecompressor(wf));
            _mp3FileReader = new Mp3FileReader(fileName, builder);
            _waveOut = new WaveOut();
            _waveOut.Init(_mp3FileReader);
        }

        public MusicPlayer(byte[] fileBytes)
        {
            var builder = new Mp3FileReader.FrameDecompressorBuilder(wf => new Mp3FrameDecompressor(wf));
            _stream = new MemoryStream(fileBytes);
            _mp3FileReader = new Mp3FileReader(_stream, builder);
            _waveOut = new WaveOut();
            _waveOut.Init(_mp3FileReader);
        }

        public MusicPlayer(Stream stream)
        {
            var builder = new Mp3FileReader.FrameDecompressorBuilder(wf => new Mp3FrameDecompressor(wf));
            _mp3FileReader = new Mp3FileReader(stream, builder);
            _waveOut = new WaveOut();
            _waveOut.Init(_mp3FileReader);
        }

        public void Play()
        {
            _waveOut.Play();
            Console.Clear();
            Console.WriteLine("odtwarzanie");
        }

        public void Stop()
        {
            _waveOut.Stop();
            Console.Clear();
            Console.WriteLine("stop");
            _mp3FileReader.Position = 0;
        }

        public void Pause()
        {
            Console.Clear();
            Console.WriteLine("pauza");
            _waveOut.Stop();
        }

        public void Closed()
        {
            Dispose();
            Console.Clear();
            Console.WriteLine("zamknięcie odtwarzacza");
        }

        public void Dispose()
        {
            _waveOut.Dispose();
            _mp3FileReader.Dispose();
            _stream?.Dispose();
        }
    }
}