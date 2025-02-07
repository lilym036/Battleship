using System;
using Battleship_Group10.Models;
using Battleship_Group10.Controllers;
using Battleship_Group10.Helpers;

namespace Battleship_Group10.Controllers
{
    public class DisplayController
    {
        static (int BufferHeight, int BufferWidth, int WindowHeight, int WindowWidth) ConsoleSize() =>
    (Console.BufferHeight, Console.BufferWidth, Console.WindowHeight, Console.WindowWidth);

        private static (int BufferHeight, int BufferWidth, int WindowHeight, int WindowWidth) consoleSize = ConsoleSize();

        public DisplayController()
        {
        }

        internal static void DisplayGrid(Grid gameGrid)
        {
            ConfigureConsole();
            Console.SetCursorPosition(0, 0);

            Message.AnnounceWelcomeMessage();
            Message.AnnounceInstructions();
            Console.WriteLine();


            // Print the column headers
            Console.WriteLine();
            Console.Write(" ");
            for (int col = 1; col <= gameGrid.Width; col++)
            {
                Console.Write($" {col} ");
            }

            // Print the game grid
            for (int row = 0; row < gameGrid.Height * 2 + 1; row++)
            {
                // Print the row header
                if (row != 0 && row % 2 == 0)
                {
                    Console.Write($"{(char)('A' + (row - 1) / 2)} ");
                }
                else
                {
                    Console.Write("  ");
                }

                int gridRow = (row - 1) / 2;
                Console.WriteLine();

                for (int col = 0; col < gameGrid.Width * 2 + 1; col++)
                {
                    int gridCol = (col - 1) / 2;
                    //Check whether to show enemy ships; check list of ships on gameGrid, if any ship IsSunk, show it.
                    var shipAtPosition = gameGrid.Ships.FirstOrDefault(ship => ship.Positions.Any(pos => pos.Coordinate.X == gridRow && pos.Coordinate.Y == gridCol));
                    if ((shipAtPosition != null && shipAtPosition.IsSunk()) && (((row -1) % 2 == 0 || (row - 1) % 2 == 1) && (col - 1) % 2 == 0))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    Console.Write(DisplayGridCell(row, col, gameGrid));
                    if (Console.BackgroundColor is not ConsoleColor.Black)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }

                }
            }
            Console.WriteLine();
        }

        static private string DisplayGridCell(int row, int col, Grid grid)
        {
            string hit = "##";
            string miss = "XX";
            string ocean = "  ";
            const int w = Grid.COLUMNS * 2;
            const int h = Grid.ROWS * 2;

            return (row, col, row % 2, col % 2) switch
            {
                (0, 0, _, _) => "┌",
                (h, 0, _, _) => "└",
                (0, w, _, _) => "┐",
                (h, w, _, _) => "┘",
                (0, _, 0, 0) => "┬",
                (_, 0, 0, 0) => "├",
                (_, w, 0, _) => "┤",
                (h, _, _, 0) => "┴",
                (_, _, 0, 0) => "┼",
                (_, _, 1, 0) => "│",
                (_, _, 0, 1) => "──",
                _ => grid.Positions[(row -1)/2, (col-1)/2].Status switch
                {
                    Status.Hit => hit,
                    Status.Miss => miss,
                    Status.Water => ocean,
                    Status.Ship => ocean
                }
            };
        }

        internal static void ConfigureConsole()
        {
            Console.CursorVisible = false;
            if (OperatingSystem.IsWindows() && Console.BufferHeight != Console.WindowHeight)
            {
                Console.BufferHeight = Console.WindowHeight;
            }
            if (OperatingSystem.IsWindows() && Console.BufferWidth != Console.WindowWidth)
            {
                Console.BufferWidth = Console.WindowWidth;
            }
            if (consoleSize != ConsoleSize())
            {
                Console.Clear();
                consoleSize = ConsoleSize();
            }

            Console.SetCursorPosition(0, 0);
            Console.WriteLine();
        }

    }
}/*
  A couple corrections (and I only want to see the relevant changes):
1. The messages when a ship has been sunk or hit disappear too quickly. How do I keep the console clear and re-prompt for input, and only clear the prior message when they enter new input
2. Can we keep the Welcome message and instructions pinned to the top, then have the board repopulate right below it with a line or two in between?
3. */