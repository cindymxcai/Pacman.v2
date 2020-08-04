using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacman2
{
    public class Maze
    {
        public ITile[,] Tiles { get; private set; }

        public int Column { get; private set; }

        public int Row { get; private set; }

        public Maze(IReadOnlyList<string> mazeData)
        {
            CreateMaze(mazeData);
        }

        private void CreateMaze(IReadOnlyList<string> rowData)
        {
            Row = rowData.Count;
            Column = rowData[0].Length;
            Tiles = new ITile[Row, Column];

            var row = 0;
            foreach (var lineData in rowData)
            {
                var col = 0;
                foreach (var tile in lineData.Select(Parser.GetTileType))
                {
                    Tiles[row, col] = tile;
                    col++;
                }
                row++;
            }
        }
        
        public void Render()
        {
            for (var row = 0; row < Row; row++)
            {
                for (var col = 0; col < Column; col++)
                {
                    Console.ForegroundColor = Tiles[row, col].Colour;
                    Console.Write(Tiles[row,col].Display);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
        
        public void UpdateArray(int row, int col, string tileDisplay, ConsoleColor tileColour)
        {
            Tiles[row, col].Display = tileDisplay;
            Tiles[row, col].Colour = tileColour;
        }
    }
}