namespace Pacman2.Interfaces
{
    public interface ILevel
    {
        int LivesRemaining { get; }
        bool PacmanIsAlive { get; }
        int Score { get; }
        void Play();
    }
}