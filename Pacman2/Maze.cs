using System;
using System.Collections.Generic;
using System.Linq;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class Maze
    {
        private readonly WallTileType _wallTileType = new WallTileType();

        private readonly IParser _parser;
        public ITile[,] Tiles { get; private set; }
        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public Maze(IReadOnlyList<string> mazeData, IParser parser)
        {
            _parser = parser;
            CreateMaze(mazeData);
        }

        private void CreateMaze(IReadOnlyList<string> rows)
        {
            Rows = rows.Count;
            Columns = rows[0].Length;
            Tiles = new ITile[Rows, Columns];

            var rowIndex = 0;
            foreach (var row in rows)
            {
                var colIndex = 0;
                foreach (var tile in row.Select(_parser.GetTile))
                {
                    Tiles[rowIndex, colIndex] = tile;
                    colIndex++;
                }

                rowIndex++;
            }
        }

        public void Render()
        {
            for (var rowIndex = 0; rowIndex < Rows; rowIndex++)
            {
                for (var colIndex = 0; colIndex < Columns; colIndex++)
                {
                    Console.ForegroundColor = GetTileTypeAtPosition(new Position(rowIndex, colIndex)).Colour;
                    Console.Write(GetTileTypeAtPosition(new Position(rowIndex, colIndex)).Display);
                    Console.ResetColor();
                }

                Console.WriteLine();
            }
        }

        public void AddTileTypeToTile(IPosition position, ITileType tileType)
        {
            Tiles[position.Row, position.Col].SpritesOnTile.Add(tileType);
        }

        public void RemoveTileTypeFromTile(IPosition position, ITileType tileType)
        {
            Tiles[position.Row, position.Col].SpritesOnTile.Remove(tileType);
        }

        public ITileType GetTileTypeAtPosition(IPosition position)
        {
            var toDisplay = Tiles[position.Row, position.Col].SpritesOnTile.OrderBy(t => t.DisplayPriority).First();
            return toDisplay;
        }

        public bool SpriteHasCollisionWithWall(IPosition newPosition)
        {
            return Tiles[newPosition.Row, newPosition.Col].SpritesOnTile.Any(d => d.Display == _wallTileType.Display);
        }

        public IPosition GetNewPosition(Direction currentDirection, IPosition currentPosition)
        {
            switch (currentDirection)
            {
                case Direction.Up when IsOutOfLowerBounds(currentPosition.Row - 1 ):
                    return new Position(Rows - 1, currentPosition.Col);
                case Direction.Down when IsOutOfUpperBounds(currentPosition.Row + 1,Rows):
                    return new Position(0, currentPosition.Col);
                case Direction.Left when IsOutOfLowerBounds(currentPosition.Col - 1 ):
                    return new Position(currentPosition.Row, Columns - 1);
                case Direction.Right when IsOutOfUpperBounds( currentPosition.Col + 1 ,Columns):
                    return new Position(currentPosition.Row, 0);
                
                case Direction.Up:
                    return new Position(currentPosition.Row - 1, currentPosition.Col);
                case Direction.Down:
                    return new Position(currentPosition.Row + 1, currentPosition.Col);
                case Direction.Left:
                    return new Position(currentPosition.Row, currentPosition.Col - 1);
                case Direction.Right:
                    return new Position(currentPosition.Row, currentPosition.Col + 1);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static bool IsOutOfLowerBounds(int newPosition)
        {
            return newPosition < 0;
        }
        
        private static bool IsOutOfUpperBounds(int newPosition, int boundary)
        {
            return newPosition > boundary -1 ;
        }
        
    }
}