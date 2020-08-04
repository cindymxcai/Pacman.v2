﻿using System;
using System.IO;

namespace Pacman2
{
    public static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello World!");
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));

            var maze = new Maze(mazeData);
            maze.Render();

            var randomMovement = new RandomMovement();
            var ghost = new Ghost(2,2, randomMovement);
            maze.UpdateArray(ghost.X, ghost.Y, ghost.Display, ghost.Colour);
            maze.Render();
        }
    }
}