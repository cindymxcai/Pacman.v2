using System;

namespace Pacman2
{
    public class Ghost : IGhost
    {
        public int PrevX;
        public int PrevY;
        private IMovementBehaviour MovementBehaviour { get; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Display { get; } = " \u1571 ";
        public ConsoleColor Colour { get; } = ConsoleColor.Red;
        
        public Direction CurrentDirection { get; private set; }

        public Ghost(int x, int y, IMovementBehaviour randomMovement)
        {
            MovementBehaviour = randomMovement;
            X = x;
            Y = y;
            CurrentDirection = randomMovement.GetNewDirection();
        }
        
        public void UpdateDirection()
        {
            CurrentDirection = MovementBehaviour.GetNewDirection();
        }

        public void UpdatePosition(Maze maze)
        {
            var (x, y) = GetNewPosition(maze);
            if (HasCollisionWithWall((x,y), maze)) return;
            PrevX = X;
            PrevY = Y;
            X = x;
            Y = y;
        }

        private (int, int) GetNewPosition(Maze maze)
        {
            return CurrentDirection switch
            {
                Direction.Up when X - 1 < 0 => (maze.Row - 1, Y),
                Direction.Up => (X - 1, Y),
                Direction.Down when X + 1 > maze.Row - 1 => (0, Y),
                Direction.Down => (X + 1, Y),
                Direction.Left when Y - 1 < 0 => (X, maze.Column - 1),
                Direction.Left => (X, Y - 1),
                Direction.Right when Y + 1 > maze.Column - 1 => (X, 0),
                Direction.Right => (X, Y + 1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool HasCollisionWithWall((int, int) newPosition, Maze maze)
        {
            var (x, y) = newPosition;
            return maze.Tiles[x, y].Display == WallTileType.Display;
        }
    }
}