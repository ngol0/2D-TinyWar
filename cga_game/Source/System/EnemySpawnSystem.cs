using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended;
using System.Collections.Generic;
using System.Diagnostics;

namespace Strategy
{
    class EnemySpawnSystem : EntityProcessingSystem
    {
        Scene scene;

        private ComponentMapper<Transform> transformMapper;
        int currentIndex = 0;
        float currentTimer;
        bool endOfFile = false;

        public EnemySpawnSystem(Scene scene) : base(Aspect.All(typeof(EnemySpawner), typeof(Transform)))
        {
            this.scene = scene;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            transformMapper = mapperService.GetMapper<Transform>();
            scene.OnRestart += Restart;
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var transform = transformMapper.Get(entityId);

            // increase timer
            currentTimer += gameTime.GetElapsedSeconds();

            // check to see if which lane to spawn enemy based on the information in level info and not end of list
            if (scene.GetLevelInfo()[currentIndex].laneNumber == transform.gridPos.y && !endOfFile)
            {
                // get random enemy types
                int randomIndex = RandomUtils.Rand(0, EnemyTypeList.enemyTypeList.Count);
                var enemyType = EnemyTypeList.enemyTypeList[randomIndex];

                // check to see if whether the current timer reaches the enemy spawn time in level info
                if (currentTimer >= scene.GetLevelInfo()[currentIndex].timeToSpawn)
                {
                    //spawn enemy
                    var enemy = Globals.entityFactory.CreateEnemy(
                        new Transform() { gridPos = transform.gridPos, worldPos = transform.worldPos, scale = transform.scale },
                        enemyType);

                    // add enemy to the approriate lane
                    scene.EnemyManager.AddEnemyToLane(transform.gridPos.y);

                    // if not end of list - move to the next item
                    if (currentIndex < scene.GetLevelInfo().Count - 1)
                    {
                        currentIndex++;
                    }
                    // else stop spawning
                    else if (currentIndex == scene.GetLevelInfo().Count - 1) endOfFile = true;
                }
            }
        }

        private void Restart()
        {
            currentIndex = 0;
            currentTimer = 0;
            endOfFile = false;

            //reset currentTimer
            //foreach (var entity in ActiveEntities)
            //{
            //    GetEntity(entity).Get<EnemySpawner>().currentTimer = 0;
            //}
        }
    }
}
