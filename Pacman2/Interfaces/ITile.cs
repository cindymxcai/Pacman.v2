using System.Collections.Generic;

namespace Pacman2.Interfaces
{
    public interface ITile
    {
        List<ISprite> SpritesOnTile { get; }
        void AddSprite(ISprite sprite);
        bool IsWall();
        ISprite GetFirstSprite();
    }
}