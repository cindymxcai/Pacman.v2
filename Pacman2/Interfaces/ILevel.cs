namespace Pacman2.Interfaces
{
    public interface ILevel
    {
        int LivesRemaining { get; }
        void Play();
        void IsPacmanEaten(IMovingSprite sprite);
        void UpdateSpritePosition(IMovingSprite sprite);
        bool HasWon();
    }
}