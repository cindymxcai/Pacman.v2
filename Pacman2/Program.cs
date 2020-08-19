using Pacman2.Factories;
using Pacman2.SpriteDisplays;

namespace Pacman2
{
    public static class Program
    {
        private static void Main()
        {
            var fileReader = new FileReader();
            var parser = new Parser();
            var mazeFactory = new MazeFactory(fileReader, parser);
            
            var playerInput = new PlayerInput();

            var display = new Display();
            var rng = new Rng();
            var randomMovement = new RandomMovement(rng);
            
            var playerMovement = new PlayerControlMovement();
            var ghostDisplay = new GhostSpriteDisplay();
            var pacmanDisplay = new PacmanSpriteDisplay();
            var levelFactory = new LevelFactory(mazeFactory, display, playerInput, randomMovement, playerMovement, ghostDisplay, pacmanDisplay);
            
            var gameSettingLoader = new GameSettingLoader(fileReader);
            var game = new Game(playerInput,gameSettingLoader, levelFactory, display);
            
            game.Play();
        }
    }
}