using System;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;

namespace Pacman2.Sprites
{
    public class MovingSprite : IMovingSprite
    {
        private IMovementBehaviour MovementBehaviour { get; }
        public IPosition CurrentPosition { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public ISpriteDisplay Display { get; }
        public IPosition PreviousPosition { get; private set; }
        
        private readonly GhostSpriteDisplay _ghostSpriteDisplay = new GhostSpriteDisplay();

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
            PreviousPosition = CurrentPosition;
            CurrentPosition = newPosition;
        }

        public bool IsPacman()
        {
            return Display.Icon != _ghostSpriteDisplay.Icon;
        }
        
    }
}