namespace Pacman2.Interfaces
{
    public interface IFileReader
    {
        string ReadAllData(string jsonFileName);
        string[] ReadFile(string fileName);
    }
}