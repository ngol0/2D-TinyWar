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
    class EnemyCollisionSystem : EntityProcessingSystem
    {
        private ComponentMapper<Transform> transformMapper;
        private ComponentMapper<EnemyComponent> enemyMapper;
        Dictionary<IntPair, bool> collisionMap = new Dictionary<IntPair, bool>();

        Scene scene;

        public EnemyCollisionSystem(Scene scene) : base(Aspect.All(typeof(EnemyComponent)))
        {
            this.scene = scene;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            transformMapper = mapperService.GetMapper<Transform>();
            enemyMapper = mapperService.GetMapper<EnemyComponent>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var transform = transformMapper.Get(entityId);
            var enemy = enemyMapper.Get(entityId);
            enemy.attackTimer += gameTime.GetElapsedSeconds();

            foreach (var otherCollider in CollisionManager.Colliders)
            {
                IntPair key = new IntPair(transform, otherCollider.Value);

                bool currentStatus = CollisionManager.IsCollided(transform, otherCollider.Value);
                if (collisionMap.ContainsKey(key))
                {
                    if (currentStatus != collisionMap[key])
                    {
                        //on collision enter
                        if (currentStatus)
                        {
                            //check for collision with bullets
                            if (otherCollider.Value.tag == "bullet")
                            {
                                enemy.currentHealth -= 50;

                                //enemy dies
                                if (enemy.currentHealth <= 0)
                                {
                                    DestroyEntity(entityId);
                                    //add score
                                    CollisionManager.OnEnemyDie?.Invoke();
                                }

                                //destroy bullet
                                DestroyEntity(otherCollider.Key);
                                CollisionManager.Colliders.Remove(otherCollider.Key);
                            }
                        }
                        //on collision exit
                        else
                        {
                            if (otherCollider.Value.tag == "unit")
                            {
                                enemy.isAttacking = false;
                                enemy.currentlyAttackedUnit = null;
                            }
                        }
                        collisionMap[key] = currentStatus;
                    }
                    //on collision stay
                    else if (currentStatus && currentStatus == collisionMap[key])
                    {
                        //check for collision with unit
                        if (otherCollider.Value.tag == "unit" && enemy.attackTimer >= enemy.enemyType.speedDealt)
                        {
                            enemy.isAttacking = true;

                            if (enemy.currentlyAttackedUnit == null)
                            {
                                Entity currUnit = GetEntity(otherCollider.Key);
                                var unitComp = currUnit.Get<UnitComponent>();
                                enemy.currentlyAttackedUnit = unitComp;
                            }
                            enemy.currentlyAttackedUnit.currentHealth -= enemy.enemyType.damageDealt;

                            enemy.attackTimer = 0.0f;

                            //if unit dies
                            if (enemy.currentlyAttackedUnit.currentHealth <= 0)
                            {
                                DestroyEntity(otherCollider.Key);
                                otherCollider.Value.worldPos = new Vector2(10000, 10000); //cheat code to remove unit key
                                enemy.isAttacking = false;
                                enemy.attackTimer = enemy.enemyType.speedDealt;

                                var unitPos = otherCollider.Value.gridPos;
                                scene.GetGridItem(unitPos).SetPlaceable(true);
                            }
                        }
                    }
                }
                else
                {
                    collisionMap[key] = false;
                }
            }
        }
    }
}
