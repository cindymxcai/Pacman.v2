using System;

namespace Pacman2.Interfaces
{
    public interface ISpriteDisplay
    {
        string Icon { get;  }
        ConsoleColor Colour { get;  }
        int Priority { get; }
        void SetSpriteDisplay(Direction? direction);
    }
}