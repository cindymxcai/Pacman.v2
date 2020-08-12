using System.Collections.Generic;
using System.Linq;
using Pacman2;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;
using Xunit;

namespace PacmanTest
{
    public class ParsingTests
    {
        [Theory]
        [MemberData (nameof(TestData))]
        public void GivenCharacterShouldParseToTile(char input, ISpriteDisplay tileDisplay)
        {
            var parser = new Parser();
            var inputData = parser.GetTile(input);
            Assert.Equal(tileDisplay.Icon, inputData.SpritesOnTile.First().Display.Icon);
            Assert.Equal(tileDisplay.Colour, inputData.SpritesOnTile.First().Display.Colour);
        }

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] {'.', new PelletSpriteDisplay()};
            yield return new object[] {'*', new WallSpriteDisplay()};
        }

        [Fact]
        public void GivenNoCharacterShouldParseToEmptyTile()
        {
            var parser = new Parser();
            var inputData = parser.GetTile(' ');
            Assert.Empty(inputData.SpritesOnTile);
        }
    }
}