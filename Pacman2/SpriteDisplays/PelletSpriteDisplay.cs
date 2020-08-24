using System;
using Pacman2.Interfaces;

namespace Pacman2.SpriteDisplays
{
    public class PelletSpriteDisplay : ISpriteDisplay
    {
        public  string Icon { get; private set; } = " \u2022 ";
        public  ConsoleColor Colour { get; private set; }
        public int Priority { get; private set; }
        public void SetSpriteDisplay(Direction direction = Direction.Up)
        {
            Icon  = " \u2022 ";
            Colour = ConsoleColor.Magenta;
            Priority= 2;        
        }
    }
}