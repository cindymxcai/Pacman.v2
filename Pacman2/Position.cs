using Pacman2.Interfaces;

namespace Pacman2
{
    public class Position : IPosition
    {
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
            throw new System.NotImplementedException();
        }

        public int Row { get; set; } 
        public int Col { get; set; }
    }
}