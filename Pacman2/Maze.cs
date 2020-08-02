using System;
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
        
        public void Render()
        {
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    Console.Write(MazeArray[i,j].Display);
                }
                Console.WriteLine();
            }
        }

        public ITile[,] MazeArray { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }
    }
}