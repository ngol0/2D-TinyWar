using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using System.Collections.Generic;

namespace Strategy.UI
{
    class PauseWindow : Window
    {
        public override void OnEnter()
        {

        }

        public override void Render(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            string announcement = "Pause";
            string control = "Press L to continue";

            spriteBatch.DrawString(spriteFont, announcement, new Vector2(155, 100), Color.Black);
            spriteBatch.DrawString(spriteFont, announcement, new Vector2(154, 100), Color.White);

            spriteBatch.DrawString(
                spriteFont, control, new Vector2(155, 250), Color.White, 0.0f, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0.0f);
        }

        public void Continue(eButtonState buttonState, Vector2 amount)
        {
            if (buttonState == eButtonState.DOWN)
            {
                Globals.windowManager.SetWindow(Globals.inGameWindow);
            }
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
