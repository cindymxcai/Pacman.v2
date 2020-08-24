using System.Collections.Generic;

namespace Pacman2.Interfaces
{
    public interface IMaze
    {
        int Columns { get; }
        int PelletsEaten { get; set; }
        bool HasNoPelletsRemaining();
        void Render();
        IPosition GetTilePosition(int rowIndex, int colIndex);
        void MoveSpriteToNewPosition(IMovingSprite sprite, IPosition newPosition);
        IPosition GetNewPosition(Direction currentDirection, IPosition currentPosition);
        bool PacmanHasCollisionWithGhost(IList<IMovingSprite> sprite);
        bool SpriteHasCollisionWithWall(IPosition newPosition);
        void ResetSpritePositions(IEnumerable<IMovingSprite> sprites);
    }
}