using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using Strategy.Grid;
using System;

namespace Strategy
{
    class EntityFactory
    {
        World world;

        public EntityFactory(World world)
        {
            this.world = world;
        }

        public Entity CreateGridItemUI(Transform transform)
        {
            var entity = world.CreateEntity();
            entity.Attach(transform);
            entity.Attach(new Sprite() { texture = SpriteLoader.groundTexture });
            entity.Attach(new BoxCollider2D() { boundingBox = new Rectangle((int)transform.worldPos.X, (int)transform.worldPos.Y, transform.scale, transform.scale) });
            entity.Attach(new GridItem(transform.gridPos));
            
            return entity;
        }

        public Entity CreateInfantryUnit(Transform transform)
        {
            var entity = world.CreateEntity();
            entity.Attach(transform);
            entity.Attach(new Sprite() { texture = SpriteLoader.infantryTexture });
            entity.Attach(new BoxCollider2D() { boundingBox = new Rectangle((int)transform.worldPos.X, (int)transform.worldPos.Y, transform.scale, transform.scale) });
            entity.Attach(new UnitMovement(transform.gridPos, 2, 1));

            return entity;
        }
    }
}
