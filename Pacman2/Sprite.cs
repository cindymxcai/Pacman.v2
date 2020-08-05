using System;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class Sprite : ISprite
    {
        private readonly ITileTypeFactory _tileTypeFactory;
        private IMovementBehaviour MovementBehaviour { get; }
        public IPosition PreviousPosition = new Position();
        public IPosition CurrentPosition { get; } = new Position();
        public Direction CurrentDirection { get; private set; }
        public ITileType TileType { get; }

        public Sprite(int x, int y, IMovementBehaviour randomMovement, ITileTypeFactory tileTypeFactory)
        {
            CurrentPosition.X = x;
            CurrentPosition.Y = y;
            _tileTypeFactory = tileTypeFactory;
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
            CurrentPosition.X = x;
            CurrentPosition.Y = y;
        }

        public (int, int) GetNewPosition(Maze maze)
        {
            return CurrentDirection switch
            {
                Direction.Up when CurrentPosition.X - 1 < 0 => (maze.Row - 1, CurrentPosition.Y),
                Direction.Up => (CurrentPosition.X - 1, CurrentPosition.Y),
                Direction.Down when CurrentPosition.X + 1 > maze.Row - 1 => (0, CurrentPosition.Y),
                Direction.Down => (CurrentPosition.X + 1, CurrentPosition.Y),
                Direction.Left when CurrentPosition.Y - 1 < 0 => (CurrentPosition.X, maze.Column - 1),
                Direction.Left => (CurrentPosition.X, CurrentPosition.Y - 1),
                Direction.Right when CurrentPosition.Y + 1 > maze.Column - 1 => (CurrentPosition.X, 0),
                Direction.Right => (CurrentPosition.X, CurrentPosition.Y + 1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool HasCollisionWithWall((int, int) newPosition, Maze maze)
        {
            var (x, y) = newPosition;
            return maze.Tiles[x, y].TileType.Display == _tileTypeFactory.Wall.Display;
        }
    }
}