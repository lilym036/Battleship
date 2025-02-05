using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Group10.Models
{
    internal class Grid
    {
        private int height { get; set; } //rows
        private int width { get; set; } //columns
        private Position[,] positions { get; set; }

        const int ROWS = 4; //MVP
        const int COLUMNS = 4; //MVP


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


    }
}
