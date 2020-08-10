
namespace Pacman2.Interfaces
{
    public interface IMovingSprite : ISprite
    {
        IPosition CurrentPosition { get; set; }
        Direction CurrentDirection { get; }
        IPosition PreviousPosition { get; }
        void UpdateDirection();
        void UpdatePosition(IPosition newPosition);
    }
}