using System;
using Pacman2.Interfaces;

namespace Pacman2.Tiles
{
    public class StaticSprite : ISprite
    {
        public StaticSprite(ISpriteDisplay spriteDisplay)
        {
            Display = spriteDisplay;
        }
        public ISpriteDisplay Display { get; }
       
    }
}