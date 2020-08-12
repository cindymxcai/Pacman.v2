using System;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class PlayerControlMovement : IMovementBehaviour
    {

        public Direction GetNewDirection(Direction currentDirection, ConsoleKey consoleKey)
        {
            var newDirection = consoleKey switch
            {
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                _ => currentDirection
            };
            
            return newDirection;
        }
        
    }
}