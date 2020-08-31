using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;
using Pacman2.Sprites;

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
                    tile.AddSprite(sprite);                    
                    return tile;
                case '.':
                    tile = new Tile();
                    sprite = new StaticSprite(new PelletSpriteDisplay());
                    tile.AddSprite(sprite);
                    return tile;
                default:
                    return new Tile();
            }
        }
    }
}