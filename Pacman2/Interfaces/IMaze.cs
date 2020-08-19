using System.Collections.Generic;

namespace Pacman2.Interfaces
{
    public interface IMaze
    {
        int Columns { get; }
        int Rows { get; }
        int PelletsEaten { get; set; }
        bool HasNoPelletsRemaining();
        void Render();
        IPosition GetTilePosition(int rowIndex, int colIndex);
        void MoveSpriteToNewPosition(IMovingSprite sprite, IPosition newPosition);
        ITile GetTileAtPosition(int row, int col);
        ITile GetTileAtPosition(IPosition position);
        IPosition GetNewPosition(Direction currentDirection, IPosition currentPosition);
        bool PacmanHasCollisionWithGhost(IMovingSprite sprite);
        bool SpriteHasCollisionWithWall(IPosition newPosition);
        void ResetSpritePositions(IEnumerable<IMovingSprite> sprites);
    }
}