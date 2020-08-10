namespace Pacman2.Interfaces
{
    public interface ISprite
    {
        ITileType Display { get; }
        IPosition PreviousPosition { get; }
        void UpdateDirection();
        void UpdatePosition(Maze maze);
    }
}