using System;

namespace Pacman2
{
    public class WallTileType : ITileType
    { 
        public static string Display { get; } = "\u2588\u2588\u2588";
        public static ConsoleColor Colour { get; } = ConsoleColor.Blue;
    }
}