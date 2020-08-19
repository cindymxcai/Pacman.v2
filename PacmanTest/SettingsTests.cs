using System;
using System.IO;
using Pacman2;
using Xunit;

namespace PacmanTest
{
    public class SettingsTests
    {

        [Fact]
        public void GivenFileFileReaderShouldReadAllLines()
        {
            var fileReader = new FileReader();
            var file = fileReader.ReadFile(Path.Combine(Environment.CurrentDirectory, "MazeData/levelOneMazeData.txt"));
            Assert.Equal(21, file.Length);
            Assert.Equal( '*',file[0][0]);
        }
        
        [Fact]
        public void GivenLevelSettingsShouldReturnCorrectInfo()
        {
            var gameSettingLoader = new GameSettingLoader(new FileReader());
            var gameSettings = gameSettingLoader.GetMazeData();
            Assert.Equal(3, gameSettings.MaxLevels);
            Assert.Equal(2,gameSettings.LevelSettings.Length);
        }
    }
}