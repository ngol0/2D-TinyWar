using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Strategy
{
    class UnitComponent
    {
        public UnitType unitType;
        public float currentTimer;
        public Vector2 bulletSpawnOffset;
    }
}
