using System;

namespace Pacman2
{
    public class PelletTileType : ITileType
    { 
        public static string Display { get; } = " \u2022 ";
        public static ConsoleColor Colour { get; } = ConsoleColor.Magenta;
        
    }
}