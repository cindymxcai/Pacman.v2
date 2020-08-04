using System;

namespace Pacman2
{
    public class Parser
    {
        public static ITile GetTileType(char inputChar)
        {
            return inputChar switch
            {
                '*' =>  new Tile(WallTileType.Display, WallTileType.Colour),
                '.' => new Tile(PelletTileType.Display, PelletTileType.Colour),
                ' ' => new Tile(EmptyTileType.Display, EmptyTileType.Colour),
            _ => throw new Exception()
            };
        }
    }
}