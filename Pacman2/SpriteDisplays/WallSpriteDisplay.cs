using System;
using Pacman2.Interfaces;

namespace Pacman2.SpriteDisplays
{
    public class WallSpriteDisplay : ISpriteDisplay
    {
        public string Icon { get; private set; } = "\u2588\u2588\u2588";
        public ConsoleColor Colour { get; private set; } = ConsoleColor.Blue;
        public int Priority { get; private set; } = 2;
        public void SetSpriteDisplay(Direction? direction)
        {

            Icon = "\u2588\u2588\u2588";
            Colour = ConsoleColor.Blue;
            Priority = 2;
        }
    }
}