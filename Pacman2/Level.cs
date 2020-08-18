using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pacman2.Interfaces;
using Pacman2.Sprites;

namespace Pacman2
{
    public class Level : ILevel
    {
        public IList<IMovingSprite> Sprites;
        private readonly IMaze _maze;
        private readonly IPlayerInput _playerInput;
        private readonly IDisplay _display;
        public bool PacmanIsAlive = true;
        public int LivesRemaining { get; private set; } = 3;

        public Level(IMaze maze, IPlayerInput playerInput, IDisplay display, IMovementBehaviour randomMovement, IMovementBehaviour playerMovement, ISpriteDisplay ghostDisplay, ISpriteDisplay pacmanDisplay)
        {  
            Sprites = new List<IMovingSprite>
            {
                new MovingSprite(maze.GetTilePosition(9, 9), randomMovement, ghostDisplay),
                new MovingSprite(maze.GetTilePosition(9, 9), randomMovement, ghostDisplay),
                new MovingSprite(maze.GetTilePosition(2, 1), playerMovement, pacmanDisplay)
            };
            
            _maze = maze;
            _playerInput = playerInput;
            _display = display;

            foreach (var sprite in Sprites)
            {
                UpdateSpritePosition(sprite);
            }
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
            foreach (var sprite in Sprites)
            {
                sprite.UpdateDirection(input);
                UpdateSpritePosition(sprite);
                IsPacmanEaten(sprite);
            }
        }

        private void HandlePacmanDeath()
        {
            _display.LostPrompt();

            if (!_playerInput.HasPressedQuit(_playerInput.TakeInput()))
            {
                PacmanIsAlive = true;
                _maze.ResetSpritePositions(Sprites, _maze.GetTilePosition(9, 9), _maze.GetTilePosition(1, 1));
            }
            else
                _display.GameEnd();
        }

        public void IsPacmanEaten(IMovingSprite sprite)
        {
            if (!_maze.PacmanHasCollisionWithGhost(sprite)) return;
            LivesRemaining--;
            _display.LifeLost(LivesRemaining);
            _maze.ResetSpritePositions(Sprites, _maze.GetTilePosition(9, 9), _maze.GetTilePosition(1, 1));
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