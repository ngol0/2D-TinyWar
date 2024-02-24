using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Strategy.Input;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Microsoft.Xna.Framework;
using Strategy.Grid;

namespace Strategy
{
    class UnitSelectionSystem : EntityProcessingSystem
    {
        private ComponentMapper<Sprite> spriteMapper;
        private ComponentMapper<BoxCollider2D> boxColliderMapper;

        GridItem currentGridItem;
        Scene scene;

        public UnitSelectionSystem(Scene scene) : base(Aspect.All(typeof(Transform), typeof(UnitMovement)))
        {
            this.scene = scene;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            boxColliderMapper = mapperService.GetMapper<BoxCollider2D>();
            spriteMapper = mapperService.GetMapper<Sprite>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var sprite = spriteMapper.Get(entityId);
            var collider = boxColliderMapper.Get(entityId);

            Vector2 mousePosition = new Vector2(Globals.input.currentMouseState.X, Globals.input.currentMouseState.Y);
            GridPosition gridPos = scene.LevelGrid.GetGridPosition(mousePosition);

            //mouse intersect with sprite
            if (Globals.input.GetMouseBounds(true).Intersects(collider.boundingBox))
            {
                if (Globals.input.GetIsMouseButtonDown(Input.MouseButton.Left, true))
                {
                    if (currentGridItem != null)
                    {
                        currentGridItem.SetSelected(false);
                    }
                    currentGridItem = scene.GetGridItem(gridPos);
                    currentGridItem.SetSelected(true);
                }
            }
            if (Globals.input.GetIsMouseButtonDown(Input.MouseButton.Right, true))
            {
                if (currentGridItem != null) { currentGridItem.SetSelected(false); }
                currentGridItem = null;
            }
        }
    }
}
