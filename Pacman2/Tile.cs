using System.Collections.Generic;
using System.Linq;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;

namespace Pacman2
{
    public class Tile : ITile
    {
        private readonly WallSpriteDisplay _wallSpriteDisplay = new WallSpriteDisplay();
        public List<ISprite> SpritesOnTile { get; } = new List<ISprite>();

        public void AddSprite(ISprite sprite)
        {
            SpritesOnTile.Add(sprite);
        }

        public bool IsWall()
        {
          return SpritesOnTile.Any(d => d.Display.Icon == _wallSpriteDisplay.Icon);
        }

        public ISprite GetFirstSprite()
        {
           return SpritesOnTile.OrderBy(t => t.Display.Priority).First();
        }
    }
}