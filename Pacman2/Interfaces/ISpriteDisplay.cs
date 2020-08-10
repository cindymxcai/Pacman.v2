using System;

namespace Pacman2.Interfaces
{
    public interface ITileType
    {
        public string Icon { get;  }
        public ConsoleColor Colour { get;  }
        int DisplayPriority { get; }
    }
}