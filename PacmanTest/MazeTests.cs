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
            var parser = new Parser();
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var maze = new Maze(mazeData, parser);
            Assert.Equal(21, maze.Row);
            Assert.Equal(19, maze.Column);
        }

        [Fact]
        public void GivenMazeDataShouldCreateArrayOfTiles()
        {
            var parser = new Parser();
            var mazeData = new[] {"...", "..."};
            var maze = new Maze(mazeData, parser);
            Assert.Equal(6, maze.Tiles.Length);
        }

        [Fact]
        public void GivenSizeAndDataShouldPopulateMazeArray()
        {
            var parser = new Parser();

            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var maze = new Maze(mazeData, parser);
            Assert.Equal(PelletTileType.Display, maze.Tiles[0,0].Display);
            Assert.Equal(WallTileType.Display, maze.Tiles[0,6].Display);
        }
        
        [Fact]
        public void GivenGhostPositionMazeShouldDisplayGhostTile()
        {
            var parser = new Parser();

            var ghost = new Ghost(0,1, new RandomMovement());
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var maze = new Maze(mazeData, parser);
            maze.UpdateArray(ghost.X, ghost.Y, ghost.Display, ghost.Colour);
            Assert.Equal(ghost.Display, maze.Tiles[0,1].Display);
        }
    }
}