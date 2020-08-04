using System;

namespace Pacman2
{
    public class Rng : IRng
    {
        private readonly Random _random;

        public Rng()
        {
            _random = new Random();
        }

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}