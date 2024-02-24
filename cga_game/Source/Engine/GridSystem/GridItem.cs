using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Strategy.Grid
{
    class GridItem
    {
        private GridSystem gridSystem;
        private GridPosition gridPos;

        private bool isAvailable = true;
        private bool isSelected = false;

        //event


        //setter
        public void SetAvailable(bool available) 
        { 
            isAvailable = available; 
        }

        public void SetSelected(bool selected)
        {
            isSelected = selected;
        }

        //public getters
        public GridPosition GetGridPosition() { return gridPos; }
        public GridSystem GetGridSystem() { return gridSystem; }
        public bool IsAvailable() { return isAvailable; }
        public bool IsSelected() { return isSelected; }

        public GridItem(GridSystem system, GridPosition pos)
        {
            gridSystem = system;
            gridPos = pos;
        }
    }
}
