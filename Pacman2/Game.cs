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
        public bool IsPlaying = true;

        public Game(IList<IMovingSprite> sprites, Maze maze, IPlayerInput playerInput)
        {
            _sprites = sprites;
            _maze = maze;
            _playerInput = playerInput;
            
            foreach (var sprite in _sprites)
            {
                _maze.UpdateSpritePosition(sprite);
            }
        }
        
        public void Play()
        {
            while (IsPlaying)
            {
                var input = _playerInput.TakeInput();
                
                while (!Console.KeyAvailable)
                {

                    foreach (var sprite in _sprites)
                    {
                        sprite.UpdateDirection(input);
                        _maze.UpdateSpritePosition(sprite);
                        IsPacmanEaten(sprite);
                    }

                    if (!IsPlaying)
                        break;
                    
                    _maze.Render();

                    Task.Delay(200).Wait();
                    Console.Clear();
                }
                if (IsPlaying) continue;
                break;
            }
        }

        public void IsPacmanEaten(IMovingSprite sprite)
        {
            if (!_maze.PacmanHasCollisionWithGhost(sprite)) return;
            IsPlaying = false;
            
            Display.LostPrompt();

            if (_playerInput.HasPressedQuit())
                Display.GameEnd();
            else
                IsPlaying = true;
        }
    }
}