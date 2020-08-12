using System;
using Moq;
using Pacman2;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;
using Xunit;

namespace PacmanTest
{
    
    public class MovingSpriteTests
    {
        [Theory]
        [InlineData(Direction.Up)]
        [InlineData(Direction.Down)]
        [InlineData(Direction.Left)]
        [InlineData(Direction.Right)]
        public void GivenABehaviourSpriteShouldBeAbleToChangeDirection(Direction direction)
        {
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection(Direction.Up, ConsoleKey.UpArrow)).Returns(direction);
            var sprite = new MovingSprite(new Position(0,1), mockRandom.Object, new GhostSpriteDisplay());
            sprite.UpdateDirection(ConsoleKey.UpArrow);
            Assert.Equal(direction, sprite.CurrentDirection);
        }
    }
}