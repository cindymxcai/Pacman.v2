using System.IO;
using Pacman2.Interfaces;

namespace Pacman2
{
    public class FileReader : IFileReader
    {
        public string ReadAllData(string jsonFileName)
        {
            return File.ReadAllText(jsonFileName);
        }
        
        public string[] ReadFile(string fileName)
        {
            return File.ReadAllLines(fileName);
        }
    }
}