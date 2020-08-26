using System;
using System.Collections.Generic;
using System.Linq;
using Pacman2.Interfaces;

namespace Pacman2
{
    /// <summary>
    /// The Maze is made up of a tile on each position, where a tile holds a list of sprites that may exist on the position
    /// </summary>
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

        public bool HasGivenSprite(string spriteDisplay)
        {
            return SpritesOnTile.Any(s => s.Icon == spriteDisplay);
        }

        public ISprite GetGivenSprite(string spriteDisplay)
        {
           return SpritesOnTile.FirstOrDefault(s => s.Icon == spriteDisplay);
        }

        public ISprite GetFirstSprite()
        {
           return SpritesOnTile.OrderBy(s => s.Priority).First();
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
                Console.ForegroundColor = sprite.Colour;
                Console.Write(sprite.Display.Icon);
                Console.ResetColor();
            }
        }
        
    }
}