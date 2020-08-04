using Moq;
using Pacman2;
using Xunit;

namespace PacmanTest
{
    public class GhostTests
    {
        [Fact]
        public void GivenAPositionAGhostShouldHoldXAndYCoordinates()
        {
            var ghost = new Ghost(0,1, new RandomMovement());
            Assert.Equal(0, ghost.X);
            Assert.Equal(1, ghost.Y);
        }

        [Theory]
        [InlineData(Direction.Up)]
        [InlineData(Direction.Down)]
        [InlineData(Direction.Left)]
        [InlineData(Direction.Right)]

        public void GivenABehaviourGhostShouldBeAbleToChangeDirection(Direction direction)
        {
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection()).Returns(direction);
            var ghost = new Ghost(0,1, mockRandom.Object);
            ghost.UpdateDirection();
            Assert.Equal(direction, ghost.CurrentDirection );
        }
    }
}