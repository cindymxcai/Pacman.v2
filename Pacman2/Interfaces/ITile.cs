using System.Collections.Generic;

namespace Pacman2.Interfaces
{
    public interface ITile
    {
        List<ISprite> SpritesOnTile { get; }
        Position Position { get; set; }
        void AddSprite(ISprite sprite);
        ISprite GetFirstSprite();
        void Render();
        void RemoveSprite(ISprite sprite);
        bool HasGivenSprite(ISpriteDisplay spriteDisplay);
        ISprite GetPelletSprite();
    }
}