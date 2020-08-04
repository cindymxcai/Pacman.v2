using System;

namespace Pacman2
{
    public class WallTileType : ITileType
    { 
        public  string Display { get; } = "\u2588\u2588\u2588";
        public  ConsoleColor Colour { get; } = ConsoleColor.Blue;
    }
}