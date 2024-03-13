using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Strategy.Grid;

namespace Strategy
{
    class MoneyGenerator
    {
        public int amount = 10;
        public float maxTimer = 3.0f;

        public float currentTimer = 0.0f;
    }
}
