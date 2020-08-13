using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;

namespace Pacman2
{
    public static class Program
    {
        private static void Main()
        {
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var parser = new Parser();
            var maze = new Maze(mazeData, parser);
            
            var ghostDisplay = new GhostSpriteDisplay();
            var pacmanDisplay = new PacmanSpriteDisplay();

            var rng = new Rng();
            var randomMovement = new RandomMovement(rng);
            
            var playerInput = new PlayerInput();
            var playerMovement = new PlayerControlMovement();
            
            List<IMovingSprite> movingSprites = new List<IMovingSprite>
            {
                new MovingSprite(maze.GetTilePosition(4, 1), randomMovement, ghostDisplay),
                new MovingSprite(maze.GetTilePosition(2, 1), playerMovement, pacmanDisplay)
            };
            
            var game = new Game(movingSprites, maze, playerInput);

           game.Play();
            
        }
    }

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
                    _maze.Render();

                    foreach (var sprite in _sprites)
                    {
                        sprite.UpdateDirection(input);
                        _maze.UpdateSpritePosition(sprite);
                        IsPacmanEaten(sprite);
                    }
                    
                    Task.Delay(200).Wait();
                    Console.Clear();
                }
                
                //todo game only stops when another key is pressed
            }
        }

        public void IsPacmanEaten(IMovingSprite sprite)
        {
            if (_maze.PacmanHasCollisionWithGhost(sprite)) 
                IsPlaying = false;
        }
    }
}