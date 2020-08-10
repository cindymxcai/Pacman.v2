using Pacman2.Interfaces;

namespace Pacman2
{
    public class Sprite : ISprite
    {
        private IMovementBehaviour MovementBehaviour { get; }
        public IPosition CurrentPosition { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public ITileType Display { get; }
        public IPosition PreviousPosition { get; private set; }

        public Sprite(IPosition position, IMovementBehaviour randomMovement, ITileType display)
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

        public void UpdatePosition(Maze maze)
        {
            var newPosition = maze.GetNewPosition(CurrentDirection, CurrentPosition);
            if (maze.SpriteHasCollisionWithWall(newPosition)) return;
            PreviousPosition = CurrentPosition;
            CurrentPosition = newPosition;
            maze.AddTileTypeToTile(CurrentPosition, Display);
        }
    }
}