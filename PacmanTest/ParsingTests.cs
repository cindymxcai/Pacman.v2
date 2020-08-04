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
            yield return new object[] {'.', Constants.PelletDisplay};
            yield return new object[] {'*', Constants.WallDisplay};
            yield return new object[] {' ', Constants.EmptyDisplay,};
        }
        
    }
}