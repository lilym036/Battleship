using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Group10.Models
{
    internal class Player
    {
        public string name { get; private set; }
        public Grid grid { get; private set; }
        public PlayerType type { get; private set; }

        public Player(PlayerType playerType, Grid grid = null, string name = "Player 1" )
        {
            this.type = playerType;
            this.grid = grid;
            this.name = playerType == PlayerType.Human ? name : "Computer";
        }

        internal enum  PlayerType
        {
            Human,
            Computer
        }
    }
}
