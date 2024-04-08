using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Strategy.Grid;

namespace Strategy
{
    class Bullet
    {
        public float speed;
        public int damageDealt;
        public Bullet() { speed = 10.0f; }
        public Bullet(float speed)
        { this.speed = speed; }
    }
}
