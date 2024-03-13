using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Strategy
{
    static class SpriteLoader
    {
        public static Texture2D groundTexture;
        public static Texture2D infantryTexture;
        public static Texture2D tankTexture;
        public static Texture2D planeTexture;
        public static Texture2D moneyTowerTexture;

        //buttons
        public static Texture2D infantryBtn;
        public static Texture2D tankBtn;
        public static Texture2D planeBtn;
        public static Texture2D moneyTowerBtn;

        public static void LoadAllSprite()
        {
            groundTexture = Globals.contentManager.Load<Texture2D>("graphic/grass");

            //unit
            infantryTexture = Globals.contentManager.Load<Texture2D>("graphic/infantry_green");
            tankTexture = Globals.contentManager.Load<Texture2D>("graphic/tank_blue");
            planeTexture = Globals.contentManager.Load<Texture2D>("graphic/plane_red");
            moneyTowerTexture = Globals.contentManager.Load<Texture2D>("graphic/money");

            //buttons
            infantryBtn = Globals.contentManager.Load<Texture2D>("graphic/infantrybtn_green");
            tankBtn = Globals.contentManager.Load<Texture2D>("graphic/tankbtn_blue");
            planeBtn = Globals.contentManager.Load<Texture2D>("graphic/planebtn_red");
            moneyTowerBtn = Globals.contentManager.Load<Texture2D>("graphic/moneybtn");
        }
    }
}
