using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using System.Collections.Generic;

namespace Strategy.UI
{
    class StartWindow : Window
    {
        public override void OnEnter()
        {
            
        }

        public void StartGame(eButtonState buttonState, Vector2 amount)
        {
            if (buttonState == eButtonState.DOWN)
            {
                Globals.windowManager.SetWindow(Globals.inGameWindow);
            }
        }

        public override void Render(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            string name = "Tiny War";
            string control = "Control: Left Mouse";
            string startButton = "Press S to Start";

            spriteBatch.DrawString(spriteFont, name, new Vector2(155, 100), Color.Black);
            spriteBatch.DrawString(spriteFont, name, new Vector2(154, 100), Color.White);

            spriteBatch.DrawString(spriteFont, control, new Vector2(155, 180), Color.Black);
            spriteBatch.DrawString(spriteFont, control, new Vector2(154, 180), Color.White);

            spriteBatch.DrawString(spriteFont, startButton, new Vector2(155, 300), Color.Black);
            spriteBatch.DrawString(spriteFont, startButton, new Vector2(154, 300), Color.White);
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
