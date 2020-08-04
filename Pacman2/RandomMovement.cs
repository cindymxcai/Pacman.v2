namespace Pacman2
{
    public class RandomMovement : IMovementBehaviour
    {
        private readonly IRng Rng;

        public RandomMovement(IRng random)
        {
            Rng = random;
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