using System;

namespace Pacman2
{
    public class Ghost
    {
        private IMovementBehaviour MovementBehaviour { get; }
        public int X { get; }
        public int Y { get; }
        public string Display { get; } = " \u1571 ";
        public ConsoleColor Colour { get; } = ConsoleColor.Red;
        
        public Direction CurrentDirection { get; set; }

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

    }
}