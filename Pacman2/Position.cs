using Pacman2.Interfaces;

namespace Pacman2
{
    public class Position : IPosition
    {
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; } 
        public int Col { get; }
    }
}