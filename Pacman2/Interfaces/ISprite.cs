namespace Pacman2.Interfaces
{
    public interface ISprite
    {
        public ISpriteDisplay Display { get; }
        void Render();
    }
}