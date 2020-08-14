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

        public bool HasPressedQuit()
        {
            return Console.ReadKey().Key == ConsoleKey.Q;
        }

        public bool HasNewInput()
        {
            return Console.KeyAvailable;
        }
    }
}