using System.Collections.Generic;
using Pacman2;
using Pacman2.Interfaces;
using Pacman2.Tiles;
using Xunit;

namespace PacmanTest
{
    public class ParsingTests
    {
        [Theory]
        [MemberData (nameof(TestData))]
        public void GivenCharacterShouldParseToTile(char input, ITileType tileDisplay)
        {
            var parser = new Parser();
            var inputData = parser.GetTile(input);
            Assert.Equal(tileDisplay.Display, inputData.TileType.Display);
            Assert.Equal(tileDisplay.Colour, inputData.TileType.Colour);
        }

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] {'.', new PelletTileType()};
            yield return new object[] {'*', new WallTileType()};
            yield return new object[] {' ', new EmptyTileType()};
        }
        
    }
}