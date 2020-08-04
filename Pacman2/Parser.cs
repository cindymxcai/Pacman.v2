using System;

namespace Pacman2
{
    public class Parser
    {
        public static ITile GetTileType(char inputChar)
        {
            return inputChar switch
            {
                '*' =>  new Tile(Constants.WallDisplay),
                '.' => new Tile(Constants.PelletDisplay),
                ' ' => new Tile(Constants.EmptyDisplay),
            _ => throw new Exception()
            };
        }
    }
}