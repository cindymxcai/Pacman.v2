using System;

namespace Pacman2
{
    public interface ITile
    {
        ITileType TileType { get; set; }
    }
}