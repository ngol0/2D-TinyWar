using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Strategy.Grid;
using System.Collections.Generic;

namespace Strategy
{
    class WaveManager
    {
        Scene scene;
        private int[] laneArray;

        public WaveManager(Scene scene)
        {
            this.scene = scene;
        }

        public void InitEnemySpawner()
        {
            laneArray = new int[scene.GridHeight];
            for (int i = 0; i < scene.GridHeight; i++)
            {
                GridPosition pos = new GridPosition(scene.GridWidth, i);

                Globals.entityFactory.CreateEnemySpawner(
                    new Transform() { gridPos = pos, worldPos = scene.GetWorldPosition(pos), scale = 40 });

                laneArray[i] = 0;
            }
        }

        public void AddEnemyToLane(int laneRow)
        {
            laneArray[laneRow] += 1;
        }

        public void RemoveEnemyFromLane(int laneRow) 
        {
            laneArray[laneRow] -= 1;
        }

        public bool LaneHasEnemy(int laneRow)
        {
            return laneArray[laneRow] > 0;
        }
    }
}
