using Pacman2.Factories;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class Game
    {
        private readonly IPlayerInput _playerInput;
        private readonly GameSettingLoader _gameSettingLoader;
        private readonly LevelFactory _levelFactory;
        private readonly IDisplay _display;
        public int CurrentLevelNumber { get; private set; } = 1;
        public bool IsPlaying { get; private set; } = true;

        public int TotalScore { get; set; }

        public Game(IPlayerInput playerInput, GameSettingLoader gameSettingLoader, LevelFactory levelFactory, IDisplay display)
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
            if (!_playerInput.HasPressedQuit(input))
            {
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