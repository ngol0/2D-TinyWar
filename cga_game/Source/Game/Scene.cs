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
        Entity[,] gridItemUIArray;

        int gridWidth = 20;
        int gridHeight = 18;
        int cellSize = 40;


        public Scene()
        {
            levelGrid = new GridSystem(gridWidth, gridHeight, cellSize);
        }

        public void InitLevelGrid()
        {
            levelGrid = new GridSystem(gridWidth, gridHeight, cellSize);
            //init grid item ui
            gridItemUIArray = new Entity[gridWidth, gridHeight];
        }

        public void InitTroop()
        {
            //init infantry
            GridPosition infantryPos = new GridPosition(10, 10);
            Globals.entityFactory.CreateInfantryUnit(new Transform() { gridPos = infantryPos, worldPos = GetWorldPosition(infantryPos), scale = 40 });

            //set grid item data to be not availabe
            levelGrid.GetGridItem(infantryPos).SetAvailable(false);

            GridPosition infantryPos2 = new GridPosition(0, 0);
            Globals.entityFactory.CreateInfantryUnit(new Transform() { gridPos = infantryPos2, worldPos = GetWorldPosition(infantryPos2), scale = 40 });

            //set grid item data to be not availabe
            levelGrid.GetGridItem(infantryPos2).SetAvailable(false);
        }

        public void InitGridItemUI()
        {
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    GridPosition position = new GridPosition(x, y);
                    Vector2 worldPos = levelGrid.GetWorldPosition(position);

                    gridItemUIArray[x, y] = Globals.entityFactory.CreateGridItemUI(new Transform() { gridPos = position, worldPos = worldPos, scale = cellSize });

                    //init grid data
                    gridItemUIArray[x, y].Get<GridItemUI>().gridData = levelGrid.GetGridItem(position);
                }
            }
        }

        public GridPosition GetGridPosition(Vector2 worldPos) => levelGrid.GetGridPosition(worldPos);
        public Vector2 GetWorldPosition(GridPosition position) => levelGrid.GetWorldPosition(position);
        public GridItem GetGridItem(GridPosition position) => levelGrid.GetGridItem(position);
    }
}
