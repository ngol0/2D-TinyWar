using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Strategy.Grid;
using System.Collections.Generic;

namespace Strategy
{
    class UnitSpawnSystem : EntityProcessingSystem
    {
        Scene scene;
        float timer;

        List<int> units = new List<int>();

        public UnitSpawnSystem(Scene scene) : base(Aspect.All(typeof(GridItem)))
        {
            this.scene = scene;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            scene.OnRestart += Restart;
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            if (timer > 10)
            {
                if (Globals.input.GetIsMouseButtonDown(Input.MouseButton.Left, true))
                {
                    if (scene.currentSelectedUnitButton == null) return;
                    if (scene.CurrentMoneyAmount < scene.CurrentSelectedUnitType.cost) return;

                    Vector2 mousePosition = new Vector2(Globals.input.currentMouseState.X, Globals.input.currentMouseState.Y);
                    GridPosition gridPos = scene.GetGridPosition(mousePosition);

                    if (scene.IsValidPosGrid(gridPos) && scene.GetGridItem(gridPos).IsWalkable)
                    {
                        var unitId = scene.InitCurrentSelectedUnit(gridPos);
                        scene.GetGridItem(gridPos).SetPlaceable(false);
                        scene.SpendMoney();

                        units.Add(unitId);
                    }

                    timer = 0;
                }
            }

            if (Globals.input.GetIsMouseButtonUp(Input.MouseButton.Left, true))
            {
                timer += gameTime.GetElapsedSeconds();
            }
        }

        public void Restart()
        {
            foreach (var unit in units)
            {
                var unitPos = GetEntity(unit).Get<Transform>();
                scene.GetGridItem(unitPos.gridPos).SetPlaceable(true);
                DestroyEntity(unit);
            }
            units.Clear();
        }
    }
}
