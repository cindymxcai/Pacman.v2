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
            var playerMovement = new PlayerControlMovement(playerInput);
            

            var ghosts = new List<IMovingSprite>
            {
                new MovingSprite(new Position(4, 1), randomMovement, ghostDisplay),
                new MovingSprite(new Position(2, 1), playerMovement, pacmanDisplay)
            };
            foreach (var sprite in ghosts)
            {
                maze.UpdateSpritePosition(sprite);
            }

            while (true)
            {
                maze.Render();

                foreach (var ghostSprite in ghosts)
                {
                    ghostSprite.UpdateDirection();
                    maze.UpdateSpritePosition(ghostSprite);
                }

                Task.Delay(100).Wait();
                Console.Clear();
            }
        }
    }
}