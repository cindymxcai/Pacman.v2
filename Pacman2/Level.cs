using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class Level : ILevel
    {
        private readonly IList<IMovingSprite> _sprites;
        private readonly IMaze _maze;
        private readonly IPlayerInput _playerInput;
        private readonly IDisplay _display;
        public bool PacmanIsAlive = true;
        public int LivesRemaining { get; private set; } = 3;

        public Level(IList<IMovingSprite> sprites, IMaze maze, IPlayerInput playerInput, IDisplay display)
        {

            _sprites = sprites;
            _maze = maze;
            _playerInput = playerInput;
            _display = display;
            foreach (var sprite in _sprites) UpdateSpritePosition(sprite);
        }
        
        public void Play()
        {
            while (PacmanIsAlive && !HasWon())
            {
                var input = _playerInput.TakeInput();
                if (_playerInput.HasPressedQuit(input)) PacmanIsAlive = false;

                while (!_playerInput.HasNewInput())
                {
                    MoveSprites(input);

                    if (!PacmanIsAlive)
                        break;

                    _maze.Render();
                    _display.Score(_maze.PelletsEaten, LivesRemaining);

                    Task.Delay(200).Wait();
                    Console.Clear();
                }

                if (PacmanIsAlive && !HasWon()) continue;
                if (!PacmanIsAlive) HandlePacmanDeath();
                if (!HasWon()) continue;
                _display.Win();
                break;
            }
        }

        private void MoveSprites(ConsoleKey input)
        {
            foreach (var sprite in _sprites)
            {
                sprite.UpdateDirection(input);
                UpdateSpritePosition(sprite);
                if (_maze.PacmanHasCollisionWithGhost(sprite)) HandleLostLife();
            }
        }

        private void HandlePacmanDeath()
        {
            _display.LostPrompt();
            var input = _playerInput.TakeInput();
            if (!_playerInput.HasPressedQuit(input))
            {
                PacmanIsAlive = true;
                _maze.ResetSpritePositions(_sprites);
            }
            else
                _display.GameEnd();
        }

        public void HandleLostLife()
        {
            LivesRemaining--;
            _display.LifeLost(LivesRemaining);
            _maze.ResetSpritePositions(_sprites);
            if (LivesRemaining == 0) PacmanIsAlive = false;
        }

        public void UpdateSpritePosition(IMovingSprite sprite)
        {
            var newPosition = _maze.GetNewPosition(sprite.CurrentDirection, sprite.CurrentPosition);

            if (_maze.SpriteHasCollisionWithWall(newPosition)) return;
            _maze.MoveSpriteToNewPosition(sprite, newPosition);
            sprite.UpdatePosition(newPosition);
        }

        public bool HasWon()
        {
            return _maze.HasNoPelletsRemaining() ;
        }
    }
}