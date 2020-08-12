using System;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;

namespace Pacman2
{
    public class Parser : IParser
    {
        public ITile GetTile(char inputChar)
        {
            ITile tile;
            StaticSprite sprite;
            switch (inputChar)
            {
                case '*':
                    tile = new Tile();
                    sprite = new StaticSprite(new WallSpriteDisplay());
                    sprite.Display.SetSpriteDisplay(null);
                    tile.AddSprite(sprite);                    
                    return tile;
                case '.':
                    tile = new Tile();
                    sprite = new StaticSprite(new PelletSpriteDisplay());
                    sprite.Display.SetSpriteDisplay(null);
                    tile.AddSprite(sprite);
                    return tile;
                default:
                    return new Tile();
            }
        }
    }
}