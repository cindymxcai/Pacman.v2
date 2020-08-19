using System;
using System.Collections.Generic;
using System.Linq;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class Tile : ITile
    {
        public List<ISprite> SpritesOnTile { get; } = new List<ISprite>();
        public Position Position { get; set; }

        public void AddSprite(ISprite sprite)
        {
            SpritesOnTile.Add(sprite);
        }
        public void RemoveSprite(ISprite sprite)
        {
            SpritesOnTile.Remove(sprite);
        }

        public bool HasGivenSprite(ISpriteDisplay spriteDisplay)
        {
            return SpritesOnTile.Any(s => s.Display.Icon == spriteDisplay.Icon);
        }

        public ISprite GetGivenSprite(ISpriteDisplay spriteDisplay)
        {
           return SpritesOnTile.FirstOrDefault(s => s.Display.Icon == spriteDisplay.Icon);
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