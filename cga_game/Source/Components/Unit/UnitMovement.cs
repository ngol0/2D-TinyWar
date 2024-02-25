using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Strategy.Grid;

namespace Strategy
{
    class UnitMovement
    {
        public int turn = 1;
        public GridPosition currentGridPosition;
        public bool isSelected;

        public List<GridItem> validGridPosList = new List<GridItem>();
        public int maxMoveDistanceX;
        public int maxMoveDistanceY;

        public UnitMovement(GridPosition pos, int maxMoveDistance, int maxMoveDistanceY)
        {
            this.currentGridPosition = pos;
            this.maxMoveDistanceX = maxMoveDistance;
            this.maxMoveDistanceY = maxMoveDistanceY;
        }
    }
}
