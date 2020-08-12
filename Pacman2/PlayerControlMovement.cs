using System;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class PlayerControlMovement : IMovementBehaviour
    {
        private readonly IPlayerInput _playerInput;

        public PlayerControlMovement(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public Direction GetNewDirection(Direction currentDirection)
        {
            var newDirection = _playerInput.TakeInput() switch
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