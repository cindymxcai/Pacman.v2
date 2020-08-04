using System;
using System.IO;
using PacmanTest;

namespace Pacman2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));

            var maze = new Maze(mazeData);
            maze.Render();
            
            var ghost = new Ghost(2,2);
            maze.UpdateArray(ghost.X, ghost.Y, ghost.Display);
            maze.Render();
        }
    }
}