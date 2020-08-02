using System.Collections.Generic;
using System.Linq;

namespace Pacman2
{
    public class Maze
    {
        public void CreateMaze(IReadOnlyList<string> mazeData)
        {
            Height = mazeData.Count;
            Width = mazeData[0].Length;
            MazeArray = new ITile[Height, Width];

            var x = 0;
            foreach (var lineData in mazeData)
            {
                var y = 0;
                foreach (var tileType in lineData.Select(Parser.GetTileType))
                {
                    MazeArray[x, y] = tileType;
                    y++;
                }
                x++;
            }
        }

        public ITile[,] MazeArray { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}