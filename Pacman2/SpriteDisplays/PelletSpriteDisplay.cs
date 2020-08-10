using System;
using Pacman2.Interfaces;

namespace Pacman2.SpriteDisplays
{
    public class PelletSpriteDisplay : ISpriteDisplay
    { 
        public  string Icon { get; } = " \u2022 ";
        public  ConsoleColor Colour { get; } = ConsoleColor.Magenta;
        public int DisplayPriority { get; } = 2;
    }
}