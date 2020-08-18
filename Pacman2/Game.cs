namespace Pacman2
{
    public class Game
    {
        private readonly Level _level;

        public Game(Level level)
        {
            _level = level;
        }
        public void Play()
        {
            _level.Play();
        }
    }
}