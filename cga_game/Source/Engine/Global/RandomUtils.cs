using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;

namespace Strategy
{
    public static class RandomUtils
    {
        private static Random rand = new Random();

        // Return a float between -1.0f and 1.0f
        public static float RandFloat()
        {
            return (float)(rand.NextDouble() - rand.NextDouble());
        }

        public static int Rand(int min, int max)
        {
            return rand.Next(min, max);
        }
    }
}
