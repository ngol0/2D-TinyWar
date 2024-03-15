using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using Strategy.Grid;
using System;
using System.Threading.Tasks;

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

        public Entity CreateUnit(Transform transform, UnitType type)
        {
            var unit = world.CreateEntity();
            unit.Attach(transform);
            unit.Attach(new BoxCollider2D() { boundingBox = new Rectangle((int)transform.worldPos.X, (int)transform.worldPos.Y, transform.scale, transform.scale) });
            unit.Attach(new UnitComponent() { unitType = type });

            if (type.name == UnitTypeString.INFANTRY)
            {
                unit.Attach(new Sprite() { texture = SpriteLoader.infantryTexture });
            }
            else if (type.name == UnitTypeString.TANK)
            {
                unit.Attach(new Sprite() { texture = SpriteLoader.tankTexture });
            }
            else if (type.name == UnitTypeString.PLANE)
            {
                unit.Attach(new Sprite() { texture = SpriteLoader.planeTexture });
            }
            else if (type.name == UnitTypeString.RESOURCE)
            {
                unit.Attach(new Sprite() { texture = SpriteLoader.moneyTowerTexture });
                unit.Attach(new MoneyGenerator());
            }

            return unit;
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

        public Entity CreateEnemySpawner(Transform transform)
        {
            var enemySpawner = world.CreateEntity();
            enemySpawner.Attach(transform);
            enemySpawner.Attach(new EnemySpawner());

            return enemySpawner;
        }

        public Entity CreateEnemy(Transform transform, EnemyType type)
        {
            var enemy = world.CreateEntity();
            enemy.Attach(transform);
            enemy.Attach(new BoxCollider2D() { boundingBox = new Rectangle((int)transform.worldPos.X, (int)transform.worldPos.Y, transform.scale, transform.scale) });

            //texture
            if (type.name == EnemyTypeString.ALIEN)
            {
                enemy.Attach(new Sprite() { texture = SpriteLoader.alienTexture });
            }
            else if (type.name == EnemyTypeString.ROBOT)
            {
                enemy.Attach(new Sprite() { texture = SpriteLoader.robotTexture });
            }
            else if (type.name == EnemyTypeString.BAT)
            {
                enemy.Attach(new Sprite() { texture = SpriteLoader.batTexture });
            }
            else if (type.name == EnemyTypeString.BOMB)
            {
                enemy.Attach(new Sprite() { texture = SpriteLoader.bombTexture });
            }

            //enemy component
            enemy.Attach(new EnemyComponent() { enemyType = type });

            return enemy;
        }

        public Entity CreateBullet(Transform transform)
        {
            var bullet = world.CreateEntity();
            bullet.Attach(transform);
            bullet.Attach(new BoxCollider2D() { boundingBox = new Rectangle((int)transform.worldPos.X, (int)transform.worldPos.Y, transform.scale, transform.scale) });
            bullet.Attach(new Bullet());

            return bullet;
        }
    }
}
