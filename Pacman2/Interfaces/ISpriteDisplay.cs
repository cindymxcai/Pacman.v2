using System;

namespace Pacman2.Interfaces
{
    public interface ISpriteDisplay
    {
        public string Icon { get;  }
        public ConsoleColor Colour { get;  }
        int Priority { get; }
        void SetSpriteDisplay(Direction? direction);
    }
}