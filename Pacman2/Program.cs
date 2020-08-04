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
            var wallTile = new WallTileType();
            var emptyTile = new EmptyTileType();
            var pelletTile = new PelletTileType();
            
            var parser = new Parser();
            var maze = new Maze(mazeData, parser);
            maze.Render();

            
            var tileTypeFactory = new TileTypeFactory( wallTile, emptyTile, pelletTile, ghostTile);

            var rng = new Rng();
            var randomMovement = new RandomMovement(rng);
            var ghost = new Ghost(2,1, randomMovement, tileTypeFactory);

            
            while (true)
            {
                maze.Render();
                
                var tileDisplay = maze.Tiles[ghost.PrevX, ghost.PrevY].TileType;
                
                ghost.UpdatePosition(ghost.GetNewPosition(maze), maze);
                maze.UpdateArray( ghost.PrevX, ghost.PrevY, tileDisplay);
                maze.UpdateArray( ghost.X, ghost.Y, ghost.TileType);
                ghost.UpdateDirection();
                Thread.Sleep(100);
                Console.Clear();
            }
        }
    }
}