using Pacman2;
using Xunit;

namespace PacmanTest
{
    public class MazeTests
    {
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

            var mazeData = new []{".* "};
            var maze = new Maze(mazeData, parser);
            Assert.Equal(new PelletTileType().Display, maze.Tiles[0,0].TileType.Display);
            Assert.Equal(new WallTileType().Display, maze.Tiles[0,1].TileType.Display);
            Assert.Equal(new EmptyTileType().Display, maze.Tiles[0,2].TileType.Display);
            Assert.Equal(new EmptyTileType().Colour, maze.Tiles[0,2].TileType.Colour);
        }
        
        [Fact]
        public void GivenGhostPositionMazeShouldDisplayGhostTile()
        {
            var parser = new Parser();

            var rng = new Rng();
            var ghost = new Sprite(new Position(0,1), new RandomMovement(rng), new GhostTileType());
            var mazeData = new []{"..."};
            var maze = new Maze(mazeData, parser);
            maze.UpdateTileTypeForTile(ghost.CurrentPosition, ghost.TileType);
            Assert.Equal(ghost.TileType, maze.Tiles[0,1].TileType);
            Assert.Equal(ghost.TileType.Colour, maze.Tiles[0,1].TileType.Colour);
            Assert.Equal(ghost.TileType.Display, maze.Tiles[0,1].TileType.Display);
        }
    }
}