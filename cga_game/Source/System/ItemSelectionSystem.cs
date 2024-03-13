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

            //mouse intersect with sprite
            if (Globals.input.GetMouseBounds(true).Intersects(collider.boundingBox))
            {
                if (Globals.input.GetIsMouseButtonDown(Input.MouseButton.Left, true))
                {
                    //get unit movement grid pos
                    if (scene.currentSelectedUnitButton != null) 
                    {
                        foreach (var s in spriteMapper.Components)
                        {
                            s.color = Color.White;
                        }
                        scene.currentSelectedUnitButton.isSelected = false;
                    }

                    scene.currentSelectedUnitButton = unitButton;
                    scene.currentSelectedUnitButton.isSelected = true;
                    sprite.color = Color.MediumSeaGreen;
                }
                //if hover...
                else
                {
                    
                }
            }
            //deselect
            if (Globals.input.GetIsMouseButtonDown(Input.MouseButton.Right, true))
            {
                sprite.color = Color.White;
                if (scene.currentSelectedUnitButton != null) 
                {
                    scene.currentSelectedUnitButton.isSelected = false;
                    scene.currentSelectedUnitButton = null;
                }
            }
        }
    }
}
