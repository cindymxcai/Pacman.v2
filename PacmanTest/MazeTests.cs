using Pacman2;
using Pacman2.Interfaces;
using Pacman2.Tiles;
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
            Assert.Equal(2, maze.Rows);
            Assert.Equal(3, maze.Columns);
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
            Assert.Equal(tileTypeFactory.Pellet.Display, maze.Tiles[0,0].TileType.Display);
            Assert.Equal(tileTypeFactory.Wall.Display, maze.Tiles[0,1].TileType.Display);
            Assert.Equal(tileTypeFactory.Empty.Display, maze.Tiles[0,2].TileType.Display);
            Assert.Equal(tileTypeFactory.Empty.Colour, maze.Tiles[0,2].TileType.Colour);
        }
        
        [Fact]
        public void GivenGhostPositionMazeShouldDisplayGhostTile()
        {
            var parser = new Parser();

            var rng = new Rng();
            var ghost = new Sprite(0,1, new RandomMovement(rng), SetUp());
            var mazeData = new []{"..."};
            var maze = new Maze(mazeData, parser);
            maze.UpdateTileTypeForTile(ghost.CurrentPosition, ghost.TileType);
            Assert.Equal(ghost.TileType, maze.Tiles[0,1].TileType);
            Assert.Equal(ghost.TileType.Colour, maze.Tiles[0,1].TileType.Colour);
            Assert.Equal(ghost.TileType.Display, maze.Tiles[0,1].TileType.Display);
        }
    }
}