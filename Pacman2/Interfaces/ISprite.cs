namespace Pacman2.Interfaces
{
    public interface ISprite
    {
        Direction CurrentDirection { get; }
        ITileType TileType { get; }
        void UpdateDirection();
        void UpdatePosition((int, int) newPosition, Maze maze);
        (int, int) GetNewPosition(Maze maze);
    }
}