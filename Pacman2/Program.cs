using System;
using System.Collections.Generic;
using System.IO;
using Pacman2.Interfaces;
using Pacman2.SpriteDisplays;
using Pacman2.Sprites;

namespace Pacman2
{
    public static class Program
    {
        private static void Main()
        {
            var mazeData = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "mazeData.txt"));
            var parser = new Parser();
            var maze = new Maze(mazeData, parser);
            
            var ghostDisplay = new GhostSpriteDisplay();
            var pacmanDisplay = new PacmanSpriteDisplay();

            var rng = new Rng();
            var randomMovement = new RandomMovement(rng);
            
            var playerInput = new PlayerInput();
            var playerMovement = new PlayerControlMovement();
            
            var display = new Display();
            
            var movingSprites = new List<IMovingSprite>
            {
                new MovingSprite(maze.GetTilePosition(9, 9), randomMovement, ghostDisplay),
                new MovingSprite(maze.GetTilePosition(9, 9), randomMovement, ghostDisplay),
                new MovingSprite(maze.GetTilePosition(2, 1), playerMovement, pacmanDisplay)
            };
            
            var game = new Game(movingSprites, maze, playerInput, display);

           game.Play();
            
        }
    }
}