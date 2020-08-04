using System;
using System.IO;
using Pacman2;
using Xunit;

namespace PacmanTest
{
    public class TileTests
    {
        [Fact]
        public void GivenConsoleColourShouldChangeTilesColour()
        {
            var parser = new Parser();

            var ghost = new Ghost(0,1, new RandomMovement());
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var maze = new Maze(mazeData, parser);
            maze.UpdateArray(ghost.X, ghost.Y, ghost.Display, ghost.Colour);
        }
    }
}