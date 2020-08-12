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
            Tile tile;
            switch (inputChar)
            {
                case '*':
                    tile = new Tile();
                    tile.AddSprite(new StaticSprite(new WallSpriteDisplay()));
                    return tile;
                case '.':
                    tile = new Tile();
                    tile.AddSprite(new StaticSprite(new PelletSpriteDisplay()));
                    return tile;
                default:
                    return new Tile();
            }
        }
    }
}