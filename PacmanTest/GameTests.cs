using Pacman2;
using Pacman2.SpriteDisplays;
using Xunit;

namespace PacmanTest
{
    public class GameTests
    {
        [Fact]
        public void GivenNewGameShouldHaveInitialStates()
        {
            var fileReader = new FileReader();
            var parser = new Parser();
            var mazeFactory = new MazeFactory(fileReader, parser);
            
            var display = new Display();
            var playerInput = new PlayerInput();
            var rng = new Rng();
            var randomMovement = new RandomMovement(rng);
            var playerMovement = new PlayerControlMovement();
            var ghostDisplay = new GhostSpriteDisplay();
            var pacmanDisplay = new PacmanSpriteDisplay();
            var levelFactory = new LevelFactory(mazeFactory, display, playerInput, randomMovement, playerMovement, ghostDisplay, pacmanDisplay);
            
            var gameSettingLoader = new GameSettingLoader(fileReader);
            var game = new Game(gameSettingLoader, levelFactory, display);
            Assert.Equal(1, game.CurrentLevelNumber);
            Assert.True(game.IsPlaying);
        }
        
        [Fact]
        public void GivenGamePlaysLevelStatesShouldChangeAccordingly()
        {
            var fileReader = new FileReader();
            var parser = new Parser();
            var mazeFactory = new MazeFactory(fileReader, parser);
            
            var display = new Display();
            var playerInput = new PlayerInput();
            var rng = new Rng();
            var randomMovement = new RandomMovement(rng);
            var playerMovement = new PlayerControlMovement();
            var ghostDisplay = new GhostSpriteDisplay();
            var pacmanDisplay = new PacmanSpriteDisplay();
            var levelFactory = new LevelFactory(mazeFactory, display, playerInput, randomMovement, playerMovement, ghostDisplay, pacmanDisplay);
            
            var gameSettingLoader = new GameSettingLoader(fileReader);
            var game = new Game(gameSettingLoader, levelFactory, display);
            game.HandleNextLevel(gameSettingLoader.GetMazeData());
            Assert.Equal(2, game.CurrentLevelNumber);
            game.HandleNextLevel(gameSettingLoader.GetMazeData());
            Assert.False(game.IsPlaying);
        }
    }
}