using System;
using Pacman2.Interfaces;

namespace Pacman2.SpriteDisplays
{
    public class WallSpriteDisplay : ISpriteDisplay
    {
        public string Icon { get; private set; } ="\u2588\u2588\u2588";
        public ConsoleColor Colour { get; private set; }
        public int Priority { get; private set; }
        public void SetSpriteDisplay(Direction direction = Direction.Up)
        {
            Icon = "\u2588\u2588\u2588";
            Colour = ConsoleColor.Blue;
            Priority = 2;        
        }
    }
}