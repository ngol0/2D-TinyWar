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

        private ComponentMapper<EnemySpawner> enemySpawnerMapper;
        private ComponentMapper<Transform> transformMapper;
        int count = 0;
        int currentIndex = 0;

        public EnemySpawnSystem(Scene scene) : base(Aspect.All(typeof(EnemySpawner), typeof(Transform)))
        {
            this.scene = scene;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            enemySpawnerMapper = mapperService.GetMapper<EnemySpawner>();
            transformMapper = mapperService.GetMapper<Transform>();

            scene.OnRestart += Restart;
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var enemySpawner = enemySpawnerMapper.Get(entityId);
            var transform = transformMapper.Get(entityId);

            enemySpawner.currentTimer += gameTime.GetElapsedSeconds();

            //if (enemySpawner.currentTimer > enemySpawner.spawnMaxTime && count <= EnemyManager.NUMBER_OF_ENEMIES && startingTimer >= 5.0f)
            //{
            //    //get a random enemy from enemy data list
            //    int randomIndex = RandomUtils.Rand(0, EnemyTypeList.enemyTypeList.Count);
            //    var enemyType = EnemyTypeList.enemyTypeList[randomIndex];

            //    //spawn enemy
            //    var enemy = Globals.entityFactory.CreateEnemy(
            //        new Transform() { gridPos = transform.gridPos, worldPos = transform.worldPos, scale = transform.scale },
            //        enemyType);

            //    scene.EnemyManager.AddEnemyToLane(transform.gridPos.y);

            //    enemySpawner.currentTimer = 0;
            //    enemySpawner.spawnMaxTime = RandomUtils.Rand(10, 20);
            //}


            if (scene.GetLevelInfo()[currentIndex].laneNumber == transform.gridPos.y)
            {
                int randomIndex = RandomUtils.Rand(0, EnemyTypeList.enemyTypeList.Count);
                var enemyType = EnemyTypeList.enemyTypeList[randomIndex];
                if (enemySpawner.currentTimer >= scene.GetLevelInfo()[currentIndex].timeToSpawn)
                {
                    //Debug.WriteLine(scene.GetLevelInfo()[currentIndex].laneNumber);
                    //Debug.WriteLine(scene.GetLevelInfo()[currentIndex].timeToSpawn);
                    //spawn enemy
                    var enemy = Globals.entityFactory.CreateEnemy(
                        new Transform() { gridPos = transform.gridPos, worldPos = transform.worldPos, scale = transform.scale },
                        enemyType);

                    scene.EnemyManager.AddEnemyToLane(transform.gridPos.y);
                    enemySpawner.currentTimer = 0;

                    if (currentIndex < scene.GetLevelInfo().Count - 1)
                    {
                        currentIndex++;
                    }
                }
            }
        }

        private void Restart()
        {
            count = 0;
            currentIndex = 0;

            //reset currentTimer
            foreach (var entity in ActiveEntities)
            {
                GetEntity(entity).Get<EnemySpawner>().currentTimer = 0;
            }
        }
    }
}
