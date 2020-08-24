using System;
using Pacman2.Interfaces;

namespace Pacman2.SpriteDisplays
{
    public class GhostSpriteDisplay : ISpriteDisplay
    {
        public string Icon { get; private set; } =  " \u1571 ";
        public  ConsoleColor Colour { get; private set; } 
        public int Priority { get; private set; }
        public void SetSpriteDisplay(Direction direction = Direction.Up)
        {
            Icon = " \u1571 ";
            Colour  = ConsoleColor.Red;
            Priority = 1;        
        }
    }
}