using Pacman2.Interfaces;

namespace Pacman2.Factories
{
    /// <summary>
    /// This is a Factory Design Pattern that takes in dependencies needed to make a maze and creates a new maze with that dependency 
    /// </summary>
    public class MazeFactory
    {
        private readonly IFileReader _fileReader;
        private readonly IParser _parser;

        public MazeFactory(IFileReader fileReader, IParser parser)
        {
            _fileReader = fileReader;
            _parser = parser;
        }
        public IMaze CreateMaze(string mazeData)
        {
            var mazeDataForLevel = _fileReader.ReadFile(mazeData);
            return new Maze(mazeDataForLevel, _parser );
        }
    }
}