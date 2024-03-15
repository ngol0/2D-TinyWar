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
        private ComponentMapper<Sprite> spriteMapper;

        public UnitShooterSystem(Scene scene) : base(Aspect.All(typeof(UnitComponent), typeof(Transform), typeof(Sprite)))
        {
            this.scene = scene;
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            unitCompMapper = mapperService.GetMapper<UnitComponent>();
            transformMapper = mapperService.GetMapper<Transform>();
            spriteMapper = mapperService.GetMapper<Sprite>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var transform = transformMapper.Get(entityId);
            var sprite = spriteMapper.Get(entityId);

            if (scene.WaveManager.LaneHasEnemy(transform.gridPos.y))
            {
                //sprite.color = Color.Red;
                //init bullet

            }
        }
    }
}
