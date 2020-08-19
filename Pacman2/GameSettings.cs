using Pacman2.Interfaces;

namespace Pacman2
{
    public class GameSettings : IGameSettings
    {
        
        public string[] LevelSettings { get; }
        public int MaxLevels { get; }
        public GameSettings(string[] levelSettings, int maxLevels)
        {
            LevelSettings = levelSettings;
            MaxLevels = maxLevels;
        }
      
    }
}