using System;
using Pacman2.Interfaces;

namespace Pacman2.SpriteDisplays
{
    public class GhostSpriteDisplay : ISpriteDisplay
    {
        public string Icon { get; } = " \u1571 ";
        public  ConsoleColor Colour { get; } = ConsoleColor.Red;
        public int DisplayPriority { get; } = 1;
    }
}