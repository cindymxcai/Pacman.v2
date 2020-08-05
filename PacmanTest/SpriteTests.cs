using Moq;
using Pacman2;
using Pacman2.Interfaces;
using Xunit;

namespace PacmanTest
{
    public class SpriteTests
    {
        private ITileTypeFactory SetUp()
        {
            var wall = new WallTileType();
            var empty = new EmptyTileType();
            var pellet = new PelletTileType();
            var sprite = new GhostTileType();
            return new TileTypeFactory(wall, empty, pellet, sprite);
        } 
        
        [Fact]
        public void GivenAPositionASpriteShouldHoldXAndYCoordinates()
        {
            var rng = new Rng();
            var sprite = new Sprite(0,1, new RandomMovement(rng), SetUp());
            Assert.Equal(0, sprite.CurrentPosition.X);
            Assert.Equal(1, sprite.CurrentPosition.Y);
        }
        
        [Theory]
        [InlineData(Direction.Up, 2, 0)]
        [InlineData(Direction.Down, 1, 0)]
        [InlineData(Direction.Left, 0, 2)]
        [InlineData(Direction.Right, 0, 1)]

        public void GivenDirectionSpriteShouldMovePosition(Direction direction, int x, int y)
        {
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection()).Returns(direction);
            var sprite = new Sprite(0,0, mockRandom.Object, SetUp());
            var parser = new Parser();
            var mazeData = new []{"...","...","..."};
            var maze = new Maze(mazeData,parser);
            
            sprite.UpdateDirection();
            var newPosition = sprite.GetNewPosition(maze);
            sprite.UpdatePosition(newPosition, maze);
            maze.UpdateArray(sprite.CurrentPosition, sprite.TileType);
            Assert.Equal(x,sprite.CurrentPosition.X);
            Assert.Equal(y,sprite.CurrentPosition.Y);
        }
        
        [Fact]
        public void GivenSpriteMovesShouldKeepTrackOfPreviousPosition()
        {
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection()).Returns(Direction.Up);
            var sprite = new Sprite(0,0, mockRandom.Object, SetUp());
            var parser = new Parser();
            var mazeData = new[] {"...", "...", "..."};
            var maze = new Maze(mazeData,parser);
            
            sprite.UpdateDirection();
            var newPosition = sprite.GetNewPosition(maze);
            sprite.UpdatePosition(newPosition, maze);
            maze.UpdateArray(sprite.CurrentPosition, sprite.TileType);
            Assert.Equal(0,sprite.PreviousPosition.X);
            Assert.Equal(0,sprite.PreviousPosition.Y);
        }

        [Fact]
        public void GivenCollisionWithWallSpriteShouldNotMoveToPosition()
        {
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection()).Returns(Direction.Right);
            var sprite = new Sprite(0,1, mockRandom.Object, SetUp());
            
            var parser = new Parser();
            var mazeData = new []{"..*",".*.","*.."};
            var maze = new Maze(mazeData, parser);
            
            sprite.UpdateDirection();
            var newPosition = sprite.GetNewPosition(maze);
            sprite.UpdatePosition(newPosition, maze);
            maze.UpdateArray(sprite.CurrentPosition, sprite.TileType);
            Assert.Equal(0,sprite.CurrentPosition.X);
            Assert.Equal(1,sprite.CurrentPosition.Y);
        }
    }
}