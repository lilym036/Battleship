using System;
using System.Text;
using Battleship_Group10.Models;
using Battleship_Group10.Controllers;


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
            Console.WriteLine($"Invalid coordinate.");
            AnnounceUserGuessInput();
        }

        public static void AnnounceRepeatedTarget()
        {
            Console.WriteLine($"Coordinate has already been targeted.");
            AnnounceUserGuessInput();
        }

        public static void AnnounceUserGuessInput()
        {
            Console.WriteLine($"Please enter firing coordinate in the format \"A4\". Letters A-{GameController.MAX_ROW_LETTER} and numbers 1 - {Grid.ROWS}.");
        }

        public static void AnnounceGameOver(Player winner)
        {
            Console.WriteLine($"GAME OVER! {winner.name} wins!");
        }

        internal static void AnnounceInstructions()
        {
            Console.WriteLine();
            Console.WriteLine("  This game is all about strategic guesses. Battleships will be");
            Console.WriteLine("  placed on a grid and Players will fire missiles at the enemy grid,");
            Console.WriteLine("  aiming to find and sink ALL enemy ships. Player wins when all enemy");
            Console.WriteLine("  ships have been sunk. Do you have what it takes to DESTROY. THEM. ALL?!?");
            Console.WriteLine();
            Console.WriteLine("  Press [escape] at any time to close the game.");
            Console.WriteLine();
            Console.WriteLine("  Press [enter] to begin...");
        }


        public static void AnnounceSunkShips(Player player)
        {
            Console.WriteLine($"{player.name} has sunk all the opponent's ships!");
        }

        // Announces when a single ship has been sunk with ship type
        public static void AnnounceSunkShip(Player winning, Player losing, Ship.ShipType shipType)
        {
            Console.WriteLine($"{winning.name} has sunk {losing.name}'s {shipType}!");
        }
    }
}

