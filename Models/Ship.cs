namespace Battleship_Group10.Models
{
    internal class Ship
    {
        public int size { get; private set; }
        public ShipType type { get; private set; }
        public List<Position> positions { get; private set; }

        public Ship(ShipType shipType)
        {
            this.type = shipType;
            this.size = GetShipSize(shipType);
            this.positions = new List<Position>(this.size);
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