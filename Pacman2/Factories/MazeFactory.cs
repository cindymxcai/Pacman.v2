using Pacman2.Interfaces;

namespace Pacman2.Factories
{
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