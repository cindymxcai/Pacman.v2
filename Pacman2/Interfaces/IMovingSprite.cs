
using System;

namespace Pacman2.Interfaces
{
    public interface IMovingSprite : ISprite
    {
        IPosition CurrentPosition { get; set; }
        Direction CurrentDirection { get; }
        IPosition PreviousPosition { get; set; }
        IMovementBehaviour MovementBehaviour { get; }
        void UpdateDirection(ConsoleKey consoleKey);
        void UpdatePosition(IPosition newPosition);
        bool IsPacman();
    }
}