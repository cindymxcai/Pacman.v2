using System;

namespace Pacman2
{
    public class Parser
    {
        public static ITile GetTileType(char inputChar)
        {
            return inputChar switch
            {
                '*' =>  new Tile(Constants.WallDisplay, Constants.WallColour),
                '.' => new Tile(Constants.PelletDisplay, Constants.PelletColour),
                ' ' => new Tile(Constants.EmptyDisplay, Constants.EmptyColour),
            _ => throw new Exception()
            };
        }
    }
}