using System;

namespace Pacman2
{
    public interface IGhost
    {
        int X { get; }
        int Y { get; }
        string Display { get; }
        ConsoleColor Colour { get; }
        Direction CurrentDirection { get; }
        void UpdateDirection();
        void UpdatePosition(Maze maze);
    }
}