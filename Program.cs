using System;
using Battleship_Group10.Controllers;

namespace Battleship_Group10
{
    class Program
    {
        static void Main(string[] args)
        {
            var gc = new GameController();
            gc.Initialize();
        }
    }
}