using Pacman2;
using Xunit;

namespace PacmanTest
{
    public class ParsingTests
    {
        [Theory]
        [InlineData('.', " . ")]
        [InlineData('*', " * ")]
        [InlineData(' ', "   ")]
        public void GivenCharacterShouldParseToTile(char input, string tileDisplay)
        {
            var inputData  = Parser.GetTileType(input);
            Assert.Equal(tileDisplay, inputData.Display);
        }
    }
}