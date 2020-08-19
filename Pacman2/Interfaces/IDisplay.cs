namespace Pacman2.Interfaces
{
    public interface IDisplay
    {
        void LostPrompt(int totalScore);
        void Score(int pelletsEaten, int livesRemaining);
        void Win(int totalScore); 
        void LifeLost(int livesRemaining);
        void Welcome();
        void NextLevel();
    }
}