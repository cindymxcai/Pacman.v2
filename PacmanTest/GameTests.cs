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
    public class GameTests
    {
        [Fact]
        public void GivenPacmanAndGhostCollideShouldStopPlaying()
        { 
            var playerInput = new Mock<IPlayerInput>();
            playerInput.Setup(p => p.TakeInput()).Returns(ConsoleKey.RightArrow);
            playerInput.Setup(p => p.HasPressedQuit(ConsoleKey.LeftArrow)).Returns(true);

            var parser = new Parser();

            var mazeData = new []{". *"};
            var maze = new Maze(mazeData, parser);
            var sprites = new List<IMovingSprite>()
            {
                new MovingSprite(new Position(0, 0), new RandomMovement(new Rng()), new GhostSpriteDisplay()),
                new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay())
            };

             var game = new Game(sprites, maze, playerInput.Object, new Display());
             Assert.True(game.PacmanIsAlive);

             foreach (var sprite in sprites)
             {
                 game.UpdateSpritePosition(sprite);
                 game.IsPacmanEaten(sprite);
             }
            
             Assert.False(game.PacmanIsAlive);
        }

        [Fact]
        public void GivenAllPelletSpritesOnMazeAreEatenGameShouldFinish()
        {
            var parser = new Parser();

            var mazeData = new []{". *"};
            var maze = new Maze(mazeData, parser);
            var pelletTile = new PelletSpriteDisplay();
            var pelletSprite =  maze.GetTileAtPosition(0, 0).GetGivenSprite(pelletTile);
            maze.Tiles[0, 0].SpritesOnTile.Remove(pelletSprite);
            
            Assert.Empty(maze.Tiles[0,0].SpritesOnTile);
            
            var sprites = new List<IMovingSprite>()
            {
                new MovingSprite(new Position(0, 0), new RandomMovement(new Rng()), new GhostSpriteDisplay()),
                new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay())
            };
            var game = new Game(sprites, maze, new PlayerInput(), new Display());
            Assert.True(game.HasWon());
        }

        [Fact]
        public void GivenANewGameShouldStartWith3Lives()
        {
            var parser = new Parser();

            var mazeData = new []{". *"};
            var maze = new Maze(mazeData, parser);
            
            var sprites = new List<IMovingSprite>()
            {
                new MovingSprite(new Position(0, 0), new RandomMovement(new Rng()), new GhostSpriteDisplay()),
                new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay())
            };
            
            var game = new Game(sprites, maze, new PlayerInput(), new Display());
            Assert.Equal(3, game.LivesRemaining);
        }
        
        [Fact]
        public void GivenPacmanGetsEatenGameShouldLoseALife()
        {
            var parser = new Parser();

            var mazeData = new []{". *"};
            var maze = new Maze(mazeData, parser);
            
            var sprites = new List<IMovingSprite>()
            {
                new MovingSprite(new Position(0, 0), new RandomMovement(new Rng()), new GhostSpriteDisplay()),
                new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay())
            };
            
            var game = new Game(sprites, maze, new PlayerInput(), new Display());
            
            foreach (var sprite in sprites)
            {
                game.UpdateSpritePosition(sprite);
                game.IsPacmanEaten(sprite);
            }
            
            Assert.False(game.PacmanIsAlive);
            Assert.Equal(2, game.LivesRemaining);
        }
    }
}