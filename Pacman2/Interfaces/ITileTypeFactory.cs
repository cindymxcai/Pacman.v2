namespace Pacman2.Interfaces
{
    public interface ITileTypeFactory
    {
        ITileType Wall { get; }
        ITileType Empty { get; }
        ITileType Pellet { get; }
        ITileType Ghost { get; set; }
    }
}