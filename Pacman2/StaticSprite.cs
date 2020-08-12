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
        public void Render() //todo move to tile 
        {
            Console.ForegroundColor = Display.Colour;
            Console.Write(Display.Icon);
            Console.ResetColor();
        }
    }
}