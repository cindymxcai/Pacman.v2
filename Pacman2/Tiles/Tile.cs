using System.Collections.Generic;
using Pacman2.Interfaces;

namespace Pacman2.Tiles
{
    public class Tile : ITile
    {
        public List<ITileType> SpritesOnTile { get; } = new List<ITileType>();
        public Tile(ITileType tileType)
        {
            SpritesOnTile.Add(tileType); 
        }
    }
}