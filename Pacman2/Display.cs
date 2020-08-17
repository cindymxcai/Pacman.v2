using System;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class Display : IDisplay
    {
        public void LostPrompt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPress enter to keep playing, or Q to quit"); 
            Console.ResetColor();
        }

        public void GameEnd()
        {
            Console.Clear();
            Console.WriteLine(@" ___  _              _         ___                 _            _            _ ");
            Console.WriteLine(@"|_ _|| |_  ___ ._ _ | |__ ___ | | '___  _ _   ___ | | ___  _ _ <_>._ _  ___ | |");
            Console.WriteLine(@" | | | . |<_> || ' || / /<_-< | |-/ . \| '_> | . \| |<_> || | || || ' |/ . ||_/");
            Console.WriteLine(@" |_| |_|_|<___||_|_||_\_\/__/ |_| \___/|_|   |  _/|_|<___|`_. ||_||_|_|\_. |<_>");
            Console.WriteLine(@"                                             |_|          <___'        <___'   ");
         }

        public void Score(int mazePelletsEaten)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"Score: {mazePelletsEaten}");
            Console.ResetColor();
        }
    }
}