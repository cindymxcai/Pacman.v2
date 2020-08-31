using System;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;

namespace Pacman2.Sprites
{
    public class MovingSprite : IMovingSprite
    {
        /// <summary>
        /// For Moving Sprites (Pacman and Ghosts) 
        /// </summary>
        private IMovementBehaviour MovementBehaviour { get; }
        public IPosition CurrentPosition { get; private set; }
        public IPosition PreviousPosition { get; private set; }

        public Direction CurrentDirection { get; private set; }
        public int Priority { get; }
        public string Icon { get; }
        public ConsoleColor Colour { get; }
        public ISpriteDisplay Display { get; }
        
        private readonly string _ghostSpriteDisplay = new GhostSpriteDisplay().Icon;
        
        public MovingSprite(IPosition position, IMovementBehaviour movementBehaviour, ISpriteDisplay spriteDisplay)
        {
            CurrentPosition = position;
            MovementBehaviour = movementBehaviour;
            CurrentDirection = Direction.Up;
            Display = spriteDisplay;
            Display.SetSpriteDisplay(CurrentDirection);
            Priority = Display.Priority;
            Icon = Display.Icon;
            Colour = Display.Colour;
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