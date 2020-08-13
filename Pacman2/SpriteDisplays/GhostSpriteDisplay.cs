using System;
using Pacman2.Interfaces;

namespace Pacman2.SpriteDisplays
{
    public class GhostSpriteDisplay : ISpriteDisplay
    {
        public string Icon { get; private set; } 
        public  ConsoleColor Colour { get; private set; } 
        public int Priority { get; private set; } 

        public void SetSpriteDisplay(Direction? direction)
        {
            Icon = " \u1571 ";
            Colour  = ConsoleColor.Red;
            Priority = 1;
        }
    }
}