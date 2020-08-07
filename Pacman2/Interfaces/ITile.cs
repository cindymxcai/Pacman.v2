using System.Collections.Generic;

namespace Pacman2.Interfaces
{
    public interface ITile
    {
        List<ITileType> SpritesOnTile { get; }
    }
}