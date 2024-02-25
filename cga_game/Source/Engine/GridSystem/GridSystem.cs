using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;

namespace Strategy.Grid
{
    class GridSystem
    {
        private int width;
        private int height;
        private int cellSize;

        public int CellSize() { return cellSize; }
        private Entity[,] gridItemArray;

        public GridSystem(int width, int height, int cellSize)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            gridItemArray = new Entity[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    GridPosition gridPosition = new GridPosition(x, y);
                    Vector2 worldPos = GetWorldPosition(gridPosition);

                    gridItemArray[x, y] = Globals.entityFactory.CreateGridItemUI(new Transform() { gridPos = gridPosition, worldPos = worldPos, scale = cellSize });
                }
            }
        }

        public GridPosition GetGridPosition(Vector2 worldPos)
        {
            return new GridPosition(
                (int)Math.Round(worldPos.X / cellSize, 1),
                (int)Math.Round(worldPos.Y / cellSize, 1));
        }

        public Vector2 GetWorldPosition(GridPosition gridPos)
        {
            return new Vector2(gridPos.x, gridPos.y) * cellSize;
        }

        public GridItem GetGridItem(GridPosition gridPos)
        {
            return gridItemArray[gridPos.x, gridPos.y].Get<GridItem>();
        }

        public bool IsValidGridPos(GridPosition gridPos)
        {
            return 0 <= gridPos.x
                && gridPos.x <= width - 1
                && 0 <= gridPos.y
                && gridPos.y <= height - 1;
        }
    }
}
