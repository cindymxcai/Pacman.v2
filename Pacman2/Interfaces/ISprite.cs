namespace Pacman2.Interfaces
{
    public interface ISprite
    {
        ISpriteDisplay Display { get; }
        void Render();
    }
}