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
            var gridItem = world.CreateEntity();
            gridItem.Attach(transform);
            gridItem.Attach(new Sprite() { texture = SpriteLoader.groundTexture });
            gridItem.Attach(new BoxCollider2D() { boundingBox = new Rectangle((int)transform.worldPos.X, (int)transform.worldPos.Y, transform.scale, transform.scale) });
            gridItem.Attach(new GridItem(transform.gridPos));
            
            return gridItem;
        }

        public Entity CreateInfantryUnit(Transform transform)
        {
            var infantry = world.CreateEntity();
            infantry.Attach(transform);
            infantry.Attach(new Sprite() { texture = SpriteLoader.infantryTexture });
            infantry.Attach(new BoxCollider2D() { boundingBox = new Rectangle((int)transform.worldPos.X, (int)transform.worldPos.Y, transform.scale, transform.scale) });

            return infantry;
        }

        public Entity CreateTank(Transform transform)
        {
            var tank = world.CreateEntity();
            tank.Attach(transform);
            tank.Attach(new Sprite() { texture = SpriteLoader.tankTexture });
            tank.Attach(new BoxCollider2D() { boundingBox = new Rectangle((int)transform.worldPos.X, (int)transform.worldPos.Y, transform.scale, transform.scale) });

            return tank;
        }

        public Entity CreatePlane(Transform transform)
        {
            var plane = world.CreateEntity();
            plane.Attach(transform);
            plane.Attach(new Sprite() { texture = SpriteLoader.planeTexture });
            plane.Attach(new BoxCollider2D() { boundingBox = new Rectangle((int)transform.worldPos.X, (int)transform.worldPos.Y, transform.scale, transform.scale) });

            return plane;
        }

        public Entity CreateMoneyTower(Transform transform)
        {
            var moneyTower = world.CreateEntity();
            moneyTower.Attach(transform);
            moneyTower.Attach(new Sprite() { texture = SpriteLoader.moneyTowerTexture });
            moneyTower.Attach(new BoxCollider2D() { boundingBox = new Rectangle((int)transform.worldPos.X, (int)transform.worldPos.Y, transform.scale, transform.scale) });
            moneyTower.Attach(new MoneyGenerator());

            return moneyTower;
        }

        public Entity CreateButton(Transform transform, string type)
        {
            var button = world.CreateEntity();
            button.Attach(transform);
            button.Attach(new BoxCollider2D() { boundingBox = new Rectangle((int)transform.worldPos.X, (int)transform.worldPos.Y, transform.scale, transform.scale) });
            button.Attach(new UnitButton());

            if (type == UnitTypeString.INFANTRY) 
            {
                button.Attach(new Sprite() { texture = SpriteLoader.infantryBtn });
            }
            else if (type == UnitTypeString.TANK)
            {
                button.Attach(new Sprite() { texture = SpriteLoader.tankBtn });
            }
            else if (type == UnitTypeString.PLANE)
            {
                button.Attach(new Sprite() { texture = SpriteLoader.planeBtn });
            }
            else if (type == UnitTypeString.RESOURCE)
            {
                button.Attach(new Sprite() { texture = SpriteLoader.moneyTowerBtn });
            }

            return button;
        }
    }
}
