using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class Game
    {
        private readonly IList<IMovingSprite> _sprites;
        private readonly Maze _maze;
        private readonly IPlayerInput _playerInput;
        private readonly IDisplay _display;
        public bool PacmanIsAlive = true;

        public Game(IList<IMovingSprite> sprites, Maze maze, IPlayerInput playerInput, IDisplay display)
        {
            _sprites = sprites;
            _maze = maze;
            _playerInput = playerInput;
            _display = display;

            foreach (var sprite in _sprites)
            {
                _maze.UpdateSpritePosition(sprite);
            }
        }
        
        public void Play()
        {
            while (PacmanIsAlive)
            {
                var input = _playerInput.TakeInput();

                while (!_playerInput.HasNewInput())
                {
                    MoveSprites(input);

                    if (!PacmanIsAlive)
                    {
                        HandlePacmanDeath();
                        break;
                    }
                    
                    _maze.Render();

                    Task.Delay(200).Wait();
                    Console.Clear();
                }

                if (!PacmanIsAlive) break;
            }
        }

        private void MoveSprites(ConsoleKey input)
        {
            foreach (var sprite in _sprites)
            {
                sprite.UpdateDirection(input);
                _maze.UpdateSpritePosition(sprite);
                IsPacmanEaten(sprite);
            }
        }

        private void HandlePacmanDeath()
        {
            _display.LostPrompt();
            
            if (_playerInput.HasPressedQuit())
                _display.GameEnd();
            else
                PacmanIsAlive = true;
        }

        public void IsPacmanEaten(IMovingSprite sprite)
        {
            if (!_maze.PacmanHasCollisionWithGhost(sprite)) return;
            PacmanIsAlive = false;
        }
    }
}