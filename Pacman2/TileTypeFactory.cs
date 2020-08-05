using Pacman2.Interfaces;

namespace Pacman2
{
    public class TileTypeFactory : ITileTypeFactory
    {
        public ITileType Wall { get; }
        public ITileType Empty { get; }
        public ITileType Pellet { get; }
        public ITileType Ghost { get; set; }

        public TileTypeFactory(ITileType wall, ITileType empty, ITileType pellet, GhostTileType ghost)
        {
            Ghost = ghost;
            Wall = wall;
            Empty = empty;
            Pellet = pellet;
        }
    }
}