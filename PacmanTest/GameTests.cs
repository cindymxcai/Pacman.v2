using System;
using System.Collections.Generic;
using Moq;
using Pacman2;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;
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
            playerInput.Setup(p => p.HasPressedQuit()).Returns(true);

            var parser = new Parser();

            var mazeData = new []{". *"};
            var maze = new Maze(mazeData, parser);
            var sprites = new List<IMovingSprite>()
            {
                new MovingSprite(new Position(0, 0), new RandomMovement(new Rng()), new GhostSpriteDisplay()),
                new MovingSprite(new Position(0, 0), new PlayerControlMovement(), new PacmanSpriteDisplay())
            };

             var game = new Game(sprites, maze, playerInput.Object);
             Assert.True(game.IsPlaying);

             foreach (var sprite in sprites)
             {
                 maze.UpdateSpritePosition(sprite);
                 game.IsPacmanEaten(sprite);
             }
            
             Assert.False(game.IsPlaying);
        }
    }
}