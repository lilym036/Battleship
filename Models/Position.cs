using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Group10.Models
{
    internal class Position
    {
        public Coordinate coordinate { get; private set; }
        public Status status { get; set; }


        public Position(Coordinate coordinate, Status status)
        {
            this.coordinate = coordinate;
            this.status = status;
        }
    }
}
