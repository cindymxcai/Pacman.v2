using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacman2
{
    public class Maze
    {
        private readonly IParser _parser;
        
        public ITile[,] Tiles { get; private set; }

        public int Column { get; private set; }

        public int Row { get; private set; }

        public Maze(IReadOnlyList<string> mazeData, IParser parser)
        {
            _parser = parser;
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
                foreach (var tile in lineData.Select(_parser.GetTileType))
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
                    Console.ForegroundColor = Tiles[row, col].TileType.Colour;
                    Console.Write(Tiles[row,col].TileType.Display);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
        
        public void UpdateArray(IPosition position, ITileType tileType)
        {
            Tiles[position.X, position.Y].TileType = tileType;
        }
    }
}