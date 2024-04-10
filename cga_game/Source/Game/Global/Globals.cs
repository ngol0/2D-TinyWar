using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Strategy.Input;
using MonoGame.Extended.Entities;
using Strategy.UI;

namespace Strategy
{
    class Globals
    {
        public static MouseManager input;
        public static EntityFactory entityFactory;
        public static WindowManager windowManager;

        //windows
        public static StartWindow startWindow;
        public static InGameWindow inGameWindow;
        public static GameOverWindow gameOverWindow;
        public static VictoryWindow victoryWindow;
        public static PauseWindow pauseWindow;
    }
}
