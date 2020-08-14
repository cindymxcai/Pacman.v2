
using System;

namespace Pacman2.Interfaces
{
    public interface IMovingSprite : ISprite
    {
        IPosition CurrentPosition { get; }
        Direction CurrentDirection { get; }
        IPosition PreviousPosition { get; set; }
        void UpdateDirection(ConsoleKey consoleKey);
        void UpdatePosition(IPosition newPosition);
    }
}