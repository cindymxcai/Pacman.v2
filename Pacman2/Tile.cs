using System;
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
        public Position Position { get; set; }

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

        public void Render()
        {
            if (SpritesOnTile == null || !SpritesOnTile.Any())
            {
                Console.Write("   ");
            }
            else
            {
                var sprite = GetFirstSprite();
                Console.ForegroundColor = sprite.Display.Colour;
                Console.Write(sprite.Display.Icon);
                Console.ResetColor();
            }
         
        }
    }
}