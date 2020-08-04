using Pacman2;
using Xunit;

namespace PacmanTest
{
    public class MazeTests
    {
        private static ITileTypeFactory SetUp()
        {
            var wall = new WallTileType();
            var empty = new EmptyTileType();
            var pellet = new PelletTileType();
            var ghost = new GhostTileType();
            return new TileTypeFactory(wall, empty, pellet, ghost);
        }
        
        [Fact]
        public void GivenMazeDataShouldGetSizeOfMaze()
        {
            var parser = new Parser();
            var mazeData = new []{"...","..."};
            var maze = new Maze(mazeData, parser);
            Assert.Equal(2, maze.Row);
            Assert.Equal(3, maze.Column);
        }

        [Fact]
        public void GivenMazeDataShouldCreateArrayOfTiles()
        {
            var parser = new Parser();
            var mazeData = new[] {"...", "..."};
            var maze = new Maze(mazeData, parser);
            Assert.Equal(6, maze.Tiles.Length);
        }

        [Fact]
        public void GivenSizeAndDataShouldPopulateMazeArray()
        {
            var parser = new Parser();

            var tileTypeFactory = SetUp();
            var mazeData = new []{".* "};
            var maze = new Maze(mazeData, parser);
            Assert.Equal(tileTypeFactory.Pellet.Display, maze.Tiles[0,0].Display);
            Assert.Equal(tileTypeFactory.Wall.Display, maze.Tiles[0,1].Display);
            Assert.Equal(tileTypeFactory.Empty.Display, maze.Tiles[0,2].Display);
        }
        
        [Fact]
        public void GivenGhostPositionMazeShouldDisplayGhostTile()
        {
            var parser = new Parser();

            var rng = new Rng();
            var ghost = new Ghost(0,1, new RandomMovement(rng), SetUp());
            var mazeData = new []{"..."};
            var maze = new Maze(mazeData, parser);
            maze.UpdateArray(ghost.X, ghost.Y, ghost.Display, ghost.Colour);
            Assert.Equal(ghost.Display, maze.Tiles[0,1].Display);
        }
    }
}