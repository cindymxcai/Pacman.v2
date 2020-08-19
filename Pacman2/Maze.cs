using System;
using System.Collections.Generic;
using System.Linq;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;

namespace Pacman2
{
    public class Maze : IMaze
    {
        private readonly WallSpriteDisplay _wallSpriteDisplay = new WallSpriteDisplay();
        private readonly GhostSpriteDisplay _ghostSpriteDisplay = new GhostSpriteDisplay();
        private readonly PelletSpriteDisplay _pelletSpriteDisplay = new PelletSpriteDisplay();

        private readonly IParser _parser;
        private ITile[,] Tiles { get; set; }
        public int Columns { get; private set; }
        public int Rows { get;  private set; }
        public int PelletsEaten { get; set; }

        public Maze(IReadOnlyList<string> mazeData, IParser parser)
        {
            _parser = parser;
            CreateMaze(mazeData);
            PopulateMaze(mazeData);
        }

        private void CreateMaze(IReadOnlyList<string> dataRows)
        {
            Rows = dataRows.Count;
            Columns = dataRows[0].Length;
            Tiles = new ITile[Rows, Columns];
        }

        private void PopulateMaze(IEnumerable<string> dataRows)
        {
            var rowIndex = 0;
            foreach (var row in dataRows)
            {
                var colIndex = 0;
                foreach (var tile in row.Select(_parser.GetTile))
                {
                    Tiles[rowIndex, colIndex] = tile;
                    GetTileAtPosition(rowIndex, colIndex).Position = new Position(rowIndex, colIndex);
                    colIndex++;
                }
                rowIndex++;
            }
        }

        public bool HasNoPelletsRemaining()
        {
            return  Tiles.Cast<ITile>().Count(tile => tile.HasGivenSprite(_pelletSpriteDisplay)) == 0;
        }

        public void Render()
        {
            for (var rowIndex = 0; rowIndex < Rows; rowIndex++)
            {
                for (var colIndex = 0; colIndex < Columns; colIndex++)
                {
                    GetTileAtPosition(rowIndex, colIndex).Render();
                }
                
                Console.WriteLine();
            }
        }

        public IPosition GetTilePosition(int rowIndex, int colIndex)
        {
            return Tiles[rowIndex, colIndex].Position;
        }

        public void MoveSpriteToNewPosition(IMovingSprite sprite, IPosition newPosition)
        {
            GetTileAtPosition(sprite.CurrentPosition).RemoveSprite(sprite);
            if (sprite.IsPacman()) EatPellet(sprite);
            GetTileAtPosition(newPosition).AddSprite(sprite);
        }

        private void EatPellet(IMovingSprite sprite)
        {
            if (!GetTileAtPosition(sprite.CurrentPosition).HasGivenSprite(_pelletSpriteDisplay)) return;

            var pelletSprite = GetTileAtPosition(sprite.CurrentPosition).GetGivenSprite(_pelletSpriteDisplay);
            GetTileAtPosition(sprite.CurrentPosition).RemoveSprite(pelletSprite);
            PelletsEaten++;
        }
        
        public ITile GetTileAtPosition(int row, int col)
        {
            return Tiles[row, col];
        }

        public ITile GetTileAtPosition(IPosition position)
        {
            return Tiles[position.Row, position.Col];
        }
        
        public IPosition GetNewPosition(Direction currentDirection, IPosition currentPosition)
        {
            return currentDirection switch
            {
                Direction.Up when IsOutOfLowerBounds(currentPosition.Row - 1) => GetTilePosition(Rows - 1, currentPosition.Col),
                Direction.Down when IsOutOfUpperBounds(currentPosition.Row + 1, Rows) => GetTilePosition(0, currentPosition.Col),
                Direction.Left when IsOutOfLowerBounds(currentPosition.Col - 1) => GetTilePosition(currentPosition.Row, Columns - 1),
                Direction.Right when IsOutOfUpperBounds(currentPosition.Col + 1, Columns) => GetTilePosition(currentPosition.Row, 0),
               
                Direction.Up => GetTilePosition(currentPosition.Row - 1, currentPosition.Col),
                Direction.Down => GetTilePosition(currentPosition.Row + 1, currentPosition.Col),
                Direction.Left => GetTilePosition(currentPosition.Row, currentPosition.Col - 1),
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
            return newPosition > boundary - 1;
        }

        public bool PacmanHasCollisionWithGhost(IMovingSprite sprite)
        {
            return sprite.IsPacman() && GetTileAtPosition(sprite.CurrentPosition).HasGivenSprite(_ghostSpriteDisplay)
                   || sprite.IsPacman() && GetTileAtPosition(sprite.PreviousPosition).HasGivenSprite(_ghostSpriteDisplay);
        }

        public bool SpriteHasCollisionWithWall(IPosition newPosition)
        {
            return GetTileAtPosition(newPosition).HasGivenSprite(_wallSpriteDisplay);
        }

        public void ResetSpritePositions(IEnumerable<IMovingSprite> sprites)
        {
            var ghostPosition = GetTilePosition(9, 9);
            var pacmanPosition = GetTilePosition(1, 1);
            foreach (var sprite in sprites)
            {
                GetTileAtPosition(sprite.CurrentPosition).RemoveSprite(sprite);
                sprite.UpdatePosition(!sprite.IsPacman() ? ghostPosition : pacmanPosition);
                MoveSpriteToNewPosition(sprite, sprite.CurrentPosition);
            }
        }
    }
}