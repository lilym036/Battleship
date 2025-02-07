using System;
using Battleship_Group10.Controllers;
using NAudio.Wave;
using System.Threading.Tasks;

namespace Battleship_Group10
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to the wav file (ensure the path is correct)
            string filePath = "battleship-music.wav";

            // Start playing the sound asynchronously in the background
            Task.Run(() => PlaySoundInBackground(filePath));

            // Initialize the game controller while the sound is playing
            var gc = new GameController();
            gc.Initialize();

            // Keep the application running, waiting for user input to close
            Console.WriteLine("Game is initializing... Press Enter to exit.");
            Console.ReadLine();
        }

        // Method to play sound in the background
        static void PlaySoundInBackground(string filePath)
        {
            // Create a WaveOutEvent object to play the sound
            using (var audioFile = new AudioFileReader(filePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                // Keep the audio playing until it finishes
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    // Optionally add a delay here to prevent busy-waiting
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
    }
}
