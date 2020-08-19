namespace Pacman2.Interfaces
{
    public interface ILevel
    {
        int LivesRemaining { get; }
        bool PacmanIsAlive { get; }
        int Score { get; set; }
        void Play();
        void HandleLostLife();
        void UpdateSpritePosition(IMovingSprite sprite);
        bool HasWon();
    }
}