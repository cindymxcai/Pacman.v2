using System;
using System.Collections.Generic;
using System.Linq;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;

namespace Pacman2
{
    /// <summary>
    /// This class is responsible for operations on the maze such as the creation, rendering and getting tiles of maze
    /// It also holds methods operating on the tiles that makes up the maze
    /// </summary>
    public class Maze : IMaze
    {
        private readonly string _wallSpriteDisplay = new WallSpriteDisplay().Icon;
        private readonly string _ghostSpriteDisplay = new GhostSpriteDisplay().Icon;
        private readonly string _pelletSpriteDisplay = new PelletSpriteDisplay().Icon;

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
            return Tiles.Cast<ITile>().Count(tile => tile.HasGivenSprite(_pelletSpriteDisplay)) == 0;
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

        public bool PacmanHasCollisionWithGhost(IList<IMovingSprite> sprites) 
        {
            var pacman = sprites.First(s => s.IsPacman());
            var ghosts = sprites.Where(s => !s.IsPacman());
            
            return GetTileAtPosition(pacman.CurrentPosition).HasGivenSprite(_ghostSpriteDisplay) || HasSpritesPassedEachOther(pacman, ghosts);
        }

        private bool HasSpritesPassedEachOther(IMovingSprite pacman, IEnumerable<IMovingSprite> ghosts)
        {
            var pacmanIsInGhostsPreviousPosition = false;
            foreach (var ghost in ghosts)
                if (GetTileAtPosition(ghost.PreviousPosition).HasGivenSprite(pacman.Icon))
                    pacmanIsInGhostsPreviousPosition = true;

            var ghostIsInPacmansPreviousPosition = GetTileAtPosition(pacman.PreviousPosition).HasGivenSprite(_ghostSpriteDisplay);
            
            return ghostIsInPacmansPreviousPosition && pacmanIsInGhostsPreviousPosition;
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