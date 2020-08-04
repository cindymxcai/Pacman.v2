using System;

namespace Pacman2
{
    public interface ITile
    {
        string Display { get; set; }
        ConsoleColor Colour { get; set; }
    }
}