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
            

            var movingSprites = new List<IMovingSprite>
            {
                new MovingSprite(maze.GetTilePosition(4, 1), randomMovement, ghostDisplay),
                new MovingSprite(maze.GetTilePosition(2, 1), playerMovement, pacmanDisplay)
            };
            
            foreach (var sprite in movingSprites)
            {
                maze.UpdateSpritePosition(sprite);
            }

            while (true)
            {
                var input = playerInput.TakeInput();
                
                while (!Console.KeyAvailable)
                {
                    maze.Render();

                    foreach (var sprite in movingSprites)
                    {
                        sprite.UpdateDirection(input);
                        maze.UpdateSpritePosition(sprite);
                    }
                    Task.Delay(200).Wait();
                    Console.Clear();
                }

            }
            
        }
    }
}