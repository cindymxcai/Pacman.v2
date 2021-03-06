using System;
using Pacman2.Interfaces;

namespace Pacman2
{
   public class PlayerInput : IPlayerInput
    {
        public ConsoleKey TakeInput()
        {
            return Console.ReadKey().Key;
        }

        public bool HasPressedQuit(ConsoleKey input)
        {
            return input == ConsoleKey.Q;
        }

        public bool HasNewInput()
        {
            return Console.KeyAvailable;
        }
    }
}