using System;
using System.IO;
using Newtonsoft.Json;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class GameSettingLoader : IGameSettingLoader
    {
        private readonly IFileReader _fileReader;

        public GameSettingLoader(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public GameSettings GetMazeData()
        {
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = _fileReader.ReadAllData(jsonFileName);
            return JsonConvert.DeserializeObject<GameSettings>(json);
        }
    }
}