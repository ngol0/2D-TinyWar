using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended;

// Update collider's position
namespace Strategy
{
    class CollisionManagerSystem : EntityProcessingSystem
    {
        Scene scene;
        public CollisionManagerSystem(Scene scene) : base(Aspect.All(typeof(BoxCollider2D)))
        {
            this.scene = scene; 
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            //fire event

        }
    }
}
