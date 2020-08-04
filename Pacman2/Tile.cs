using System;

namespace Pacman2
{
    public class Tile : ITile
    {
        public string Display { get; set; }
        public ConsoleColor Colour { get; set; }
        public Tile(ITileType tileType)
        {
            Display = tileType.Display;
            Colour = tileType.Colour;
        }
    }
}