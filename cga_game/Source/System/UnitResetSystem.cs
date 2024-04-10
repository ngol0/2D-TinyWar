using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended;


namespace Strategy
{
    class UnitResetSystem : EntityProcessingSystem
    {
        Scene scene;

        public UnitResetSystem(Scene scene) : base(Aspect.All(typeof(UnitComponent)))
        {
            this.scene = scene;
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            scene.OnRestart += Restart;
        }

        public override void Process(GameTime gameTime, int entityId)
        {

        }

        private void Restart()
        {
            foreach (var entity in ActiveEntities)
            {
                var transform = GetEntity(entity).Get<Transform>();
                scene.GetGridItem(transform.gridPos).SetPlaceable(true);

                DestroyEntity(entity);
            }
        }
    }
}
