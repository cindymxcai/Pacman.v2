using Pacman2.Factories;
using Pacman2.Interfaces;

namespace Pacman2
{
    /// <summary>
    /// The highest level object that handles wrapping up responsibility of flow before and after a level is played
    /// as well as starting each new level
    /// </summary>
    public class Game 
    {
        private readonly IPlayerInput _playerInput;
        private readonly IGameSettingLoader _gameSettingLoader;
        private readonly LevelFactory _levelFactory;
        private readonly IDisplay _display;
        public int CurrentLevelNumber { get; private set; } = 1;
        public bool IsPlaying { get; private set; } = true;
        private int TotalScore { get; set; }

        public Game(IPlayerInput playerInput, IGameSettingLoader gameSettingLoader, LevelFactory levelFactory, IDisplay display)
        {
            _playerInput = playerInput;
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
                TotalScore += level.Score;
                
                if (level.PacmanIsAlive)
                    HandleNextLevel(mazeData);
                else
                    HandlePacmanDeath();
            }
        }
        
        private void HandlePacmanDeath()
        {
            _display.LostPrompt(TotalScore);
            var input = _playerInput.TakeInput();
            if (_playerInput.HasPressedQuit(input))
                IsPlaying = false;
            else
            {
                TotalScore = 0;
                CurrentLevelNumber = 1;   
            }
        }

        public void HandleNextLevel(IGameSettings mazeData)
        {
            CurrentLevelNumber++;
            if (CurrentLevelNumber < mazeData.MaxLevels)
                _display.NextLevel();
            else
            {
                IsPlaying = false;
                _display.Win(TotalScore);
            }
        }
    }
}