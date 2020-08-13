using System;
using Pacman2.Interfaces;

namespace Pacman2.SpriteDisplays
{
    public class PacmanSpriteDisplay : ISpriteDisplay
    {
        public string Icon { get; private set; }
        public ConsoleColor Colour { get; private set; } 
        public int Priority { get; private set; }
        private bool IsChomping { get; set; }
        public void SetSpriteDisplay(Direction? direction)
        {
            IsChomping = !IsChomping;

            Icon = IsChomping
                ? " \u25EF "
                : direction switch
                {
                    Direction.Up => " \u15E2 ",
                    Direction.Down => " \u15E3 ",
                    Direction.Left => " \u15E4 ",
                    Direction.Right => " \u15E7 ",
                    _ => " "
                };
            Colour = ConsoleColor.Yellow;
            Priority  = 1;
        }
    }
}