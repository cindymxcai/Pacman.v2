using System;
using Pacman2;

namespace PacmanTest
{
    public class Ghost
    {
        public int X { get; }
        public int Y { get; }
        public string Display { get; } = " \u1571 ";
        public ConsoleColor Colour { get; } = ConsoleColor.Red;
        public Ghost(int x, int y)
        {
            X = x;
            Y = y; 
        }
    }
}