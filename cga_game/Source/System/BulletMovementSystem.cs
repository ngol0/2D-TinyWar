using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Strategy.Grid;

namespace Strategy
{
    class BulletMovementSystem : EntityProcessingSystem
    {
        private ComponentMapper<Bullet> bulletCompMapper;
        private ComponentMapper<Transform> transformMapper;
        Scene scene;

        public BulletMovementSystem(Scene scene) : base(Aspect.All(typeof(Bullet), typeof(Transform)))
        {
            this.scene = scene;
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            bulletCompMapper = mapperService.GetMapper<Bullet>();
            transformMapper = mapperService.GetMapper<Transform>();

            scene.OnRestart += Restart;
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var bullet = bulletCompMapper.Get(entityId);
            var transform = transformMapper.Get(entityId);

            transform.worldPos.X += gameTime.GetElapsedSeconds() * bullet.speed;
        }

        private void Restart()
        {
            foreach (var entity in ActiveEntities)
            {
                DestroyEntity(entity);
            }
        }
    }
}
