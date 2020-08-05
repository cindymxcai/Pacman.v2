using System;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class Parser : IParser
    {
        public ITile GetTile(char inputChar)
        {
            return inputChar switch
            {
                '*' => new Tile(new WallTileType()),
                '.' => new Tile(new PelletTileType()),
                ' ' => new Tile(new EmptyTileType()),
            _ => throw new Exception()
            };
        }
    }
}