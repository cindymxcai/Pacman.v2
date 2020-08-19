using System;
using System.Runtime.InteropServices;

namespace Pacman2.Interfaces
{
    public interface IMovementBehaviour
    {
        Direction GetNewDirection(Direction direction, [Optional]ConsoleKey consoleKey);
    }
}