using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended;

namespace Strategy
{
    class MoneyGenerateSystem : EntityUpdateSystem
    {
        Scene scene;

        public MoneyGenerateSystem(Scene scene) : base(Aspect.All(typeof(MoneyGenerator)))
        {
            this.scene = scene;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entityId in ActiveEntities)
            {
                var entity = GetEntity(entityId);
                var money = entity.Get<MoneyGenerator>();

                money.currentTimer += gameTime.GetElapsedSeconds();

                if (money.currentTimer > money.maxTimer && money != null)
                {
                    scene.AddMoney(money.amount);
                    money.currentTimer = 0;
                }
            }
        }
    }
}
