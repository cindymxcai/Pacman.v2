using System;
using System.Collections.Generic;
using System.Linq;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;

namespace Pacman2
{
    public class Maze : IMaze
    {
        
        private readonly GhostSpriteDisplay _ghostSpriteDisplay = new GhostSpriteDisplay();
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
                    tile.Position = new Position(rowIndex, colIndex);
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
                    var tile = GetTileAtPosition(rowIndex, colIndex);
                    tile.Render();
                }
                Console.WriteLine();
            }
        }
        
        public IPosition GetTilePosition( int rowIndex, int colIndex)
        {
            return Tiles[rowIndex, colIndex].Position;
        }

        public void UpdateSpritePosition(IMovingSprite sprite)
        {
            var newPosition = GetNewPosition(sprite.CurrentDirection,  sprite.CurrentPosition);
            
            if (SpriteHasCollisionWithWall(newPosition)) return;
            MoveSpriteToNewPosition(sprite, newPosition);
            sprite.UpdatePosition(newPosition);
        }

        private void MoveSpriteToNewPosition(IMovingSprite sprite, IPosition newPosition)
        {
            Tiles[sprite.CurrentPosition.Row, sprite.CurrentPosition.Col].RemoveSprite(sprite);
            Tiles[newPosition.Row, newPosition.Col].AddSprite(sprite);
        }

        private IPosition GetNewPosition(Direction currentDirection, IPosition currentPosition)
        {
            return currentDirection switch
            {
                Direction.Up when IsOutOfLowerBounds(currentPosition.Row - 1) 
                => GetTilePosition(Rows-1, currentPosition.Col),
                Direction.Down when IsOutOfUpperBounds(currentPosition.Row + 1, Rows) 
                => GetTilePosition(0, currentPosition.Col),
                Direction.Left when IsOutOfLowerBounds(currentPosition.Col - 1) 
                => GetTilePosition(currentPosition.Row, Columns - 1),
                Direction.Right when IsOutOfUpperBounds(currentPosition.Col + 1, Columns) 
                => GetTilePosition(currentPosition.Row, 0), 
                
                Direction.Up => GetTilePosition(currentPosition.Row - 1, currentPosition.Col),
                Direction.Down => GetTilePosition(currentPosition.Row + 1, currentPosition.Col),
                Direction.Left =>GetTilePosition(currentPosition.Row, currentPosition.Col - 1),
                Direction.Right => GetTilePosition(currentPosition.Row, currentPosition.Col + 1),
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

        public ITile GetTileAtPosition(int row, int col)
        {
            return Tiles[row, col];
        }

        public bool PacmanHasCollisionWithGhost(IMovingSprite sprite)
        {
            _ghostSpriteDisplay.SetSpriteDisplay(null);
            return sprite.Display.Icon != _ghostSpriteDisplay.Icon && Tiles[sprite.CurrentPosition.Row, sprite.CurrentPosition.Col].HasGivenSprite(_ghostSpriteDisplay);
        }
        
        private bool SpriteHasCollisionWithWall(IPosition newPosition)
        {
            _wallSpriteDisplay.SetSpriteDisplay(null);
            return Tiles[newPosition.Row, newPosition.Col].HasGivenSprite(_wallSpriteDisplay); 
        }
    }
}