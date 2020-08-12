using System;
using Pacman2;

namespace PacmanTest
{
    public interface IPlayerInput
    {
        ConsoleKey TakeInput();
    }

    class PlayerInput : IPlayerInput
    {
        public ConsoleKey TakeInput()
        {
            return Console.ReadKey().Key;
        }
    }
}