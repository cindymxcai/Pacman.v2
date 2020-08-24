using Pacman2.Interfaces;

namespace Pacman2
{
    public struct Position : IPosition
    {
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }
    }
}