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
            
            var rng = new Rng();
            var randomMovement = new RandomMovement(rng);
            var ghostDisplay = new GhostSpriteDisplay();

            var ghosts = new List<IMovingSprite>
            {
                new MovingSprite(new Position(4, 1), randomMovement, ghostDisplay),
                new MovingSprite(new Position(2, 1), randomMovement, ghostDisplay)
            };

            while (true)
            {
                maze.Render();

                foreach (var ghostSprite in ghosts)
                {
                    maze.UpdateSpritePosition(ghostSprite);
                    ghostSprite.UpdateDirection();
                }

                Task.Delay(100).Wait();
                Console.Clear();
            }
        }
    }
}