using System;

namespace Pacman2
{
    public static class Display
    {
        public static void LostPrompt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPress any key to replay, or Q to quit"); 
            Console.ResetColor();
        }

        public static void GameEnd()
        {
            Console.WriteLine(@" ___  _              _         ___                 _            _            _ ");
            Console.WriteLine(@"|_ _|| |_  ___ ._ _ | |__ ___ | | '___  _ _   ___ | | ___  _ _ <_>._ _  ___ | |");
            Console.WriteLine(@" | | | . |<_> || ' || / /<_-< | |-/ . \| '_> | . \| |<_> || | || || ' |/ . ||_/");
            Console.WriteLine(@" |_| |_|_|<___||_|_||_\_\/__/ |_| \___/|_|   |  _/|_|<___|`_. ||_||_|_|\_. |<_>");
            Console.WriteLine(@"                                             |_|          <___'        <___'   ");
         }
    }
}