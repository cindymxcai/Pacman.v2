using System;

namespace Pacman2
{
    public class Ghost : IGhost
    {
        private readonly ITileTypeFactory _tileTypeFactory;
        public int PrevX;
        public int PrevY;
        private IMovementBehaviour MovementBehaviour { get; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Display { get; } 
        public ConsoleColor Colour { get; } 

        public Direction CurrentDirection { get; private set; }

        public Ghost(int x, int y, IMovementBehaviour randomMovement, ITileTypeFactory tileTypeFactory)
        {
            _tileTypeFactory = tileTypeFactory;
            MovementBehaviour = randomMovement;
            X = x;
            Y = y;
            CurrentDirection = randomMovement.GetNewDirection();
            Display = tileTypeFactory.Ghost.Display;
            Colour = tileTypeFactory.Ghost.Colour;

        }
        
        public void UpdateDirection()
        {
            CurrentDirection = MovementBehaviour.GetNewDirection();
        }

        public void UpdatePosition((int, int) newPosition, Maze maze)
        {
            var (x, y) = newPosition;
            if (HasCollisionWithWall((x,y), maze)) return;
            PrevX = X;
            PrevY = Y;
            X = x;
            Y = y;
        }

        public (int, int) GetNewPosition(Maze maze)
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
            return maze.Tiles[x, y].Display == _tileTypeFactory.Wall.Display;
        }
    }
}