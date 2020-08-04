using System;

namespace Pacman2
{
    public class Parser
    {
        public static ITile GetTileType(char inputChar)
        {
            return inputChar switch
            {
                '*' =>  new WallTile(),
                '.' => new PelletTile(),
                ' ' => new EmptyTile(),
            _ => throw new Exception()
            };
        }
    }
}