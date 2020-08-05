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
            var wallTile = new WallTileType();
            var emptyTile = new EmptyTileType();
            var pelletTile = new PelletTileType();
            var parser = new Parser();
            var maze = new Maze(mazeData, parser);
            var tileTypeFactory = new TileTypeFactory(wallTile, emptyTile, pelletTile, ghostTile);
            var rng = new Rng();
            var randomMovement = new RandomMovement(rng);
            var ghost = new Sprite(new Position(2,1), randomMovement, tileTypeFactory);
            while (true)
            {
                maze.Render();
                var tileDisplay = maze.Tiles[ghost.PreviousPosition.Row, ghost.PreviousPosition.Col].TileType; //todo encapsulate 
                ghost.UpdatePosition(ghost.GetNewPosition(maze), maze); //todo encapsulate 
                maze.UpdateTileTypeForTile(ghost.PreviousPosition, tileDisplay);
                maze.UpdateTileTypeForTile(ghost.CurrentPosition, ghost.TileType);
                ghost.UpdateDirection();
                Thread.Sleep(100);
                Console.Clear();
            }
        }
    }
}