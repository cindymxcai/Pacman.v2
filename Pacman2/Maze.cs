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
        public ITile[,] Tiles { get; private set; }
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public int PelletsEaten { get; private set; }

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

        public bool HasNoPelletsRemaining()
        {
            var count = Tiles.Cast<ITile>().Count(tile => tile.HasGivenSprite(_pelletSpriteDisplay));

            return count == 0;
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

        public IPosition GetTilePosition(int rowIndex, int colIndex)
        {
            return Tiles[rowIndex, colIndex].Position;
        }

        public void MoveSpriteToNewPosition(IMovingSprite sprite, IPosition newPosition)
        {
            Tiles[sprite.CurrentPosition.Row, sprite.CurrentPosition.Col].RemoveSprite(sprite);
            EatPellet(sprite);
            Tiles[newPosition.Row, newPosition.Col].AddSprite(sprite);
        }

        private void EatPellet(IMovingSprite sprite)
        {
            if (!sprite.IsPacman()) return;
            if (!Tiles[sprite.CurrentPosition.Row, sprite.CurrentPosition.Col].HasGivenSprite(_pelletSpriteDisplay)) return;
           
            var pelletSprite = Tiles[sprite.CurrentPosition.Row, sprite.CurrentPosition.Col].GetGivenSprite(_pelletSpriteDisplay);
            Tiles[sprite.CurrentPosition.Row, sprite.CurrentPosition.Col].RemoveSprite(pelletSprite);
            PelletsEaten++;
        }


        public ITile GetTileAtPosition(int row, int col)
        {
            return Tiles[row, col];
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
            return sprite.IsPacman() && Tiles[sprite.CurrentPosition.Row, sprite.CurrentPosition.Col]
                    .HasGivenSprite(_ghostSpriteDisplay) || sprite.Display.Icon != _ghostSpriteDisplay.Icon &&
                Tiles[sprite.PreviousPosition.Row, sprite.PreviousPosition.Col].HasGivenSprite(_ghostSpriteDisplay);
        }

        public bool SpriteHasCollisionWithWall(IPosition newPosition)
        {
            return Tiles[newPosition.Row, newPosition.Col].HasGivenSprite(_wallSpriteDisplay);
        }

        public void ResetSpritePositions(IEnumerable<IMovingSprite> sprites, IPosition ghostPosition, IPosition pacmanPosition)
        {
            foreach (var sprite in sprites)
            {
                Tiles[sprite.CurrentPosition.Row, sprite.CurrentPosition.Col].RemoveSprite(sprite);
                sprite.UpdatePosition(!sprite.IsPacman() ? ghostPosition : pacmanPosition);
                MoveSpriteToNewPosition(sprite, sprite.CurrentPosition);
            }
        }
    }
}