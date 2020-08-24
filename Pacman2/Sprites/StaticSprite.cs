using System;
using Pacman2.Interfaces;

namespace Pacman2.Sprites
{
    public class StaticSprite : ISprite
    {
        public int Priority { get; }
        public string Icon { get; }
        public ConsoleColor Colour { get; }
        public ISpriteDisplay Display { get; set; }


        public StaticSprite(ISpriteDisplay spriteDisplay)
        {
            spriteDisplay.SetSpriteDisplay();
            Display = spriteDisplay;
            Priority = spriteDisplay.Priority;
            Icon = spriteDisplay.Icon;
            Colour = spriteDisplay.Colour;
        }
    }
}