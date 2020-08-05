using Pacman2.Interfaces;

namespace Pacman2.Tiles
{
    public class TileTypeFactory : ITileTypeFactory //todo rename 
    {
        public ITileType Wall { get; }
        public ITileType Empty { get; }
        public ITileType Pellet { get; }
        public ITileType Ghost { get; set; }

        public TileTypeFactory(ITileType wall, ITileType empty, ITileType pellet, ITileType ghost)
        {
            Ghost = ghost;
            Wall = wall;
            Empty = empty;
            Pellet = pellet;
        }
    }
}