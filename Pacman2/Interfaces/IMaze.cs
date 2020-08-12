namespace Pacman2.Interfaces
{
    public interface IMaze
    {
        ITile[,] Tiles { get; }
        int Columns { get; }
        int Rows { get; }
        void Render();
        void UpdateSpritePosition(IMovingSprite sprite);

        ISprite GetSpriteAtPosition(IPosition position) 
            ;
    }
}