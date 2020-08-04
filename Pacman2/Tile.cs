namespace Pacman2
{
    public class Tile : ITile
    {
        public Tile(string display)
        {
            Display = display;
        }
        public string Display { get; set; }
    }
}