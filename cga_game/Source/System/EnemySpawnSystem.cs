using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended;
using System.Collections.Generic;

namespace Strategy
{
    class EnemySpawnSystem : EntityProcessingSystem
    {
        Scene scene;

        private ComponentMapper<EnemySpawner> enemySpawnerMapper;
        private ComponentMapper<Transform> transformMapper;
        private List<int> enemies = new List<int>();

        float startingTimer = 0.0f;

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
            startingTimer += gameTime.GetElapsedSeconds();

            if (enemySpawner.currentTimer > enemySpawner.spawnMaxTime && enemies.Count <= EnemyManager.NUMBER_OF_ENEMIES && startingTimer >= 5.0f)
            {
                //get a random enemy from enemy data list
                int randomIndex = RandomUtils.Rand(0, EnemyTypeList.enemyTypeList.Count);
                var enemyType = EnemyTypeList.enemyTypeList[randomIndex];

                //spawn enemy
                var enemy = Globals.entityFactory.CreateEnemy(
                    new Transform() { gridPos = transform.gridPos, worldPos = transform.worldPos, scale = transform.scale }, 
                    enemyType);

                enemies.Add(enemy.Id);

                scene.EnemyManager.AddEnemyToLane(transform.gridPos.y);

                enemySpawner.currentTimer = 0;
                enemySpawner.spawnMaxTime = RandomUtils.Rand(10, 20);
            }
        }

        public void Restart()
        { 
            foreach (var enemy in enemies) 
            {
                DestroyEntity(enemy);
            }
            enemies.Clear();
            startingTimer = 0.0f;
        }
    }
}
