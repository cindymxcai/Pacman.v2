using System;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;
using Pacman2.Tiles;

namespace Pacman2
{
    public class Parser : IParser
    {
        public ITile GetTile(char inputChar)
        {
            return inputChar switch
            {
                '*' => new Tile(new StaticSprite(new WallSpriteDisplay())),
                '.' => new Tile(new StaticSprite(new PelletSpriteDisplay())),
                ' ' => new Tile(new StaticSprite(new EmptySpriteDisplay())),
            _ => throw new Exception()
            };
        }
    }
}