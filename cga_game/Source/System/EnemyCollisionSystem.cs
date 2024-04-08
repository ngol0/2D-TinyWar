﻿using System;
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

        public EnemyCollisionSystem() : base(Aspect.All(typeof(EnemyComponent)))
        {
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
                                    CollisionManager.OnBulletCollision?.Invoke();
                                }

                                //destroy bullet
                                DestroyEntity(otherCollider.Key);
                                CollisionManager.Colliders.Remove(otherCollider.Key);
                            }
                        }
                        //on collision exit
                        else
                        {
                            if (otherCollider.Value.tag == "unit") enemy.isAttacking = false;
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

                            Entity currUnit = GetEntity(otherCollider.Key);
                            var unitComp = currUnit.Get<UnitComponent>();
                            unitComp.currentHealth -= enemy.enemyType.damageDealt;

                            enemy.attackTimer = 0.0f;

                            //if unit dies
                            if (unitComp.currentHealth <= 0)
                            {
                                DestroyEntity(otherCollider.Key);
                                otherCollider.Value.worldPos = new Vector2(10000, 10000); //cheat code to remove unit
                                enemy.isAttacking = false;
                                enemy.attackTimer = enemy.enemyType.speedDealt;
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
