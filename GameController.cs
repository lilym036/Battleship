using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;

namespace Battleship_Group10
{
    public class GameController
    {
        Player player1 = new Player("Human");
        Player player2 = new Player("Computer");

        Grid grid = new Grid();

        public GameController()
        {

        }
    }

}