using System;

namespace Pacman2
{
    public class GhostTileType : ITileType
    {
        public string Display { get; } = " \u1571 ";
        public ConsoleColor Colour { get; } = ConsoleColor.Red;
    }
}