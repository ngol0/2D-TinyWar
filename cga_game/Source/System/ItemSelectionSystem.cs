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
    class ItemSelectionSystem : EntityProcessingSystem
    {
        private ComponentMapper<UnitButton> unitButtonMapper;
        private ComponentMapper<BoxCollider2D> boxColliderMapper;
        private ComponentMapper<Sprite> spriteMapper;

        Scene scene;

        public ItemSelectionSystem(Scene scene) : base(Aspect.All(typeof(BoxCollider2D), typeof(UnitButton), typeof(Sprite)))
        {
            this.scene = scene;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            boxColliderMapper = mapperService.GetMapper<BoxCollider2D>();
            unitButtonMapper = mapperService.GetMapper<UnitButton>();
            spriteMapper = mapperService.GetMapper<Sprite>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var unitButton = unitButtonMapper.Get(entityId);
            var collider = boxColliderMapper.Get(entityId);
            var sprite = spriteMapper.Get(entityId);

            //grey the button out if cost is higher than the current money
            if (scene.CurrentMoneyAmount < unitButton.unitType.cost)
            {
                sprite.color = Color.Gray;
                unitButton.isSelected = false;

                if (unitButton == scene.currentSelectedUnitButton)
                {
                    scene.currentSelectedUnitButton = null;
                }
                return;
            }

            //on mouse click
            if (Globals.input.GetMouseBounds(true).Intersects(collider.boundingBox))
            {
                if (Globals.input.GetIsMouseButtonDown(Input.MouseButton.Left, true))
                {
                    foreach (var s in unitButtonMapper.Components)
                    {
                        if (s!=null) s.isSelected = false;
                    }

                    scene.currentSelectedUnitButton = unitButton;
                    scene.currentSelectedUnitButton.isSelected = true;

                }
                //if hover...
                else
                {

                }
            }
            //deselect
            if (Globals.input.GetIsMouseButtonDown(Input.MouseButton.Right, true))
            {
                if (scene.currentSelectedUnitButton != null)
                {
                    scene.currentSelectedUnitButton.isSelected = false;
                    scene.currentSelectedUnitButton = null;
                }
            }

            if (unitButton.isSelected) { sprite.color = Color.MediumSeaGreen; }
            else { sprite.color = Color.White; }
        }
    }
}
