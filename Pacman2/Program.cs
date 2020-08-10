using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Pacman2.Interfaces;

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
            var ghostTile = new GhostTileType();

            var ghosts = new List<ISprite>
            {
                new Sprite(new Position(4, 1), randomMovement, ghostTile),
                new Sprite(new Position(2, 1), randomMovement, ghostTile)
            };

            while (true)
            {
                maze.Render();

                foreach (var ghostSprite in ghosts)
                {
                    ghostSprite.UpdatePosition(maze);
                    ghostSprite.UpdateDirection();
                    maze.RemoveTileTypeFromTile(ghostSprite.PreviousPosition, ghostSprite.Display);
                }
                
                Thread.Sleep(100);
                Console.Clear();
            }
        }
    }
}