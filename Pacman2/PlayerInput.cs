using System;

namespace Pacman2.Interfaces
{
   public class PlayerInput : IPlayerInput
    {
        public ConsoleKey TakeInput()
        {
            return Console.ReadKey().Key;
        }
    }
}