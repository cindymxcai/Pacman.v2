using System;
using System.Threading;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class Display : IDisplay
    {
        public void LostPrompt(int totalScore)
        {
            GameEnd(totalScore);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPress enter to play again, or Q to quit"); 
            Console.ResetColor();
        }

        private void GameEnd(int totalScore)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
             Console.WriteLine(
                     @"
████████╗██╗  ██╗ █████╗ ███╗   ██╗██╗  ██╗███████╗    ███████╗ ██████╗ ██████╗     ██████╗ ██╗      █████╗ ██╗   ██╗██╗███╗   ██╗ ██████╗ ██╗
╚══██╔══╝██║  ██║██╔══██╗████╗  ██║██║ ██╔╝██╔════╝    ██╔════╝██╔═══██╗██╔══██╗    ██╔══██╗██║     ██╔══██╗╚██╗ ██╔╝██║████╗  ██║██╔════╝ ██║
   ██║   ███████║███████║██╔██╗ ██║█████╔╝ ███████╗    █████╗  ██║   ██║██████╔╝    ██████╔╝██║     ███████║ ╚████╔╝ ██║██╔██╗ ██║██║  ███╗██║
   ██║   ██╔══██║██╔══██║██║╚██╗██║██╔═██╗ ╚════██║    ██╔══╝  ██║   ██║██╔══██╗    ██╔═══╝ ██║     ██╔══██║  ╚██╔╝  ██║██║╚██╗██║██║   ██║╚═╝
   ██║   ██║  ██║██║  ██║██║ ╚████║██║  ██╗███████║    ██║     ╚██████╔╝██║  ██║    ██║     ███████╗██║  ██║   ██║   ██║██║ ╚████║╚██████╔╝██ 
   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝    ╚═╝      ╚═════╝ ╚═╝  ╚═╝    ╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚═╝");
            Console.WriteLine($"Total Score: {totalScore}");
        }
        
        public void LevelStats(int score, int livesRemaining)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"Score: {score}                             Lives Remaining: {livesRemaining}");
            Console.ResetColor();
        }

        public void Win(int totalScore)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@" 
 ██████╗ ██████╗ ███╗   ██╗ ██████╗ ██████╗  █████╗ ████████╗██╗   ██╗██╗      █████╗ ████████╗██╗ ██████╗ ███╗   ██╗███████╗██╗
██╔════╝██╔═══██╗████╗  ██║██╔════╝ ██╔══██╗██╔══██╗╚══██╔══╝██║   ██║██║     ██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║██╔════╝██║
██║     ██║   ██║██╔██╗ ██║██║  ███╗██████╔╝███████║   ██║   ██║   ██║██║     ███████║   ██║   ██║██║   ██║██╔██╗ ██║███████╗██║
██║     ██║   ██║██║╚██╗██║██║   ██║██╔══██╗██╔══██║   ██║   ██║   ██║██║     ██╔══██║   ██║   ██║██║   ██║██║╚██╗██║╚════██║╚═╝
╚██████╗╚██████╔╝██║ ╚████║╚██████╔╝██║  ██║██║  ██║   ██║   ╚██████╔╝███████╗██║  ██║   ██║   ██║╚██████╔╝██║ ╚████║███████║██╗
 ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝╚═╝");
            Console.WriteLine($"Total Score: {totalScore}");
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
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.ResetColor();
        }

        public void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(
                @"
██████╗   █████╗   ██████╗ ███╗   ███╗  █████╗  ███╗   ██╗
██╔══██╗ ██╔══██╗ ██╔════╝ ████╗ ████║ ██╔══██╗ ████╗  ██║
██████╔╝ ███████║ ██║      ██╔████╔██║ ███████║ ██╔██╗ ██║
██╔═══╝  ██╔══██║ ██║      ██║╚██╔╝██║ ██╔══██║ ██║╚██╗██║
██║      ██║  ██║ ╚██████╗ ██║ ╚═╝ ██║ ██║  ██║ ██║ ╚████║
╚═╝      ╚═╝  ╚═╝  ╚═════╝ ╚═╝     ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═══╝"
            );
            CountDown();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Use the arrow keys to navigate Pacman! To quit at any time, press Q");
            Console.WriteLine("Press any key to start!");
        }
        

        private void CountDown()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Console.Clear();
            Console.WriteLine(@"
██████╗ 
╚════██╗
 █████╔╝
 ╚═══██╗
██████╔╝
╚═════╝ ");
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.Clear();
            Console.WriteLine(@"
██████╗ 
╚════██╗
 █████╔╝
██╔═══╝ 
███████╗
╚══════╝");
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.Clear();
            Console.WriteLine(@"
 ██╗
███║
╚██║
 ██║
 ██║
 ╚═╝");
           
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.Clear();
        }

        public void NextLevel()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            
            Console.WriteLine(@"
███╗   ██╗███████╗██╗  ██╗████████╗    ██╗     ███████╗██╗   ██╗███████╗██╗     ██╗
████╗  ██║██╔════╝╚██╗██╔╝╚══██╔══╝    ██║     ██╔════╝██║   ██║██╔════╝██║     ██║
██╔██╗ ██║█████╗   ╚███╔╝    ██║       ██║     █████╗  ██║   ██║█████╗  ██║     ██║
██║╚██╗██║██╔══╝   ██╔██╗    ██║       ██║     ██╔══╝  ╚██╗ ██╔╝██╔══╝  ██║     ╚═╝    
██║ ╚████║███████╗██╔╝ ██╗   ██║       ███████╗███████╗ ╚████╔╝ ███████╗███████╗██╗    
╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝   ╚═╝       ╚══════╝╚══════╝  ╚═══╝  ╚══════╝╚══════╝╚═╝ ");
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.ResetColor();
        }
    }
}