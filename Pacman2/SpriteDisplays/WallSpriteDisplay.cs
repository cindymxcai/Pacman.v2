using System;
using Pacman2.Interfaces;

namespace Pacman2.SpriteDisplays
{
    public class WallSpriteDisplay : ISpriteDisplay
    {
        public string Icon { get; private set; }
        public ConsoleColor Colour { get; private set; }
        public int Priority { get; private set; } 
        public void SetSpriteDisplay(Direction? direction)
        {
            Icon = "\u2588\u2588\u2588";
            Colour = ConsoleColor.Blue;
            Priority = 2;
        }
    }
}