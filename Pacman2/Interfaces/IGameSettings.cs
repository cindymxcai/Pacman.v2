namespace Pacman2.Interfaces
{
    public interface IGameSettings
    {
        string[] LevelSettings { get; }
        int MaxLevels { get; }
    }
}