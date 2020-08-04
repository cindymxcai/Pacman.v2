using Moq;
using Pacman2;
using Xunit;

namespace PacmanTest
{
    
    public class GhostTests
    {
        private ITileTypeFactory SetUp()
        {
            var wall = new WallTileType();
            var empty = new EmptyTileType();
            var pellet = new PelletTileType();
            var ghost = new GhostTileType();
            return new TileTypeFactory(wall, empty, pellet, ghost);
        }
        
        [Fact]
        public void GivenAPositionAGhostShouldHoldXAndYCoordinates()
        {
            var rng = new Rng();
            var ghost = new Ghost(0,1, new RandomMovement(rng), SetUp());
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
            var ghost = new Ghost(0,1, mockRandom.Object, SetUp());
            ghost.UpdateDirection();
            Assert.Equal(direction, ghost.CurrentDirection );
        }

        [Theory]
        [InlineData(Direction.Up, 2, 0)]
        [InlineData(Direction.Down, 1, 0)]
        [InlineData(Direction.Left, 0, 2)]
        [InlineData(Direction.Right, 0, 1)]

        public void GivenDirectionGhostShouldMovePosition(Direction direction, int x, int y)
        {
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection()).Returns(direction);
            var ghost = new Ghost(0,0, mockRandom.Object, SetUp());
            var parser = new Parser();
            var mazeData = new []{"...","...","..."};
            var maze = new Maze(mazeData,parser);
            
            ghost.UpdateDirection();
            var newPosition = ghost.GetNewPosition(maze);
            ghost.UpdatePosition(newPosition, maze);
            maze.UpdateArray(ghost.X, ghost.Y, ghost.TileType);
            Assert.Equal(x,ghost.X);
            Assert.Equal(y,ghost.Y);
        }
        
        [Fact]
        public void GivenGhostMovesShouldKeepTrackOfPreviousPosition()
        {
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection()).Returns(Direction.Up);
            var ghost = new Ghost(0,0, mockRandom.Object, SetUp());
            var parser = new Parser();
            var mazeData = new[] {"...", "...", "..."};
            var maze = new Maze(mazeData,parser);
            
            ghost.UpdateDirection();
            var newPosition = ghost.GetNewPosition(maze);
            ghost.UpdatePosition(newPosition, maze);
            maze.UpdateArray(ghost.X, ghost.Y, ghost.TileType);
            Assert.Equal(0,ghost.PrevX);
            Assert.Equal(0,ghost.PrevY);
        }

        [Fact]
        public void GivenCollisionWithWallGhostShouldNotMoveToPosition()
        {
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection()).Returns(Direction.Right);
            var ghost = new Ghost(0,1, mockRandom.Object, SetUp());
            
            var parser = new Parser();
            var mazeData = new []{"..*",".*.","*.."};
            var maze = new Maze(mazeData, parser);
            
            ghost.UpdateDirection();
            var newPosition = ghost.GetNewPosition(maze);
            ghost.UpdatePosition(newPosition, maze);
            maze.UpdateArray(ghost.X, ghost.Y, ghost.TileType);
            Assert.Equal(0,ghost.X);
            Assert.Equal(1,ghost.Y);
        }
    }
}