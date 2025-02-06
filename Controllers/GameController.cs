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
        }
        
        public void Initialize()
        {
            Message.AnnounceWelcomeMessage();
            SetupGameGrid();
            CreatePlayers();
        }


        private void SetupGameGrid()
        {
            gameGrid = new Grid();
        }


        private void FireMissile()
        {
            // Get the coordinate from the user
            Coordinate target = UserGuessInput();

            // Check the status of the targeted coordinate
            CheckPosition(target);
        }

        private void CheckPosition(Coordinate target)
        {
            // Get the position of the targeted coordinate
            Position position = ComputerGrid.positions[target.X, target.Y];

            // Check the status of the position
            switch (position.status)
            {
                case Status.Water:
                    Message.AnnounceMisses(target);
                    //Updates the status of the position to Miss
                    position.status = Status.Miss;
                    //ToDo: Update Status of the position to Miss on SHIPS, list of positions
                    //ToDo: Ship list of positions is a reference to the position of the game grid.
                    //So, when we update the position on the game grid, this should automatically update status of positions in list
                    break;
                case Status.Ship:
                    Message.AnnounceHits(target);
                    //Updates the status of the position to Hit
                    position.status = Status.Hit;
                    //ToDo: Update the status of the position to Hit on SHIPS, list of positions (DONE?)
                    //See note above re: ship list of positions

                    //ToDO: Check Sunk Ship
                    //ToDo: Check GameOver
                    break;
            }
        }

        private Coordinate UserGuessInput()
        {
            Coordinate coordinate = null;
            while (coordinate == null)
            {
                Message.AnnounceUserGuessInput();
                string input = Console.ReadLine();
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



        private void CreatePlayers()
        {
            humanPlayer = new Player("Human", Player.PlayerType.Human);
            computerPlayer = new Player("Computer", Player.PlayerType.Computer);
        }

        private Coordinate CheckRepeatedTarget(Coordinate coordinate)
        {
            if (ComputerGrid.positions[coordinate.X, coordinate.Y].status == Status.Hit || ComputerGrid.positions[coordinate.X, coordinate.Y].status == Status.Miss)
            {
                Message.AnnounceRepeatedTarget();
                return null;
            }
            return coordinate;
        }

        private Coordinate ValidateUserInput(string? input)
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
                    coordinate = new Coordinate(x, y);
                    return CheckRepeatedTarget(coordinate);
                }
            }
            return null;
        }
    }
}