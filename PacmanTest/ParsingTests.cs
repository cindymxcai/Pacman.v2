using System.Collections.Generic;
using Pacman2;
using Xunit;

namespace PacmanTest
{
    public class ParsingTests
    {
        [Theory]
        [MemberData (nameof(TestData))]
        public void GivenCharacterShouldParseToTile(char input, string tileDisplay)
        {
            var inputData = Parser.GetTileType(input);
            Assert.Equal(tileDisplay, inputData.Display);
        }

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] {'.', PelletTileType.Display};
            yield return new object[] {'*', WallTileType.Display};
            yield return new object[] {' ', EmptyTileType.Display,};
        }
        
    }
}