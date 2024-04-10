using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using System.Collections.Generic;

namespace Strategy.UI
{
    internal class InGameWindow : Window
    {
        public override void OnEnter()
        {

        }

        public void PauseGame(eButtonState buttonState, Vector2 amount)
        {
            if (buttonState == eButtonState.DOWN)
            {
                Globals.windowManager.SetWindow(Globals.pauseWindow);
            }
        }

        public override void Render(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            
        }

        public override bool IsInGame()
        {
            return true;
        }

        public override void OnExit()
        {

        }
    }
}
