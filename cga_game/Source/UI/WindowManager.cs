using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Strategy.UI
{
    class WindowManager
    {
        public Window currentWindow;
        KeyboardManager keyboardManager;
        Scene scene;

        public void Init(Scene scene)
        {
            this.scene = scene;

            // init window objects for FSM
            Globals.inGameWindow = new InGameWindow();
            Globals.startWindow = new StartWindow();
            Globals.victoryWindow = new VictoryWindow();
            Globals.gameOverWindow = new GameOverWindow();
            Globals.pauseWindow = new PauseWindow();

            // set the first window
            SetWindow(Globals.startWindow);

            // init keyboard manager
            keyboardManager = new KeyboardManager();
            InitKeyboardBinding();
        }

        public void SetWindow(Window window)
        {
            //do something before changing to the new one
            if (currentWindow != null)
            {
                currentWindow.OnExit();
            }
            currentWindow = window;  //pointing to the new one
            currentWindow.OnEnter(); //do something right after switching
        }

        public void Update(GameTime gameTime) 
        {
            keyboardManager.Update();
        }

        public void Render(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (currentWindow != null)
            {
                currentWindow.Render(spriteBatch, font);
            }
        }

        private void InitKeyboardBinding()
        {
            keyboardManager.AddKeyboardBinding(Keys.S, Globals.startWindow.StartGame);
            keyboardManager.AddKeyboardBinding(Keys.P, Globals.inGameWindow.PauseGame);
            keyboardManager.AddKeyboardBinding(Keys.L, Globals.pauseWindow.Continue);
            keyboardManager.AddKeyboardBinding(Keys.R, Restart);
        }

        public void Restart(eButtonState buttonState, Vector2 amount)
        {
            if (buttonState == eButtonState.DOWN && currentWindow != Globals.startWindow)
            {
                //restart enemy/units/etc.
                scene.Restart();
                if (currentWindow!=Globals.inGameWindow) SetWindow(Globals.inGameWindow);
            }
        }

        public int GetScore()
        {
            return scene.Score;
        }

        public int GetHighScore()
        {
            return scene.GetCurrentPlayerStat().Score;
        }

        public Window GetCurrentWindow() { return currentWindow; }
    }
}
