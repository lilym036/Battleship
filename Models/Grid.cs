using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Battleship_Group10.Models.Ship;

namespace Battleship_Group10.Models
{
    internal class Grid
    {
        public int Height { get; private set; } //rows
        public int Width { get; private set; } //columns
        public Position[,] Positions { get; private set; }
        public List<Ship> Ships { get; private set; }

        public const int ROWS = 9; //MVP; 10 for Stretch
        public const int COLUMNS = 9; //MVP; 10 for Stretch
        public Random random = new Random();


        // Constructor; height(rows) and width(cols) are set to 6 x 6 by default(MVP) or 10x10(Stretch)
        public Grid()
        {
            this.Height = ROWS;
            this.Width = COLUMNS;

            this.Positions = new Position[ROWS, COLUMNS];
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    Positions[i, j] = new Position(new Coordinate(i,j), Status.Water);
                }
            }

            int shipTypeCount = Enum.GetValues(typeof(Ship.ShipType)).Length;
            this.Ships = new List<Ship>(shipTypeCount);
            foreach (ShipType shipType in Enum.GetValues(typeof(ShipType)))
            {
                Ship ship = new Ship(shipType);
                Ships.Add(ship);
            }
            randomizeShipPlacement();
        }

        // Randomly place ships on the grid, vertically
        public void randomizeShipPlacement()
        {
            foreach (Ship ship in Ships)
            {
                // Choose a random starting position for the ship from grid positions that have Ocean status.
                // Confirm the ship can be placed in the chosen position, vertically using the size of the ship and checking the bounds of the grid
                // Keep checking until valid place can be found.
                // We will place it treating position[0,0] as the top left corner of the grid and ship position [0] as the head of the ship
                // Maybe make it more sophisticated by only checking rows where length of ship + row < height of grid
                bool isPlaced = false;

                while (!isPlaced)
                {
                    //ToDo: Can add equivalent check for horizontal placement when we add that functionality
                    int startX = random.Next(0, ROWS - ship.Size + 1);
                    int startY = random.Next(0, COLUMNS);
                    bool canPlace = true;

                    for (int i = 0; i < ship.Size; i++)
                    {
                        if (Positions[startX + i, startY].Status != Status.Water)
                        {
                            canPlace = false;
                            break;
                        }
                    }

                    if (canPlace)

                    {
                        for (int i = 0; i < ship.Size; i++)
                        {
                            Positions[startX + i, startY].Status = Status.Ship;
                            ship.Positions.Add(Positions[startX + i, startY]);
                        }
                        isPlaced = true;
                    }
                }


            }

        }

    }
}
