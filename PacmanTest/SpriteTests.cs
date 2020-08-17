using System;
using Moq;
using Pacman2;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;
using Xunit;

namespace PacmanTest
{
    public class SpriteTests
    {
        [Fact]
        public void GivenAPositionASpriteShouldHoldXAndYCoordinates()
        {
            var rng = new Rng();
            var sprite = new MovingSprite(new Position(0, 1), new RandomMovement(rng), new GhostSpriteDisplay());
            Assert.Equal(0, sprite.CurrentPosition.Row);
            Assert.Equal(1, sprite.CurrentPosition.Col);
        }

        [Theory]
        [InlineData(Direction.Up, 2, 0)]
        [InlineData(Direction.Down, 1, 0)]
        [InlineData(Direction.Left, 0, 2)]
        [InlineData(Direction.Right, 0, 1)]
        public void GivenDirectionSpriteShouldMovePosition(Direction direction, int x, int y)
        {
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection(Direction.Up, ConsoleKey.UpArrow)).Returns(direction);
            var sprite = new MovingSprite(new Position(0, 0), mockRandom.Object, new GhostSpriteDisplay());
            var parser = new Parser();
            var mazeData = new[] {"...", "...", "..."};
            var maze = new Maze(mazeData, parser);
            
            sprite.UpdateDirection(ConsoleKey.UpArrow);

            var newPosition = maze.GetNewPosition(sprite.CurrentDirection,  sprite.CurrentPosition);
            maze.MoveSpriteToNewPosition(sprite, newPosition);
            sprite.UpdatePosition(newPosition);
            
            Assert.Equal(x, sprite.CurrentPosition.Row);
            Assert.Equal(y, sprite.CurrentPosition.Col);
        }

        [Fact]
        public void GivenCollisionWithWallSpriteShouldNotMoveToPosition()
        {
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection(Direction.Right, ConsoleKey.UpArrow)).Returns(Direction.Right);
            var sprite = new MovingSprite(new Position(0, 1), mockRandom.Object, new PacmanSpriteDisplay());
         
            sprite.UpdateDirection(ConsoleKey.UpArrow);
            
            sprite.UpdatePosition(sprite.CurrentPosition);
            Assert.Equal(0, sprite.CurrentPosition.Row);
            Assert.Equal(1, sprite.CurrentPosition.Col);
        }

        [Fact]
        public void GivenSpriteMovesShouldKeepTrackOfPreviousPosition()
        {
            var sprite = new MovingSprite(new Position(0, 0), new RandomMovement(new Rng()), new GhostSpriteDisplay());
            
            sprite.UpdatePosition(sprite.CurrentPosition);
            Assert.Equal(0, sprite.PreviousPosition.Row);
            Assert.Equal(0, sprite.PreviousPosition.Col);
        }
    }
}