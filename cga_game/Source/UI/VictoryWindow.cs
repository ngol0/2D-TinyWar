using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Strategy.UI
{
    class VictoryWindow : Window
    {
        public override void OnEnter()
        {

        }

        public override void Render(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            string announcement = "You Won!";
            string score = "Score: " + Globals.windowManager.GetScore();

            spriteBatch.DrawString(spriteFont, announcement, new Vector2(155, 100), Color.Black);
            spriteBatch.DrawString(spriteFont, announcement, new Vector2(154, 100), Color.White);

            spriteBatch.DrawString(spriteFont, score, new Vector2(155, 180), Color.Black);
            spriteBatch.DrawString(spriteFont, score, new Vector2(154, 180), Color.White);
        }

        public override bool IsInGame()
        {
            return false;
        }

        public override void OnExit()
        {

        }
    }
}
