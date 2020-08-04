using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacman2
{
    public class Maze
    {
        public ITile[,] Tiles { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Maze(IReadOnlyList<string> mazeData)
        {
            CreateMaze(mazeData);
        }

        private void CreateMaze(IReadOnlyList<string> rowData)
        {
            Height = rowData.Count;
            Width = rowData[0].Length;
            Tiles = new ITile[Height, Width];

            var x = 0;
            foreach (var lineData in rowData)
            {
                var y = 0;
                foreach (var tile in lineData.Select(Parser.GetTileType))
                {
                    Tiles[x, y] = tile;
                    y++;
                }
                x++;
            }
        }
        
        public void Render()
        {
            for (var x = 0; x < Height; x++)
            {
                for (var y = 0; y < Width; y++)
                {
                    Console.ForegroundColor = Tiles[x, y].Colour;
                    Console.Write(Tiles[x,y].Display);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
        
        public void UpdateArray(int x, int y, string tileDisplay, ConsoleColor colour)
        {
            Tiles[x, y].Display = tileDisplay;
            Tiles[x, y].Colour = colour;
        }
        
    }
}