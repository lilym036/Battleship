using System;
using Battleship_Group10.Models;


namespace Battleship_Group10.Helpers
{

    internal class Message
    {
        public Message()
        {
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
    }

}

