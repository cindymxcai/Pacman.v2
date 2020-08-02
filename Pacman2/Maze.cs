using System.Collections.Generic;

namespace Pacman2
{
    public class Maze
    {
        public void CreateMaze(IReadOnlyList<string> mazeData)
        {
            Height = mazeData.Count;
            Width = mazeData[0].Length;
        }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}