using System;
using System.IO;
using System.Threading;
using Pacman2.Tiles;

namespace Pacman2
{
    public static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello World!");
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var ghostTile = new GhostTileType();
            var parser = new Parser();
            var maze = new Maze(mazeData, parser);
            var rng = new Rng();
            var randomMovement = new RandomMovement(rng);
            var ghost = new Sprite(new Position(2,1), randomMovement, ghostTile);
            while (true)
            {
                maze.Render();
                var newPosition =  ghost.GetNewPosition(maze);
                var tileDisplay = maze.GetTileDisplayAtPosition(newPosition); //todo encapsulate 
                
                ghost.UpdatePosition(newPosition, maze);
                maze.UpdateTileTypeForTile(ghost.PreviousPosition, tileDisplay);
                maze.UpdateTileTypeForTile(ghost.CurrentPosition, ghost.TileType);
                ghost.UpdateDirection();
                Thread.Sleep(100);
                Console.Clear();
            }
        }
    }
}