using System;

namespace Pacman2.Interfaces
{
    public interface IMovementBehaviour
    {
        Direction GetNewDirection(Direction direction, ConsoleKey consoleKey);
    }
}