using System;

namespace Pacman2
{
    public interface ITileType
    {
        public string Display { get;  }
        public ConsoleColor Colour { get;  }
    }
}