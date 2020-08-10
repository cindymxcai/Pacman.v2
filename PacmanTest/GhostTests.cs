using Moq;
using Pacman2;
using Pacman2.Interfaces;
using Xunit;

namespace PacmanTest
{
    
    public class GhostTests
    {
        [Theory]
        [InlineData(Direction.Up)]
        [InlineData(Direction.Down)]
        [InlineData(Direction.Left)]
        [InlineData(Direction.Right)]
        public void GivenABehaviourGhostShouldBeAbleToChangeDirection(Direction direction)
        {
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection()).Returns(direction);
            var ghost = new MovingSprite(new Position(0,1), mockRandom.Object, new GhostSpriteDisplay());
            ghost.UpdateDirection();
            Assert.Equal(direction, ghost.CurrentDirection );
        }
    }
}