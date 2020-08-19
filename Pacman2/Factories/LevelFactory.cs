using System.Collections.Generic;
using Pacman2.Interfaces;
using Pacman2.Sprites;

namespace Pacman2.Factories
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

        public ILevel CreateLevel(IGameSettings mazeData, int levelNumber)
        {
            var maze = _mazeFactory.CreateMaze(mazeData.LevelSettings[levelNumber - 1]);
            
            var sprites = new List<IMovingSprite>
            {
                new MovingSprite(maze.GetTilePosition(9, 9), _randomMovement, _ghostDisplay),
                new MovingSprite(maze.GetTilePosition(9, 9), _randomMovement, _ghostDisplay),
                new MovingSprite(maze.GetTilePosition(2, 1), _playerMovement, _pacmanDisplay)
            };
            
            return new Level(sprites, maze, _playerInput, _display);
        }
    }
}