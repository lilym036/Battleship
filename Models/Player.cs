using System;
using System.Collection.Generic;

public class Player
{
    public string Name { get; private set; }
    public List<Ship> Ships { get; private set; }
    public Grid Grid { get; private set; }
    public PlayerType Type { get; private set; }

    public enum PlayerType
    {
        Human,
        Computer
    }

    public Player(string name, PlayerType type)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        this.Name = name;
        this.Type = type;
        this.Ships = new List<Ship>();
        this.Grid = new Grid();
    }
}
