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

        const string word = "BATTLESHIP!";
        const string shipSymbol = "🚢";
        static ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.White, ConsoleColor.Gray };
        static ConsoleColor[] patrioticColors = { ConsoleColor.White, ConsoleColor.DarkRed, ConsoleColor.Red, ConsoleColor.DarkBlue, ConsoleColor.Blue, ConsoleColor.Cyan};

        static string[] flashyWelcomeMessage = new string[]
    {
        " __        __   _                            _          _   _ ",
        " \\ \\      / /__| | ___ ___  _ __ ___   ___  | |_ ___   | |_| |__   ___ ",
        "  \\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ | __/ _ \\  | __| '_ \\ / _ \\",
        "   \\ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) | | |_| | | |  __/",
        "    \\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|  \\__\\___/   \\__|_| |_|\\___|",
        " ",
        "  ____        _   _   _      _     _       _     _ ",
        " | __ )  __ _| |_| |_| | ___| |_  | | __ _| |__ | | ___  ___ ",
        " |  _ \\ / _` | __| __| |/ _ \\ __| | |/ _` | '_ \\| |/ _ \\/ __|",
        " | |_) | (_| | |_| |_| |  __/ |_  | | (_| | |_) | |  __/\\__ \\",
        " |____/ \\__,_|\\__|\\__|_|\\___|\\__| |_|\\__,_|_.__/|_|\\___||___/",
        " "
    };

        static string[] flashyWelcomeMessage2 = new string[]
    {
        "                (             )     *                      )                                 (          (        )  (    (     ",
        " (  (           )\\ )   (   ( /(   (  `           *   )  ( /(      (     (       *   )  *   ) )\\ )       )\\ )  ( /(  )\\ ) )\\ )  ",
        " )\\))(   ' (   (()/(   )\\  )\\())  )\\))(   (    ` )  /(  )\\())   ( )\\    )\\    ` )  /(` )  /((()/(  (   (()/(  )\\())(()/((()/(  ",
        "((_)()\\ )  )\\   /(_))(((_)((_\\  ((_)()\\  )\\    ( )(_))((_\\    )((_)((((_)(   ( )(_))( )(_))/(_)) )\\   /(_))((_\\  /(_))/(_)) ",
        "_(())\\_)()((_) (_))  )\\___  ((_) (_()((_)((_)  (_(_())   ((_)  ((_)_  )\\ _ )\\ (_(_())(_(_())(_))  ((_) (_))   _((_)(_)) (_))   ",
        "\\ \\((_)/ /| __|| |  ((/ __|/ _ \\ |  \\/  || __| |_   _|  / _ \\   | _ ) (_)_\\(_)|_   _||_   _|| |   | __|/ __| | || ||_ _|| _ \\  ",
        " \\ \\/\\/ / | _| | |__ | (__| (_) || |\\/| || _|    | |   | (_) |  | _ \\  / _ \\    | |    | |  | |__ | _| \\__ \\ | __ | | | |  _/  ",
        "  \\_/\\_/  |___||____| \\___|\\___/ |_|  |_||___|   |_|    \\___/   |___/ /_/ \\_\\   |_|    |_|  |____||___||___/ |_||_||___||_|    ",
        "                                                                                                                               "
    };
        static string waterGraphic = @"
 ~~~~~~~  ~~~~~~~  ~~~~~~~  ~~~~~~~  ~~~~~~~  ~~~~~~~  ~~~~~~~  ~~~~~~~  ~~~~~~~  ~~~~~~~
";

        internal static void AnnounceFlashyWelcome()
        {
            foreach (string line in flashyWelcomeMessage2)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    Console.ForegroundColor = patrioticColors[i % patrioticColors.Length];
                    Console.Write(line[i]);
                }
                Console.WriteLine();
            }

            // Print the ship symbol
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(shipSymbol);

            // Print the water graphic
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(waterGraphic);

            // Reset the color to default
            Console.ResetColor();
        }

        public static void AnnounceWelcomeMessage()
        {
            StringBuilder welcomeMessage = new StringBuilder("WELCOME TO ");

            // Append the word "Battleship" in different colors
            for (int i = 0; i < word.Length; i++)
            {
                Console.ForegroundColor = colors[i % colors.Length];
                welcomeMessage.Append(word[i]);
            }

            // Append the boat symbol
            Console.ForegroundColor = ConsoleColor.White;
            welcomeMessage.Append(shipSymbol);

            // Print the entire message in one line
            Console.WriteLine(welcomeMessage.ToString());

            // Reset the color to default
            Console.ResetColor();
        }


        /*
        internal static void AnnounceFlashyWelcome()
        {
            foreach (string line in flashyWelcomeMessage2)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    Console.ForegroundColor = patrioticColors[i % patrioticColors.Length];
                    Console.Write(line[i]);
                }
                Console.WriteLine();
            }

            // Print the ship symbol
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(shipSymbol);

            // Reset the color to default
            Console.ResetColor();

        }
        
        public static void AnnounceWelcomeMessage()
        {
            Console.WriteLine("WELCOME TO ");

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
        */

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
            Console.WriteLine("     This is a strategic guessing game. Battleships will be");
            Console.WriteLine("     placed on a grid and Players will fire missiles at the enemy grid. ");
            Console.WriteLine("     Player wins when they have found and SUNK all enemy ships. ");
            Console.WriteLine("     Do you have what it takes to DESTROY. THEM. ALL?!?");
            Console.WriteLine();
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

        internal static void AnnouncePlayAgain()
        {
            Console.WriteLine("  Play again press [Enter] |or| quit press [Escape]?");
        }

        internal static void AnnounceRestart()
        {
            Console.WriteLine("Restarting game...");
        }

        internal static void AnnounceExit()
        {
            Console.WriteLine("Exiting game...");
        }

        internal static void AnnouncePressEnterOrExit()
        {
            Console.WriteLine("  Press [Escape] at any time to close the game.");
            Console.WriteLine();
            Console.WriteLine("  Press [Enter] to begin...");
        }

    }
}

