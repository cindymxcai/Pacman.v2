using System.Linq;
using Moq;
using Pacman2;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;
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
            Assert.Equal(new PelletSpriteDisplay().Icon, maze.Tiles[0,0].SpritesOnTile.First().Display.Icon);
            Assert.Equal(new WallSpriteDisplay().Icon, maze.Tiles[0,1].SpritesOnTile.First().Display.Icon);
            Assert.Empty(maze.Tiles[0,2].SpritesOnTile);
        }
        
        [Fact]
        public void GivenGhostPositionMazeShouldDisplayGhostTile()
        {
            var parser = new Parser();

            var rng = new Rng();
            var ghost = new MovingSprite(new Position(0,0), new RandomMovement(rng), new GhostSpriteDisplay());
            var mazeData = new []{"..."};
            var maze = new Maze(mazeData, parser);
            ghost.UpdatePosition(new Position(0,1));
            maze.UpdateSpritePosition(ghost);
            Assert.Equal(ghost.Display.Icon, maze.Tiles[0,1].SpritesOnTile[1].Display.Icon);
            Assert.Equal(ghost.Display.Colour, maze.Tiles[0,1].SpritesOnTile[1].Display.Colour);
            Assert.Equal(ghost.Display.Icon, maze.Tiles[0,1].SpritesOnTile[1].Display.Icon);
        }

        [Fact]
        public void GivenTilesHavePriorityMazeShouldDisplayCorrectTileType()
        {
            var parser = new Parser();

            var mazeData = new []{". *"};
            var maze = new Maze(mazeData, parser);
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection()).Returns(Direction.Right);
            
            var ghost = new MovingSprite(new Position(0,0), mockRandom.Object, new GhostSpriteDisplay());
            ghost.UpdateDirection();

            var previousPosition = maze.GetSpriteAtPosition(ghost.CurrentPosition);
            maze.UpdateSpritePosition(ghost);
            Assert.Equal(new PelletSpriteDisplay().Icon, previousPosition.Display.Icon);
            Assert.Equal(ghost.Display, maze.GetSpriteAtPosition(ghost.CurrentPosition).Display);
        }
    }
}