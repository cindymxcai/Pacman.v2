using System;
using System.IO;

namespace Pacman2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));

            var maze = new Maze();
            maze.CreateMaze(mazeData);
            maze.Render();
        }
    }
}