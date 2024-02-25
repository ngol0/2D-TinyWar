using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Strategy.Input;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Microsoft.Xna.Framework;
using Strategy.Grid;
using System.Collections.Generic;

namespace Strategy
{
    class UnitSelectionSystem : EntityProcessingSystem
    {
        private ComponentMapper<UnitMovement> unitMovementMapper;
        private ComponentMapper<BoxCollider2D> boxColliderMapper;

        UnitMovement currentUnitMovement;
        Scene scene;

        public UnitSelectionSystem(Scene scene) : base(Aspect.All(typeof(BoxCollider2D), typeof(UnitMovement)))
        {
            this.scene = scene;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            boxColliderMapper = mapperService.GetMapper<BoxCollider2D>();
            unitMovementMapper = mapperService.GetMapper<UnitMovement>();
        }

        private void GetValidGridPosition(GridPosition unitGridPos)
        {
            int maxMovementDistX = currentUnitMovement.maxMoveDistanceX;
            int maxMovementDistY = currentUnitMovement.maxMoveDistanceY;

            for (int x = -maxMovementDistX; x <= maxMovementDistX; x++)
            {
                for (int y = -maxMovementDistY; y <= maxMovementDistY; y++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, y);
                    GridPosition testGridPosition = unitGridPos + offsetGridPosition;

                    if (!scene.IsValidPosGrid(testGridPosition)) continue;
                    if (testGridPosition == unitGridPos) continue;
                    if (!scene.IsGridWalkable(testGridPosition)) continue;

                    GridItem gridItem = scene.GetGridItem(testGridPosition);
                    currentUnitMovement.validGridPosList.Add(gridItem);
                    gridItem.color = Color.Orange;
                }
            }
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var unitMovement = unitMovementMapper.Get(entityId);
            var collider = boxColliderMapper.Get(entityId);

            Vector2 mousePosition = new Vector2(Globals.input.currentMouseState.X, Globals.input.currentMouseState.Y);
            GridPosition gridPos = scene.LevelGrid.GetGridPosition(mousePosition);

            //mouse intersect with sprite
            if (Globals.input.GetMouseBounds(true).Intersects(collider.boundingBox))
            {
                if (Globals.input.GetIsMouseButtonDown(Input.MouseButton.Left, true))
                {
                    //get unit movement grid pos
                    if (currentUnitMovement != null) { ResetMovementGrid(); }

                    currentUnitMovement = unitMovement;
                    currentUnitMovement.isSelected = true;
                    scene.GetGridItem(currentUnitMovement.currentGridPosition).color = Color.Green;

                    if (currentUnitMovement.turn <= 0) return;

                    GetValidGridPosition(gridPos);
                }
                //if hover...
                else
                {
                    //todo: if hover > show unit info
                }
            }
            //deselect
            if (Globals.input.GetIsMouseButtonDown(Input.MouseButton.Right, true))
            {
                if (currentUnitMovement != null) { ResetMovementGrid(); }
            }
        }

        private void ResetMovementGrid()
        {
            scene.GetGridItem(currentUnitMovement.currentGridPosition).color = Color.White;
            currentUnitMovement.isSelected = false;

            foreach (var grid in currentUnitMovement.validGridPosList)
            {
                grid.color = Color.White;
            }
            currentUnitMovement.validGridPosList.Clear();
        }
    }
}
