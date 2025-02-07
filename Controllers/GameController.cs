using System;
using System.Globalization;
using Battleship_Group10.Helpers;
using Battleship_Group10.Models;
using static System.Formats.Asn1.AsnWriter;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Battleship_Group10.Controllers
{
    internal class GameController
    {
        private Player humanPlayer;
        private Player computerPlayer;
        private Grid gameGrid;
        private bool isGameOver = false;
        public const char MIN_ROW_LETTER = 'A';
        public const char MAX_ROW_LETTER = (char)('A' + Grid.ROWS - 1);


        public GameController() 
        {
            this.gameGrid = new Grid();

            // MVP: Human player has no grid, computer player has a grid
            this.humanPlayer = new Player(Player.PlayerType.Human);
            this.computerPlayer = new Player(Player.PlayerType.Computer, gameGrid);
        }
        
        public void Initialize()
        {
            Console.Clear();
            Message.AnnounceFlashyWelcome();
            Task.Run(() => Program.PlaySoundInBackground("WELCOME.wav", 1.0f));
            Message.AnnounceWelcomeMessage();
            Message.AnnounceInstructions();
            Message.AnnouncePressEnterOrExit();
            checkStartOrExit();

            Console.Clear();
            humanPlayer.AskPlayerName();
            PlayGame();
        }

        private void PlayGame()
        {
            // Display the game grid
            DisplayController.DisplayGrid(gameGrid);

            // Loop through the game until the game is over
            while (!isGameOver)
            {
                // Fire missile
                FireMissile();
                // Display the game grid
                DisplayController.DisplayGrid(gameGrid);
            }
            if (isGameOver)
            {
                Message.AnnouncePlayAgain();
                checkRestartOrExit();
            }
        }

        private void checkRestartOrExit()
        {
            bool validInput = false;
            while (!validInput)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Message.AnnounceRestart();
                        ResetGame();
                        Initialize();
                        validInput = true;
                        break;
                    case ConsoleKey.Escape:
                        Message.AnnounceExit();
                        validInput = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void checkStartOrExit()
        {
            bool validInput = false;
            while (!validInput)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        validInput = true;
                        break;
                    case ConsoleKey.Escape:
                        Message.AnnounceExit();
                        validInput = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void ResetGame()
        {
            this.gameGrid = new Grid();
            this.humanPlayer = new Player(Player.PlayerType.Human);
            this.computerPlayer = new Player(Player.PlayerType.Computer, gameGrid);
            this.isGameOver = false;
        }


        private void FireMissile()
        {
            // Get the coordinate from the user
            Coordinate target = UserGuessInput();

            // Check the status of the targeted coordinate
            CheckPosition(target);

            // Display the game grid
            DisplayController.DisplayGrid(gameGrid);
        }

        public void CheckGameOver()
        {
            //check if humanPlayer's grid is not null and all their ships are sunk
            if (humanPlayer.grid != null && humanPlayer.grid.Ships.All(ship => ship.IsSunk()))
            {
                Message.AnnounceSunkShips(computerPlayer);
                Message.AnnounceGameOver(computerPlayer);
                isGameOver = true;
            }
            //check if computerPlayer's grid is not null and all their ships are sunk
            else if (computerPlayer.grid != null && computerPlayer.grid.Ships.All(ship => ship.IsSunk()))
            {
                Task.Run(() => Program.PlaySoundInBackground("GAME_WON.wav", 1.0f));
                Message.AnnounceSunkShips(humanPlayer);
                Message.AnnounceGameOver(humanPlayer);
                Message.AnnounceFlashyGameOver();
                Message.AnnounceFlashyWin();
                isGameOver = true;
            }
            else
            {
                isGameOver = false;
            }
        }

        private void CheckPosition(Coordinate target)
        {
            // Get the position of the targeted coordinate
            Position position = gameGrid.Positions[target.X, target.Y];

            // Check the status of the position
            switch (position.Status)
            {
                case Status.Water:
                    Task.Run(() => Program.PlaySoundInBackground("WATER_SPLASH.wav", 0.5f));
                    Task.Run(() => Program.PlaySoundInBackground("MISSED.wav", 1.0f));
                    Message.AnnounceMisses(target);
                    //DisplayMessageAndWait();
                    position.Status = Status.Miss;
                    // Display the game grid
                    DisplayController.DisplayGrid(gameGrid);
                    break;
                
                case Status.Ship:
                    Task.Run(() => Program.PlaySoundInBackground("EXPLOSION.wav", 0.5f));
                    Task.Run(() => Program.PlaySoundInBackground("HIT_SHIP.wav", 1.0f));
                    Message.AnnounceHits(target);
                    //DisplayMessageAndWait();
                    position.Status = Status.Hit;
                    // Check if the ship is sunk and if Game is over
                    Ship? ship = GetShipAtPosition(target);
                    CheckSunkShip(ship);
                    CheckGameOver();
                    // Display the game grid
                    DisplayController.DisplayGrid(gameGrid);
                    break;
            }
        }

        private void DisplayMessageAndWait()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }

        private void CheckSunkShip(Ship? ship)
        {
            if (ship != null && ship.IsSunk())
            {
                // ToDo: Would need to be updated if we support dual grids to logically determine who sunk whose ship
                //Task.Run(() => Program.PlaySoundInBackground("SHIP_SUNK.wav", 1.0f));
                Message.AnnounceSunkShip(humanPlayer, computerPlayer, ship.Type);
                //DisplayMessageAndWait();
            }

        }

        private Ship? GetShipAtPosition(Coordinate target)
        {
            foreach(var ship in gameGrid.Ships)
            {
                if(ship.Positions.Any(pos => pos.Coordinate.X == target.X && pos.Coordinate.Y == target.Y))
                {
                    return ship;
                }
            }
            return null;
        }
        private Coordinate UserGuessInput()
        {
            Coordinate? coordinate = null;
            while (coordinate == null)
            {
                Message.AnnounceUserGuessInput();
                string? input = Console.ReadLine();
                // Validate the user input, format and whether it has already been targeted
                coordinate = ValidateUserInput(input);
                if (coordinate == null)
                {
                    Message.AnnounceInvalidCoordinate();
                    continue;
                }
                if (CheckRepeatedTarget(coordinate) == null)
                {
                    Message.AnnounceRepeatedTarget();
                    coordinate = null;
                }
                // Display the game grid
                Console.Clear();
                DisplayController.DisplayGrid(gameGrid);
            }
            return coordinate;
        }

        private Coordinate? CheckRepeatedTarget(Coordinate coordinate)
        {
            if (gameGrid.Positions[coordinate.X, coordinate.Y].Status == Status.Hit || gameGrid.Positions[coordinate.X, coordinate.Y].Status == Status.Miss)
            {
                return null;
            }
            return coordinate;
        }

        private Coordinate? ValidateUserInput(string? input)
        {
            // Check if input is null
            if (input == null)
            {
                return null;
            }
            // Check if input length is 2

            if (input.Length != 2)
            {
                return null;
            }
            char letter = char.ToUpper(input[0]);
            if (!char.IsLetter(letter) || letter < MIN_ROW_LETTER || letter > MAX_ROW_LETTER)
            {
                return null;
            }
            if (!int.TryParse(input[1].ToString(), out int y) || y < 1 || y > Grid.COLUMNS)
            {
                return null;
            }
            int x = letter - MIN_ROW_LETTER;
            y = y - 1;

            var coordinate = new Coordinate(x, y);
            return coordinate;
        }
    }
}