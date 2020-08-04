namespace Pacman2
{
    public interface ITileTypeFactory
    {
        ITileType Wall { get; }
        ITileType Empty { get; }
        ITileType Pellet { get; }
        ITileType Ghost { get; set; }
        void DisplayTile(ITile tile);
    }
}