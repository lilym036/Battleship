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
    }

}

