namespace Pacman2.Interfaces
{
    public interface IDisplay
    {
        void LostPrompt();
        void GameEnd();
        void Score(int pelletsEaten);
        void Win(); 
        void LifeLost(int livesRemaining);
    }
}