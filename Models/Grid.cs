using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Group10.Models
{
    internal class Grid
    {
        public int height { get; private set; } //rows
        public int width { get; private set; } //columns
        public Position[,] positions { get; private set; }
        public List<Ship> ships { get; private set; }

        const int ROWS = 4; //MVP; 10 for Stretch
        const int COLUMNS = 4; //MVP; 10 for Stretch
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
                    positions[i, j] = new Position(new Coordinate(i,j), Position.Status.Water);
                }
            }
        }


        public void randomizeShipPlacement()
        {
            random = new Random();
            int x = random.Next(0, ROWS);
            int y = random.Next(0, COLUMNS);
            positions[x, y].status = Position.Status.Ship;
        }

    }
}
