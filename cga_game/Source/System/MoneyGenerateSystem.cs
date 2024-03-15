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
        private ComponentMapper<MoneyGenerator> moneyGenerator;

        public MoneyGenerateSystem(Scene scene) : base(Aspect.All(typeof(MoneyGenerator)))
        {
            this.scene = scene;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            moneyGenerator = mapperService.GetMapper<MoneyGenerator>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entityId in ActiveEntities) 
            {
                var moneyGen = moneyGenerator.Get(entityId);

                if (moneyGen != null) 
                {
                    moneyGen.currentTimer += gameTime.GetElapsedSeconds();

                    if (moneyGen.currentTimer > moneyGen.maxTimer)
                    {
                        scene.AddMoney(moneyGen.amount);
                        moneyGen.currentTimer = 0;
                    }
                }
            }
        }
    }
}
