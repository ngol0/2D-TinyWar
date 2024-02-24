using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Strategy.Grid;

namespace Strategy
{
    class GridDebugSystem : EntityProcessingSystem
    {
        //private ComponentMapper<BoxCollider2D> boxColliderMapper;
        private ComponentMapper<GridItemUI> gridItemUIMapper;

        public GridDebugSystem() : base(Aspect.All(typeof(Sprite), typeof(BoxCollider2D), typeof(GridItemUI)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            //boxColliderMapper = mapperService.GetMapper<BoxCollider2D>();
            gridItemUIMapper = mapperService.GetMapper<GridItemUI>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var grid = gridItemUIMapper.Get(entityId);

            grid.color = grid.gridData.IsSelected() ? Color.Green : Color.White;
        }
    }
}
