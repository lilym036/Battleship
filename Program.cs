using System;
using Battleship_Group10.Controllers;
using NAudio.Wave;
using System.Threading.Tasks;

namespace Battleship_Group10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Path to the wav file (ensure the path is correct)
            string filePath = "battleship-music.wav";

            // Start playing the sound asynchronously in the background
            Task.Run(() => PlaySoundInBackground(filePath, 0.1f)); // Set volume to 10%

            // Initialize the game controller while the sound is playing
            var gc = new GameController();
            gc.Initialize();

            // Keep the application running, waiting for user input to close
            Console.WriteLine("Game is initializing... Press Enter to exit.");
            Console.ReadLine();
        }

        // Method to play sound in the background
        public static void PlaySoundInBackground(string filePath, float volume)
        {
            // Create a WaveOutEvent object to play the sound
            using (var audioFile = new AudioFileReader(filePath))
            using (var volumeStream = new WaveChannel32(audioFile) { Volume = volume })
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(volumeStream);
                outputDevice.Play();

                // Keep the audio playing until it finishes
                //while (outputDevice.PlaybackState == PlaybackState.Playing)

                // Loop the audio playback
                // Loop the audio playback indefinitely without stopping and restarting
                while (true)
                {
                    // If the playback is stopped, reset the position to the start and continue playing
                    if (outputDevice.PlaybackState == PlaybackState.Stopped)
                    {
                        audioFile.Position = 0;  // Reset to the beginning of the audio file
                        outputDevice.Play();     // Resume playback
                    }
                }
                    {
                    // Optionally add a delay here to prevent busy-waiting
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
    }
}
