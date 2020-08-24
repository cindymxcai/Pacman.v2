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
        public int Priority { get; }
        public string Icon { get; }
        public ConsoleColor Colour { get; }
        public ISpriteDisplay Display { get; set; }
        public IPosition PreviousPosition { get; private set; }
        
        private readonly string _ghostSpriteDisplay = new GhostSpriteDisplay().Icon;
        
        public MovingSprite(IPosition position, IMovementBehaviour movementBehaviour, ISpriteDisplay spriteDisplay)
        {
            CurrentPosition = position;
            MovementBehaviour = movementBehaviour;
            CurrentDirection = Direction.Up;
            spriteDisplay.SetSpriteDisplay(CurrentDirection);
            Display = spriteDisplay;
            Priority = spriteDisplay.Priority;
            Icon = spriteDisplay.Icon;
            Colour = spriteDisplay.Colour;
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
            return Icon != _ghostSpriteDisplay;
        }
    }
}