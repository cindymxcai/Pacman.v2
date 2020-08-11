using System;
using System.Collections.Generic;
using System.Linq;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;

namespace Pacman2
{
    public class Maze
    {
        private readonly WallSpriteDisplay _wallSpriteDisplay = new WallSpriteDisplay();

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
                    var sprite = GetSpriteAtPosition(new Position(rowIndex, colIndex));
                    sprite.Render();
                }
                Console.WriteLine();
            }
        }

        //todo refactor this
        public void UpdateSpritePosition(IMovingSprite sprite)
        {
            var newPosition = GetNewPosition(sprite.CurrentDirection,  sprite.CurrentPosition);
            if (SpriteHasCollisionWithWall(newPosition)) return;
            Tiles[sprite.CurrentPosition.Row, sprite.CurrentPosition.Col].SpritesOnTile.Remove(sprite);
            sprite.UpdatePosition(newPosition);
            Tiles[sprite.CurrentPosition.Row, sprite.CurrentPosition.Col].SpritesOnTile.Add(sprite);
        }

        private bool SpriteHasCollisionWithWall(IPosition newPosition)
        {
            return Tiles[newPosition.Row, newPosition.Col].SpritesOnTile.Any(d => d.Display.Icon == _wallSpriteDisplay.Icon);
        }

        private IPosition GetNewPosition(Direction currentDirection, IPosition currentPosition)
        {
            return currentDirection switch
            {
                Direction.Up when IsOutOfLowerBounds(currentPosition.Row - 1) 
                => new Position(Rows - 1, currentPosition.Col),
                Direction.Down when IsOutOfUpperBounds(currentPosition.Row + 1, Rows) 
                => new Position(0, currentPosition.Col),
                Direction.Left when IsOutOfLowerBounds(currentPosition.Col - 1) 
                => new Position(currentPosition.Row, Columns - 1),
                Direction.Right when IsOutOfUpperBounds(currentPosition.Col + 1, Columns) 
                => new Position(currentPosition.Row, 0), 
                
                Direction.Up => new Position(currentPosition.Row - 1, currentPosition.Col),
                Direction.Down => new Position(currentPosition.Row + 1, currentPosition.Col),
                Direction.Left => new Position(currentPosition.Row, currentPosition.Col - 1),
                Direction.Right => new Position(currentPosition.Row, currentPosition.Col + 1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static bool IsOutOfLowerBounds(int newPosition)
        {
            return newPosition < 0;
        }
        
        private static bool IsOutOfUpperBounds(int newPosition, int boundary)
        {
            return newPosition > boundary -1 ;
        }

        public ISprite GetSpriteAtPosition(IPosition position)
        {
            return Tiles[position.Row, position.Col].SpritesOnTile.OrderBy(t => t.Display.DisplayPriority).First();
        }
    }
}