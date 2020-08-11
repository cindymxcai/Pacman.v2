using System;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class MovingSprite : IMovingSprite
    {
        private IMovementBehaviour MovementBehaviour { get; }
        public IPosition CurrentPosition { get; set; }
        public Direction CurrentDirection { get; private set; }
        public ISpriteDisplay Display { get; }

        public MovingSprite(IPosition position, IMovementBehaviour randomMovement, ISpriteDisplay display)
        {
            CurrentPosition = position;
            MovementBehaviour = randomMovement;
            CurrentDirection = Direction.Up;
            Display = display; //todo move to behavior
        }

        public void UpdateDirection()
        {
            CurrentDirection = MovementBehaviour.GetNewDirection();
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