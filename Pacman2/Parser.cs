using System;

namespace Pacman2
{
    public class Parser : IParser
    {
        public ITile GetTileType(char inputChar)
        {
            return inputChar switch
            {
                '*' =>  new Tile(new WallTileType()),
                '.' => new Tile(new PelletTileType()),
                ' ' => new Tile(new EmptyTileType()),
            _ => throw new Exception()
            };
        }
    }
}