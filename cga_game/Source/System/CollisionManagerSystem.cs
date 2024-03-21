using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended;
using System.Collections.Generic;
using System.Diagnostics;

// Update collider's position
namespace Strategy
{
    class CollisionManagerSystem : EntityProcessingSystem
    {
        Dictionary<IntPair, bool> collisionMap = new Dictionary<IntPair, bool>();
        private ComponentMapper<BoxCollider2D> colliderMapper;
        private ComponentMapper<Transform> transformMapper;


        public CollisionManagerSystem() : base(Aspect.All(typeof(BoxCollider2D)))
        {
            //this.scene = scene; 
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            colliderMapper = mapperService.GetMapper<BoxCollider2D>();
            transformMapper = mapperService.GetMapper<Transform>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            //update all the collision pos
            var collider = colliderMapper.Get(entityId);
            var transform = transformMapper.Get(entityId);

            collider.boundingBox.X = (int)transform.worldPos.X;
            collider.boundingBox.Y = (int)transform.worldPos.Y;

            for (int i = 0; i < CollisionManager.Colliders.Count; i++)
            {
                var otherCollider = colliderMapper.Get(CollisionManager.Colliders[i]);

                if (collider == null || otherCollider == null) return;
                if (collider.tag == otherCollider.tag) continue;

                bool currentStatus = IsCollided(collider, otherCollider);

                IntPair key = new IntPair(entityId, CollisionManager.Colliders[i]);

                if (collisionMap.ContainsKey(key))
                {
                    if (currentStatus != collisionMap[key])
                    {
                        if (currentStatus)
                        {
                            collider.OnCollisionEnter?.Invoke(entityId, CollisionManager.Colliders[i]);
                            otherCollider.OnCollisionEnter?.Invoke(CollisionManager.Colliders[i], entityId);
                        }
                        else
                        {

                        }

                        collisionMap[key] = currentStatus;
                    }
                    else
                    {

                    }
                }
                else
                {
                    collisionMap[key] = false;
                }
            }        
        }

        private static bool IsCollided(BoxCollider2D firstCollider, BoxCollider2D secondCollider)
        {
            return firstCollider.boundingBox.Intersects(secondCollider.boundingBox);
        }

        private static bool CollisionExists(Transform bulletTransform, Transform shipTransform)
        {
            return Vector2.Distance(bulletTransform.worldPos, shipTransform.worldPos) < 10.0f;
        }
    }
}
