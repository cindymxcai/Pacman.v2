using System.Collections.Generic;
using Pacman2;
using Xunit;

namespace PacmanTest
{
    public class ParsingTests
    {
        private static ITileTypeFactory SetUp()
        {
            var wall = new WallTileType();
            var empty = new EmptyTileType();
            var pellet = new PelletTileType();
            var ghost = new GhostTileType();
            return new TileTypeFactory(wall, empty, pellet, ghost);
        }
        
        [Theory]
        [MemberData (nameof(TestData))]
        public void GivenCharacterShouldParseToTile(char input, string tileDisplay)
        {
            var parser = new Parser();
            var inputData = parser.GetTileType(input);
            Assert.Equal(tileDisplay, inputData.Display);
        }

        public static IEnumerable<object[]> TestData()
        {
            var tileTypeFactory = SetUp();
            yield return new object[] {'.', tileTypeFactory.Pellet.Display};
            yield return new object[] {'*', tileTypeFactory.Wall.Display};
            yield return new object[] {' ', tileTypeFactory.Empty.Display,};
        }
        
    }
}