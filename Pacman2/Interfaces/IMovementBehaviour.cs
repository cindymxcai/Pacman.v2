using System;

namespace Pacman2.Interfaces
{
    public interface IMovementBehaviour
    {
        public Direction GetNewDirection(Direction direction, ConsoleKey consoleKey);
    }
}