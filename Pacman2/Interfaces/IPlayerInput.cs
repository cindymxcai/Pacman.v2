using System;

namespace Pacman2.Interfaces
{
    public interface IPlayerInput
    {
        ConsoleKey TakeInput();
        bool HasPressedQuit(ConsoleKey input);
        bool HasNewInput();
    }
}