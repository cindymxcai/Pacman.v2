using Pacman2;
using Pacman2.Factories;
using Pacman2.SpriteDisplays;
using Xunit;

namespace PacmanTest
{
    public class FactoryTests
    {
        
        [Fact]
        public void MazeFactoryMakesMaze()
        {
            
            var fileReader = new FileReader();
            var parser = new Parser();
            var mazeFactory = new MazeFactory(fileReader, parser);
            

            var gameSettingLoader = new GameSettingLoader(fileReader);
            
            Assert.Equal(19, mazeFactory.CreateMaze(gameSettingLoader.GetMazeData().LevelSettings[0]).Columns);
        }
        
        [Fact]
        public void LevelFactoryMakesLevel()
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
            
            Assert.Equal(3, levelFactory.CreateLevel(gameSettingLoader.GetMazeData(), 1).LivesRemaining);
        }
    }
}