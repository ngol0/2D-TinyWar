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
    class MoneyHUDSystem : IDrawSystem
    {
        SpriteBatch spriteBatch;
        Scene scene;
        SpriteFont spriteFont;

        public MoneyHUDSystem(SpriteBatch spriteBatch, SpriteFont font, Scene scene)
        {
            this.spriteBatch = spriteBatch;
            this.scene = scene;
            spriteFont = font;
        }

        public void Initialize(World world) { }

        public void Dispose() { }

        public void Draw(GameTime gameTime)
        {
            string text = "Money: " + scene.CurrentMoneyAmount.ToString();

            // Draw the string twice to create a drop shadow, first colored black
            // and offset one pixel to the bottom right, then again in white at the
            // intended position. This makes text easier to read over the background.
            spriteBatch.DrawString(spriteFont, text, new Vector2(355, 520), Color.Black);
            spriteBatch.DrawString(spriteFont, text, new Vector2(354, 520), Color.White);

            string infantryMoney = UnitData.infantry.cost.ToString();
            //spriteBatch.DrawString(spriteFont, infantryMoney, new Vector2(55, 560), Color.Black);
            spriteBatch.DrawString(spriteFont, infantryMoney, new Vector2(55, 560), Color.Black, 0.0f, new Vector2(0,0), 0.5f, SpriteEffects.None, 0.0f);
        } 
    }
}
