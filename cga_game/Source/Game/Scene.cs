using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using SharpDX.Direct3D9;
using Strategy.Grid;

namespace Strategy
{
    class Scene
    {
        GridSystem levelGrid;
        public GridSystem LevelGrid => levelGrid;

        int gridWidth = 20;
        int gridHeight = 18;
        int cellSize = 40;

        public Scene() { }

        public void Init()
        {
            InitLevelGrid();
            InitTroop();
        }

        private void InitLevelGrid()
        {
            levelGrid = new GridSystem(gridWidth, gridHeight, cellSize);
        }

        private void InitTroop()
        {
            Vector2 infantryOffset = new Vector2(2, 2);
            //init infantry
            GridPosition infantryPos = new GridPosition(1, 1);
            Globals.entityFactory.CreateInfantryUnit(new Transform() { gridPos = infantryPos, worldPos = GetWorldPosition(infantryPos), scale = 35 });

            //set grid item data to be not availabe
            levelGrid.GetGridItem(infantryPos).SetWalkable(false);

            GridPosition infantryPos2 = new GridPosition(0, 0);
            Globals.entityFactory.CreateInfantryUnit(new Transform() { gridPos = infantryPos2, worldPos = GetWorldPosition(infantryPos2), scale = 35 });

            //set grid item data to be not availabe
            levelGrid.GetGridItem(infantryPos2).SetWalkable(false);
        }

        public GridPosition GetGridPosition(Vector2 worldPos) => levelGrid.GetGridPosition(worldPos);
        public Vector2 GetWorldPosition(GridPosition position) => levelGrid.GetWorldPosition(position);
        public GridItem GetGridItem(GridPosition position) => levelGrid.GetGridItem(position);
        public bool IsValidPosGrid(GridPosition position) => levelGrid.IsValidGridPos(position);
        public bool IsGridWalkable(GridPosition position) => levelGrid.GetGridItem(position).IsWalkable;
    }
}
