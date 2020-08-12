using System;
using Moq;
using Pacman2;
using Pacman2.Interfaces;
using Xunit;

namespace PacmanTest
{
    public class PacmanBehaviourTest
    {
        [Theory]
        [InlineData(ConsoleKey.UpArrow, Direction.Up)]
        [InlineData(ConsoleKey.DownArrow, Direction.Down)]
        [InlineData(ConsoleKey.LeftArrow, Direction.Left)]
        [InlineData(ConsoleKey.RightArrow, Direction.Right)]
        public void GivenConsoleKeyShouldReturnADirection(ConsoleKey consoleKey, Direction direction)
        {
            var playerInput = new Mock<IPlayerInput>();
            playerInput.Setup(p => p.TakeInput()).Returns(consoleKey);
            var playerMovement = new PlayerControlMovement(playerInput.Object);
            var newDirection = playerMovement.GetNewDirection();
            Assert.Equal(direction, newDirection);
        }
    }
}