namespace Battleship_Group10.Models
{
    internal class Ship
    {
        public int Size { get; private set; }
        public ShipType Type { get; private set; }
        public List<Position> Positions { get; private set; }

        public Ship(ShipType shipType)
        {
            Type = shipType;
            Size = GetShipSize(shipType);
            Positions = new List<Position>(Size);
      
        }

        // ToDo: Included, but muted Stretch Goal Types for future implementation
        private int GetShipSize(ShipType type)
        {
            return type switch
            {
                //ShipType.Carrier => 5,
                //ShipType.Battleship => 4,
                //ShipType.Cruiser => 3,
                ShipType.Submarine => 3,
                ShipType.Destroyer => 2,
                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown ship type: {type}")
            };
        }

        public bool IsSunk()
        {  
            return Positions.All(pos => pos.Status == Status.Hit); 
        }

        public bool IsHidden()
        {
            return Positions.All(pos => pos.Status != Status.Hit);
        }

        // ToDo: Included, but muted Stretch Goal Types for future implementation
        internal enum ShipType
        {
            //Carrier,
            //Battleship,
            //Cruiser,
            Submarine,
            Destroyer
        }
    }
}