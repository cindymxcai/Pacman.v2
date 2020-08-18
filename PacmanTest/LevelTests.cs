using System;
using System.Collections.Generic;
using Moq;
using Pacman2;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;
using Pacman2.Sprites;
using Xunit;

namespace PacmanTest
{
    public class LevelTests
    {
        [Fact]
        public void GivenAllPelletSpritesOnMazeAreEatenLevelShouldFinish()
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
            var pelletTile = new PelletSpriteDisplay();
            foreach (var tile in maze.Tiles)
            {
                var pelletSprite =  tile.GetGivenSprite(pelletTile);
                tile.SpritesOnTile.Remove(pelletSprite);
               Assert.DoesNotContain(tile.SpritesOnTile, s=> s.Display == pelletTile);
            }
            Assert.True(maze.HasNoPelletsRemaining());
            var level = new Level(maze, new PlayerInput(), new Display(), new RandomMovement(new Rng()), new PlayerControlMovement(), new GhostSpriteDisplay(), new PacmanSpriteDisplay() );
            Assert.True(level.HasWon());
        }

        [Fact]
        public void GivenANewLevelShouldStartWith3Lives()
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
            
            var level = new Level(maze, new PlayerInput(), new Display(), new RandomMovement(new Rng()), new PlayerControlMovement(), new GhostSpriteDisplay(), new PacmanSpriteDisplay()  );
            Assert.Equal(3, level.LivesRemaining);
        }
        
        [Fact]
        public void GivenPacmanGetsEatenLevelShouldLoseALife()
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

            var level = new Level(maze, new PlayerInput(), new Display(), new RandomMovement(new Rng()),
                new PlayerControlMovement(), new GhostSpriteDisplay(), new PacmanSpriteDisplay())
            {
                Sprites = new List<IMovingSprite>()
                {
                    new MovingSprite(new Position(0, 0), new RandomMovement(new Rng()), new GhostSpriteDisplay()),
                    new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay())
                }
            };

            foreach (var sprite in level.Sprites)
            {
                level.UpdateSpritePosition(sprite);
                level.IsPacmanEaten(sprite);
            }
            
            Assert.Equal(2, level.LivesRemaining);
        }
        
        [Fact]
        public void GivenAllLivesLostShouldStopPlaying()
        { 
            var playerInput = new Mock<IPlayerInput>();
            playerInput.Setup(p => p.TakeInput()).Returns(ConsoleKey.RightArrow);
            playerInput.Setup(p => p.HasPressedQuit(ConsoleKey.LeftArrow)).Returns(true);

            var parser = new Parser();
            var mazeData = new []
            {
                ". *.......",". *.......",". *.......",
                ". *.......",". *.......",". *.......",
                ". *.......",". *.......",". *.......",
                ". *.......",". *.......",". *......."
            };
            
            var maze = new Maze(mazeData, parser);

            var sprites = new List<IMovingSprite>()
            {
                new MovingSprite(new Position(0, 0), new RandomMovement(new Rng()), new GhostSpriteDisplay()),
                new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay())
            };

            var level = new Level(maze, new PlayerInput(), new Display(), new RandomMovement(new Rng()), new PlayerControlMovement(), new GhostSpriteDisplay(), new PacmanSpriteDisplay()  );
            Assert.True(level.PacmanIsAlive);

            for (var i = 0; i < 3; i++)
            {
                foreach (var sprite in sprites)
                { 
                    sprite.UpdatePosition( new Position(0,0));
                    level.UpdateSpritePosition(sprite);
                    level.IsPacmanEaten(sprite);
                }
            }
            Assert.False(level.PacmanIsAlive);
        }
    }
}