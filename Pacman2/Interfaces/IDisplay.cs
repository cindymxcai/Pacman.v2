namespace Pacman2.Interfaces
{
    public interface IDisplay
    {
        void LostPrompt();
        void GameEnd();
        void Score(int pelletsEaten, int livesRemaining);
        void Win(); 
        void LifeLost(int livesRemaining);
    }
}