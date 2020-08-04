using System;
using System.IO;
using Pacman2;
using Xunit;

namespace PacmanTest
{
    public class MazeTests
    {
        [Fact]
        public void GivenMazeDataShouldGetSizeOfMaze()
        {
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var maze = new Maze(mazeData);
            Assert.Equal(21, maze.Height);
            Assert.Equal(19, maze.Width);
        }

        [Fact]
        public void GivenMazeSizeShouldCreateArrayOfTiles()
        {
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var maze = new Maze(mazeData);
            Assert.Equal(399, maze.MazeArray.Length);
        }

        [Fact]
        public void GivenSizeAndDataShouldPopulateMazeArray()
        {
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var maze = new Maze(mazeData);
            Assert.Equal(" . ", maze.MazeArray[0,0].Display);
            Assert.Equal(" * ", maze.MazeArray[0,6].Display);

        }
    }
}