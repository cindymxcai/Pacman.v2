using System;
using System.Linq;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class Sprite : ISprite
    {
        private readonly WallTileType _wallTileType = new WallTileType();
        private IMovementBehaviour MovementBehaviour { get; }
        public IPosition PreviousPosition;
        public IPosition CurrentPosition { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public ITileType TileType { get; }

        public Sprite(IPosition position, IMovementBehaviour randomMovement, ITileType tileType)
        {
            CurrentPosition = position;
            MovementBehaviour = randomMovement;
            CurrentDirection = Direction.Up;
            TileType = tileType; //todo move to behavior
        }

        public void UpdateDirection()
        {
            CurrentDirection = MovementBehaviour.GetNewDirection();
        }

        public void UpdatePosition(IPosition newPosition, Maze maze)
        {
            if (HasCollisionWithWall(newPosition, maze)) return;
            PreviousPosition = CurrentPosition;

            CurrentPosition = newPosition;
           //maze.Tiles[CurrentPosition.Row, CurrentPosition.Col].SpritesOnTile.Add(TileType);
        }

        public IPosition GetNewPosition(Maze maze)
        {
            return CurrentDirection switch
            {
                Direction.Up when CurrentPosition.Row - 1 < 0 => new Position(maze.Rows - 1, CurrentPosition.Col),
                Direction.Up => new Position(CurrentPosition.Row - 1, CurrentPosition.Col),
                Direction.Down when CurrentPosition.Row + 1 > maze.Rows - 1 => new Position(0, CurrentPosition.Col),
                Direction.Down => new Position(CurrentPosition.Row + 1, CurrentPosition.Col),
                Direction.Left when CurrentPosition.Col - 1 < 0 => new Position(CurrentPosition.Row, maze.Columns - 1),
                Direction.Left => new Position(CurrentPosition.Row, CurrentPosition.Col - 1),
                Direction.Right when CurrentPosition.Col + 1 > maze.Columns - 1 => new Position(CurrentPosition.Row, 0),
                Direction.Right => new Position(CurrentPosition.Row, CurrentPosition.Col + 1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool HasCollisionWithWall(IPosition newPosition, Maze maze) //todo maze
        {
            return maze.Tiles[newPosition.Row, newPosition.Col].SpritesOnTile.Any(d => d.Display == _wallTileType.Display);
        }
    }
}