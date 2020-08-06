namespace Pacman2.Interfaces
{
    public interface ISprite
    {
        Direction CurrentDirection { get; }
        ITileType TileType { get; }
        void UpdateDirection();
        void UpdatePosition(IPosition position, Maze maze);
        IPosition GetNewPosition(Maze maze);
    }
}