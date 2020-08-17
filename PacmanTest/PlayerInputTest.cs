using System;
using Pacman2;
using Xunit;

namespace PacmanTest
{
    public class PlayerInputTest
    {
        [Fact]
        public void GivenInputIsQShouldReturnTrue()
        {
            var playerInput = new PlayerInput();
            Assert.True(playerInput.HasPressedQuit(ConsoleKey.Q));
            Assert.False(playerInput.HasPressedQuit(ConsoleKey.P));
            Assert.False(playerInput.HasPressedQuit(ConsoleKey.UpArrow));
        }
    }
}