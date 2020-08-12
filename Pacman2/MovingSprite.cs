using System;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class MovingSprite : IMovingSprite
    {
        private IMovementBehaviour MovementBehaviour { get; }
        public IPosition CurrentPosition { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public ISpriteDisplay Display { get; }

        public MovingSprite(IPosition position, IMovementBehaviour movementBehaviour, ISpriteDisplay display)
        {
            CurrentPosition = position;
            MovementBehaviour = movementBehaviour;
            CurrentDirection = Direction.Up;
            display.SetSpriteDisplay(CurrentDirection);
            Display = display; 
        }

        public void UpdateDirection(ConsoleKey consoleKey)
        {
            CurrentDirection = MovementBehaviour.GetNewDirection(CurrentDirection, consoleKey);
            Display.SetSpriteDisplay(CurrentDirection);
        }

        public void UpdatePosition(IPosition newPosition)
        {
            CurrentPosition = newPosition;
        }

        public void Render()
        {
            Console.ForegroundColor = Display.Colour;
            Console.Write(Display.Icon);
            Console.ResetColor();
        }
    }
}