using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Strategy.Grid;
using System;

namespace Strategy
{
    class UnitShooterSystem : EntityProcessingSystem
    {
        Scene scene;
        private ComponentMapper<UnitComponent> unitCompMapper;
        private ComponentMapper<Transform> transformMapper;
        //private ComponentMapper<Sprite> spriteMapper;

        public UnitShooterSystem(Scene scene) : base(Aspect.All(typeof(UnitComponent), typeof(Transform), typeof(Sprite)).Exclude(typeof(MoneyGenerator)))
        {
            this.scene = scene;
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            unitCompMapper = mapperService.GetMapper<UnitComponent>();
            transformMapper = mapperService.GetMapper<Transform>();
            //spriteMapper = mapperService.GetMapper<Sprite>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var transform = transformMapper.Get(entityId);
            var unit = unitCompMapper.Get(entityId);
            //var sprite = spriteMapper.Get(entityId);

            unit.currentTimer += gameTime.GetElapsedSeconds();

            if (scene.EnemyManager.LaneHasEnemy(transform.gridPos.y) && unit.currentTimer >= unit.unitType.shootTimer)
            {
                //sprite.color = Color.Red;
                Vector2 bulletPos = transform.worldPos + unit.bulletSpawnOffset;
                //init bullet
                Globals.entityFactory.CreateBullet(
                    new Transform() { gridPos = scene.GetGridPosition(bulletPos), worldPos = bulletPos, scale = 10 },
                    unit.unitType);

                unit.currentTimer = 0;
            }
        }
    }
}
