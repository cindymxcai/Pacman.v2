using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Pacman2;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;
using Pacman2.Sprites;
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
        public void GivenSizeAndDataShouldPopulateMazeArray()
        {
            var parser = new Parser();
            var mazeData = new[] {".* "};
            var maze = new Maze(mazeData, parser);
            Assert.Equal(" \u2022 ", maze.GetTileAtPosition(0,0).SpritesOnTile.First().Display.Icon);
            Assert.Equal("\u2588\u2588\u2588", maze.GetTileAtPosition(0,1).SpritesOnTile.First().Display.Icon);
            Assert.Empty(maze.GetTileAtPosition(0,2).SpritesOnTile);
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

            maze.MoveSpriteToNewPosition(ghost, ghost.CurrentPosition);
            Assert.Equal(ghost.Display.Icon, maze.GetTileAtPosition(0,1).SpritesOnTile[1].Display.Icon);
            Assert.Equal(ghost.Display.Colour, maze.GetTileAtPosition(0,1).SpritesOnTile[1].Display.Colour);
            Assert.Equal(ghost.Display.Icon,  maze.GetTileAtPosition(0,1).SpritesOnTile[1].Display.Icon);
        }

        [Fact]
        public void GivenPacmanPositionMazeShouldDisplayPacmanTile()
        {
            var parser = new Parser();
            var pacman = new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay());
            var mazeData = new[] {"..."};
            var maze = new Maze(mazeData, parser);
            pacman.UpdatePosition(new Position(0, 1));

            maze.MoveSpriteToNewPosition(pacman, pacman.CurrentPosition);
            Assert.Equal(pacman.Display.Icon, maze.GetTileAtPosition(pacman.CurrentPosition).SpritesOnTile[0].Display.Icon);
            Assert.Equal(pacman.Display.Colour, maze.GetTileAtPosition(pacman.CurrentPosition).SpritesOnTile[0].Display.Colour);
            Assert.Equal(pacman.Display.Priority, maze.GetTileAtPosition(pacman.CurrentPosition).SpritesOnTile[0].Display.Priority);
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
            
            maze.MoveSpriteToNewPosition(ghost, ghost.CurrentPosition);
            
            Assert.Equal(ghost.Display, maze.GetTileAtPosition(ghost.CurrentPosition.Row, ghost.CurrentPosition.Col).GetFirstSprite().Display);
        }

        [Fact]
        public void GivenPacmanAndGhostAreOnSameTileShouldHaveCollision()
        {
            var parser = new Parser();
            var mazeData = new[] {". *"};
            var maze = new Maze(mazeData, parser);
            var ghost = new MovingSprite(new Position(0, 0), new RandomMovement(new Rng()), new GhostSpriteDisplay());
            var pacman = new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay());

            maze.MoveSpriteToNewPosition(ghost, ghost.CurrentPosition);
            maze.MoveSpriteToNewPosition(pacman, pacman.CurrentPosition);


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

            maze.MoveSpriteToNewPosition(ghost, ghost.CurrentPosition);
            maze.MoveSpriteToNewPosition(pacman, pacman.CurrentPosition);
            ghost.UpdatePosition(ghost.CurrentPosition);
            pacman.UpdatePosition(pacman.CurrentPosition);

            ghost.PreviousPosition.Col = 1;
            ghost.PreviousPosition.Row = 0;
            pacman.PreviousPosition.Col = 0;
            pacman.PreviousPosition.Row = 0;
            Assert.True(maze.PacmanHasCollisionWithGhost(pacman));
        }

        [Fact]
        public void GivenPacmanMovesOntoATilePelletSpriteShouldBeRemovedFromTile()
        {
            var parser = new Parser();
            var mazeData = new[] {"....*"};
            var maze = new Maze(mazeData, parser);
     
            Assert.Contains(maze.GetTileAtPosition(0,0).SpritesOnTile, s => s.Display.Icon == new PelletSpriteDisplay().Icon);

            var pacman = new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay());
            maze.MoveSpriteToNewPosition(pacman, pacman.CurrentPosition);
            Assert.DoesNotContain(maze.GetTileAtPosition(pacman.CurrentPosition).SpritesOnTile, s => s.Display.Icon == new PelletSpriteDisplay().Icon);
        }

        [Fact]
        public void GivenGameRestartsMazeShouldMoveSpritesToDefaultPosition()
        {
            var parser = new Parser();
            var mazeData = new []
            {
                ". *.......",". *.......",". *.......",
                ". *.......",". *.......",". *.......",
                ". *.......",". *.......",". *.......",
                ". *.......",". *.......",". *......."
            };                   
            var maze = new Maze(mazeData, parser);
            
            var ghost = new MovingSprite(new Position(0, 0), new RandomMovement(new Rng()), new GhostSpriteDisplay());
            var pacman = new MovingSprite(new Position(0, 1), new PlayerControlMovement(), new PacmanSpriteDisplay());

            var movingSprites = new List<IMovingSprite> {ghost, pacman};
            maze.MoveSpriteToNewPosition(ghost, ghost.CurrentPosition);
            maze.MoveSpriteToNewPosition(pacman, pacman.CurrentPosition);
         
            maze.ResetSpritePositions(movingSprites);
            
            Assert.Equal(9, ghost.CurrentPosition.Row);
            Assert.Equal(9, ghost.CurrentPosition.Col);
            Assert.Equal(1, pacman.CurrentPosition.Row);
            Assert.Equal(1, pacman.CurrentPosition.Col);
        }
        
        [Fact]
        public void GivenPacmanIsEatingPelletsMazeShouldCountPelletsEaten()
        {
            var playerInput = new Mock<IPlayerInput>();
            playerInput.Setup(p => p.TakeInput()).Returns(ConsoleKey.RightArrow);
            playerInput.Setup(p => p.HasPressedQuit(ConsoleKey.LeftArrow)).Returns(true);

            var parser = new Parser();

            var mazeData = new []{". *"};
            var maze = new Maze(mazeData, parser);

            var pacman = new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay());

            maze.MoveSpriteToNewPosition(pacman, pacman.CurrentPosition);
            
            Assert.Equal(1,maze.PelletsEaten);
        }
        
        [Fact]
        public void GivenPelletsAreOnMazePelletsRemainingShouldCountPellets()
        {
            var parser = new Parser();

            var mazeData = new []{". *.."};
            var maze = new Maze(mazeData, parser);

            Assert.False( maze.HasNoPelletsRemaining());
        }

        [Fact]
        public void GivenNoPelletsAreOnMazePelletsRemainingShouldBeZero()
        {
            var parser = new Parser();

            var mazeData = new []{". *"};
            var maze = new Maze(mazeData, parser);
            
            var pelletTile = new PelletSpriteDisplay();
            var pelletSprite =   maze.GetTileAtPosition(0, 0).SpritesOnTile.First(s => s.Display.Icon == pelletTile.Icon);
            maze.GetTileAtPosition(0,0).SpritesOnTile.Remove(pelletSprite);
            
            Assert.True(maze.HasNoPelletsRemaining());
        }
    }
}