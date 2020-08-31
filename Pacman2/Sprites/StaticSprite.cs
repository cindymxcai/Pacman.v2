using System;
using Pacman2.Interfaces;

namespace Pacman2.Sprites
{
    public class StaticSprite : ISprite
    {
        /// <summary>
        /// For Non - Moving Sprites (Pellets and Walls)
        /// For context, Sprite refers to an object that can occupy a tile on the maze
        /// </summary>
        public int Priority { get; }
        public string Icon { get; }
        public ConsoleColor Colour { get; }
        public ISpriteDisplay Display { get; }

        public StaticSprite(ISpriteDisplay spriteDisplay)
        {
            Display = spriteDisplay;
            Display.SetSpriteDisplay();
            Priority = Display.Priority;
            Icon = Display.Icon;
            Colour = Display.Colour;
        }
    }
}