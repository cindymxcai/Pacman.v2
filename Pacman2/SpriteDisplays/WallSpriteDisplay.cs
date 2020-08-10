using System;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class WallSpriteDisplay : ISpriteDisplay
    {
        public string Icon { get; } = "\u2588\u2588\u2588";
        public ConsoleColor Colour { get; } = ConsoleColor.Blue;
        public int DisplayPriority { get; } = 2;
    }
}