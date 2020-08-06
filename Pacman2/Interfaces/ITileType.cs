using System;

namespace Pacman2.Interfaces
{
    public interface ITileType
    {
        public string Display { get;  }
        public ConsoleColor Colour { get;  }
        public int DisplayPriority { get; }
    }
}