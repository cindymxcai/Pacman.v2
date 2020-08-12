
using System;

namespace Pacman2.Interfaces
{
    public interface IMovingSprite : ISprite
    {
        IPosition CurrentPosition { get; }
        Direction CurrentDirection { get; }
        void UpdateDirection(ConsoleKey consoleKey);
        void UpdatePosition(IPosition newPosition);
    }
}