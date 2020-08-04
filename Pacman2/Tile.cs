using System;

namespace Pacman2
{
    public class Tile : ITile
    {
     
        public Tile(ITileType tileType)
        {
            TileType = tileType;
        }

        public ITileType TileType { get; set; }
    }
}