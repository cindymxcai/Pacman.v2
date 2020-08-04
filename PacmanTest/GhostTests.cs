using System;
using System.IO;
using Pacman2;
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

        [Fact]
        public void GivenGhostPositionMazeShouldDisplayGhostTile()
        {
            var ghost = new Ghost(0,1);
            
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var maze = new Maze(mazeData);
            maze.UpdateArray(ghost.X, ghost.Y, ghost.Display);
            Assert.Equal(ghost.Display, maze.MazeArray[0,1].Display);
        }
    }
}