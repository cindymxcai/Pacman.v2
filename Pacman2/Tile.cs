using System.Collections.Generic;
using Pacman2.Interfaces;

namespace Pacman2.Tiles
{
    public class Tile : ITile
    {
        public List<ISprite> SpritesOnTile { get; } = new List<ISprite>();
        public Tile(ISprite sprite)
        {
            SpritesOnTile.Add(sprite); 
        }
    }
}