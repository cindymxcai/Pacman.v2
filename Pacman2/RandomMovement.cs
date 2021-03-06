using System;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class RandomMovement : IMovementBehaviour
    {
        private readonly IRng _rng;

        public RandomMovement(IRng random)
        {
            _rng = random;
        }
        
        public Direction GetNewDirection(Direction currentDirection, ConsoleKey consoleKey = (ConsoleKey) 0)
        {
            currentDirection = _rng.Next(0, Enum.GetValues(typeof(Direction)).Length) switch
            {
                0 => Direction.Up,
                1 => Direction.Down,
                2 => Direction.Left,
                3 => Direction.Right,
                _ => throw new Exception()
            };
            return currentDirection;
        }
    }
}