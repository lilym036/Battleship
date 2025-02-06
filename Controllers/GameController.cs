using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using Battleship_Group10.Models;

namespace Battleship_Group10.Controllers
{
    public class GameController
    {
        private Player humanPlayer;
        private Player computerPlayer;
        private Grid gameGrid;

        public void Initialize()
        {
            Message.AnnounceWelcomeMessage();
            SetupGameGrid();
            CreatePlayers();
        }


        public void SetupGameGrid()
        {
            gameGrid = new Grid();
        }

        public void CreatePlayers()
        {
            humanPlayer = new Player("Human", Player.PlayerType.Human);
            computerPlayer = new Player("Computer", Player.PlayerType.Computer);
        }
    }

}