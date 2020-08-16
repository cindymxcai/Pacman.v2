using System;
using System.Collections.Generic;
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
            var mazeData = new[] {"...", "..."};
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
            var mazeData = new[] {".* "};
            var maze = new Maze(mazeData, parser);
            Assert.Equal(" \u2022 ", maze.Tiles[0, 0].SpritesOnTile.First().Display.Icon);
            Assert.Equal("\u2588\u2588\u2588", maze.Tiles[0, 1].SpritesOnTile.First().Display.Icon);
            Assert.Empty(maze.Tiles[0, 2].SpritesOnTile);
        }

        [Fact]
        public void GivenGhostPositionMazeShouldDisplayGhostTile()
        {
            var parser = new Parser();
            var rng = new Rng();
            var ghost = new MovingSprite(new Position(0, 0), new RandomMovement(rng), new GhostSpriteDisplay());
            var mazeData = new[] {"..."};
            var maze = new Maze(mazeData, parser);
            ghost.UpdatePosition(new Position(0, 1));
            var game = new Game(new List<IMovingSprite>(), maze, new PlayerInput(), new Display());
            game.UpdateSpritePosition(ghost);
            var newPosition = maze.GetNewPosition(ghost.CurrentDirection, ghost.CurrentPosition);
            if (maze.SpriteHasCollisionWithWall(newPosition)) return;
            maze.MoveSpriteToNewPosition(ghost, newPosition);
            ghost.UpdatePosition(newPosition);
            Assert.Equal(ghost.Display.Icon, maze.Tiles[0, 1].SpritesOnTile[1].Display.Icon);
            Assert.Equal(ghost.Display.Colour, maze.Tiles[0, 1].SpritesOnTile[1].Display.Colour);
            Assert.Equal(ghost.Display.Icon, maze.Tiles[0, 1].SpritesOnTile[1].Display.Icon);
        }

        [Fact]
        public void GivenPacmanPositionMazeShouldDisplayPacmanTile()
        {
            var parser = new Parser();
            var pacman = new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay());
            var mazeData = new[] {"..."};
            var maze = new Maze(mazeData, parser);
            pacman.UpdatePosition(new Position(0, 1));
            var game = new Game(new List<IMovingSprite>(), maze, new PlayerInput(), new Display());
            game.UpdateSpritePosition(pacman);
            Assert.Equal(pacman.Display.Icon, maze.Tiles[0, 1].SpritesOnTile[1].Display.Icon);
            Assert.Equal(pacman.Display.Colour, maze.Tiles[0, 1].SpritesOnTile[1].Display.Colour);
            Assert.Equal(pacman.Display.Icon, maze.Tiles[0, 1].SpritesOnTile[1].Display.Icon);
            Assert.Equal(pacman.Display.Priority, maze.Tiles[0, 1].SpritesOnTile[1].Display.Priority);
        }

        [Fact]
        public void GivenTilesHavePriorityMazeShouldDisplayCorrectTileType()
        {
            var parser = new Parser();
            var mazeData = new[] {". *"};
            var maze = new Maze(mazeData, parser);
            var mockRandom = new Mock<IMovementBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection(Direction.Down, ConsoleKey.DownArrow)).Returns(Direction.Right);
            var ghost = new MovingSprite(new Position(0, 0), mockRandom.Object, new GhostSpriteDisplay());
            ghost.UpdateDirection(ConsoleKey.DownArrow);
            var previousPosition = maze.GetTileAtPosition(ghost.CurrentPosition.Row, ghost.CurrentPosition.Col);
            Assert.Equal(" \u2022 ", previousPosition.GetFirstSprite().Display.Icon);
            var game = new Game(new List<IMovingSprite>(), maze, new PlayerInput(), new Display());
            game.UpdateSpritePosition(ghost);
            Assert.Equal(ghost.Display,
                maze.GetTileAtPosition(ghost.CurrentPosition.Row, ghost.CurrentPosition.Col).GetFirstSprite().Display);
        }

        [Fact]
        public void GivenPacmanAndGhostAreOnSameTileShouldHaveCollision()
        {
            var parser = new Parser();
            var mazeData = new[] {". *"};
            var maze = new Maze(mazeData, parser);
            var ghost = new MovingSprite(new Position(0, 0), new RandomMovement(new Rng()), new GhostSpriteDisplay());
            var pacman = new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay());
            var game = new Game(new List<IMovingSprite>(), maze, new PlayerInput(), new Display());
            game.UpdateSpritePosition(ghost);
            game.UpdateSpritePosition(pacman);
            Assert.True(maze.PacmanHasCollisionWithGhost(pacman));
        }

        [Fact]
        public void GivenPacmanAndGhostPassEachOtherShouldHaveCollision()
        {
            var parser = new Parser();
            var mazeData = new[] {". *"};
            var maze = new Maze(mazeData, parser);
            var ghost = new MovingSprite(new Position(0, 0), new RandomMovement(new Rng()), new GhostSpriteDisplay());
            var pacman = new MovingSprite(new Position(0, 1), new PlayerControlMovement(), new PacmanSpriteDisplay());
            var game = new Game(new List<IMovingSprite>(), maze, new PlayerInput(), new Display());
            game.UpdateSpritePosition(ghost);
            game.UpdateSpritePosition(pacman);
            ghost.PreviousPosition.Col = 1;
            ghost.PreviousPosition.Row = 0;
            pacman.PreviousPosition.Col = 0;
            pacman.PreviousPosition.Row = 0;
            Assert.True(maze.PacmanHasCollisionWithGhost(pacman));
        }
    }
}