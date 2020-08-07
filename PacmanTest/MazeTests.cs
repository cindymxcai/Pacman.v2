using System.Linq;
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
            Assert.Equal(new PelletTileType().Display, maze.Tiles[0,0].SpritesOnTile.First().Display);
            Assert.Equal(new WallTileType().Display, maze.Tiles[0,1].SpritesOnTile.First().Display);
            Assert.Equal(new EmptyTileType().Display, maze.Tiles[0,2].SpritesOnTile.First().Display);
            Assert.Equal(new EmptyTileType().Colour, maze.Tiles[0,2].SpritesOnTile.First().Colour);
        }
        
        [Fact]
        public void GivenGhostPositionMazeShouldDisplayGhostTile()
        {
            var parser = new Parser();

            var rng = new Rng();
            var ghost = new Sprite(new Position(0,1), new RandomMovement(rng), new GhostTileType());
            var mazeData = new []{"..."};
            var maze = new Maze(mazeData, parser);
            maze.AddTileTypeToTile(ghost.CurrentPosition, ghost.TileType);
            Assert.Equal(ghost.TileType, maze.Tiles[0,1].SpritesOnTile[1]);
            Assert.Equal(ghost.TileType.Colour, maze.Tiles[0,1].SpritesOnTile[1].Colour);
            Assert.Equal(ghost.TileType.Display, maze.Tiles[0,1].SpritesOnTile[1].Display);
        }

        [Fact]
        public void GivenTilesHavePriorityMazeShouldDisplayCorrectTileType()
        {
            var parser = new Parser();

            var mazeData = new []{".* "};
            var maze = new Maze(mazeData, parser);
            var position =  maze.GetTileTypeAtPosition(new Position(0, 0));
            Assert.Equal(position.Display, new PelletTileType().Display);
            var ghost = new Sprite(new Position(0,0), new RandomMovement(new Rng()), new GhostTileType());
            maze.AddTileTypeToTile(ghost.CurrentPosition, ghost.TileType);
            position =  maze.GetTileTypeAtPosition(ghost.CurrentPosition);
            Assert.Equal( ghost.TileType.Display, position.Display);

            ghost.UpdatePosition(ghost.GetNewPosition(maze), maze );
            maze.RemoveTileTypeFromTile(ghost.PreviousPosition, ghost.TileType);

            var previousPosition = maze.GetTileTypeAtPosition(ghost.PreviousPosition);
            Assert.Equal(new PelletTileType().Display, previousPosition.Display);
        }
    }
}