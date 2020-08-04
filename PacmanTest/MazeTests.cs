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
            Assert.Equal(399, maze.Tiles.Length);
        }

        [Fact]
        public void GivenSizeAndDataShouldPopulateMazeArray()
        {
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var maze = new Maze(mazeData);
            Assert.Equal(Constants.PelletDisplay, maze.Tiles[0,0].Display);
            Assert.Equal(Constants.WallDisplay, maze.Tiles[0,6].Display);
        }
        
        [Fact]
        public void GivenGhostPositionMazeShouldDisplayGhostTile()
        {
            var ghost = new Ghost(0,1);
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var maze = new Maze(mazeData);
            maze.UpdateArray(ghost.X, ghost.Y, ghost.Display, ghost.Colour);
            Assert.Equal(ghost.Display, maze.Tiles[0,1].Display);
        }
    }
}