using Pacman2.Interfaces;

namespace Pacman2
{
    public class Game
    {

        private readonly GameSettingLoader _gameSettingLoader;
        private readonly LevelFactory _levelFactory;
        private readonly IDisplay _display;
        public int CurrentLevelNumber { get; set; } = 1;
        public bool IsPlaying { get; set; } = true;

        public Game(GameSettingLoader gameSettingLoader, LevelFactory levelFactory, IDisplay display)
        {
            _gameSettingLoader = gameSettingLoader;
            _levelFactory = levelFactory;
            _display = display;
        }
        
        public void Play()
        {
            _display.Welcome();
            var mazeData = _gameSettingLoader.GetMazeData();

            while (IsPlaying)
            {
                var level = _levelFactory.CreateLevel(mazeData, CurrentLevelNumber); 
                level.Play();
                HandleNextLevel(mazeData);
            }
        }

        public void HandleNextLevel(IGameSettings mazeData)
        {
            CurrentLevelNumber++;
            if (CurrentLevelNumber >= mazeData.MaxLevels) IsPlaying = false;
        }
    }
}