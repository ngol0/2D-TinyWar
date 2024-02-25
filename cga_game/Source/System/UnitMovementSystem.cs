using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Microsoft.Xna.Framework;
using Strategy.Grid;
using System.Collections.Generic;

namespace Strategy
{
    class UnitMovementSystem : EntityProcessingSystem
    {
        private ComponentMapper<UnitMovement> unitMovementMapper;
        private ComponentMapper<BoxCollider2D> boxColliderMapper;
        private ComponentMapper<Transform> transformMapper;

        Scene scene;

        public UnitMovementSystem(Scene scene) : base(Aspect.All(typeof(BoxCollider2D), typeof(UnitMovement)))
        {
            this.scene = scene;
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            boxColliderMapper = mapperService.GetMapper<BoxCollider2D>();
            unitMovementMapper = mapperService.GetMapper<UnitMovement>();
            transformMapper = mapperService.GetMapper<Transform>();
        }

        private bool IsValidMovementPos(UnitMovement unitMovement, GridPosition gridPos)
        {
            return unitMovement.validGridPosList.Contains(scene.GetGridItem(gridPos));
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var unitMovement = unitMovementMapper.Get(entityId);
            var collider = boxColliderMapper.Get(entityId);
            var transform = transformMapper.Get(entityId);

            Vector2 mousePosition = new Vector2(Globals.input.currentMouseState.X, Globals.input.currentMouseState.Y);
            GridPosition gridPos = scene.LevelGrid.GetGridPosition(mousePosition);

            if (Globals.input.GetIsMouseButtonDown(Input.MouseButton.Left, true) && unitMovement.isSelected)
            {
                if (!scene.IsValidPosGrid(gridPos)) return;
                if (IsValidMovementPos(unitMovement, gridPos) && unitMovement.turn >= 1)
                {
                    //move unit
                    GridPosition oldGrid = unitMovement.currentGridPosition;

                    transform.gridPos = gridPos;
                    transform.worldPos = scene.GetWorldPosition(gridPos);

                    //remove previous grid data 
                    scene.GetGridItem(oldGrid).SetWalkable(true);

                    //update unit's current grid data
                    unitMovement.currentGridPosition = transform.gridPos;
                    scene.GetGridItem(gridPos).SetWalkable(false);

                    unitMovement.turn -= 1;

                    //reset and update grid visual
                    UpdateGridVisual(unitMovement, oldGrid, gridPos);

                    //update collider position
                    collider.boundingBox = new Rectangle((int)transform.worldPos.X, (int)transform.worldPos.Y, transform.scale, transform.scale);
                }
            }
        }

        private void UpdateGridVisual(UnitMovement unitMovement, GridPosition oldGrid, GridPosition newGrid)
        {
            foreach (var grid in unitMovement.validGridPosList)
            {
                grid.color = Color.White;
            }
            unitMovement.validGridPosList.Clear();

            scene.GetGridItem(oldGrid).color = Color.White;
            scene.GetGridItem(newGrid).color = Color.Green;
        }
    }
}
