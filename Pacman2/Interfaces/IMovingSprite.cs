
namespace Pacman2.Interfaces
{
    public interface IMovingSprite : ISprite
    {
        IPosition CurrentPosition { get; }
        Direction CurrentDirection { get; }
        void UpdateDirection();
        void UpdatePosition(IPosition newPosition);
    }
}