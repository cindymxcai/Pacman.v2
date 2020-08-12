using System;
using Pacman2.Interfaces;

namespace Pacman2.SpriteDisplays
{
    public class PacmanSpriteDisplay : ISpriteDisplay
    {
        public void SetSpriteDisplay(Direction? direction)
        {
            var icon = direction switch
            {
                Direction.Up => " \u15E2 ",
                Direction.Down => " \u15E3 ",
                Direction.Left => " \u15E4 ",
                Direction.Right => " \u15E7 ",
                _ => " "
            };

            Icon = icon;
            Colour = ConsoleColor.Yellow;
            Priority  = 1;
        }

        public string Icon { get; private set; } = " \u15E2 ";
        public ConsoleColor Colour { get; private set; } = ConsoleColor.Yellow;
        public int Priority { get; private set; } = 1;
    }
}