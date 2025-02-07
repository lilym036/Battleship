using System;
using System.Text;
using Battleship_Group10.Models;


namespace Battleship_Group10.Helpers
{

    internal static class Message
    {
        static Message()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public static void AnnounceWelcomeMessage()
        {
            Console.WriteLine("WELCOME TO ");

            string word = "BATTLESHIP!";
            string shipSymbol = "🚢";
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.White, ConsoleColor.Gray };

            // Print the word "Battleship" in different colors
            for (int i = 0; i < word.Length; i++)
            {
                Console.ForegroundColor = colors[i % colors.Length];
                Console.Write(word[i]);
            }

            // Print the boat symbol
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(shipSymbol);

            // Reset the color to default
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void AnnounceHits(Coordinate coordinate)
        {
            Console.WriteLine($"Your missile has HIT a ship at row {coordinate.X}, column {coordinate.Y}");
        }

        public static void AnnounceMisses(Coordinate coordinate)
        {
            Console.WriteLine($"Your missile has MISSED and landed in the ocean at row {coordinate.X}, column {coordinate.Y}");
        }

        public static void AnnounceInvalidCoordinate()
        {
            Console.WriteLine($"Invalid coordinate. Please enter two numbers 1 - {Grid.ROWS} in the format \"x y\".");
        }

        public static void AnnounceRepeatedTarget()
        {
            Console.WriteLine($"Coordinate has already been targeted. Please enter a new coordinate. Two numbers 1 - {Grid.ROWS} in the format \"x y\".");
        }

        public static void AnnounceUserGuessInput()
        {
            Console.WriteLine($"Please enter two numbers 1 - {Grid.ROWS} in the format \"x y\".");
        }

        public static void AnnounceGameOver(string winner)
        {
            Console.WriteLine($"GAME OVER! {winner}");
        }

        public static void AnnounceSunkShips(Player player)
        {
            Console.WriteLine($"{player.name} has sunk all the opponents's ships!");
        }
    }
}

