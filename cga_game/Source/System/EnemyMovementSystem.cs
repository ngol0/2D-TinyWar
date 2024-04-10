using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended;

namespace Strategy
{
    class EnemyMovementSystem : EntityProcessingSystem
    {
        private ComponentMapper<EnemyComponent> enemyComponentMapper;
        private ComponentMapper<Transform> transformMapper;

        Scene scene;

        public EnemyMovementSystem(Scene scene) : base(Aspect.All(typeof(EnemyComponent), typeof(Transform)))
        {
            this.scene = scene;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            enemyComponentMapper = mapperService.GetMapper<EnemyComponent>();
            transformMapper = mapperService.GetMapper<Transform>();

            scene.OnRestart += Restart;
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var enemy = enemyComponentMapper.Get(entityId);
            var transform = transformMapper.Get(entityId);

            if (!enemy.isAttacking) transform.worldPos.X -= gameTime.GetElapsedSeconds() * enemy.enemyType.speed;

            //lost condition
            if (transform.worldPos.X < -5) //lose
            {
                Globals.windowManager.SetWindow(Globals.gameOverWindow);
            }
        }

        public void Restart()
        {
            foreach (var id in ActiveEntities)
            {
                DestroyEntity(id);
            }
        }
    }
}
