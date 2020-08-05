using System;

namespace Pacman2
{
    public interface IGhost
    {
        int X { get; }
        int Y { get; }
        Direction CurrentDirection { get; }
        void UpdateDirection();
        void UpdatePosition((int, int) newPosition, Maze maze);
    }
}