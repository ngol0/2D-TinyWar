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
    class GameHUDSystem : IDrawSystem
    {
        SpriteBatch spriteBatch;
        Scene scene;
        SpriteFont spriteFont;

        Vector2 offSet = new Vector2(20, 40);

        public GameHUDSystem(SpriteBatch spriteBatch, SpriteFont font, Scene scene)
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
            string score = "Score: " + scene.Score.ToString();

            // Draw the string twice to create a drop shadow, first colored black
            // and offset one pixel to the bottom right, then again in white at the
            // intended position. This makes text easier to read over the background.
            spriteBatch.DrawString(spriteFont, text, new Vector2(355, 520), Color.Black);
            spriteBatch.DrawString(spriteFont, text, new Vector2(354, 520), Color.White);

            string firstItemMoney = scene.UnitList[0].cost.ToString();
            spriteBatch.DrawString(
                spriteFont, firstItemMoney, scene.StartingButtonPos + offSet + new Vector2(0,0), Color.Black, 0.0f, new Vector2(0,0), 0.5f, SpriteEffects.None, 0.0f);

            string secondItemMoney = scene.UnitList[1].cost.ToString();
            spriteBatch.DrawString(
                spriteFont, secondItemMoney, scene.StartingButtonPos + offSet + new Vector2(70, 0), Color.Black, 0.0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0.0f);

            string thirdItemMoney = scene.UnitList[2].cost.ToString();
            spriteBatch.DrawString(
                spriteFont, thirdItemMoney, scene.StartingButtonPos + offSet + new Vector2(70*2, 0), Color.Black, 0.0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0.0f);

            string fourthItemMoney = scene.UnitList[3].cost.ToString();
            spriteBatch.DrawString(
                spriteFont, fourthItemMoney, scene.StartingButtonPos + offSet + new Vector2(70*3, 0), Color.Black, 0.0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0.0f);

            spriteBatch.DrawString(spriteFont, score, new Vector2(355, 580), Color.Black);
            spriteBatch.DrawString(spriteFont, score, new Vector2(354, 580), Color.White);
        } 
    }
}
