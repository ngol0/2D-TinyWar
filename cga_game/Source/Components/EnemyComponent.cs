using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Strategy.Grid;

namespace Strategy
{
    class EnemyComponent
    {
        public EnemyType enemyType;
        public int currentHealth;
        public bool isAttacking = false;

        public UnitComponent currentlyAttackedUnit;
        public float attackTimer = 200.0f;
    }
}
