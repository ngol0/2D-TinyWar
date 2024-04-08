using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Strategy.Grid;

namespace Strategy
{
    class UnitSpawnSystem : EntityProcessingSystem
    {
        Scene scene;
        float timer;

        public UnitSpawnSystem(Scene scene) : base(Aspect.All(typeof(GridItem)))
        {
            this.scene = scene;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {

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
                        scene.InitCurrentSelectedUnit(gridPos);
                        scene.GetGridItem(gridPos).SetPlaceable(false);
                        scene.SpendMoney();
                    }

                    timer = 0;
                }
            }

            if (Globals.input.GetIsMouseButtonUp(Input.MouseButton.Left, true))
            {
                timer += gameTime.GetElapsedSeconds();
            }
        }
    }
}
