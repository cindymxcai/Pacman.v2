using System;
using System.IO;
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

        [Theory]
        [InlineData(Direction.Up, 20, 0)]
        [InlineData(Direction.Down, 1, 0)]
        [InlineData(Direction.Left, 0, 18)]
        [InlineData(Direction.Right, 0, 1)]

        public void GivenDirectionGhostShouldMovePosition(Direction direction, int x, int y)
        {
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection()).Returns(direction);
            var ghost = new Ghost(0,0, mockRandom.Object);
            
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var maze = new Maze(mazeData);
            
            ghost.UpdateDirection();
            ghost.UpdatePosition(maze);
            maze.UpdateArray(ghost.X, ghost.Y, ghost.Display, ghost.Colour);
            Assert.Equal(x,ghost.X);
            Assert.Equal(y,ghost.Y);
        }
    }
}