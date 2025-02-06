using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Group10.Models
{
    internal class Position
    {
        public Coordinate Coordinate { get; private set; }
        public Status Status { get; set; }


        public Position(Coordinate coordinate, Status status)
        {
            Coordinate = coordinate;
            Status = status;
        }
    }
}
