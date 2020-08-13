using System;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class StaticSprite : ISprite
    {
        public StaticSprite(ISpriteDisplay spriteDisplay)
        {
            spriteDisplay.SetSpriteDisplay(null);
            Display = spriteDisplay;
        }

        public ISpriteDisplay Display { get; }
    }
}