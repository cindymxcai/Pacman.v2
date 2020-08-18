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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.WriteLine(@" ___  _              _         ___                 _            _            _ ");
            Console.WriteLine(@"|_ _|| |_  ___ ._ _ | |__ ___ | | '___  _ _   ___ | | ___  _ _ <_>._ _  ___ | |");
            Console.WriteLine(@" | | | . |<_> || ' || / /<_-< | |-/ . \| '_> | . \| |<_> || | || || ' |/ . ||_/");
            Console.WriteLine(@" |_| |_|_|<___||_|_||_\_\/__/ |_| \___/|_|   |  _/|_|<___|`_. ||_||_|_|\_. |<_>");
            Console.WriteLine(@"                                             |_|          <___'        <___'   ");
         }

      

        public void Score(int mazePelletsEaten, int livesRemaining)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"Score: {mazePelletsEaten}                             Lives Remaining: {livesRemaining}");
            Console.ResetColor();
        }

        public void Win()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@" ___  ___  _ _  ___   ___  ___  ___  _ _  _    ___  ___  _  ___  _ _  ___");
            Console.WriteLine(@"|  _>| . || \ |/  _> | . \| . ||_ _|| | || |  | . ||_ _|| || . || \ |/ __>");
            Console.WriteLine(@"| <__| | ||   || <_/\|   /|   | | | | ' || |_ |   | | | | || | ||   |\__ \");
            Console.WriteLine(@"`___/`___'|_\_|`____/|_\_\|_|_| |_| `___'|___||_|_| |_| |_|`___'|_\_|<___/");
            Console.ResetColor();
        }

        public void LifeLost(int livesRemaining)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@" .-. ");
            Console.WriteLine(@"| OO|  ╭⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼╮");
            Console.WriteLine(@"|   | <  Lives left: " + livesRemaining + " |");
            Console.WriteLine(@"'^^^'  ╰⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼╯");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.ResetColor();
        }

        public void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@" ______    ______     ______     __    __     ______     __   __    ");
            Console.WriteLine(@"/\  == \  /\  __ \   /\  ___\   /\ `-./  \   /\  __ \   /\ `-.\ \   ");
            Console.WriteLine(@"\ \  _-/  \ \  __ \  \ \ \____  \ \ \-./\ \  \ \  __ \  \ \ \-.  \ ");
            Console.WriteLine(@" \  \_\    \ \_\ \_\  \ \_____\  \ \_\ \ \_\  \ \_\ \_\  \ \_\\ \_\ ");
            Console.WriteLine(@"  \/_/      \/_/\/_/   \/_____/   \/_/  \/_/   \/_/\/_/   \/_/ \/_/");
            Console.WriteLine("\nUse the arrow keys to navigate Pacman! To quit at any time, press Q");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPress any key to play!");
            Console.ResetColor();        
        }
    }
}