namespace Pacman2
{
    public class RandomMovement : IMovementBehaviour
    {
        private IRng Rng;

        public RandomMovement()
        {
            Rng = new Rng();
        }
        
        public Direction GetNewDirection()
        {
           var newDirection = Rng.Next(0, 4) switch
            {
                0 => Direction.Up,
                1 => Direction.Down,
                2 => Direction.Left,
                3 => Direction.Right,
            };
            return newDirection;
        }
    }
}