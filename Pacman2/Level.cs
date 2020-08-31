using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pacman2.Interfaces;

namespace Pacman2
{
    /// <summary>
    /// The Level class handles playing one level by simulating a tick every .2 seconds,
    /// taking in player control, updating the model and view in return and checking for win/lose conditions
    /// </summary>
    public class Level : ILevel
    {
        private readonly IList<IMovingSprite> _sprites;
        private readonly IMaze _maze;
        private readonly IPlayerInput _playerInput;
        private readonly IDisplay _display;
        public bool PacmanIsAlive { get; private set; } = true;
        public int LivesRemaining { get; private set; } = 3;
        public int Score { get; private set; }

        private int ScoreLost { get; set; }

        public Level(IList<IMovingSprite> sprites, IMaze maze, IPlayerInput playerInput, IDisplay display)
        {
            _sprites = sprites;
            _maze = maze;
            _playerInput = playerInput;
            _display = display;
            _maze.ResetSpritePositions(_sprites);
        }

        public void Play()
        {
            while (PacmanIsAlive && !HasWon())
            {
                var input = _playerInput.TakeInput();
                if (_playerInput.HasPressedQuit(input))
                    PacmanIsAlive = false;

                while (!_playerInput.HasNewInput())
                {
                    MoveSprites(input);

                    if (!PacmanIsAlive)
                        break;

                    if (HasWon())
                        break;
                    
                    _maze.Render();
                    _display.LevelStats(Score, LivesRemaining);
                    Score = _maze.PelletsEaten - ScoreLost;

                    Task.Delay(200).Wait();
                    Console.Clear();
                }

                if (PacmanIsAlive && !HasWon())
                    continue;
                break;
            }
        }

        private void MoveSprites(ConsoleKey input)
        {
            foreach (var sprite in _sprites)
            {
                sprite.UpdateDirection(input);
                UpdateSpritePosition(sprite);
            }
            if (_maze.PacmanHasCollisionWithGhost(_sprites)) HandleLostLife();
        }

        public void UpdateSpritePosition(IMovingSprite sprite)
        {
            var newPosition = _maze.GetNewPosition(sprite.CurrentDirection, sprite.CurrentPosition);

            if (_maze.SpriteHasCollisionWithWall(newPosition)) return;
            _maze.MoveSpriteToNewPosition(sprite, newPosition);
            sprite.UpdatePosition(newPosition);
        }

        public void HandleLostLife()
        {
            LivesRemaining--;
            ScoreLost += 10;
            _display.LifeLost(LivesRemaining);
            _maze.ResetSpritePositions(_sprites);
            if (LivesRemaining == 0) PacmanIsAlive = false;
        }


        public bool HasWon()
        {
            return _maze.HasNoPelletsRemaining();
        }
    }
}