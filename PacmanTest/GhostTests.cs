using Xunit;

namespace PacmanTest
{
    public class GhostTests
    {
        [Fact]
        public void GivenAPositionAGhostShouldHoldXAndYCoordinates()
        {
            var ghost = new Ghost(0,1);
            Assert.Equal(0, ghost.X);
            Assert.Equal(1, ghost.Y);
        }
    }
}