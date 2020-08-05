using System;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class Sprite : ISprite
    {
        private readonly WallTileType _wallTileType = new WallTileType();
        private IMovementBehaviour MovementBehaviour { get; }
        public IPosition PreviousPosition;
        public IPosition CurrentPosition { get; }
        public Direction CurrentDirection { get; private set; }
        public ITileType TileType { get; }

        public Sprite(IPosition position, IMovementBehaviour randomMovement, ITileTypeFactory tileTypeFactory)
        {
            CurrentPosition = position;
            MovementBehaviour = randomMovement;
            CurrentDirection = randomMovement.GetNewDirection();
            TileType = tileTypeFactory.Ghost; //todo move to behavior
        }

        public void UpdateDirection()
        {
            CurrentDirection = MovementBehaviour.GetNewDirection();
        }

        public void UpdatePosition((int, int) newPosition, Maze maze)
        {
            var (x, y) = newPosition;
            if (HasCollisionWithWall((x, y), maze)) return;
            PreviousPosition = CurrentPosition;

            CurrentPosition.Row = x;
            CurrentPosition.Col = y;
        }

        public (int, int) GetNewPosition(Maze maze)
        {
            return CurrentDirection switch
            {
                Direction.Up when CurrentPosition.Row - 1 < 0 => (maze.Rows - 1, CurrentPosition.Col), //todo extract out to code and move to maze
                Direction.Up => (CurrentPosition.Row - 1, CurrentPosition.Col),
                Direction.Down when CurrentPosition.Row + 1 > maze.Rows - 1 => (0, CurrentPosition.Col),
                Direction.Down => (CurrentPosition.Row + 1, CurrentPosition.Col),
                Direction.Left when CurrentPosition.Col - 1 < 0 => (CurrentPosition.Row, maze.Columns - 1),
                Direction.Left => (CurrentPosition.Row, CurrentPosition.Col - 1),
                Direction.Right when CurrentPosition.Col + 1 > maze.Columns - 1 => (CurrentPosition.Row, 0),
                Direction.Right => (CurrentPosition.Row, CurrentPosition.Col + 1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool HasCollisionWithWall((int, int) newPosition, Maze maze) //todo maze
        {
            var (x, y) = newPosition;
            return maze.Tiles[x, y].TileType.Display == _wallTileType.Display;
        }
    }
}