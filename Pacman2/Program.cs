using System;
using System.IO;
using System.Threading;

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
                /// ...
                /// ...
                /// .O.
                maze.AddTileTypeToTile(ghost.CurrentPosition, ghost.TileType); //todo, rename to add tile type to tile, or similar to represent new logic
                maze.Render();
                var newPosition =  ghost.GetNewPosition(maze);
                
                ghost.UpdatePosition(newPosition, maze);
                ghost.UpdateDirection();
                maze.RemoveTileTypeFromTile(ghost.PreviousPosition, ghost.TileType);

                Thread.Sleep(100);
                Console.Clear();

            }
        }
    }
}