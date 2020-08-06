using System;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class EmptyTileType : ITileType
    {
        public string Display { get; } = "   ";
        public ConsoleColor Colour { get; } = ConsoleColor.Black;
        public int DisplayPriority { get; } = 2;
    }
}