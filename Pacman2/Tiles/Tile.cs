using System.Collections.Generic;
using Pacman2.Interfaces;

namespace Pacman2.Tiles
{
    public class Tile : ITile
    {
        //todo list of current sprites 

        public List<ITileType> SpritesOnTile = new List<ITileType>{};
        public Tile(ITileType tileType)
        {
            TileType = tileType;
        }
        public ITileType TileType { get; set; }
    }
}