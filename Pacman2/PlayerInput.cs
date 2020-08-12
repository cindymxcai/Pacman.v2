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
    }
}