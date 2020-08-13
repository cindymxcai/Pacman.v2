using System;
using System.Runtime.InteropServices;

namespace Pacman2.Interfaces
{
    public interface ISpriteDisplay
    {
        string Icon { get;  }
        ConsoleColor Colour { get;  }
        int Priority { get; }
        void SetSpriteDisplay([Optional]Direction direction);
    }
}