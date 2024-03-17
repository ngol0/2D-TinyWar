using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended;

namespace Strategy
{
    class UnitCollisionSystem : EntityProcessingSystem
    {
        Scene scene;
        private ComponentMapper<BoxCollider2D> boxColliderMapper;
        private ComponentMapper<UnitComponent> unitCompMapper;

        public UnitCollisionSystem(Scene scene) : base(Aspect.All(typeof(BoxCollider2D), typeof(UnitComponent)))
        {
            this.scene = scene;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            boxColliderMapper = mapperService.GetMapper<BoxCollider2D>();
            unitCompMapper = mapperService.GetMapper<UnitComponent>();

            //listen to collision event
            foreach (var collision in boxColliderMapper.Components) 
            {
                collision.OnCollisionEnter += PlayerCollisionResponse;
            }
        }

        private void PlayerCollisionResponse(Entity player, Entity other)
        {
            var tag = other.Get<BoxCollider2D>().tag;
            if (tag == "enemy")
            {
                var enem = other.Get<EnemyComponent>();
                var playerComp = player.Get<UnitComponent>();
                playerComp.unitType.health -= enem.enemyType.damageDealt;

                if (playerComp.unitType.health <= 0) 
                {
                    DestroyEntity(player.Id);
                }
            }
        }

        public override void Process(GameTime gameTime, int entityId)
        {
        }
    }
}
