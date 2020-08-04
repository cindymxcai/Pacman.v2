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

            var parser = new Parser();
            var maze = new Maze(mazeData, parser);
            maze.Render();

            var rng = new Rng();
            var randomMovement = new RandomMovement(rng);
            var ghost = new Ghost(2,1, randomMovement);

            
            while (true)
            {
                maze.Render();
                
                var newPosition = ghost.GetNewPosition(maze);
                var tileDisplay = maze.Tiles[newPosition.Item1, newPosition.Item2].Display;
                var tileColour = maze.Tiles[newPosition.Item1, newPosition.Item2].Colour;

                ghost.UpdatePosition(newPosition, maze);
                maze.UpdateArray( ghost.PrevX, ghost.PrevY, tileDisplay, tileColour);
                maze.UpdateArray( ghost.X, ghost.Y, ghost.Display, ghost.Colour);
                ghost.UpdateDirection();
              
                Thread.Sleep(100);
            }
        }
    }
}