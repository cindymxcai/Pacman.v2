using Pacman2.Interfaces;

namespace Pacman2
{
    public class LevelFactory
    {
        private readonly MazeFactory _mazeFactory;
        private readonly IDisplay _display;
        private readonly IPlayerInput _playerInput;
        private readonly IMovementBehaviour _randomMovement;
        private readonly IMovementBehaviour _playerMovement;
        private readonly ISpriteDisplay _ghostDisplay;
        private readonly ISpriteDisplay _pacmanDisplay;
        
        public LevelFactory(MazeFactory mazeFactory, IDisplay display, IPlayerInput playerInput, IMovementBehaviour randomMovement, IMovementBehaviour playerMovement, ISpriteDisplay ghostDisplay, ISpriteDisplay pacmanDisplay)
        {
            _mazeFactory = mazeFactory;
            _display = display;
            _playerInput = playerInput;
            _randomMovement = randomMovement;
            _playerMovement = playerMovement;
            _ghostDisplay = ghostDisplay;
            _pacmanDisplay = pacmanDisplay;
        }

        public ILevel CreateLevel(GameSettings mazeData, int levelNumber)
        {
            return new Level(_mazeFactory.CreateMaze(mazeData.LevelSettings[levelNumber-1]), _playerInput, _display, _randomMovement, _playerMovement, _ghostDisplay, _pacmanDisplay);
        }
    }
}