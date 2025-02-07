using System;
using Battleship_Group10.Helpers;
using Battleship_Group10.Models;

namespace Battleship_Group10.Controllers
{
    internal class GameController
    {
        private Player humanPlayer;
        private Player computerPlayer;
        private Grid gameGrid;

        public GameController() 
        {
            this.gameGrid = new Grid();

            // MVP: Human player has no grid, computer player has a grid
            this.humanPlayer = new Player(Player.PlayerType.Human);
            this.computerPlayer = new Player(Player.PlayerType.Computer, gameGrid);
        }
        
        public void Initialize()
        {
            Message.AnnounceWelcomeMessage();
        }


        private void FireMissile()
        {
            // Get the coordinate from the user
            Coordinate target = UserGuessInput();

            // Check the status of the targeted coordinate
            CheckPosition(target);
        }

        public bool CheckGameOver()
        {
            //check if humanPlayer's grid is not null and all their ships are sunk
            if (humanPlayer.grid != null && humanPlayer.grid.ships.All(ship => ship.IsSunk()))
            {
                Message.AnnounceGameOver("Computer wins!");
                return true;
            }
            //check if computerPlayer's grid is not null and all their ships are sunk
            else if (computerPlayer.grid != null && computerPlayer.grid.ships.All(ship => ship.IsSunk()))
            {
                Message.AnnounceGameOver("Player wins!");
                return true;
            }
            return false;
        }

        private void CheckPosition(Coordinate target)
        {
            // Get the position of the targeted coordinate
            Position position = gameGrid.positions[target.X, target.Y];

            // Check the status of the position
            switch (position.Status)
            {
                case Status.Water:
                    Message.AnnounceMisses(target);
                    
                    //Updates the status of the position to Miss
                    position.Status = Status.Miss;

                    //ToDo: Update Status of the position to Miss on SHIPS, list of positions
                    //ToDo: Ship list of positions is a reference to the position of the game grid.
                    //So, when we update the position on the game grid, this should automatically update status of positions in list
                    break;
                
                case Status.Ship:
                    Message.AnnounceHits(target);
                    //Updates the status of the position to Hit
                    position.Status = Status.Hit;
                    //ToDo: Update the status of the position to Hit on SHIPS, list of positions (DONE?)
                    //See note above re: ship list of positions

                    //ToDO: Check Sunk Ship
                    //ToDo: Check GameOver
                    Ship? ship = GetShipAtPosition(target);
                    if (ship != null && ship.IsSunk())
                    {
                        Message.AnnounceSunkShips(humanPlayer);
                        if(CheckGameOver())
                        {
                            return; //Game over.
                        }
                    }
                    
                    break;
            }
        }

        private Ship? GetShipAtPosition(Coordinate target)
        {
            foreach(var ship in humanPlayer.grid?.ships ?? computerPlayer.grid?.ships ?? new List<Ship>()) 
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
            }
            return coordinate;
        }

        private Coordinate? CheckRepeatedTarget(Coordinate coordinate)
        {
            if (gameGrid.positions[coordinate.X, coordinate.Y].status == Status.Hit || gameGrid.positions[coordinate.X, coordinate.Y].status == Status.Miss)
            {
                Message.AnnounceRepeatedTarget();
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
            // Check if input contains two numbers
            string[] parts = input.Split(' ');
            if (parts.Length != 2)
            {
                return null;
            }
            if (int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y))
            {
                if (x < 0 || x >= Grid.ROWS || y < 0 || y >= Grid.COLUMNS)
                {
                    var coordinate = new Coordinate(x, y);
                    return CheckRepeatedTarget(coordinate);
                }
            }
            return null;
        }
    }
}