namespace Pacman2.Interfaces
{
    public interface ILevel
    {
        int LivesRemaining { get; }
        void Play();
        void HandleLostLife();
        void UpdateSpritePosition(IMovingSprite sprite);
        bool HasWon();
    }
}