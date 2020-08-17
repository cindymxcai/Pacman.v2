using Pacman2.Interfaces;

namespace Pacman2.Sprites
{
    public class StaticSprite : ISprite
    {
        public StaticSprite(ISpriteDisplay spriteDisplay)
        {
            spriteDisplay.SetSpriteDisplay();
            Display = spriteDisplay;
        }

        public ISpriteDisplay Display { get; }
    }
}