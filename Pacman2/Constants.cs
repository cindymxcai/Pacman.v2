using System;

namespace Pacman2
{
    public static class Constants
    { 
        //TODO group into objects 
        public static string EmptyDisplay { get; } = "   ";
        public static string PelletDisplay { get; } = " \u2022 ";
        public static string WallDisplay { get; } = "\u2588\u2588\u2588";

        public static ConsoleColor EmptyColour { get; } = ConsoleColor.Black;
        public static ConsoleColor PelletColour { get; } = ConsoleColor.Magenta;
        public static ConsoleColor WallColour { get; } = ConsoleColor.Blue;
    }
}