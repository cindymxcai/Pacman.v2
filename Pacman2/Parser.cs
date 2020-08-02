using System;

namespace Pacman2
{
    public static class Parser
    {
        public static ITileType GetTileType(char inputChar)
        {
            return inputChar switch
            {
                '*' => (ITileType) new WallTile(),
                '.' => new PelletTile(),
                _ => throw new Exception()
            };
        }
    }
}