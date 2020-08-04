using System;

namespace Pacman2
{
    public class PelletTileType : ITileType
    { 
        public  string Display { get; } = " \u2022 ";
        public  ConsoleColor Colour { get; } = ConsoleColor.Magenta;
        
    }
}