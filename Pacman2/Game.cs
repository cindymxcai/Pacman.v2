using Pacman2.Interfaces;

namespace Pacman2
{
    public class Game
    {
        private int CurrentLevelNumber { get; set; }

        private readonly GameSettingLoader _gameSettingLoader;
        private readonly LevelFactory _levelFactory;
        private readonly IDisplay _display;

        public Game(GameSettingLoader gameSettingLoader, LevelFactory levelFactory, IDisplay display)
        {
            _gameSettingLoader = gameSettingLoader;
            _levelFactory = levelFactory;
            _display = display;
        }
        
        public void Play()
        {
            CurrentLevelNumber = 1;
            _display.Welcome();
            var mazeData = _gameSettingLoader.GetMazeData();
            
            var level = _levelFactory.CreateLevel(mazeData, CurrentLevelNumber); 
            level.Play();
        }
    }
}