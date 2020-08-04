using System;

namespace Pacman2
{
    public class EmptyTileType : ITileType
    {
        public static string Display { get; } = "   ";
        public static ConsoleColor Colour { get; } = ConsoleColor.Black;
    }
}