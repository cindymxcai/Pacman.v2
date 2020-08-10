using System;
using Pacman2.Interfaces;

namespace Pacman2.SpriteDisplays
{
    public class EmptySpriteDisplay : ISpriteDisplay
    {
        public string Icon { get; } = "   ";
        public ConsoleColor Colour { get; } = ConsoleColor.Black;
        public int DisplayPriority { get; } = 2;
    }
}