using System;

namespace Pacman2
{
    public class Tile : ITile
    {
        public Tile(string display, ConsoleColor colour)
        {
            Display = display;
            Colour = colour;
        }
        public string Display { get; set; }
        public ConsoleColor Colour { get; set; }
    }
}