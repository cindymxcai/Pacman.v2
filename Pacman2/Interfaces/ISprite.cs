using System;

namespace Pacman2.Interfaces
{
    public interface ISprite
    {
        int Priority { get; }
        string Icon { get; }
        ConsoleColor Colour { get; }
        ISpriteDisplay Display { get; }
    }
}