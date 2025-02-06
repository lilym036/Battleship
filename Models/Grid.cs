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
        public int height { get; private set; } //rows
        public int width { get; private set; } //columns
        public Position[,] positions { get; private set; }
        public List<Ship> ships { get; private set; }

        public const int ROWS = 4; //MVP; 10 for Stretch
        public const int COLUMNS = 4; //MVP; 10 for Stretch
        public Random random;


        // Constructor; height(rows) and width(cols) are set to 4 x 4 by default(MVP) or 10x10(Stretch)
        public Grid()
        {
            this.height = ROWS;
            this.width = COLUMNS;

            this.positions = new Position[ROWS, COLUMNS];
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    positions[i, j] = new Position(new Coordinate(i,j), Status.Water);
                }
            }

            int shipTypeCount = Enum.GetValues(typeof(Ship.ShipType)).Length;
            this.ships = new List<Ship>(shipTypeCount);
            foreach (ShipType shipType in Enum.GetValues(typeof(ShipType)))
            {
                Ship ship = new Ship(shipType);
                ships.Add(ship);
            }
            randomizeShipPlacement();
        }

        // Randomly place ships on the grid, vertically
        public void randomizeShipPlacement()
        {
            random = new Random();
            foreach (Ship ship in ships)
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
                    int startX = random.Next(0, ROWS - ship.size + 1);
                    int startY = random.Next(0, COLUMNS);
                    bool canPlace = true;

                    for (int i = 0; i < ship.size; i++)
                    {
                        if (positions[startX + i, startY].status != Status.Water)
                        {
                            canPlace = false;
                            break;
                        }
                    }

                    if (canPlace)

                    {
                        for (int i = 0; i < ship.size; i++)
                        {
                            positions[startX + i, startY].status = Status.Ship;
                            ship.positions.Add(positions[startX + i, startY]);
                        }
                        isPlaced = true;
                    }
                }


            }

        }

    }
}
