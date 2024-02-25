using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Strategy.Grid
{
    class GridItem
    {
        private GridPosition gridPos;

        private bool isWalkable = true;

        //UI
        public Color color = Color.White;

        //setter
        public void SetWalkable(bool available) 
        { 
            isWalkable = available; 
        }

        //public getters
        public GridPosition GetGridPosition() { return gridPos; }
        public bool IsWalkable => isWalkable;

        public GridItem(GridPosition pos)
        {
            gridPos = pos;
        }
    }
}
