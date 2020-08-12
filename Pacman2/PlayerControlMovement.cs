using System;
using PacmanTest;

namespace Pacman2
{
    public class PlayerControlMovement
    {
        private readonly IPlayerInput _playerInput;

        public PlayerControlMovement(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public Direction GetNewDirection()
        {
            var newDirection = _playerInput.TakeInput() switch
            {
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                _ => throw new Exception()
            };

            return newDirection;
        }
    }
}