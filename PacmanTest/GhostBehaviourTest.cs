using System;
using Moq;
using Pacman2;
using Pacman2.Interfaces;
using Xunit;

namespace PacmanTest
{
    public class GhostBehaviourTest
    {
        [Theory]
        [InlineData(0, Direction.Up)]
        [InlineData(1, Direction.Down)]
        [InlineData(2, Direction.Left)]
        [InlineData(3, Direction.Right)]

        public void GivenRandomNumberShouldReturnADirection(int number, Direction direction)
        {
            var random = new Mock<IRng>();
            random.Setup(r => r.Next(0, 4)).Returns(number);
            var randomMovement = new RandomMovement(random.Object);
            var newDirection = randomMovement.GetNewDirection(Direction.Down, ConsoleKey.DownArrow);
            Assert.Equal(direction, newDirection);
        }
    }
}